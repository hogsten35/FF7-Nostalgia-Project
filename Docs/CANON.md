# Echoes Canon Reference

This file exists to prevent placeholder names and implementation experiments from silently replacing story canon.

## Core Identity

**Title:** Echoes  
**Tagline:** Valence. Extraction. Collapse.

Echoes is a character-driven RPG about complicity, systemic collapse, moral compromise, and the question of what makes someone real.

## World

### Valence
A megacity of roughly 100 million people whose civilization depends on extraction of Remnant.

Three primary layers:
- The Spires — elite sky-level districts
- The Plates — working-class government/commercial/residential level
- The Depths — underground industrial zones, extraction sites, and slums

### Remnant
A biological planetary substrate left from an earlier ecosystem. It is living, networked, finite, and defensive when harvested. Valence depends on it for power.

## Main Cast

### Cipher Vocc
- Protagonist
- Age 34
- Former Sentinel Corps, Command Unit
- Left Sentinel five years before the game
- Archetype: The Worn Soldier
- Weapon: modular blade/pistol hybrid
- Combat role: tank / physical attacker
- Core ability: REDIRECT — takes damage for allies
- Limit Break: RECALL — relives traumatic memory for massive damage

### Kira
- Extracted prototype from a classified Sentinel program
- Appears approximately 19
- Uses RESONANCE connected to Remnant
- Central question: is she becoming herself, or what Sentinel made her?

### Petra Serin
- Fixer and information broker
- Hires Cipher for the opening black-site job
- Strategist/support role
- Uses hacking, enemy disruption, and buffs

### Darrow Hayes
- Cipher's past
- Still active Sentinel
- Believes the system may be reformable from within
- Aggressive physical damage dealer

### Maisie Tang
- Remnant miner and black-market salvager
- Survivor archetype
- Uses mining tools and SCAVENGE

## Korvoth

Korvoth is **not the protagonist**.

Current approved concept:
- Villain / major antagonist figure
- Dark sorcerer
- Driven by an insatiable thirst for knowledge

His exact relationship to Sentinel, Remnant, Kira, and the existing act bosses is not yet defined. Do not invent those relationships in implementation files until they are deliberately decided.

## Factions

### Sentinel Government
Wants Kira dead or recaptured. Sentinel fears systemic collapse and is willing to sacrifice ethics to preserve Valence.

### Eco-Resistance
Wants extraction stopped. The movement is fractured; some factions accept or actively seek total collapse despite Valence's population depending on the system.

### Syndicates
Want to exploit Kira and the instability around Remnant for profit.

## Opening Sequence

1. The Job — Petra visits Cipher's apartment on the Plates
2. Infiltration — Sentinel Black Site exterior; first patrol encounter
3. The Descent — facility exploration and combat
4. The Discovery — Cipher finds Kira in the extraction chamber
5. The Guardian — Extraction Chamber Guardian boss fight
6. The Escape — collapsing facility
7. The Outside — first reveal of the Depths; Petra leads them toward a safe house

## Combat Canon / Implementation Amendment

The original game bible specifies turn-based combat with initiative based on Speed.

Current project decision: use an **Active Time Battle (ATB)** implementation while preserving Speed as the primary timing stat.

This is an approved implementation amendment, not original bible wording.

### Difficulty Philosophy

Echoes should preserve an old-school JRPG sense of danger and preparation. The intended experience is deliberately challenging rather than a low-friction story walkthrough.

Core rules:
- Standard encounters must be capable of punishing careless play, especially in groups or when the party is already worn down.
- Resource attrition matters. HP, MP, healing items, status recovery, and save/recovery opportunities should affect route decisions.
- Players should sometimes need to stop, improve equipment, learn enemy behavior, refine ability loadouts, or gain a few levels before pushing forward.
- A modest amount of grinding is intentional. Progression should reward preparation without requiring excessive repetitive farming.
- Bosses should be legitimate skill and preparation checks. Entering underleveled, poorly equipped, or without understanding mechanics should create a real chance of defeat.
- Optional encounters and exploration should provide meaningful advantages so players who engage with the world become better prepared.
- Difficulty should come primarily from enemy patterns, resource pressure, party composition, timing, weaknesses, status effects, and tactical mistakes rather than arbitrary one-shot attacks or unavoidable randomness.
- Victory should feel earned. The game should allow and expect occasional player defeats.
- Do not automatically scale all enemies to the player. Areas should have meaningful power expectations so progression and returning stronger remain satisfying.

Difficulty tuning should aim for the feeling of classic console JRPGs: demanding enough that preparation matters, but fair enough that players can identify why they lost and change their approach.

### Enemy HP Presentation Rule

Enemy and boss HP must remain hidden from the player at all times during normal gameplay.

Do not display:
- enemy HP numbers
- enemy HP percentages
- enemy health bars
- boss health bars
- UI elements that directly reveal remaining HP

The battle engine may track exact HP internally for damage, phase transitions, AI, rewards, and victory conditions, but the presentation layer must not expose it.

Battle readability should instead come from authored feedback such as:
- hit reactions and animation changes
- damaged posture or staggered movement
- visual wear, sparks, wounds, or environmental damage where appropriate
- phase transitions
- behavior changes
- combat dialogue or enemy reactions

Player-party HP and MP may remain visible in the combat UI.

## Naming Rule

Avoid generic AI-fantasy naming patterns and placeholder terms. New names should feel authored, grounded in the culture/function of Valence, and consistent with existing names such as Cipher Vocc, Petra Serin, Darrow Hayes, and Maisie Tang.

When a new story fact conflicts with the game bible, update this file deliberately rather than allowing code or prompts to become accidental canon.
