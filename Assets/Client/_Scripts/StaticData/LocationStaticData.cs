using UnityEngine;

namespace Scripts.StaticData
{
    [CreateAssetMenu(fileName = "LocationData", menuName = "Static Data/Location")]
    public class LocationStaticData : ScriptableObject
    {
        public string level;

        [Range(1f, 20f)]
        public int gridWidth;
        [Range(1f, 20f)]
        public int gridHeight;

        [Range(1f, 100f)]
        public int startMana;

        public int chestAmount;

        public int blueEnemyAmount;
        public int greenEnemyAmount;

       

    }
}