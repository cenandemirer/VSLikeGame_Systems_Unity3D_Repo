using UnityEngine;

namespace VSLikeGame.Enemies
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        [SerializeField] Transform[] spawnPoints;

        public Vector3 GetSpawnPosition(Vector3 fallback)
        {
            if (spawnPoints == null || spawnPoints.Length == 0) return fallback;
            var p = spawnPoints[Random.Range(0, spawnPoints.Length)];
            return p ? p.position : fallback;
        }
    }
}
