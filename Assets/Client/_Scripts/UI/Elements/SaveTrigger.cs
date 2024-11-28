using Scripts.Infrastructure.States;
using Scripts.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace Scripts.UI.Elements
{
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;

        [Inject]
        public void Construct(ISaveLoadService saveLoadService) => _saveLoadService = saveLoadService;

        public void Save() => _saveLoadService.SaveProgress();
    }
}