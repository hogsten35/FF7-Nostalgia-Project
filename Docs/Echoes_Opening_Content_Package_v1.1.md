ECHOES: OPENING CONTENT PACKAGE

Act 1 - The Heist

Revision 1.1 - Canon Sync, Hidden-HP Rule, and Difficulty Pass

TABLE OF CONTENTS

1\. Petra/Cipher Apartment Dialogue

2\. Black Site Exterior Objective Flow

3\. Sentinel Trooper Enemy Definition

4\. Surveillance Drone Definition

5\. Remnant Collector Definition

6\. Kira Discovery Dialogue

7\. Guardian Boss Phase Logic

8\. Escape Sequence Objectives

9\. Depths Reveal Dialogue

10\. Item & Reward Tables

1\. PETRA/CIPHER APARTMENT DIALOGUE

SCENE: Cipher's apartment. Evening. Sparse, functional. Cipher is alone
when Petra arrives.

DIALOGUE TREE

\[Petra enters. Cipher is at the window.\]

PETRA: 'You still live like a soldier. Sparse. Clean. No attachments.'

CIPHER: \[No response. Waits.\]

PETRA: 'I have a job. High pay. In and out. No questions.'

CIPHER RESPONSE OPTIONS:

\[A\] 'Questions would slow me down anyway.' (PRAGMATIC)

→ Petra nods. 'I knew you'd understand.'

\[B\] 'What kind of job?' (CAUTIOUS)

→ Petra smiles slightly. 'The kind where knowing too much makes you a
liability.'

\[C\] 'Not interested.' (DIRECT)

→ Petra sets down a case with money. 'Reconsidering?'

PETRA (continuing): 'Sentinel black site. Underground facility. You go
in, retrieve what I tell you to retrieve, you come out. Twelve hours
max.'

CIPHER RESPONSE OPTIONS:

\[A\] 'What are we stealing?' (CURIOUS)

→ PETRA: 'Data. Research files. Things Sentinel doesn't want the public
knowing.'

→ FLAG SET: \[dialogue_choice_apartment_A\]

\[B\] 'How much?' (PRAGMATIC)

→ PETRA: \[Opens case. Stacks of credits.\] 'This much.'

→ FLAG SET: \[dialogue_choice_apartment_B\]

\[C\] 'Why me?' (SUSPICIOUS)

→ PETRA: 'Because you left Sentinel five years ago and you're still
good. That combination is rare.'

→ FLAG SET: \[dialogue_choice_apartment_C\]

PETRA: 'I've gotten you schematics. Entry point is here. Security
pattern is predictable. You have a forged keycard that'll get you
through the first checkpoint.'

\[Petra shows holographic building layout\]

CIPHER RESPONSE OPTIONS (FINAL):

\[A\] 'When?' (ACCEPTANCE)

→ PETRA: 'Tonight. The sooner the better.'

→ PROGRESSION FLAG: \[apartment_dialogue_complete_accepting\]

\[B\] 'I need time to prepare.' (CAUTIOUS)

→ PETRA: 'You have two hours. After that, the window closes.'

→ PROGRESSION FLAG: \[apartment_dialogue_complete_hesitant\]

\[C\] 'This feels wrong.' (DOUBT)

→ PETRA: 'Wrong and profitable are often the same thing. But it's your
choice.'

→ PROGRESSION FLAG: \[apartment_dialogue_complete_doubtful\]

SCENE ENDS. CIPHER MOVES TO BLACK SITE EXTERIOR.

Dialogue Impact Summary:

These choices don't gate progression (player always takes the job) but
set Cipher's characterization for Petra. In Act 2, Petra's dialogue
adjusts based on which path was chosen.

2\. BLACK SITE EXTERIOR OBJECTIVE FLOW

LOCATION: Sentinel industrial sector. Night. Facility disguised as a
power generation plant.

OBJECTIVE BREAKDOWN

CHECKPOINT 1: PERIMETER APPROACH

Player enters exterior zone. Objective: Reach the main entrance
undetected (or avoid patrols). Two patrol routes visible (can scout with
binoculars).

CHOICE POINT A: Stealth vs. Direct

→ STEALTH PATH: Avoid 2x Sentinel Trooper patrols. Takes ~3 minutes.
Safe but slow.

→ DIRECT PATH: Combat encounter with 2x Sentinel Troopers. Victory
triggers alarm (reduced but still alert).

CHECKPOINT 2: SECURITY GATE

Approach checkpoint. Guard (NPC, non-hostile) scans Cipher's keycard.
Keycard is forged but passes scan.

GUARD (NPC): 'Maintenance shift?'

CIPHER RESPONSE OPTIONS:

\[A\] 'Generator diagnostics.' (LIE - succeeds)

\[B\] 'Power supply audit.' (LIE - succeeds)

\[C\] \[Silent nod, walk past.\]

→ Gate opens. Player enters facility.

CHECKPOINT 3: ENTRANCE VESTIBULE

Interior. Environmental storytelling: Facility hum, soft bio-luminescent
lights in walls. Obvious corporate/scientific aesthetic.

Objective: Reach elevator to descend. Surveillance drones visible on
ceiling. Two camera cones sweep the hall.

CHOICE POINT B: Evasion Tactics

→ DESTROY DRONES: Combat encounter with 1x Surveillance Drone.
Destruction creates noise (facility alert raised to YELLOW).

→ WAIT & MOVE: Observe patterns, move between sweeps. No combat. Takes
time.

FACILITY ALERT SYSTEM (tracking):

• GREEN: No alert. Patrols routine.

• YELLOW: Elevated alert. Patrols more frequent. Alarms will trigger if
player is caught.

• RED: Full lockdown. Enemy reinforcements. Escape becomes priority.

PROGRESSION FLAG SUMMARY:

At end of exterior sequence, track: \[alert_level_at_exterior_end\]
(will affect facility interior difficulty).

3\. SENTINEL TROOPER ENEMY DEFINITION

Basic infantry. Trained but not elite. Operates in squads.

|          |                |                                                     |
|----------|----------------|-----------------------------------------------------|
| Stat     | Value          | Notes                                               |
| HP       | 20             | Low. Dies quickly to any damage.                    |
| Attack   | 6              | Moderate damage. Ranged attacks.                    |
| Defense  | 4              | Light armor. No magic resistance.                   |
| Speed    | 6              | Average. Acts after Cipher but before slow enemies. |
| Weakness | None (neutral) | Takes normal damage from all sources.               |

MOVE SET

SHOOT (60% action probability)

Ranged attack. Targets front-line party member. Deals 8 damage.

SMOKE GRENADE (30% action probability)

Used when HP < 50%. Throws smoke, removes Trooper from combat for 1
turn, then returns to battle.

TAKE COVER (10% action probability)

When grouped (2+ Troopers together), one Trooper reduces damage taken by
50% for 1 turn.

COMBAT AI BEHAVIOR

• Solo Trooper: Attacks immediately. No tactical thinking.

• Grouped (2-3 Troopers): Use TAKE COVER to protect each other. Switch
targets if one ally dies.

• Grouped (4+): One Trooper uses SMOKE GRENADE to escape, drawing player
attention while others attack.

LOOT DROPS

• Credits: 15-25

• Equipment: Sentinel Uniform Piece (Armor, +2 Defense, +5 HP)

• Ammunition: Standard Rifle Clip (reusable item, restores 1 ammo to
ranged users)

4\. SURVEILLANCE DRONE DEFINITION

Automated security construct. Hovers. Fast but fragile. Prioritizes
alerting security before attacking.

|          |          |                                                                                                                      |
|----------|----------|----------------------------------------------------------------------------------------------------------------------|
| Stat     | Value    | Notes                                                                                                                |
| HP       | 15       | Very low. Priority kill target.                                                                                      |
| Attack   | 7        | Decent for its fragility. Energy blast.                                                                              |
| Defense  | 2        | Minimal. Any attack lands.                                                                                           |
| Speed    | 12       | Very fast. Acts before most party members.                                                                           |
| Weakness | Tech/EMP | Tech attacks deal 1.5x damage. Petra can exploit this later; Cipher does not have Petra in the opening infiltration. |

MOVE SET

SCAN & ALERT (100% first action if spotted)

Drone alerts facility security. Triggers ALERT ESCALATION. Increases
facility_alert_level by 1.

ENERGY BLAST (50% normal turns)

Ranged attack. Targets random party member. Deals 10 damage.

EVASIVE MANEUVER (50% when HP < 50%)

Dodge roll. Drone gains +3 Defense for 1 turn.

COMBAT AI BEHAVIOR

• First action: Always SCAN & ALERT (alerts facility and potentially
brings reinforcements).

• Subsequent turns: ENERGY BLAST until HP < 50%, then EVASIVE MANEUVER.

• If destroyed quickly (<2 turns), alert is NOT escalated
retroactively.

LOOT DROPS

• Credits: 20-35

• Equipment: Drone Core (Weapon mod, +1 Attack to ranged weapons)

• Tech Component: Circuits (Crafting material for Petra's upgrades)

5\. REMNANT COLLECTOR DEFINITION

Bio-engineered construct tied to Remnant. Common enemy type. Dangerous
because it can drain health. First hint at Remnant's nature.

|          |          |                                                           |
|----------|----------|-----------------------------------------------------------|
| Stat     | Value    | Notes                                                     |
| HP       | 25       | Moderate durability. Dangerous because of draining magic. |
| Attack   | 5        | Low physical. Uses magic instead.                         |
| Magic    | 8        | Dangerous. Can drain health.                              |
| Defense  | 3        | Soft body. Physical attacks preferred.                    |
| Speed    | 10       | Fast. Acts before Cipher.                                 |
| Weakness | Physical | Takes 1.5x damage from physical attacks.                  |

MOVE SET

LIFE DRAIN (60% action probability)

Magic attack. Targets single party member. Deals 12 damage to target and
heals Collector by 6 HP.

RESONANCE PULSE (30% action probability)

AOE magic attack. Hits entire party for 8 damage.

HIDE (10% when HP < 25%)

Collector becomes invisible. Cannot be targeted. Returns to battle after
1 turn at full health.

COMBAT AI BEHAVIOR

• Aggressive. Prioritizes LIFE DRAIN for sustain.

• Uses RESONANCE PULSE if facing 3+ party members.

• Uses HIDE as escape mechanism when critically wounded.

LOOT DROPS

• Credits: 30-50

• Equipment: Remnant Core (Accessory, +2 Magic, user gains 1 HP/turn in
combat)

• Consumable: Remnant Extract (healing item, restores 25 HP and grants
+1 Speed for 2 turns)

6\. KIRA DISCOVERY DIALOGUE

SCENE: Extraction chamber. Deep underground. Kira is in a tank, wired to
machinery. Red and blue fluids flow through tubes.

CIPHER ENTERS. Silently observes. Machinery humming. Bio-luminescent
walls.

\[Machinery beeps. Kira's eyes flutter. She's not asleep—she's
sedated.\]

INTERNAL MONOLOGUE (Cipher's thoughts):

\[This isn't data. This is a person.\]

CIPHER RESPONSE OPTIONS (CRITICAL):

\[A\] 'What are you?' (DETACHED)

→ Cipher touches the glass. Stares.

→ FLAG SET: \[kira_discovery_detached\]

\[B\] 'This is insane. I'm getting you out.' (IMPULSIVE)

→ Cipher immediately moves to disconnect her from the machinery.

→ FLAG SET: \[kira_discovery_protective\]

\[C\] \[Silence. Cipher observes the equipment, then begins
disconnecting her.\]

→ Cipher begins methodically disconnecting tubes and wires.

→ FLAG SET: \[kira_discovery_pragmatic\]

\[Disconnection sequence. Alarms sound. Facility shifts to RED ALERT.\]

SYSTEM ANNOUNCEMENT (facility intercom): 'Extraction chamber breach.
Security to level nine.'

\[Kira's eyes open. First time truly conscious. She's confused.
Vulnerable.\]

KIRA (groggy, disoriented): 'Where... where am I? Who are you?'

CIPHER RESPONSE OPTIONS:

\[A\] 'Someone here to get you out. Move.' (PRACTICAL)

→ Cipher takes her arm. Moves toward exit.

→ KIRA: 'Okay... okay.' \[Trusts. Follows.\]

→ RELATIONSHIP FLAG: kira_trust +1

\[B\] 'You're Kira. You're not safe here.' (DIRECT)

→ Cipher offers hand. Waits.

→ KIRA: \[Hesitantly takes his hand.\] 'You know my name. They never
told me... anyone knew my name.'

→ RELATIONSHIP FLAG: kira_understanding +1

\[C\] 'I don't know. But we need to go. Now.' (HONEST)

→ Cipher helps her up. She's weak but mobile.

→ KIRA: \[Looks at her own hands. Confused but not afraid.\] 'Okay.'

→ RELATIONSHIP FLAG: kira_trust +2

PROGRESSION TRIGGER: Guardian Boss Fight initiates immediately.

7\. GUARDIAN BOSS PHASE LOGIC SPECIFICATION

Multi-phase security construct. Designed to protect extraction chambers.
Doesn't think—only follows programs.

PHASE STRUCTURE

PHASE 1: ACTIVE DEFENSE (220 HP → 150 HP)

Guardian is mobile, attacking relentlessly. Goal: Survive. Damage the
Guardian.

Guardian Moves (Phase 1):

→ SWEEP ATTACK (60% probability): Hits entire party. Deals 10 damage to
each. High priority.

→ FOCUSED STRIKE (30% probability): Single target. Deals 15 damage.

→ GRAPPLE (10% probability when adjacent): Locks target in place for 1
turn. Deals 8 damage.

Player Strategy (Phase 1):

• Use Cipher’s defensive tools to survive the opening pressure

• Use Cipher’s REDIRECT/protection tools to keep Kira safe

• Sustained damage output. Guard against SWEEP

PHASE 2: LOCKDOWN (150 HP → 0 HP)

\[At 150 HP, Guardian's behavior changes. It stops moving and enters
stationary defensive position.\]

Guardian Moves (Phase 2):

→ LOCKDOWN DEFENSE: Takes 50% reduced damage from all sources. Cannot
move or use physical attacks.

→ ENERGY PULSE (70% probability): AOE magic attack. Hits party for 12
damage each. Reflects back 50% of damage taken.

→ SYSTEM RESET (30% probability when damaged below 50 HP): Restores 20
HP. Can only use once per phase.

Player Strategy (Phase 2):

• High damage output. LOCKDOWN reduces incoming damage but Guardian is
stationary.

• Magic is less effective (reflects damage). Physical attacks preferred.

• Use the Resonance disruption window for concentrated physical damage

KIRA'S SPECIAL INTERACTION

Kira is present as a protected story NPC, not an active combatant. The
first time Guardian enters LOCKDOWN, Kira reacts instinctively with
RESONANCE:

→ RESONANCE DISRUPTION: Kira channels Remnant energy. Guardian's
LOCKDOWN is DISRUPTED for 2 turns.

→ Effect: Guardian takes 2x damage during the 2-turn disruption. Kira
suffers recoil; this is handled as story-combat state, not through an
enemy-style HP display.

→ LORE FLAG: \[guardian_resonance_discovered\] - Facility logs this. Has
narrative consequence in Act 2.

BOSS DEFEAT

Guardian reaches 0 HP. Shuts down. Extraction chamber door opens.

REWARDS:

• Experience: 150 XP (split among party)

• Credits: 80

• Equipment: Guardian Core (Armor upgrade, +3 Defense, +20 HP)

• Loot: Research Fragment (key item, starts questline in Act 2)

8\. ESCAPE SEQUENCE OBJECTIVES

After Guardian defeat, facility is in RED ALERT. Reinforcements
incoming. Time-sensitive escape.

ESCAPE CHECKPOINT 1: CHAMBER TO CORRIDOR

Objective: Exit extraction chamber. Kira is weak (30% movement speed).

Kira must reach exit or be carried. Cipher can carry her (reduces his
speed, but keeps her safe).

CHECKPOINT 2: COLLAPSING CORRIDOR

Facility structural damage. Sections of ceiling collapsing. Objective:
Avoid debris, reach checkpoint.

CHOICE POINT: Left corridor or right corridor?

→ LEFT: Clear path. 2x Sentinel Troopers. Combat required.

→ RIGHT: Debris obstacles. No enemies. Requires time/agility to
navigate.

CHECKPOINT 3: SECONDARY OBJECTIVE

Optional: Destroy evidence. Destroy data terminals in facility before
escape (optional objectives).

• If destroyed: Facility can't report Kira's extraction details. Slower
pursuit later.

• If not destroyed: Facility fully alerts authorities. Increased bounty
on Kira.

CHECKPOINT 4: MAIN ENTRANCE RUSH

Objective: Reach main entrance. Final enemy encounter: 1x Sentinel
Commander + 2x Sentinel Troopers.

COMMANDER STATS:

• HP: 60 \| Attack: 10 \| Defense: 8 \| Speed: 8

• Move: TACTICAL STRIKE (targets lowest HP party member, 15 damage)

• Move: RALLY (buffs 2 adjacent allies, +3 Attack for 1 turn)

CHECKPOINT 5: EXTERIOR ESCAPE

Final escape. Fade to black. Cipher and Kira emerge in industrial
wasteland.

9\. DEPTHS REVEAL DIALOGUE

SCENE: Industrial wasteland of The Depths. Night. Broken infrastructure.
Bioluminescent flora growing through cracks.

\[Cipher emerges carrying Kira. Both exhausted. Alarms echo behind them
from the facility.\]

\[A figure emerges from shadows. It's Petra. She's been waiting.\]

PETRA: 'You got her. Good.'

CIPHER: 'This wasn't the job.'

PETRA: 'No. It's bigger than the job. Come on. Safe house isn't far.'

\[They move through the Depths. Cipher looks around. The true cost of
Valence's machinery is exposed here.\]

CIPHER (internal monologue): 'The Depths. Raw, exposed. All the cost the
Plates never have to look at. And people live here anyway.'

PETRA: 'Welcome to the bottom of the city. Where the machinery actually
runs.'

\[They reach a safe house. Garage entrance. Petra opens it. They
enter.\]

INTERIOR: Safe house is spartan but livable. Medical supplies.
Equipment. Petra places Kira on a cot.

PETRA: 'She needs to sleep. Her body's been under heavy sedation.'

CIPHER: 'Who is she, Petra?'

PETRA: 'Someone the government doesn't want existing. That's all you
need to know right now.'

\[Kira stirrs. Eyes open slightly.\]

KIRA: 'What is this place? Are we... safe?'

CIPHER RESPONSE OPTIONS:

\[A\] 'For now.' (HONEST)

→ Kira nods. Trusts.

\[B\] 'Yes. Rest.' (REASSURING)

→ Kira closes eyes. Falls asleep immediately.

\[C\] \[Cipher doesn't answer. Just sits. Watches over her.\]

→ Petra and Cipher sit in silence.

PETRA: 'Rest. Tomorrow we figure out what comes next.'

\[SCENE ENDS. ACT 1, SCENE 1 COMPLETE.\]

\[TRANSITION: Next morning. Safe house. Cipher wakes. Finds Kira already
awake, looking out window at the Depths.\]

10\. ITEM & REWARD TABLES

EQUIPMENT DROPS BY ENEMY TYPE

|                    |          |                        |                    |
|--------------------|----------|------------------------|--------------------|
| Enemy              | Rarity   | Item                   | Stat Bonus         |
| Sentinel Trooper   | Common   | Sentinel Uniform Piece | +2 DEF, +5 HP      |
| Surveillance Drone | Common   | Drone Core             | +1 ATK (ranged)    |
| Remnant Collector  | Uncommon | Remnant Core           | +2 MAG, +1 HP/turn |
| Guardian           | Rare     | Guardian Core          | +3 DEF, +20 HP     |

CONSUMABLE ITEMS

|                 |                                          |                     |
|-----------------|------------------------------------------|---------------------|
| Item Name       | Effect                                   | Drop Source         |
| First Aid Kit   | Restore 30 HP to single target           | Facility loot boxes |
| Energy Drink    | Restore 20 HP + grant +1 SPD for 2 turns | Sentinel Troopers   |
| Remnant Extract | Restore 25 HP + grant +1 SPD for 2 turns | Remnant Collector   |
| Stimulant       | Grant +3 ATK for 3 turns                 | Facility safes      |

KEY ITEMS (QUEST-CRITICAL)

|                   |                   |                                                             |
|-------------------|-------------------|-------------------------------------------------------------|
| Item              | Location          | Purpose                                                     |
| Forged Keycard    | Petra (apartment) | Opens exterior security gate. Allows entry to facility.     |
| Research Fragment | Guardian loot     | Triggers Act 1 questline. Petra wants to know what's on it. |

CURRENCY & REWARDS SUMMARY

Total credits available (if full looting): ~300-400 credits

Total XP available (if defeating all enemies): ~400-500 XP

Equipment upgrades obtained: 3-4 items (depends on enemy encounters)

COMBAT PRESENTATION RULE

Enemy and boss HP is tracked internally but never shown to the player as
numbers, percentages, or health bars. Use animation, posture, VFX,
behavior, dialogue, and phase transitions to communicate condition.

DIFFICULTY TARGET

The opening is intentionally demanding. Regular encounters should create
attrition, the Guardian should be a genuine early-game preparation
check, enemies do not automatically level-scale, and underprepared
players may benefit from better tactics, equipment, recovery items, or a
few additional levels.

END OF OPENING CONTENT PACKAGE

Ready to export to JSON for Unity integration.
