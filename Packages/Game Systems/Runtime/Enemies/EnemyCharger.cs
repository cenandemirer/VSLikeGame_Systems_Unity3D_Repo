using UnityEngine;

namespace VSLikeGame.Enemies
{
    public sealed class EnemyCharger : MonoBehaviour
    {
        [SerializeField] Enemy enemy;

        float chargeT;
        float cdT;
        Vector3 chargeDir;

        void Reset() => enemy = GetComponent<Enemy>();

        void Update()
        {
            if (enemy == null) return;

            var a = enemy.Archetype;
            if (a == null) return;
            if (a.behavior != EnemyBehaviorType.Charger) return;

            if (cdT > 0f) cdT -= Time.deltaTime;

            if (chargeT > 0f)
            {
                chargeT -= Time.deltaTime;
                transform.position += chargeDir * (a.chargeSpeed * Time.deltaTime);
                return;
            }

            var t = enemy.Target ? enemy.Target : FindPlayer();
            if (!t) return;

            var to = t.position - transform.position;
            to.y = 0f;

            if (cdT <= 0f && to.sqrMagnitude <= a.engageRange * a.engageRange && to.sqrMagnitude > 1.25f)
            {
                chargeDir = to.normalized;
                chargeT = a.chargeDuration;
                cdT = a.chargeCooldown;
                transform.rotation = Quaternion.LookRotation(chargeDir, Vector3.up);
            }
        }

        Transform FindPlayer()
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            return p ? p.transform : null;
        }
    }
}
