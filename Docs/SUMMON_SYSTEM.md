# Echoes Summon Framework

## Current scope

The summon system architecture exists globally, but **no summon is available in the opening vertical slice**.

The opening Black Site sequence must not grant, equip, unlock, tutorialize, or expose a Summon command.

## Data-driven design

Future summon definitions live in `GameData/Summons/summon_definitions.json` and may declare:

- unique summon ID and display name
- element
- resource type and cost
- power
- target rules
- battle effect
- unlock flag
- model hook
- animation hook
- VFX hook
- audio hook
- cinematic camera sequence hook
- return-to-battle sequence hook

## Battle integration

`BattleCommandType.Summon` is reserved for summon actions. `SummonCommandFactory` converts unlocked definitions into normal battle commands so summon damage/healing can reuse the battle core while presentation remains Unity-facing.

The current factory supports MP as the resource contract. Other summon-resource systems can be added later without changing the summon data format.

## Presentation philosophy

Summons should eventually feel like major cinematic events rather than oversized normal spells. Unity presentation is expected to temporarily hand control to a summon sequence, play model/animation/VFX/audio/camera choreography, resolve the battle effect, then return cleanly to ATB combat.

## Locked rules

- No summons in the opening vertical slice.
- No concrete summon is canon merely because the framework exists.
- Summons never reveal enemy or boss HP, percentages, or health bars.
- Unlocks should be story/progression rewards, not automatically granted to Cipher.
- Summon lore should connect naturally to Echoes, Remnant, and the setting before individual summon identities are locked.
