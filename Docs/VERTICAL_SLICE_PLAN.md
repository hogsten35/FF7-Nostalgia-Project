# Vertical Slice Plan

## Goal
Create a polished 10–15 minute playable JRPG slice that proves the core fantasy: exploration, cinematic presentation, random encounter transition, ATB combat, character progression, and a boss payoff.

## Slice Flow
1. Title screen
2. Short opening field scene in Rookfen
3. Player gains control of Kael
4. Exploration/tutorial interaction
5. One optional treasure chest
6. Step-based random encounter
7. ATB battle tutorial against a Marsh Hound
8. Short story scene at the entrance to Gnashtooth Hollow
9. Mini-dungeon with 2–3 authored field screens
10. One normal encounter variant
11. Save point / recovery interaction
12. Boss battle
13. Victory scene and world-map reveal teaser

## Definition of Playable
A build qualifies as the first playable when a player can launch the game, control Kael, trigger a battle, select Attack/Magic/Item/Defend, win or lose, return to the field, and reach a clear end-of-demo screen without editor intervention.

## Must-Have Systems
- Player field movement
- Fixed cinematic field camera
- Walkmesh/collision boundaries
- Interaction prompts
- Dialogue box
- Scene transitions
- Step-based encounter meter
- ATB gauges
- Command menu
- Target selection
- HP/MP updates
- Attack, Fire, Cure, Item, Defend
- Basic enemy AI
- Victory/defeat handling
- EXP and Gil reward screen
- Simple inventory
- Save/load checkpoint
- Boss encounter

## Deliberately Deferred
Do not block the vertical slice on these:
- Full world map
- Full party roster
- Summons
- Limit Break tree
- Advanced Materia combinations
- Crafting
- Chocobos
- Minigame hub
- Large inventory
- Shops beyond one simple vendor
- Full cinematic voice acting

## Content Budget
### Player
- Kael: 1 field model, 1 battle model (may be shared initially)
- Animations: idle, walk, run, battle idle, attack, hit, cast, defend, victory, KO

### Enemies
- Marsh Hound
- Mire Wasp or Sewer Vermin
- Gnashtooth boss

### Environments
- Rookfen opening field
- Gnashtooth entrance
- Gnashtooth interior A
- Gnashtooth interior B
- Boss arena

### UI
- Main menu
- Dialogue box
- ATB combat HUD
- Command menu
- Target cursor
- Victory/results panel
- Pause/status menu (minimal)

## First Evening in Unity
1. Create URP project.
2. Copy `Scripts/Core` and `Scripts/Battle` into `Assets/Scripts/Core`.
3. Compile before adding any scene logic.
4. Create a greybox battle scene with capsules/cubes only.
5. Wire `BattleEngine` to a lightweight MonoBehaviour presenter.
6. Display HP, MP, and ATB using basic Unity UI.
7. Make Attack selectable and complete one full player/enemy exchange.
8. Add Fire, Cure, and Defend.
9. Only after combat works, replace greybox visuals with character/environment assets.

## Quality Bar
The slice should feel intentionally small, not unfinished. Fewer screens with strong camera composition, music, transitions, and responsive menus are preferable to a large empty area.
