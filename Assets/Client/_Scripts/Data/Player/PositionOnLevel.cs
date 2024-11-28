using System;

namespace Scripts.Data.Player
{
    [Serializable]
    public class PositionOnLevel
    {
        public string level;
        public Vector3Data position;
        private string initialLevel;

        public PositionOnLevel(string initialLevel)
        {
            level = initialLevel;
        }

        public PositionOnLevel(string level, Vector3Data position)
        {
            this.level = level;
            this.position = position;
        }
    }
}