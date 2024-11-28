using Scripts.Infrastructure.AssetManagement;
using Scripts.Infrastructure.Factory;
using Scripts.Services.Ads;
using Scripts.Services.ManaService;
using Scripts.Services.StaticDataService;
using Scripts.Services.WindowService;
using Scripts.StaticData;
using Scripts.UI.Windows;
using Scripts.UI.Windows.Pause;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Scripts.UI.UIFactory
{
    public class UIFactory : IUIFactory
    {
        
        private readonly IAssetProvider _assets;
        private Transform _uiRoot;
        private readonly IAdsService _adsService;
        private readonly IManaManager _manaManager;
        private readonly IStaticDataService _staticData;
        private readonly DiContainer _container;

        private IGameFactory _gameFactory;

        public UIFactory(IAssetProvider assets, IStaticDataService staticData, IManaManager manaManager, IAdsService adsService, DiContainer container)
        {
            _assets = assets;
            _staticData = staticData;
            _manaManager = manaManager;
            _adsService = adsService;
            _container = container;
        }

        public void SetFactory(IGameFactory gameFactory) => _gameFactory = gameFactory;
        public void CreatePause()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Pause);
            PauseWindow windowBase = _container.InstantiatePrefabForComponent<WindowBase>(config.prefab, _uiRoot) as PauseWindow;
        }

        public async Task CreateUIRoot()
        {
            GameObject rootObject = await _assets.Instantiate(AssetAdress.UIRootPath);
            _uiRoot = rootObject.transform;
        }
   
    }
}
