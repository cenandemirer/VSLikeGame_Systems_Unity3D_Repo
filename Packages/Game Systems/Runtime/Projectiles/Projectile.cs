using UnityEngine;
using VSLikeGame.Enemies;
using VSLikeGame.VFX;
using VSLikeGame.Core;

namespace VSLikeGame.Projectiles
{
    public sealed class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 14f;
        [SerializeField] float lifetime = 2.5f;
        [SerializeField] LayerMask enemyMask;
        [SerializeField] bool destroyOnHit = true;

        float damage;
        float t;

        public void Init(float dmg, float overrideSpeed = -1f)
        {
            damage = Mathf.Max(0f, dmg);
            if (overrideSpeed > 0f) speed = overrideSpeed;
            t = lifetime;
        }

        void Update()
        {
            transform.position += transform.forward * (speed * Time.deltaTime);

            t -= Time.deltaTime;
            if (t <= 0f) Destroy(gameObject);
        }

        void OnTriggerEnter(Collider other)
        {

            if (((1 << other.gameObject.layer) & enemyMask.value) == 0) return;

            var enemy = other.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.Damage(damage);
                var vfx = ServiceRegistry.Get<HitVfx>();
                vfx?.Spawn(transform.position, transform.rotation);
                if (destroyOnHit) Destroy(gameObject);
            }
        }
    }
}
