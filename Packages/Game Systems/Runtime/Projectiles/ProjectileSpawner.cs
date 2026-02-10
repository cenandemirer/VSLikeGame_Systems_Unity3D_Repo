using UnityEngine;

namespace VSLikeGame.Projectiles
{
    public sealed class ProjectileSpawner : MonoBehaviour
    {
        [SerializeField] Transform muzzle;
        [SerializeField] GameObject projectilePrefab;

        public bool TrySpawn(float damage, Vector3 direction, float overrideSpeed = -1f)
        {
            if (!projectilePrefab) return false;

            Vector3 pos = muzzle ? muzzle.position : transform.position;
            Quaternion rot = Quaternion.LookRotation(direction.normalized, Vector3.up);

            var go = Instantiate(projectilePrefab, pos, rot);
            var p = go.GetComponent<Projectile>();
            if (p) p.Init(damage, overrideSpeed);
            return true;
        }
    }
}
