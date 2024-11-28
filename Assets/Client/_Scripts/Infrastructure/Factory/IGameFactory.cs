using Scripts.Services.SaveLoad;
using Scripts.StaticData;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.Infrastructure.Factory
{
    public interface IGameFactory
    {
        Task<GameObject> CreateEntity(string path, Vector2 at);
        Task<GameObject> CreateHero(string path, Vector2 at, int manaAmount);
        Task CreateHud();

        List<ISavedProgressReader> progressReaders { get; }
        List<ISavedProgress> progressWriters { get; }
        GameObject HeroGameObject { get; set; }

        void Cleanup();
        Task CreateGrid(int width, int height, GameObject at);
        Task<GameObject> CreateStartPoint();
        Task<GameObject> CreateChest(string path, Vector2 at);
        Task<GameObject> CreateMonster(MonsterTypeId typeId, Transform parent);
        Task WarmUpAsync();
        Task<GameObject> CreateMonsterSpawner(Vector2 at, MonsterTypeId type);
    }
}
