# Echoes Vertical Slice Plan

## Purpose

The vertical slice should prove the complete JRPG loop using the opening sequence already defined in the Echoes game bible. Target play time: approximately 10–15 minutes for the first polished slice, with room to expand the full opening later.

## Canon Sequence

### Scene 1 — The Job
**Location:** Cipher's apartment, Plates level

Petra arrives with a high-paying black-site theft job. Cipher asks minimal questions. Dialogue options communicate attitude (pragmatic, cautious, aggressive) without creating fail states.

**Slice requirements**
- Dialogue box
- 3-choice response UI
- Basic camera blocking
- Quest objective update

### Scene 2 — Infiltration
**Location:** Sentinel Black Site exterior

Cipher encounters two Sentinel Troopers. The bible allows stealth or combat; the first implementation may force combat until stealth exists.

**Tutorial battle**
- Cipher Vocc
- 2 Sentinel Troopers
- ATB explanation
- Attack
- Defend
- Victory/defeat

Sentinel Trooper bible stats:
- HP 20
- Attack 6
- Defense 4
- Speed 6

### Scene 3 — The Descent
**Location:** Black Site interior

Explore deeper into the facility. Biological material appears on walls and a soft hum increases. Optional research notes/audio logs foreshadow the classified program.

**Slice requirements**
- Field movement
- Interactable object
- One optional lore pickup
- One additional encounter

Candidate enemies from the bible:
- Sentinel Trooper
- Surveillance Drone
- Remnant Collector

### Scene 4 — The Discovery
**Location:** Extraction Chamber

Cipher finds Kira suspended in a tank and wired to machinery. He realizes this was never a simple data theft.

Bible dialogue choices:
- "Who are you?"
- "This is insane. I'm getting you out."
- Say nothing and begin disconnecting her

Alarms trigger after the interaction.

### Scene 5 — The Guardian
**Location:** Extraction Chamber

First boss battle.

Bible boss stats:
- HP 220
- Attack 14
- Defense 12
- Speed 8

Mechanics:
- SWEEP attacks the whole party
- LOCKDOWN activates during phase 2
- LOCKDOWN grants 50% damage reduction
- Kira's RESONANCE can disrupt LOCKDOWN early

For the earliest playable version, Kira may be represented as a scripted support action rather than a full controllable party member.

### Scene 6 — The Escape

Kira is conscious but disoriented. Cipher carries or escorts her through collapsing corridors toward the emergency exit.

**Initial implementation**
- Short linear escape route
- Alarm lighting/audio
- One encounter or scripted obstruction
- Exit trigger

### Scene 7 — The Outside
**Location:** The Depths

The player sees the industrial wasteland of the Depths for the first time. Petra appears and directs Cipher and Kira toward a safe house.

End the vertical slice on this reveal.

## ATB Amendment

The original bible describes turn-based initiative based on Speed. Current implementation direction is Active Time Battle.

For the prototype:
- Each participant owns an ATB gauge
- Speed determines gauge fill rate
- A full gauge enables an action
- Gauge resets after the action resolves
- Enemy ATB continues progressing during battle

This is a project implementation amendment and should remain clearly distinguished from the original bible text.

## Definition of Done

The vertical slice is successful when a new player can:

1. Watch/play the opening conversation
2. Control Cipher through a field scene
3. Trigger and complete an ATB battle
4. Explore the Black Site
5. Discover Kira
6. Defeat the Guardian
7. Escape to the Depths
8. Reach an obvious end-of-slice moment

## Explicitly Out of Scope Until This Works

- Full 4-member party
- Complete progression system
- Large inventory/economy
- World map
- Minigames
- Multiple towns
- Full relationship branching
- Large spell/ability catalog
- Final-quality cinematics

The goal is not breadth. The goal is one convincing, polished chain of field exploration, story, ATB combat, boss mechanics, and payoff.
