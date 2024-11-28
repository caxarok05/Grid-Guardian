using System;

namespace Scripts.Data.Loot
{
    [Serializable]
    public class ChestData
    {
        public string id;
        public string level;
        public Vector3Data position;
        public bool IsOpened;

    }
}