using UnityEngine;
using VSLikeGame.VFX;
using VSLikeGame.Core;

namespace VSLikeGame.Core
{
    public sealed class Bootstrapper : MonoBehaviour
    {
        [SerializeField] HitVfx hitVfx;

        void Awake()
        {
            if (hitVfx != null) ServiceRegistry.Register<HitVfx>(hitVfx);
        }

        void OnDestroy()
        {
            ServiceRegistry.Unregister<HitVfx>();
        }
    }
}
