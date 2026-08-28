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
