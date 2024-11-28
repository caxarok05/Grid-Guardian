using Scripts.Data;
using Scripts.Infrastructure.AssetManagement;
using Scripts.Infrastructure.States;
using Scripts.Logic.Enemy;
using Scripts.Services.GridService;
using Scripts.Services.ManaService;
using Scripts.Services.SaveLoad;
using Scripts.Services.StaticDataService;
using Scripts.Services.WindowService;
using Scripts.StaticData;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {                                                                                 
        private readonly IAssetProvider _assets;                                      
        private readonly IStaticDataService _staticData;                              
        private readonly IGridManager _gridManager;
        private readonly IManaManager _manaManager;
        private readonly IWindowService _windowService;
        private readonly IGameStateMachine _gameStateMachine;

        private readonly DiContainer _container;

        public List<ISavedProgressReader> progressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> progressWriters { get; } = new List<ISavedProgress>();
        public GameObject HeroGameObject { get; set; }


        public GameFactory(IAssetProvider asset, IStaticDataService staticData, IGridManager gridManager,
            IManaManager manaManager, IWindowService windowService, IGameStateMachine gameStateMachine, DiContainer container)
        {
            _assets = asset;
            _staticData = staticData;
            _gridManager = gridManager;
            _manaManager = manaManager;
            _windowService = windowService;
            _gameStateMachine = gameStateMachine;
            _container = container;
        }

        public async Task WarmUpAsync()
        {
            await _assets.Load<GameObject>(AssetAdress.EnemySpawnPoint);
            await _assets.Load<GameObject>(AssetAdress.PlayerPath);
            await _assets.Load<GameObject>(AssetAdress.HudPath);
            await _assets.Load<GameObject>(AssetAdress.GridTilePath);
            await _assets.Load<GameObject>(AssetAdress.ChestPath);
        }

        public async Task<GameObject> CreateEntity(string path, Vector2 at)
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAdress.GridTilePath);          
            return InstantiateRegistred(prefab, at);
        }

        public async Task CreateGrid(int width, int height, GameObject at)
        {
            await _gridManager.GenerateGrid(width, height, at.transform, this);
        }

        public async Task<GameObject> CreateStartPoint()
        {
            _gridManager.ChangeAvailability(_gridManager.GetTiles().First().Key, isFree: false);
            GameObject startPoint = await InstantiateRegistredAsync(AssetAdress.StartPointPath, _gridManager.GetTiles().First().Value.transform.position);
            return startPoint;
        }

        public async Task CreateHud()
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAdress.HudPath);
            GameObject hud = InstantiateRegistred(prefab);
        }

        public async Task<GameObject> CreateHero(string path, Vector2 at, int manaAmount)
        {
            GameObject prefab = await _assets.Load<GameObject>(path);

            HeroGameObject = InstantiateRegistred(prefab, at);

            _manaManager.RegisterUser(HeroGameObject.GetComponent<IManaUser>(), manaAmount);
            return HeroGameObject;

        }

        public async Task<GameObject> CreateChest(string path, Vector2 at)
        {
            GameObject prefab = await _assets.Load<GameObject>(path);
            GameObject chest = InstantiateRegistred(prefab, at);
            return chest;
        }

        public async Task<GameObject> CreateMonsterSpawner(Vector2 at, MonsterTypeId type)
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAdress.EnemySpawnPoint);

            GameObject enemy = InstantiateRegistred(prefab, at);
            enemy.GetComponent<EnemySpawnPoint>().Construct(type, this);
            return enemy;
        }

        public async Task<GameObject> CreateMonster(MonsterTypeId typeId, Transform parent)
        {
            MonsterStaticData monsterData = _staticData.ForMonster(typeId);

            GameObject prefab = await _assets.Load<GameObject>(monsterData.prefabReference);

            GameObject monster = _container.InstantiatePrefab(prefab, parent.position, Quaternion.identity, parent);

            Attack attack = monster.GetComponent<Attack>();
            attack._manaToKill = monsterData.hp;
            attack._rewardMana = monsterData.rewardMana;

            AgressiveBehaviour behaviour = monster.GetComponent<AgressiveBehaviour>();
            behaviour.angrySprite = monsterData.angrySprite;
            behaviour.calmSprite = monsterData.calmSprite;
            
            return monster;
        }

        public void Cleanup()
        {
            progressReaders.Clear();
            progressWriters.Clear();

            _assets.CleanUp();
        }


        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                progressWriters.Add(progressWriter);

            progressReaders.Add(progressReader);
        }
        
        private async Task<GameObject> InstantiateRegistredAsync(string path, Vector2 at)
        {
            GameObject gameObject = await _assets.Instantiate(path, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
        private GameObject InstantiateRegistred(GameObject prefab, Vector2 at)
        {
            GameObject gameObject = _container.InstantiatePrefab(prefab, at, Quaternion.identity, null);
           
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
        private async Task<GameObject> InstantiateRegistredAsync(string path)
        {
            GameObject gameObject = await _assets.Instantiate(path);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
        private GameObject InstantiateRegistred(GameObject prefab)
        {
            GameObject gameObject = _container.InstantiatePrefab(prefab);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponents<ISavedProgressReader>())
                Register(progressReader);
        }
    }
}
