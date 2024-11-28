using Scripts.Data;
using Scripts.Infrastructure.Factory;
using Scripts.Services.PersistantProgress;
using UnityEngine;

namespace Scripts.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly IGameFactory _gameFactory;
        private readonly IPersistantProgressService _progressService;

        public SaveLoadService(IGameFactory gameFactory, IPersistantProgressService progressService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();

        public void SaveProgress()
        {
            foreach(ISavedProgress progressWriters in _gameFactory.progressWriters)
                progressWriters.UpdateProgress(_progressService.progress);

            PlayerPrefs.SetString(ProgressKey, _progressService.progress.ToJson());
        }
    }
}
