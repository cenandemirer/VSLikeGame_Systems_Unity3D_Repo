namespace VSLikeGame.Audio
{
    public interface IAudioBus
    {
        void Play(string eventId);
        void PlayAt(string eventId, UnityEngine.Vector3 position);
    }
}
