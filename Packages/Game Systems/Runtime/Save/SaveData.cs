using System;

namespace VSLikeGame.Save
{
    [Serializable]
    public sealed class SaveData
    {
        public int version = 1;
        public int metaCurrency;
        public int bestRunSeconds;
        public int totalKills;
    }
}
