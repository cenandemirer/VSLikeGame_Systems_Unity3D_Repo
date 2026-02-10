using UnityEngine;

namespace VSLikeGame.Skills
{
    [System.Serializable]
    public sealed class SkillCooldown
    {
        [SerializeField] float duration = 1f;
        float t;

        public bool Ready => t <= 0f;
        public float Normalized => duration <= 0f ? 0f : Mathf.Clamp01(t / duration);

        public SkillCooldown(float seconds)
        {
            duration = Mathf.Max(0f, seconds);
            t = 0f;
        }

        public void Tick(float dt)
        {
            if (t > 0f) t -= dt;
        }

        public void Start() => t = duration;
    }
}
