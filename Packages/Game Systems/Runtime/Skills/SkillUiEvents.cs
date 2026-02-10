using System;

namespace VSLikeGame.Skills
{
    public static class SkillUiEvents
    {
        public static event Action<SkillSlot, string> OnSkillUsed;
        public static event Action<SkillSlot, float> OnCooldownChanged;

        public static void RaiseSkillUsed(SkillSlot slot, string id) => OnSkillUsed?.Invoke(slot, id);
        public static void RaiseCooldown(SkillSlot slot, float normalized) => OnCooldownChanged?.Invoke(slot, normalized);
    }
}
