using System;

namespace VSLikeGame.Core
{
    public static class GameEvents
    {
        public static event Action<bool> OnInputLockChanged;
        public static event Action<bool> OnPausedChanged;

        public static event Action OnRunStarted;
        public static event Action OnRunEnded;

        public static void RaiseInputLock(bool locked) => OnInputLockChanged?.Invoke(locked);
        public static void RaisePaused(bool paused) => OnPausedChanged?.Invoke(paused);

        public static void RaiseRunStarted() => OnRunStarted?.Invoke();
        public static void RaiseRunEnded() => OnRunEnded?.Invoke();
    }
}
