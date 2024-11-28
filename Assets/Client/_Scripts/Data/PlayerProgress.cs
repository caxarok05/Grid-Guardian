using Scripts.Data.Location;
using Scripts.Data.Monsters;
using Scripts.Data.Player;
using System;

namespace Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData worldData;
        public ManaAmount manaAmount;
        public KillData killData;
        public GridData gridData;

        public PlayerProgress(string initialLevel)
        {
            worldData = new WorldData(initialLevel);
            manaAmount = new ManaAmount();
            killData = new KillData();
            gridData = new GridData(initialLevel);
        }

    }
}