using UnityEngine;

namespace VSLikeGame.Enemies
{
    public enum EnemyBehaviorType
    {
        Basic,
        Charger,
        Ranged
    }

    [CreateAssetMenu(menuName = "VSLikeGame/Enemies/EnemyArchetype")]
    public sealed class EnemyArchetype : ScriptableObject
    {
        public string id = "enemy_basic";

        public EnemyBehaviorType behavior = EnemyBehaviorType.Basic;

        public float maxHealth = 10f;
        public float moveSpeed = 2.2f;

        public float contactDamage = 1f;
        public float contactCooldown = 0.4f;
        public float stopDistance = 1.1f;

        public float engageRange = 8f;

        public float chargeSpeed = 7.5f;
        public float chargeDuration = 0.6f;
        public float chargeCooldown = 1.8f;

        public float fireDamage = 1.5f;
        public float fireInterval = 1.1f;
        public float fireRange = 10f;
        public float keepDistance = 4f;
        public float strafeSpeed = 1.2f;
    }
}
