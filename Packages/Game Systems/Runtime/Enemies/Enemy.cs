using UnityEngine;
using VSLikeGame.Run;
using VSLikeGame.Audio;
using VSLikeGame.Core;

namespace VSLikeGame.Enemies
{
    public sealed class Enemy : MonoBehaviour
    {
        [SerializeField] EnemyArchetype archetype;
        [SerializeField] Transform target;

        public EnemyArchetype Archetype => archetype;
        public Transform Target => target;

        float hp;
        float contactT;

        void Awake()
        {
            hp = archetype ? archetype.maxHealth : 10f;
        }

        public void SetTarget(Transform t) => target = t;

        void Update()
        {
            TickMove();
            if (contactT > 0f) contactT -= Time.deltaTime;
        }

        void TickMove()
        {
            if (!target) return;

            var dir = target.position - transform.position;
            dir.y = 0f;

            float stop = archetype ? archetype.stopDistance : 1.1f;
            if (dir.sqrMagnitude <= stop * stop) return;

            float speed = archetype ? archetype.moveSpeed : 2.2f;
            transform.position += dir.normalized * (speed * Time.deltaTime);

            if (dir.sqrMagnitude > 0.001f)
                transform.rotation = Quaternion.LookRotation(dir.normalized, Vector3.up);
        }

        public void Damage(float amount)
        {
            if (amount <= 0f) return;
            hp -= amount;
            if (hp <= 0f)
            {
                var run = ServiceRegistry.Get<IRunStats>();
                run?.AddKill();

                var audio = ServiceRegistry.Get<IAudioBus>();
                audio?.PlayAt("enemy_death", transform.position);

                Destroy(gameObject);
            }
        }

        void OnTriggerStay(Collider other)
        {
            if (contactT > 0f) return;
            var ph = other.GetComponentInParent<VSLikeGame.Combat.Health>();
            if (ph == null) return;

            float dmg = archetype ? archetype.contactDamage : 1f;
            ph.Damage(dmg);

            contactT = archetype ? archetype.contactCooldown : 0.4f;
        }
    }
}
