# Minimal Test Scene — 5 Minutes Setup

## Hierarchy

Scene
 ├── Systems
 │    ├── RunStats
 │    ├── SaveManager
 │    ├── AudioBus
 │    └── Bootstrapper
 ├── Player
 ├── Enemies
 ├── Spawners
 └── UI

## Systems Setup

### AudioBus
- source2D -> Audio2D AudioSource (spatialBlend 0)
- source3D -> Audio3D AudioSource (spatialBlend 1)
- Events:
  - skill_area
  - skill_projectile
  - enemy_death

### Bootstrapper
- HitVfx prefab assigned

## Player Setup

Components:
- CharacterController
- Health
- KeyboardMouseInput
- PlayerController
- SkillRunner
- ProjectileSpawner
- ProjectileSkill
- AreaSkill

Connections:
- SkillRunner.input -> KeyboardMouseInput
- SkillRunner.skills -> ProjectileSkill + AreaSkill
- ProjectileSpawner.muzzle -> Muzzle
- ProjectileSkill.spawner -> ProjectileSpawner

## Enemy Setup

Each prefab:
- Enemy
- Trigger collider
- Layer = Enemy
- Archetype assigned

Charger:
- EnemyCharger

Ranged:
- EnemyRanged
- ProjectileSpawner
- Muzzle child

## UI

- RunHud (kills + time)
- SkillHud (2 fill images)
