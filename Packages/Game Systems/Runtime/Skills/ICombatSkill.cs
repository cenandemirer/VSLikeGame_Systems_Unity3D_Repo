namespace VSLikeGame.Skills
{
    public interface ICombatSkill
    {
        string Id { get; }
        bool IsReady { get; }
        void Tick(float dt);
        bool TryUse();
    }
}
