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

## Repository Structure

```text
GameData/
  Schemas/      Save/data contracts
  Examples/     Character save examples
  Encounters/   Encounter and boss definitions
Scripts/
  Core/         Engine-agnostic ATB logic
  Battle/       Battle actors, commands, damage, AI, battle flow
  Field/        Unity-facing field and encounter adapters
Prompts/
  ComfyUI/      Character, enemy, and prop prompt bank
Docs/           Architecture, canon, Unity integration, vertical slice plan
```

## Vertical Slice Goal

Build the opening Black Site sequence:

1. The Job — Cipher's apartment on the Plates
2. Infiltration — first Sentinel patrol encounter
3. The Descent — facility exploration and combat
4. The Discovery — Kira in the extraction chamber
5. The Guardian — first boss battle
6. The Escape — collapsing facility
7. The Outside — first reveal of the Depths and transition to the larger game

## Core Engineering Rule

Gameplay-domain code should remain independent from Unity wherever practical. Unity MonoBehaviours handle input, presentation, animation, UI, audio, and scene integration while delegating battle rules to plain C# classes.

## Current Priority

Get one polished 10–15 minute vertical slice playable before expanding scope. Advanced progression, large party systems, minigames, world-map content, and broader systems wait until the core field → encounter → battle → story loop is fun.
