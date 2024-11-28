
using Scripts.Logic;

namespace Scripts.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Main";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {           
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel() =>
          _stateMachine.Enter<LoadProgressState, string>(Initial);

    }
}