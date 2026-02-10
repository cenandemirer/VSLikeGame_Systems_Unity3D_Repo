using System.IO;
using UnityEngine;
using VSLikeGame.Run;

namespace VSLikeGame.Save
{
    public interface ISaveManager
    {
        SaveData Data { get; }
        void Load();
        void Save();
        void ApplyRun(IRunStats run);
    }

    public sealed class SaveManager : MonoBehaviour, ISaveManager
    {
        public SaveData Data => data;

        [SerializeField] string fileName = "vslike_save.json";
        SaveData data = new();

        string PathToFile => System.IO.Path.Combine(Application.persistentDataPath, fileName);

        void Awake() => Load();

        public void Load()
        {
            try
            {
                if (!File.Exists(PathToFile))
                {
                    data = new SaveData();
                    return;
                }
                data = JsonUtility.FromJson<SaveData>(File.ReadAllText(PathToFile)) ?? new SaveData();
            }
            catch
            {
                data = new SaveData();
            }
        }

        public void Save()
        {
            try
            {
                File.WriteAllText(PathToFile, JsonUtility.ToJson(data, true));
            }
            catch { }
        }

        public void ApplyRun(IRunStats run)
        {
            if (run == null) return;

            data.totalKills += run.Kills;

            int seconds = Mathf.RoundToInt(run.TimeAlive);
            if (seconds > data.bestRunSeconds) data.bestRunSeconds = seconds;

            Save();
        }
    }
}
