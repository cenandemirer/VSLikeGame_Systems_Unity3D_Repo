using System.Collections.Generic;
using UnityEngine;
using VSLikeGame.Player;

namespace VSLikeGame.Skills
{
    public sealed class SkillRunner : MonoBehaviour
    {
        [SerializeField] MonoBehaviour inputProviderBehaviour;
        [SerializeField] List<MonoBehaviour> skillBehaviours = new();

        IInputProvider input;
        readonly List<ICombatSkill> skills = new();
        readonly List<ISlottedSkill> slotted = new();

        void Awake()
        {
            input = inputProviderBehaviour as IInputProvider;
            for (int i = 0; i < skillBehaviours.Count; i++)
            {
                if (skillBehaviours[i] is ICombatSkill s) skills.Add(s);
                if (skillBehaviours[i] is ISlottedSkill ss) slotted.Add(ss);
            }
        }

        void Update()
        {
            float dt = Time.deltaTime;

            for (int i = 0; i < skills.Count; i++) skills[i].Tick(dt);

            if (input == null) return;

            if (input.SkillPressed) TryUseSlot(SkillSlot.Primary);

            if (input.DashPressed) TryUseSlot(SkillSlot.Secondary);
        }

        void TryUseSlot(SkillSlot slot)
        {
            for (int i = 0; i < slotted.Count; i++)
            {
                if (slotted[i].Slot == slot)
                    slotted[i].TryUse();
            }
        }
    }
}
