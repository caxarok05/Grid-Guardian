using UnityEngine;

namespace Scripts.UI.Windows.Pause
{
    public class PauseWindow : WindowBase
    {
        public RewardedAdItem adItem;

        public void Exit() => Application.Quit();

        protected override void Init() => adItem.Initialize();

        protected override void SubscribeUpdates() => adItem.Subscribe();

        protected override void Cleanup()
        {
            base.Cleanup();
            adItem.CleanUp();
        }
    }
}
