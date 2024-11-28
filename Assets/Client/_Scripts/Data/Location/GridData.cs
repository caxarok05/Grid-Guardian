using Scripts.Data.Loot;
using Scripts.Data.Monsters;
using System;
using System.Collections.Generic;

namespace Scripts.Data.Location
{
    [Serializable]
    public class GridData
    {
        public string level;
        public string id;

        public int gridWidth;
        public int gridHeight;

        public int blueEnemiesAmount;
        public int greenEnemiesAmount;

        public List<EnemyData> blueEnemies;
        public List<EnemyData> greenEnemies;

        public int chestAmount;
        public List<ChestData> chestData;

        public GridData(string levelName)
        {
            level = levelName;
        }

    }
}