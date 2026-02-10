using UnityEngine;
using UnityEngine.UI;
using VSLikeGame.Skills;

namespace VSLikeGame.UI
{
    public sealed class SkillHud : MonoBehaviour
    {
        [SerializeField] Image primaryFill;
        [SerializeField] Image secondaryFill;

        void OnEnable() => SkillUiEvents.OnCooldownChanged += HandleCooldown;
        void OnDisable() => SkillUiEvents.OnCooldownChanged -= HandleCooldown;

        void HandleCooldown(SkillSlot slot, float normalized)
        {
            float fill = 1f - Mathf.Clamp01(normalized);
            if (slot == SkillSlot.Primary && primaryFill) primaryFill.fillAmount = fill;
            if (slot == SkillSlot.Secondary && secondaryFill) secondaryFill.fillAmount = fill;
        }
    }
}
