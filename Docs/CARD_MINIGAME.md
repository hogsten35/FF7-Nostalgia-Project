# Echo Grid

## Scope

**Echo Grid** is Echoes' collectible 3x3 directional card battler. It is inspired by the feel of classic late-1990s JRPG side games while using original Echoes names, art, cards, rewards, opponents, and world integration.

The framework exists now, but Echo Grid is intentionally **not available in the opening vertical slice**.

## Core Match

- 3x3 board
- 5 cards per player
- Each card has four ranks: north, east, south, west
- Ranks range from 1 to 10
- Players alternate placing one card into an empty board cell
- When a placed card has a higher touching rank than an opposing adjacent card, that card changes ownership
- After all nine cells are occupied, the side controlling more total cards wins

## Advanced Rules

The engine supports optional rule flags:

- **Open Hands** — presentation layer may reveal both hands
- **Random Hand** — future deck-selection layer may choose a random legal hand
- **Same** — matching two or more touching values triggers special captures
- **Plus** — matching two or more touching sums triggers special captures
- **Combo** — cards captured through Same/Plus can chain normal captures outward
- **Sudden Death** — reserved for rematch behavior after a draw
- **Elemental** — reserved for future board-cell/card element modifiers

Same, Plus, and Combo are enabled by default in `GameData/CardGame/rules.json`.

## Architecture

`Scripts/Minigames/CardGrid/CardModels.cs`
- card definitions and instances
- owner tracking
- rule set
- match result and move result

`Scripts/Minigames/CardGrid/CardMatch.cs`
- board state
- hand validation
- placement
- normal captures
- Same
- Plus
- combo propagation
- score and match result

`Scripts/Minigames/CardGrid/CardGridConfig.cs`
- loads rule configuration from JSON
- validates 3x3 / five-card / rank 1-10 assumptions
- protects the current vertical-slice exclusion

`GameData/CardGame/card_definitions.json`
- intentionally contains no canon card collection yet
- establishes the future data format

## Future Content Pass

A later pass can add:

1. Echo Grid visual identity and UI treatment.
2. A starter set of Echoes cards.
3. Card rarity and acquisition tables.
4. NPC opponents and opponent deck profiles.
5. Win/loss card-trade rules.
6. Regional rule sets if desired.
7. Unity board UI, drag/place input, flip animations, sound, and card art.
8. Collection binder and deck builder.
9. Quest rewards and hidden/rare cards.
10. AI difficulty tiers.

## Design Guardrails

- Echo Grid is the official in-world name of the card minigame.
- Do not use copyrighted card art, names, UI assets, music, or game data from another title.
- Mechanical familiarity is intentional, but Echoes should have its own card-game identity and presentation.
- Rare cards should reward exploration, optional bosses, difficult quests, and strong NPC opponents.
- The minigame should become meaningful side progression, not mandatory filler.
- Do not add Echo Grid to the opening vertical slice unless scope is deliberately changed later.
