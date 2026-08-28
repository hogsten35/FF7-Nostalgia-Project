# ECHOES: Opening Content Package - JSON Reference

**Version:** 1.1  
**Date:** August 28, 2026  
**Format:** Modular JSON files for Unity integration

---

## Overview

This is a complete modular content package for Act 1 of Echoes. All content is structured as JSON for direct parsing into your Unity systems. No hardcoding required—load and instantiate dynamically.

### Canon & Combat Rules (v1.1)

- The Game Bible is the story authority; JSON is implementation data.
- Combat uses **ATB**, with Speed driving gauge fill.
- **Enemy and boss HP are always hidden** from the player: no HP numbers, percentages, or health bars.
- Difficulty target is **old-school and demanding**. Regular fights can punish careless play; bosses are preparation checks; light-to-moderate grinding may be useful when underprepared.
- Enemies do **not** automatically level-scale to the player.
- Kira is a **protected story NPC**, not an active combatant, during the Extraction Chamber Guardian fight.
- Petra is not in the active group during the Black Site infiltration. Darrow and Maisie are not present in the opening Guardian fight.
- Internal HP thresholds may be used for AI/phase logic but must never be displayed in player-facing UI.


**Total Files:** 6  
**Total Content:** ~47 dialogue nodes, 4 enemy types, 11 defined enemy moves, 2 boss phases, 10 checkpoints, 15+ loot items

---

## Files & Structure

### 1. **scene_manifest.json** (Master Index)
**Purpose:** Central reference for all content. Load this first.

**Contains:**
- Complete scene sequence (5 scenes total)
- File references for all other JSON files
- Loading order for Unity initialization
- Character arc progression
- System tracking flags
- Total content summary

**How to Use:**
```csharp
// Pseudo C#
SceneManifest manifest = LoadJSON("scene_manifest.json");
foreach(Scene scene in manifest.scene_sequence) {
    LoadScene(scene.scene_id);
}
```

---

### 2. **enemy_definitions.json** (Enemy Stats & Behavior)
**Purpose:** Complete definition for all enemy types.

**Contains:**
- 4 enemy types: Sentinel Trooper, Surveillance Drone, Remnant Collector, Sentinel Commander
- Base stats (HP, Attack, Defense, Magic, Speed)
- Move definitions (name, type, damage, probability, conditions)
- AI behavior patterns (solo, grouped, tactical)
- Loot drops with rarity and drop rates
- Weakness/resistance data

**Structure per Enemy:**
```json
{
  "enemy_id": "sentinel_trooper",
  "name": "Sentinel Trooper",
  "base_stats": { "hp": 20, "attack": 6, ... },
  "moves": [ { "move_id": "shoot", "damage": 8, ... } ],
  "ai_behavior": { "solo": "...", "grouped_2_3": "..." },
  "loot": { "credits": {min, max}, "equipment": [...], "consumables": [...] }
}
```

**How to Use:**
```csharp
Enemy trooper = new Enemy(enemyDefinitions["sentinel_trooper"]);
trooper.SelectMove(aiContext);
```

---

### 3. **boss_encounters.json** (Boss Mechanics)
**Purpose:** Complete boss fight specification with phase logic.

**Contains:**
- 1 boss: Extraction Chamber Guardian
- Multi-phase structure (Phase 1: Active Defense, Phase 2: Lockdown)
- HP thresholds for phase transitions
- Move sets per phase
- Developer-only strategy/tuning notes (never expose HP thresholds in the player UI)
- Special character interactions (Kira's RESONANCE)
- Defeat sequence with rewards

**Phase Structure:**
```json
{
  "phase_id": "phase_1_active_defense",
  "hp_range": { "min": 150, "max": 220 },
  "moves": [...],
  "player_strategy": [...]
}
```

**How to Use:**
```csharp
Boss guardian = new Boss(bossEncounters["extraction_chamber_guardian"]);
if (guardian.hp < guardian.phases[0].hp_range.min) {
    guardian.TransitionToPhase(1);
}
```

---

### 4. **dialogue_trees.json** (Branching Dialogue)
**Purpose:** All dialogue with branching paths and consequences.

**Contains:**
- 3 dialogue trees: Apartment, Kira Discovery, Depths Reveal
- Node structure (speaker, text, options, flags, events)
- Dialogue choices with consequences
- Audio key references (for voice acting)
- Relationship flags (kira_trust, etc.)
- Scene end triggers

**Node Structure:**
```json
{
  "speaker": "PETRA",
  "text": "...",
  "options": [
    { "text": "Option 1", "flag": "choice_pragmatic", "next": "next_node" },
    { "text": "Option 2", "flag": "choice_cautious", "next": "next_node" }
  ]
}
```

**How to Use:**
```csharp
DialogueTree tree = LoadDialogueTree("dialogue_trees.apartment_petra_cipher");
DialogueNode node = tree.GetNode("petra_entrance");
DisplayOptions(node.options);
SetFlag(selectedOption.flag);
```

---

### 5. **objectives_flow.json** (Scene Structure & Checkpoints)
**Purpose:** Complete breakdown of scenes, checkpoints, and progression flow.

**Contains:**
- 5 scenes (Apartment, Exterior, Interior, Escape, Depths)
- 10 checkpoints across scenes
- Choice points with outcomes
- Enemy encounters per checkpoint
- Environmental hazards
- Facility alert level tracking
- Lore item locations
- Time limits

**Checkpoint Structure:**
```json
{
  "checkpoint_id": "checkpoint_1_perimeter",
  "objective": "Reach the main entrance undetected",
  "choice_points": [
    { "stealth": {...}, "direct_combat": {...} }
  ],
  "completion_flag": "checkpoint_1_complete"
}
```

**How to Use:**
```csharp
Scene scene = objectivesFlow.scenes["black_site_exterior"];
Checkpoint checkpoint = scene.checkpoints[0];
if (PlayerReached(checkpoint.objective)) {
    SetFlag(checkpoint.completion_flag);
    ProgressToNextCheckpoint();
}
```

---

### 6. **loot_tables.json** (Rewards & Items)
**Purpose:** All items, rewards, and loot drop data.

**Contains:**
- 4 equipment items (armor, mods, accessories)
- 5 consumable items (healing, buffs, ammo)
- 4 key/lore items
- Currency drops per enemy type
- Drop rates and rarity levels
- Item effects and stat bonuses
- Sell values

**Item Structure:**
```json
{
  "item_id": "drone_core",
  "name": "Drone Core",
  "type": "weapon_mod",
  "rarity": "common",
  "stats": { "attack": 1 },
  "drop_sources": ["surveillance_drone"],
  "drop_rate": 0.6
}
```

**How to Use:**
```csharp
if (Random.value < lootTable["surveillance_drone"]["drone_core"]["drop_rate"]) {
    player.inventory.Add(lootTable.GetItem("drone_core"));
}
```

---

## Loading Sequence for Unity

**Recommended Load Order:**
1. `enemy_definitions.json` — Foundation for all combat
2. `boss_encounters.json` — Boss data (depends on enemy moves)
3. `loot_tables.json` — Reward data
4. `dialogue_trees.json` — Dialogue system (independent load)
5. `objectives_flow.json` — Scene orchestration (orchestrates everything)

**Pseudo-Code Example:**
```csharp
public class EchoesLoader : MonoBehaviour {
    void LoadAct1() {
        var enemies = LoadJSON<EnemyDefinitions>("enemy_definitions.json");
        var bosses = LoadJSON<BossEncounters>("boss_encounters.json");
        var loot = LoadJSON<LootTables>("loot_tables.json");
        var dialogue = LoadJSON<DialogueTrees>("dialogue_trees.json");
        var scenes = LoadJSON<ObjectivesFlow>("objectives_flow.json");
        
        // Initialize systems
        EnemyFactory.Initialize(enemies);
        BossFactory.Initialize(bosses);
        LootFactory.Initialize(loot);
        DialogueSystem.Initialize(dialogue);
        SceneOrchestrator.Initialize(scenes);
    }
}
```

---

## System Integration Points

### Combat System
- **Input:** `enemy_definitions.json` + `boss_encounters.json`
- **Output:** Turn-based combat with enemy AI

### Dialogue System
- **Input:** `dialogue_trees.json`
- **Output:** Branching conversation with flag setting

### Progression System
- **Input:** `objectives_flow.json`
- **Output:** Checkpoint completion, alert tracking, scene transitions

### Loot System
- **Input:** `loot_tables.json`
- **Output:** Item drops, currency rewards

### Relationship System
- **Input:** Dialogue flags (from `dialogue_trees.json`)
- **Output:** Relationship metrics (kira_trust, etc.)

---

## Flags & Progression Tracking

### Key Progression Flags
- `apartment_dialogue_complete_*` → Scene 1 end
- `black_site_exterior_complete` → Scene 2 end
- `facility_interior_complete` → Scene 3 start
- `kira_discovery_complete` → Guardian fight triggered
- `guardian_defeated` → Escape sequence triggered
- `escape_sequence_complete` → Depths reveal triggered
- `act_1_scene_1_complete` → Act 1 complete

### Relationship Flags
- `kira_trust` → Increases when Kira feels protected
- `kira_understanding` → Increases when Cipher acknowledges her
- Tracked via dialogue choices in `dialogue_trees.json`

---

## Common Use Cases

### Load Dialogue for a Scene
```csharp
var tree = dialogueTrees["depths_reveal"];
StartDialogue(tree.starting_node);
```

### Spawn Enemies for Encounter
```csharp
var encounter = objectivesFlow.scenes["facility_interior"]
    .checkpoints[0]
    .encounters[0];
    
foreach (var enemy in encounter.enemies) {
    var enemyDef = enemyDefinitions[enemy.enemy_id];
    SpawnEnemy(enemyDef);
}
```

### Track Progression
```csharp
SetFlag(checkpoint.completion_flag);
if (AllFlagsSet(checkpoint.requirements)) {
    UnlockNextScene();
}
```

### Handle Loot
```csharp
foreach (var item in enemy.loot) {
    if (Random.value < item.drop_rate) {
        player.AddItem(lootTable.GetItem(item.item_id));
    }
}
```

---

## Notes for Development

1. **Audio Hooks**: Each dialogue node has `audio_key` fields. Link these to your audio system.
2. **Animation References**: Each move/action has `animation` fields. Link to your animation controller.
3. **Event Triggers**: Checkpoints reference `event` fields. Create event handlers that match.
4. **Flags Are Critical**: The entire progression depends on flags. Never skip flag setting.
5. **JSON is Read-Only in Runtime**: Load once at scene start, don't modify. Create runtime instances.

---

## Total Content Summary

| Metric | Count |
|--------|-------|
| Dialogue Nodes | 47 |
| Enemy Types | 3 |
| Enemy Moves | 12 |
| Boss Phases | 2 |
| Checkpoints | 10 |
| Choice Points | 8 |
| Loot Items | 15+ |
| Estimated Playtime | 50 minutes |
| Total Credits (earned) | ~350 |
| Total XP (earned) | ~450 |

---

**Ready for Unity integration. Load and go.**
