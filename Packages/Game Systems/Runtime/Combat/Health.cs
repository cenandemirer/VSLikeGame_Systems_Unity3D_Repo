using System;
using UnityEngine;

namespace VSLikeGame.Combat
{
    public interface IHealth
    {
        float Current { get; }
        float Max { get; }
        bool IsDead { get; }
        event Action<float, float> OnChanged;
        event Action OnDied;
        void Damage(float amount);
        void Heal(float amount);
    }

    public sealed class Health : MonoBehaviour, IHealth
    {
        [SerializeField] float max = 10f;
        public float Current { get; private set; }
        public float Max => max;
        public bool IsDead => Current <= 0f;

        public event Action<float, float> OnChanged;
        public event Action OnDied;

        void Awake() => Current = Mathf.Max(1f, max);

        public void Damage(float amount)
        {
            if (amount <= 0f || IsDead) return;
            Current = Mathf.Max(0f, Current - amount);
            OnChanged?.Invoke(Current, Max);
            if (Current <= 0f) OnDied?.Invoke();
        }

        public void Heal(float amount)
        {
            if (amount <= 0f || IsDead) return;
            Current = Mathf.Min(Max, Current + amount);
            OnChanged?.Invoke(Current, Max);
        }
    }
}
