# FF7 Nostalgia Project

Foundation repository for an original classic-JRPG project inspired by late-1990s console RPG systems and presentation.

## Repository Structure

```text
GameData/
  Schemas/      JSON Schema contracts
  Examples/     Example save payloads
Scripts/
  Core/         Engine-agnostic C# gameplay logic
Prompts/
  ComfyUI/      Character, enemy, and prop prompt bank
Docs/           Architecture and Unity integration notes
```

## Included Foundation

- Character Save State JSON Schema
- Example hero save data
- Pure C# `ATBCharacter`
- Pure C# `ATBManager` with `OnTurnReady`
- ComfyUI hero turnaround prompt
- Monster/boss prompt bank
- Modular environment-prop prompts
- Unity adapter guidance
- First-hour-at-home implementation checklist

## Core Rule

Gameplay-domain code should remain independent from Unity whenever practical. Unity-facing MonoBehaviours should handle input, timing, animation, UI, audio, and scene integration while delegating battle rules to plain C# classes.

## Recommended Next Milestone

Build one tiny battle sandbox with one hero, one enemy, ATB gauges, Attack, HP damage, and victory/defeat. Once the loop is stable, layer in party members, Materia-style commands, status effects, animation, VFX, and additional battle systems.

> Use original characters, assets, names, story, code, and game data. The project can study classic JRPG design patterns without redistributing proprietary game content.
