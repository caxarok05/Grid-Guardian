using Scripts.Data;
using Scripts.Data.Loot;
using Scripts.Data.Monsters;
using Scripts.Services.PersistantProgress;
using Scripts.Services.SaveLoad;
using Scripts.Services.StaticDataService;
using Scripts.StaticData;
using System.Collections.Generic;

namespace Scripts.Infrastructure.States
{
    public class LoadProgressState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistantProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IStaticDataService _staticDataService;

        private string levelName = "Main";
        public LoadProgressState(GameStateMachine gameStateMachine, IPersistantProgressService progressService, ISaveLoadService saveLoadService, IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _staticDataService = staticDataService;
        }

        public void Enter(string levelName)
        {
            SetScene(levelName);
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.progress.worldData.positionOnlevel.level); 
        }

        public void Exit()
        {
        }

        public void SetScene(string sceneName)
        {
            levelName = sceneName;
        }

        private void LoadProgressOrInitNew() =>
            _progressService.progress = _saveLoadService.LoadProgress() ?? NewProgress();

        private PlayerProgress NewProgress()
        {
            LocationStaticData data = _staticDataService.ForLocation(levelName);
            PlayerProgress progress = new PlayerProgress(initialLevel: levelName);

            progress.manaAmount.currentMana = data.startMana;

            progress.gridData.gridWidth = data.gridWidth;
            progress.gridData.gridHeight = data.gridHeight;

            progress.gridData.chestAmount = data.chestAmount;
            progress.gridData.chestData = new List<ChestData>();

            progress.gridData.greenEnemiesAmount = data.greenEnemyAmount;     
            progress.gridData.greenEnemies = new List<EnemyData>();
            progress.gridData.blueEnemiesAmount = data.blueEnemyAmount;
            progress.gridData.blueEnemies = new List<EnemyData>();
            return progress;
        }
    }
}