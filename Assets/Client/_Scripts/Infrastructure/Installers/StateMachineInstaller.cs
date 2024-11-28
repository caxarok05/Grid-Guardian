using Scripts.Infrastructure.States;
using Zenject;

namespace Scripts.Infrastructure.Installers
{
    public class StateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStates();
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
        }

        private void BindStates()
        {
            Container.Bind<BootstrapState>().AsSingle().NonLazy();
            Container.Bind<LoadProgressState>().AsSingle().NonLazy();
            Container.Bind<LoadLevelState>().AsSingle().NonLazy();
            Container.Bind<GameLoopState>().AsSingle().NonLazy();
        }
    }
}