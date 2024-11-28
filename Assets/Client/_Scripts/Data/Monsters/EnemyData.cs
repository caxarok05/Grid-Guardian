using Scripts.StaticData;
using System;

namespace Scripts.Data.Monsters
{
    [Serializable]
    public class EnemyData
    {
        public MonsterTypeId Type;
        public Vector3Data position;
        public string level;
        public string id;
    }
}