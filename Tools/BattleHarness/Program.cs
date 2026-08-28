using FF7Nostalgia.Core.Battle;
using FF7Nostalgia.Core.Data;
using FF7Nostalgia.Core.Magic;

var contentDirectory = Path.Combine(AppContext.BaseDirectory, "GameData", "Act1");
var content = Act1ContentLoader.LoadFromDirectory(contentDirectory);
var spellCatalog = SpellCatalog.Load(Path.Combine(AppContext.BaseDirectory, "GameData", "Magic", "spell_definitions.json"));

var implementedSpells = spellCatalog.All.Values.Where(spell => spell.Implemented).ToArray();
foreach (var spell in implementedSpells)
    _ = SpellCommandFactory.Create(spell);

var cipherDefinition = content.Characters.Characters["cipher_vocc"];
var trooperDefinition = content.Enemies.Enemies["sentinel_trooper"];

var cipher = BattleActorFactory.CreateCharacter(cipherDefinition);
var trooperA = BattleActorFactory.CreateEnemy(trooperDefinition, "a");
var trooperB = BattleActorFactory.CreateEnemy(trooperDefinition, "b");
var enemies = new[] { trooperA, trooperB };
var definitionByActorId = new Dictionary<string, EnemyDefinition>
{
    [trooperA.Id] = trooperDefinition,
    [trooperB.Id] = trooperDefinition
};

var engine = new BattleEngine(
    new[] { cipher },
    enemies,
    new DamageResolver(seed: 7));

var timeline = new ATBBattleController(new[] { cipher, trooperA, trooperB });
var enemyAI = new EnemyMoveAI(seed: 11);
BattleActor? awaitingPlayer = null;

engine.OnBattleLog += Console.WriteLine;
engine.OnBattleEnded += result => Console.WriteLine($"\n=== {result.ToString().ToUpperInvariant()} ===");

timeline.OnActorReady += actor =>
{
    if (!actor.IsAlive || engine.Result != BattleResult.InProgress)
        return;

    if (actor.ConsumeRemovedTurn())
    {
        Console.WriteLine($"{actor.Name} remains obscured by smoke and misses the turn.");
        timeline.ConsumeTurn(actor);
        return;
    }

    if (actor.IsPlayerControlled)
    {
        awaitingPlayer = actor;
        return;
    }

    var definition = definitionByActorId[actor.Id];
    var move = enemyAI.ChooseMove(definition, actor, enemies);
    var target = move.Target == "self" ? actor : cipher;

    engine.ExecuteEnemyMove(actor, move, target);
    timeline.ConsumeTurn(actor);
};

Console.WriteLine("ECHOES - Act 1 Battle Harness");
Console.WriteLine("Black Site patrol: Cipher Vocc vs. 2 Sentinel Troopers");
Console.WriteLine("Enemy HP is intentionally hidden.");
Console.WriteLine("Sentinel behavior is loaded from GameData/Act1/enemy_definitions.json.");
Console.WriteLine($"Magic catalog loaded: {spellCatalog.All.Count} spells ({implementedSpells.Length} damage/healing spells executable).\n");
Console.WriteLine("Cipher has no canon MP/magic allocation yet, so magic is not assigned to him in this harness.");
Console.WriteLine("Commands: 1 Attack | 4 Defend\n");

while (engine.Result == BattleResult.InProgress)
{
    timeline.Tick(0.1f);

    if (awaitingPlayer is null)
        continue;

    Console.WriteLine($"\nCipher HP {cipher.CurrentHP}/{cipher.MaxHP}");

    var livingEnemies = enemies.Where(enemy => enemy.IsTargetable).ToArray();
    if (livingEnemies.Length == 0)
    {
        Console.WriteLine("No enemy is currently targetable.");
        timeline.ConsumeTurn(cipher);
        awaitingPlayer = null;
        continue;
    }

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
