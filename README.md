# Echoes

**Valence. Extraction. Collapse.**

Echoes is a character-driven JRPG about complicity, systemic collapse, and moral compromise. The project studies the pacing, readability, ATB pressure, cinematic field presentation, and party-driven structure of classic late-1990s console RPGs while using original characters, story, setting, assets, code, and game data.

## Current Canon

- Setting: Valence, a megacity powered by extraction of Remnant
- Protagonist: Cipher Vocc, former Sentinel Corps command soldier
- Key characters: Kira, Petra Serin, Darrow Hayes, Maisie Tang
- Major additional antagonist: Korvoth — a dark sorcerer driven by an insatiable thirst for knowledge
- Opening: Petra hires Cipher for a black-site theft; Cipher discovers Kira in an extraction chamber and the job becomes an escape
- Combat direction: Active Time Battle (ATB), with Speed controlling gauge fill rate
- Enemy and boss HP is never shown to the player
- Difficulty target: old-school and demanding; preparation, attrition, tactics, equipment, and some grinding matter

## Active Content Package

The cleaned **Echoes v1.1** package is the active Act 1 content source.

- `GameData/Act1/scene_manifest.json`
- `GameData/Act1/enemy_definitions.json`
- `GameData/Act1/boss_encounters.json`
- `GameData/Act1/dialogue_trees.json`
- `GameData/Act1/objectives_flow.json`
- `GameData/Act1/loot_tables.json`
- `Docs/Echoes_Game_Bible_v1.1.md`
- `Docs/Echoes_Opening_Content_Package_v1.1.md`
- `Docs/Content/ACT1_JSON_REFERENCE_v1.1.md`
- `Docs/Content/CLEANUP_NOTES_v1.1.md`

Older encounter/example files remain in the repository for history and prototypes, but new implementation work should use `GameData/Act1/` unless a later canon revision replaces it.

## Repository Structure

```text
GameData/
  Act1/         Active Echoes v1.1 opening content package
  Schemas/      Save/data contracts
  Examples/     Character save examples
  Encounters/   Earlier prototype encounter definitions
Scripts/
  Core/         Engine-agnostic ATB logic
  Battle/       Battle actors, commands, damage, AI, battle flow
  Field/        Unity-facing field and encounter adapters
Prompts/
  ComfyUI/      Character, enemy, and prop prompt bank
Docs/           Game bible, opening package, architecture, canon, Unity integration
```

## Opening Sequence

1. The Job — Cipher's apartment on the Plates
2. Infiltration — Black Site exterior and first Sentinel patrol
3. The Descent — facility exploration, alert state, and combat
4. The Discovery — Kira in the extraction chamber
5. The Guardian — first major preparation-check boss
6. The Escape — collapsing facility and Sentinel pursuit
7. The Outside — first reveal of the Depths and safe-house transition

## Core Engineering Rule

Gameplay-domain code should remain independent from Unity wherever practical. Unity MonoBehaviours handle input, presentation, animation, UI, audio, and scene integration while delegating battle rules to plain C# classes.

## Current Priority

Turn the v1.1 data package into a playable Unity loop in this order: load Act 1 JSON → instantiate Cipher and Sentinel enemies → run ATB combat with hidden enemy HP → return to field → drive objectives/dialogue from data → implement Guardian phase logic.
