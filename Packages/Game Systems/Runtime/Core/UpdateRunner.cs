using System.Collections.Generic;
using UnityEngine;

namespace VSLikeGame.Core
{
    public interface ITickable
    {
        void Tick(float dt);
    }

    public sealed class UpdateRunner : MonoBehaviour
    {
        private readonly List<ITickable> tickables = new();

        public void Add(ITickable t)
        {
            if (t != null && !tickables.Contains(t)) tickables.Add(t);
        }

        public void Remove(ITickable t) => tickables.Remove(t);

        void Update()
        {
            float dt = Time.deltaTime;
            for (int i = 0; i < tickables.Count; i++)
                tickables[i].Tick(dt);
        }
    }
}
