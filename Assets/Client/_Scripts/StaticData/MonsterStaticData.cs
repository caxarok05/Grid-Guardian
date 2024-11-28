using UnityEngine;
using UnityEngine.AddressableAssets;



namespace Scripts.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "Static Data/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeId monsterTypeID;

        [Range(1f, 50f)]
        public int hp;
        [Range(1f, 50f)]
        public int rewardMana;

        public AssetReferenceGameObject prefabReference;

        public Sprite calmSprite;
        public Sprite angrySprite;
    }
}