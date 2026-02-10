using UnityEngine;
using VSLikeGame.Enemies;
using VSLikeGame.Audio;
using VSLikeGame.Core;

namespace VSLikeGame.Skills
{
    public sealed class AreaSkill : MonoBehaviour, ISlottedSkill
    {
        public string Id => "area_skill";
        public SkillSlot Slot => slot;
        public bool IsReady => cd.Ready;
        public float CooldownNormalized => cd.Normalized;

        [SerializeField] SkillSlot slot = SkillSlot.Secondary;

        [SerializeField] float radius = 4.5f;
        [SerializeField] float damage = 3f;
        [SerializeField] float cooldownSeconds = 1.0f;
        [SerializeField] LayerMask enemyMask;

        [SerializeField] string sfxEventId = "skill_area";

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

            var hits = Physics.OverlapSphere(transform.position, radius, enemyMask, QueryTriggerInteraction.Ignore);
            if (hits.Length == 0) return false;

            for (int i = 0; i < hits.Length; i++)
            {
                var enemy = hits[i].GetComponentInParent<Enemy>();
                if (enemy != null) enemy.Damage(damage);
            }

            var audio = ServiceRegistry.Get<IAudioBus>();
            audio?.PlayAt(sfxEventId, transform.position);

            cd.Start();
            SkillUiEvents.RaiseSkillUsed(slot, Id);
            return true;
        }
    }
}
