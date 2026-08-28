using FF7Nostalgia.Core.Battle;
using FF7Nostalgia.Core.Data;

var contentDirectory = Path.Combine(AppContext.BaseDirectory, "GameData", "Act1");
var content = Act1ContentLoader.LoadFromDirectory(contentDirectory);

var cipherDefinition = content.Characters.Characters["cipher_vocc"];
var trooperDefinition = content.Enemies.Enemies["sentinel_trooper"];

var cipher = BattleActorFactory.CreateCharacter(cipherDefinition);
var trooperA = BattleActorFactory.CreateEnemy(trooperDefinition, "a");
var trooperB = BattleActorFactory.CreateEnemy(trooperDefinition, "b");
var enemies = new[] { trooperA, trooperB };

var engine = new BattleEngine(
    new[] { cipher },
    enemies,
    new DamageResolver(seed: 7));

var timeline = new ATBBattleController(new[] { cipher, trooperA, trooperB });
var enemyAI = new EnemyAI(seed: 11);
BattleActor? awaitingPlayer = null;

engine.OnBattleLog += Console.WriteLine;
engine.OnBattleEnded += result => Console.WriteLine($"\n=== {result.ToString().ToUpperInvariant()} ===");

timeline.OnActorReady += actor =>
{
    if (!actor.IsAlive || engine.Result != BattleResult.InProgress)
        return;

    if (actor.IsPlayerControlled)
    {
        awaitingPlayer = actor;
        return;
    }

    var command = enemyAI.ChooseCommand(actor);
    engine.Execute(actor, command, cipher);
    timeline.ConsumeTurn(actor);
};

Console.WriteLine("ECHOES - Act 1 Battle Harness");
Console.WriteLine("Black Site patrol: Cipher Vocc vs. 2 Sentinel Troopers");
Console.WriteLine("Enemy HP is intentionally hidden.");
Console.WriteLine("Commands: 1 Attack | 4 Defend\n");

while (engine.Result == BattleResult.InProgress)
{
    timeline.Tick(0.1f);

    if (awaitingPlayer is null)
        continue;

    Console.WriteLine($"\nCipher HP {cipher.CurrentHP}/{cipher.MaxHP}");

    var livingEnemies = enemies.Where(enemy => enemy.IsAlive).ToArray();
    for (var i = 0; i < livingEnemies.Length; i++)
        Console.WriteLine($"{i + 1}. {livingEnemies[i].Name}");

    Console.Write("Command [1 Attack / 4 Defend]: ");
    var input = Console.ReadLine();
    var command = input == "4" ? BattleCommand.Defend() : BattleCommand.Attack();

    BattleActor target;
    if (command.Type == BattleCommandType.Defend)
    {
        target = cipher;
    }
    else
    {
        Console.Write("Target number: ");
        var targetInput = Console.ReadLine();
        var selectedIndex = int.TryParse(targetInput, out var parsed) ? parsed - 1 : 0;
        selectedIndex = Math.Clamp(selectedIndex, 0, livingEnemies.Length - 1);
        target = livingEnemies[selectedIndex];
    }

    if (engine.Execute(cipher, command, target))
        timeline.ConsumeTurn(cipher);

    awaitingPlayer = null;
}
