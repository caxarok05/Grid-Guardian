using Scripts.Data;
using Scripts.Data.Location;
using Scripts.Data.Loot;
using Scripts.Data.Monsters;
using Scripts.Infrastructure.AssetManagement;
using Scripts.Infrastructure.Factory;
using Scripts.Logic;
using Scripts.Services.GridService;
using Scripts.Services.PersistantProgress;
using Scripts.Services.SaveLoad;
using Scripts.StaticData;
using Scripts.UI.UIFactory;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Scripts.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private const string spawnerTag = "EnemySpawnerPoint";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistantProgressService _progressService;
        private readonly IGridManager _gridManager;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IPersistantProgressService progressService, IGridManager gridManager, IUIFactory uiFactory)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _gridManager = gridManager;
            _uiFactory = uiFactory;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _gameFactory.Cleanup();
            _gameFactory.WarmUpAsync();
            _sceneLoader.Load(sceneName, onLoaded);
        }

        public void Exit() =>
          _loadingCurtain.Hide();

        private async void onLoaded()
        {
            await InitUIRoot();
            await InitLocation();

            _loadingCurtain.Hide();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private async Task InitUIRoot() => await _uiFactory.CreateUIRoot();

        private void InformProgressReaders()
        {
            foreach(ISavedProgressReader progressReader in _gameFactory.progressReaders)
            {
                progressReader.LoadProgress(_progressService.progress);
            }
        }

        private async Task InitLocation()
        {       
            await _gameFactory.CreateGrid(_progressService.progress.gridData.gridWidth, _progressService.progress.gridData.gridHeight, GameObject.FindWithTag(InitialPointTag));
            GameObject startPoint = await _gameFactory.CreateStartPoint();
            await _gameFactory.CreateHero(AssetAdress.PlayerPath, startPoint.transform.position, _progressService.progress.manaAmount.currentMana);

            if (_progressService.progress.gridData.id != null)
            {
                await LoadChests();
                await LoadMonstersSpawners(_progressService.progress.gridData.greenEnemiesAmount, _progressService.progress.gridData.greenEnemies);
                await LoadMonstersSpawners(_progressService.progress.gridData.blueEnemiesAmount, _progressService.progress.gridData.blueEnemies);
            }
            else
            {
                await CreateNewChests();
                await CreateNewMonstersSpawners(_progressService.progress.gridData.greenEnemiesAmount, MonsterTypeId.GreenEnemy);
                await CreateNewMonstersSpawners(_progressService.progress.gridData.blueEnemiesAmount, MonsterTypeId.BlueEnemy);
            }
            await _gameFactory.CreateHud();
        }

        private async Task CreateNewChests()
        {
            GridData progress = _progressService.progress.gridData;
            List<Vector2> keys = new List<Vector2>(_gridManager.GetTiles().Keys);

            for (int i = 0; i < progress.chestAmount; i++)
            {
                int chosenKey = Random.Range(0, keys.Count);
                if (_gridManager.GetTileAtPosition(keys[chosenKey])._isFree == true)
                {
                    await _gameFactory.CreateChest(AssetAdress.ChestPath, _gridManager.GetTiles()[keys[chosenKey]].transform.position);
                    _gridManager.ChangeAvailability(_gridManager.GetTiles()[keys[chosenKey]].transform.position, isFree: false);
                }
            }         
        }

        private async Task LoadChests()
        {
            foreach(ChestData chestData in _progressService.progress.gridData.chestData)
            {
                Vector2 chosenKey = chestData.position.AsUnityVector();
                if (_gridManager.GetTileAtPosition(chosenKey)._isFree == true)
                {
                    GameObject chest = await _gameFactory.CreateChest(AssetAdress.ChestPath, chosenKey);
                    chest.GetComponent<LootPiece>()._picked = chestData.IsOpened;
                    _gridManager.ChangeAvailability(chestData.position.AsUnityVector(), isFree: false);
                }
            }
        }

        private async Task CreateNewMonstersSpawners(int amount, MonsterTypeId type)
        {
            List<Vector2> keys = new List<Vector2>(_gridManager.GetTiles().Keys);
            while (true)
            {
                for (int i = 0; i < amount; i++)
                {
                    int chosenKey = Random.Range(0, keys.Count);
                    if (_gridManager.GetTileAtPosition(keys[chosenKey])._isFree == true)
                    {
                        await _gameFactory.CreateMonsterSpawner(_gridManager.GetTiles()[keys[chosenKey]].transform.position, type);
                        _gridManager.ChangeAvailability(keys[chosenKey], isFree: false);

                    }
                }
                return;
            }
        }

        private async Task LoadMonstersSpawners(int amount, List<EnemyData> enemyData)
        {
            List<Vector2> keys = new List<Vector2>(_gridManager.GetTiles().Keys);
            foreach (EnemyData spawner in enemyData)
            {
                if (_gridManager.GetTileAtPosition(spawner.position.AsUnityVector())._isFree == true)
                {
                    GameObject enemy = await _gameFactory.CreateMonsterSpawner(spawner.position.AsUnityVector(), spawner.Type);
                    enemy.GetComponent<UniqueId>().Id = spawner.id;
                    _gridManager.ChangeAvailability(spawner.position.AsUnityVector(), isFree: false);
                }
            }
        }
    }
}
