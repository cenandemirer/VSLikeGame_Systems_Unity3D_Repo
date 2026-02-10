using UnityEngine;

namespace VSLikeGame.Player
{
    public interface IInputProvider
    {
        Vector2 Move { get; }
        Vector2 Look { get; }
        bool DashPressed { get; }
        bool SkillPressed { get; }
    }
}
