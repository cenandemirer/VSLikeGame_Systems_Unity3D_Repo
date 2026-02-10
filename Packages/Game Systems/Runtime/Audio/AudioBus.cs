using System.Collections.Generic;
using UnityEngine;
using VSLikeGame.Core;

namespace VSLikeGame.Audio
{
    [System.Serializable]
    public sealed class AudioEvent
    {
        public string id;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
        public bool spatial = false;
        [Range(0f, 1f)] public float spatialBlend = 1f;
    }

    public sealed class AudioBus : MonoBehaviour, IAudioBus
    {
        [SerializeField] AudioSource source2D;
        [SerializeField] AudioSource source3D;
        [SerializeField] List<AudioEvent> events = new();

        readonly Dictionary<string, AudioEvent> map = new();

        void Awake()
        {
            for (int i = 0; i < events.Count; i++)
            {
                var e = events[i];
                if (e != null && !string.IsNullOrWhiteSpace(e.id) && !map.ContainsKey(e.id))
                    map.Add(e.id, e);
            }
            ServiceRegistry.Register<IAudioBus>(this);

            if (source3D != null) source3D.spatialBlend = 1f;
            if (source2D != null) source2D.spatialBlend = 0f;
        }

        void OnDestroy() => ServiceRegistry.Unregister<IAudioBus>();

        public void Play(string eventId)
        {
            if (!TryGet(eventId, out var e)) return;
            var src = e.spatial ? source3D : source2D;
            if (src == null || e.clip == null) return;

            src.PlayOneShot(e.clip, e.volume);
        }

        public void PlayAt(string eventId, Vector3 position)
        {
            if (!TryGet(eventId, out var e)) return;
            if (e.clip == null) return;

            if (e.spatial)
            {
                AudioSource.PlayClipAtPoint(e.clip, position, e.volume);
            }
            else
            {
                if (source2D == null) return;
                source2D.PlayOneShot(e.clip, e.volume);
            }
        }

        bool TryGet(string id, out AudioEvent e)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                e = null;
                return false;
            }
            return map.TryGetValue(id, out e);
        }
    }
}
