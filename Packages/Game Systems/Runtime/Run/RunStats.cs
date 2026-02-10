using UnityEngine;
using VSLikeGame.Core;

namespace VSLikeGame.Run
{
    public interface IRunStats
    {
        int Kills { get; }
        float TimeAlive { get; }
        RunState State { get; }
        void StartRun();
        void EndRun();
        void AddKill();
    }

    public sealed class RunStats : MonoBehaviour, IRunStats, ITickable
    {
        public int Kills { get; private set; }
        public float TimeAlive { get; private set; }
        public RunState State { get; private set; } = RunState.None;

        void Awake() => ServiceRegistry.Register<IRunStats>(this);
        void OnDestroy() => ServiceRegistry.Unregister<IRunStats>();

        public void StartRun()
        {
            Kills = 0;
            TimeAlive = 0f;
            State = RunState.Running;
            GameEvents.RaiseRunStarted();
        }

        public void EndRun()
        {
            if (State != RunState.Running) return;
            State = RunState.Ended;
            GameEvents.RaiseRunEnded();
        }

        public void AddKill()
        {
            if (State == RunState.Running) Kills++;
        }

        public void Tick(float dt)
        {
            if (State == RunState.Running) TimeAlive += dt;
        }
    }
}
