using UnityEngine;
using VSLikeGame.Audio;
using VSLikeGame.Core;
using VSLikeGame.Enemies;

namespace VSLikeGame.Skills
{
    public sealed class ProjectileSkill : MonoBehaviour, ISlottedSkill
    {
        public string Id => "projectile_skill";
        public SkillSlot Slot => slot;
        public bool IsReady => cd.Ready;
        public float CooldownNormalized => cd.Normalized;

        [SerializeField] SkillSlot slot = SkillSlot.Primary;

        [SerializeField] VSLikeGame.Projectiles.ProjectileSpawner spawner;

        [SerializeField] float damage = 2f;
        [SerializeField] float cooldownSeconds = 0.35f;
        [SerializeField] float maxTargetRange = 12f;
        [SerializeField] LayerMask enemyMask;
        [SerializeField] bool aimAtNearestEnemy = true;

        [SerializeField] string sfxEventId = "skill_projectile";

        SkillCooldown cd;

        void Awake() => cd = new SkillCooldown(cooldownSeconds);

        public void Tick(float dt)
        {
            cd.Tick(dt);
            SkillUiEvents.RaiseCooldown(slot, CooldownNormalized);
        }

        public bool TryUse()
        {
            if (!cd.Ready) return false;
            if (spawner == null) return false;

            Vector3 dir = transform.forward;

            if (aimAtNearestEnemy)
            {
                if (TryFindNearestEnemy(transform.position, out var enemyPos))
                    dir = (enemyPos - transform.position).normalized;
            }

            if (!spawner.TrySpawn(damage, dir)) return false;

            var audio = ServiceRegistry.Get<IAudioBus>();
            audio?.PlayAt(sfxEventId, transform.position);

            cd.Start();
            SkillUiEvents.RaiseSkillUsed(slot, Id);
            return true;
        }

        bool TryFindNearestEnemy(Vector3 origin, out Vector3 pos)
        {
            pos = default;

            var hits = Physics.OverlapSphere(origin, maxTargetRange, enemyMask, QueryTriggerInteraction.Ignore);
            if (hits == null || hits.Length == 0) return false;

            float best = float.MaxValue;
            Transform bestT = null;

            for (int i = 0; i < hits.Length; i++)
            {
                var e = hits[i].GetComponentInParent<Enemy>();
                if (e == null) continue;

                float d = (e.transform.position - origin).sqrMagnitude;
                if (d < best)
                {
                    best = d;
                    bestT = e.transform;
                }
            }

            if (bestT == null) return false;
            pos = bestT.position;
            return true;
        }
    }
}
