# Unity Integration

The battle code under `Scripts/Core` is deliberately free of `UnityEngine` dependencies.

## Recommended Unity Adapter

Create a MonoBehaviour later that owns an `ATBManager`, subscribes to `OnTurnReady`, and calls `Tick(Time.deltaTime)` from `Update()`.

```csharp
using UnityEngine;
using FF7Nostalgia.Core.Battle;

public sealed class BattleClockBehaviour : MonoBehaviour
{
    private readonly ATBManager _manager = new();

    private void Awake()
    {
        _manager.OnTurnReady += HandleTurnReady;
    }

    private void Update()
    {
        _manager.Tick(Time.deltaTime);
    }

    private void HandleTurnReady(ATBCharacter character)
    {
        Debug.Log($"{character.Name} is ready.");
        // Open command UI or queue AI decision here.
    }
}
```

## Suggested Next Layers

1. `BattleActor` for HP/MP/status state.
2. `BattleCommand` abstraction for Attack, Skill, Item, Defend.
3. `TargetingService` for allies/enemies/multi-target rules.
4. `BattleActionResolver` for formulas and damage.
5. UI adapters for gauges and command menus.
6. Animation/audio adapters that respond to resolved battle events.

Keep calculations and rules in plain C# whenever possible. Let MonoBehaviours translate game-state events into visuals, input, animation, and sound.
