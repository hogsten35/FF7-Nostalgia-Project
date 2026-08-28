# First Hour at Home — Echoes

The first goal is not art polish. The first goal is to prove that the opening encounter works end-to-end in Unity using the code and data already prepared in the repository.

## 1. Create the Unity project

- Start a new Unity 6 URP project.
- Pull or copy the repository content into the project.
- Import `Scripts/Core`, `Scripts/Battle`, and the Unity-facing adapters.
- Resolve compile errors before importing character art or models.

## 2. Build the ugliest possible battle sandbox

Create one battle scene using primitives:

- Capsule = Cipher Vocc
- Two cubes = Sentinel Troopers
- One camera
- Simple ground plane
- Basic command panel
- HP display
- ATB gauges

Use `GameData/Encounters/first_encounter.json` as the encounter reference.

## 3. Wire the first playable battle

Required commands for the first test:

- Attack
- Defend

Required behavior:

- Cipher's ATB fills based on Speed
- Each Sentinel Trooper's ATB fills based on Speed
- A full gauge enables an action
- Attack applies damage
- Defend reduces incoming damage
- Enemies choose and execute attacks
- Dead actors stop taking turns
- Victory triggers when both troopers are defeated
- Defeat triggers when Cipher reaches 0 HP

Do not spend time on final fonts, particles, camera animation, or character models until this loop works.

## 4. Build the first field test

After the battle works:

- Create a simple Black Site exterior field scene
- Add a controllable capsule for Cipher
- Add collision
- Add one interaction point
- Add an encounter trigger that loads the battle scene
- After victory, return Cipher to the field

This proves the core Echoes loop:

**Field → encounter → ATB battle → victory → return to field**

## 5. Only then begin presentation work

Once the loop is stable:

- Generate Cipher's turnaround from `Prompts/ComfyUI/HeroCharacter.md`
- Create or import a rough Cipher model
- Create a Sentinel Trooper placeholder/model
- Add idle, run, attack, hit reaction, and victory animations
- Replace primitive battle actors one at a time

## 6. Next target: Extraction Chamber Guardian

After the tutorial fight is reliable, create the boss scene from `GameData/Encounters/extraction_guardian_boss.json`.

Implement in this order:

1. Guardian HP / Attack / Defense / Speed
2. SWEEP
3. Phase transition at roughly half HP
4. LOCKDOWN
5. 50% damage reduction
6. Scripted Kira RESONANCE interruption
7. Victory event

## Stop Condition for Night One

Night one is a success if you can launch Unity, move Cipher in a simple field, enter a battle against two Sentinel Troopers, defeat them using ATB combat, and return to the field.

Everything beyond that is bonus progress.
