# Core Architecture

## Goal

Keep game rules independent from Unity wherever practical so the systems can be tested, reused, and modified without coupling them to scenes or MonoBehaviours.

## Layers

### Game Data

`GameData/Schemas` defines save-data contracts.

`GameData/Examples` contains realistic example payloads for testing and tooling.

### Core Battle Logic

`Scripts/Core` contains plain C# domain logic. It must not reference `UnityEngine`.

Current types:

- `ATBCharacter`: owns speed and gauge state.
- `ATBManager`: advances gauges and emits `OnTurnReady`.

### Unity Adapter Layer

Unity-specific scripts should own or reference core-domain objects and translate Unity input/time/UI events into core-system calls.

### Presentation Layer

UI, cameras, animation, VFX, SFX, and scene objects should react to battle-state changes rather than contain battle rules themselves.

## Save-State Direction

The Character Save State separates equipment from Materia slot state. Each slot records whether it participates in a link and the ID of its partner slot. Materia records type, level, and AP progression so the save data can support growth without hard-coding specific abilities into the character object.

## Next Architecture Milestone

Build a vertical battle slice with:

1. Actor HP/MP and alive/dead state.
2. Attack command.
3. Target selection.
4. Damage resolver.
5. Enemy AI command selection.
6. Victory/defeat state.
7. Materia-granted commands.

After the prototype is working, decide whether the final game should keep ATB or migrate the same command/resolver layer to a CTB turn-order scheduler.
