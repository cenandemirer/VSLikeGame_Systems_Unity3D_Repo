namespace VSLikeGame.Skills
{
    public interface ISlottedSkill : ICombatSkill
    {
        SkillSlot Slot { get; }
        float CooldownNormalized { get; }
    }
}
