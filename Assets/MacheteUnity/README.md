# Machete - Unity 3D Asset

Game-ready machete based on reference photo. Real-world scale (meters), pivot at guard/grip for holding.

## Contents
- `Meshes/Machete.fbx` - 10 meshes: Blade, Fuller0/1, Etch1/2, Guard, Collar, Handle, Rivet + Machete_Root. UV-unwrapped (Smart Project), modifiers applied, transforms baked (-Z forward, Y up).
- `Textures/` - 1024/512 PNGs: T_Blade_Albedo, T_Blade_Roughness, T_Handle_Albedo
- `Machete_Unity.blend` - source, `Machete_Unity_Preview.png` - render

## Import to Unity (URP / Built-in)
1. Copy `MacheteUnity` folder into your Unity `Assets/` (e.g. `Assets/Machete/`).
2. Select `Machete.fbx` in Project:
   - Rig: None, Animation: uncheck Import Animation
   - Materials: Extract Materials to `Materials/`
   - Generate Lightmap UVs: ON
   - Scale Factor: 1 (modeled in meters, total ~1.1m)
3. Materials (create 4x URP Lit):
   - M_Blade: Base Map T_Blade_Albedo, Metallic 0.9, Smoothness 0.7 (use T_Blade_Roughness inverted as Smoothness source if desired)
   - M_Handle: Base Map T_Handle_Albedo, Metallic 0.05, Smoothness 0.4
   - M_Guard: color #474C52, Metallic 0.95, Smoothness 0.6
   - M_Rivet: color #BFC1C4, Metallic 1.0, Smoothness 0.7
   Assign to matching mesh slots (Blade+Fullers share M_Blade family).
4. Drag `Machete_Root` into scene. Pivot is at guard - ideal for hand socket.
   - Add BoxCollider (size ~0.75, 0.08, 0.05, center x+0.35) for hits, or MeshCollider (convex) for athletic accuracy.
   - Rigidbody: use Kinematic for held weapon.
5. Scale check: blade 0.75m, handle 0.36m. If too big/small, adjust Root scale, Apply.

## Tips
- Blade forward is +Z (Unity standard) after bake. For FPS hands, parent to hand bone, rotate to taste.
- For HDRP, use Lit shader with same maps + Mask Map (R=Metallic, A=Smoothness from roughness inverted).
