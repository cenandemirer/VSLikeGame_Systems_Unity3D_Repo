using UnityEngine;

namespace VSLikeGame.Enemies
{
    public sealed class WaveDirector : MonoBehaviour
    {
        [System.Serializable]
        public struct Wave
        {
            public float durationSeconds;
            public int maxAlive;
            public float spawnPerSecond;
            public GameObject[] enemyPrefabs;
        }

        [Header("References")]
        [SerializeField] EnemySpawner spawner;
        [SerializeField] Transform player;

        [Header("Waves")]
        [SerializeField] Wave[] waves;

        [Header("Spawn Area")]
        [SerializeField] float fallbackSpawnRadius = 14f;

        int waveIndex;
        float waveT;
        float spawnAcc;
        int alive;

        void Update()
        {
            if (waves == null || waves.Length == 0 || !player) return;

            var w = waves[Mathf.Clamp(waveIndex, 0, waves.Length - 1)];

            waveT += Time.deltaTime;
            spawnAcc += w.spawnPerSecond * Time.deltaTime;

            while (spawnAcc >= 1f)
            {
                spawnAcc -= 1f;
                if (alive >= w.maxAlive) break;
                SpawnFromWave(w);
            }

            if (waveT >= Mathf.Max(1f, w.durationSeconds))
            {
                waveT = 0f;
                waveIndex = Mathf.Min(waveIndex + 1, waves.Length - 1);
            }
        }

        void SpawnFromWave(Wave w)
        {
            if (w.enemyPrefabs == null || w.enemyPrefabs.Length == 0) return;

            var prefab = w.enemyPrefabs[Random.Range(0, w.enemyPrefabs.Length)];
            if (!prefab) return;

            Vector3 fallback = player.position + (Random.insideUnitSphere.normalized * fallbackSpawnRadius);
            fallback.y = player.position.y;

            Vector3 pos = spawner ? spawner.GetSpawnPosition(fallback) : fallback;

            var go = Instantiate(prefab, pos, Quaternion.identity);
            var enemy = go.GetComponent<Enemy>();
            if (enemy) enemy.SetTarget(player);

            alive++;
            go.AddComponent<AliveCounter>().Init(this);
        }

        sealed class AliveCounter : MonoBehaviour
        {
            WaveDirector d;
            public void Init(WaveDirector director) => d = director;
            void OnDestroy() { if (d) d.alive = Mathf.Max(0, d.alive - 1); }
        }
    }
}
