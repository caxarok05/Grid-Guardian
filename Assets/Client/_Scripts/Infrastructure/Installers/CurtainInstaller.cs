using UnityEngine;
using Zenject;

namespace Scripts.Infrastructure.Installers
{
    public class CurtainInstaller : MonoInstaller
    {
        public GameObject curtainPrefab;
        public override void InstallBindings()
        {
            InstantiateCurtain();
        }
        private void InstantiateCurtain()
        {
            LoadingCurtain curtain = Container.InstantiatePrefabForComponent<LoadingCurtain>(curtainPrefab);
            Container.BindInterfacesAndSelfTo<LoadingCurtain>().FromInstance(curtain).AsSingle();
        }
    }
}