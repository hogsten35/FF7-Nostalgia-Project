using FF7Nostalgia.Core.Battle;

var hero = new BattleActor(
    id: "char_kael_001",
    name: "Kael",
    isPlayerControlled: true,
    maxHP: 620,
    maxMP: 54,
    strength: 34,
    defense: 20,
    magic: 25,
    magicDefense: 18,
    speed: 31);

var enemy = new BattleActor(
    id: "enemy_marsh_hound",
    name: "Marsh Hound",
    isPlayerControlled: false,
    maxHP: 210,
    maxMP: 0,
    strength: 18,
    defense: 8,
    magic: 6,
    magicDefense: 7,
    speed: 24);

var engine = new BattleEngine(
    new[] { hero },
    new[] { enemy },
    new DamageResolver(seed: 7));

var timeline = new ATBBattleController(new[] { hero, enemy });
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
    engine.Execute(actor, command, hero);
    timeline.ConsumeTurn(actor);
};

Console.WriteLine("FF7 Nostalgia Project - Battle Harness");
Console.WriteLine("Commands: 1 Attack | 2 Fire | 3 Cure | 4 Defend\n");

while (engine.Result == BattleResult.InProgress)
{
    timeline.Tick(0.1f);

    if (awaitingPlayer is null)
        continue;

    Console.WriteLine($"\nKael HP {hero.CurrentHP}/{hero.MaxHP} MP {hero.CurrentMP}/{hero.MaxMP}");
    Console.WriteLine($"Marsh Hound HP {enemy.CurrentHP}/{enemy.MaxHP}");
    Console.Write("> ");

    var input = Console.ReadLine();
    var command = input switch
    {
        "2" => BattleCommand.Fire(),
        "3" => BattleCommand.Cure(),
        "4" => BattleCommand.Defend(),
        _ => BattleCommand.Attack()
    };

    var target = command.Id == "cure" ? hero : enemy;
    if (engine.Execute(hero, command, target))
        timeline.ConsumeTurn(hero);

    awaitingPlayer = null;
}
