using Scripts.Logic;
using UnityEngine;
using Zenject;

namespace Scripts.Infrastructure.Installers
{
    public class SceneLoaderInstaller : MonoInstaller
    {
        public GameObject coroutineRunner;
        public override void InstallBindings()
        {
            InstantiateCoroutineRunner();
            Container.Bind<SceneLoader>().AsSingle();
        }
        private void InstantiateCoroutineRunner()
        {
            CoroutineRunner bootstraper = Container.InstantiatePrefabForComponent<CoroutineRunner>(coroutineRunner);
            Container.BindInterfacesAndSelfTo<CoroutineRunner>().FromInstance(bootstraper).AsSingle();
        }
    }
}