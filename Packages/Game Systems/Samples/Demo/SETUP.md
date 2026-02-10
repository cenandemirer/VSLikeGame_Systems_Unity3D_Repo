# Demo Setup (minimal)

## Scene objects
- RunStats
- SaveManager
- Player: CharacterController + KeyboardMouseInput + PlayerController + Health + SkillRunner + AreaSkill
- Enemies: EnemySpawner + WaveDirector

## Enemy prefab
- CapsuleCollider (isTrigger = true)
- Enemy (assign EnemyArchetype)


- Add AudioBus with 2 AudioSources (2D + 3D) and map eventIds: `skill_area`, `skill_projectile`, `enemy_death`
- Add ProjectileSpawner under Player (muzzle transform)
- Add ProjectileSkill to SkillRunner list
- Create Enemy prefabs:
  - Charger: Enemy + EnemyCharger (capsule trigger)
  - Ranged: Enemy + EnemyRanged + ProjectileSpawner (projectile prefab)


- Add Bootstrapper and assign HitVfx
- Add SkillHud with two UI Images for cooldown fills
- In EnemyArchetype: set behavior = Charger or Ranged for prefabs
