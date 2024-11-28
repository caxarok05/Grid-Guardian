using System;

namespace Scripts.Data.Loot
{
    [Serializable]
    public class Chest
    {
        public bool isOpened;

        public Chest(bool isOpened)
        {
            this.isOpened = isOpened;
        }
    }
}