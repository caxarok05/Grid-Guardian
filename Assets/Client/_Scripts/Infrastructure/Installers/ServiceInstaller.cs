using Scripts.Infrastructure.AssetManagement;
using Scripts.Infrastructure.Factory;
using Scripts.Services.Ads;
using Scripts.Services.GridService;
using Scripts.Services.ManaService;
using Scripts.Services.PersistantProgress;
using Scripts.Services.SaveLoad;
using Scripts.Services.StaticDataService;
using Scripts.Services.WindowService;
using Scripts.UI.UIFactory;
using Zenject;

namespace Scripts.Infrastructure.Installers
{
    public class ServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStaticData();
            BindAdsService();
            BindAssetProvider();
            BindPersistantProgressService();
            BindGridManager();
            BindManaManager();
            BindUIFactory();
            BindWindowService();
            BindGameFactory();
            BindSaveLoadService();
            BindStateFactory();
        }

        private void BindSaveLoadService() => Container.BindInterfacesAndSelfTo<SaveLoadService>().AsSingle();

        private void BindGameFactory() => Container.BindInterfacesAndSelfTo<GameFactory>().AsSingle();

        private void BindWindowService() => Container.BindInterfacesAndSelfTo<WindowService>().AsSingle();

        private void BindUIFactory() => Container.BindInterfacesAndSelfTo<UIFactory>().AsSingle();

        private void BindManaManager() => Container.BindInterfacesAndSelfTo<ManaManager>().AsSingle();

        private void BindGridManager() => Container.BindInterfacesAndSelfTo<GridManager>().AsSingle();

        private void BindPersistantProgressService() => Container.BindInterfacesAndSelfTo<PersistantProgressService>().AsSingle();

        private void BindAssetProvider() => Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();

        private void BindAdsService() => Container.BindInterfacesAndSelfTo<AdsService>().AsSingle();

        private void BindStaticData() => Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();

        private void BindStateFactory() => Container.Bind<StateFactory>().AsSingle();

    }
}