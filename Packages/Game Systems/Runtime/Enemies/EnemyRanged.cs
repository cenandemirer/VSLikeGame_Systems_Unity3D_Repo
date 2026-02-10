using UnityEngine;
using VSLikeGame.Projectiles;

namespace VSLikeGame.Enemies
{
    public sealed class EnemyRanged : MonoBehaviour
    {
        [SerializeField] Enemy enemy;
        [SerializeField] ProjectileSpawner spawner;

        float t;

        void Reset()
        {
            enemy = GetComponent<Enemy>();
            spawner = GetComponentInChildren<ProjectileSpawner>();
        }

        void Update()
        {
            if (enemy == null || spawner == null) return;

            var a = enemy.Archetype;
            if (a == null) return;
            if (a.behavior != EnemyBehaviorType.Ranged) return;

            var target = enemy.Target ? enemy.Target : FindPlayer();
            if (!target) return;

            t -= Time.deltaTime;

            Vector3 to = target.position - transform.position;
            to.y = 0f;
            float dist = to.magnitude;

            if (dist < a.keepDistance && dist > 0.01f)
            {
                transform.position -= to.normalized * (a.strafeSpeed * Time.deltaTime);
            }
            else
            {
                var right = Vector3.Cross(Vector3.up, to.normalized);
                transform.position += right * (Mathf.Sin(Time.time * 2f) * 0.2f) * Time.deltaTime;
            }

            if (dist <= a.fireRange)
            {
                transform.rotation = Quaternion.LookRotation(to.normalized, Vector3.up);

                if (t <= 0f)
                {
                    spawner.TrySpawn(a.fireDamage, to.normalized);
                    t = Mathf.Max(0.1f, a.fireInterval);
                }
            }
        }

        Transform FindPlayer()
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            return p ? p.transform : null;
        }
    }
}
