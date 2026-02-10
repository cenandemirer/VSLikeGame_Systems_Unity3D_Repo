using UnityEngine;

namespace VSLikeGame.VFX
{
    public sealed class HitVfx : MonoBehaviour
    {
        [SerializeField] GameObject prefab;

        public void Spawn(Vector3 position, Quaternion rotation)
        {
            if (!prefab) return;
            Instantiate(prefab, position, rotation);
        }
    }
}
