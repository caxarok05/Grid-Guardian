using System;

namespace Scripts.Data.Player
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel positionOnlevel;

        private string initialLevel;

        public WorldData(string initialLevel)
        {
            positionOnlevel = new PositionOnLevel(initialLevel);

        }
    }
}