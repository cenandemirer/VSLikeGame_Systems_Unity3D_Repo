# VSLikeGame — Documentation

This package is intentionally small but system-shaped.

## Flow
1) `RunStats.StartRun()` begins the run
2) `WaveDirector` spawns enemies using wave rules
3) `SkillRunner` triggers skills (demo: left click)
4) Enemies die → `RunStats.AddKill()`
5) Run ends → `SaveManager.ApplyRun(run)` persists meta stats


- Projectile pipeline: Projectile + ProjectileSpawner + ProjectileSkill
- Enemy variants: EnemyCharger, EnemyRanged
- Audio hooks: IAudioBus + AudioBus (eventId based)

- Data-driven enemy behavior via EnemyArchetype.behavior
- Projectile impact VFX hook via HitVfx (registered in Bootstrapper)
- Skill slots + UI hooks (SkillUiEvents) + SkillHud


## Extra Guides
- TestScene.md
- PrefabChecklist.md
- Roadmap.md
