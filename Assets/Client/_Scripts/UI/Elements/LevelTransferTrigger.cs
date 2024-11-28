using Scripts.Infrastructure.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Scripts.UI.Elements
{
    public class LevelTransferTrigger : MonoBehaviour
    {
        public string TransferTo;
        private IGameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Transfer()
        {
            _gameStateMachine.Enter<LoadProgressState, string>(TransferTo);
        }
    }   
}
