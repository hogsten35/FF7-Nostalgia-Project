# First Hour at Home

## 1. Generate concept assets

- Open ComfyUI.
- Start with `Prompts/ComfyUI/HeroCharacter.md`.
- Generate the clean turnaround before dramatic poses.
- Keep the weapon separate for easier character reconstruction and rigging.
- Generate one boss and a few modular props only after the hero pipeline works.

## 2. Convert concept to 3D

- Use your preferred image-to-3D workflow.
- Export to OBJ, FBX, or GLB depending on the tool.
- Inspect topology and silhouette before spending time on materials.

## 3. Clean in Blender

- Fix obvious mesh intersections and holes.
- Retopologize or remesh only as aggressively as needed.
- Separate weapon/accessories from the body where practical.
- Apply transforms and verify scale.
- Create clean material slots.

## 4. Rig and animate

- Use Mixamo or another auto-rigger if the mesh proportions are compatible.
- Test idle, walk, run, attack, hit reaction, and victory first.
- Verify foot contact and root motion before building a large animation library.

## 5. Create the Unity project

- Start a new Unity 6 URP project.
- Copy `Scripts/Core` into the project.
- Add a Unity-facing adapter similar to the example in `Docs/UNITY_INTEGRATION.md`.
- Confirm the core project compiles before adding UI or imported models.

## 6. First playable target

Do not build a full game scene first. Build a tiny battle sandbox containing:

- One player character.
- One enemy.
- One camera.
- Basic command UI.
- ATB gauges.
- Attack command.
- HP reduction.
- Victory condition.

Once that loop is stable, add Materia-style abilities, additional party members, animations, effects, and more complex battle rules.
