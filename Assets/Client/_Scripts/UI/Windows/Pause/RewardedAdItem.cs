using Scripts.Infrastructure.Factory;
using Scripts.Services.Ads;
using Scripts.Services.ManaService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.UI.Windows.Pause
{
    public class RewardedAdItem : MonoBehaviour
    {
        public Button showAdButton;

        public GameObject[] AdActiveObjects;
        public GameObject[] AdInactiveObjects;

        private IAdsService _adsService;
        private IManaManager _manaManager;
        private IManaUser _manaUser;

        [Inject]
        public void Construct(IAdsService adsService, IManaManager manaManager, IGameFactory factory)
        {
            _adsService = adsService;
            _manaManager = manaManager;
            _manaUser = factory.HeroGameObject.GetComponent<IManaUser>();
        }
        public void Initialize()
        {
            showAdButton.onClick.AddListener(OnShowAdClicked);

            RefreshAvailableAd();
        }

        public void Subscribe() => _adsService.rewardedVideoReady += RefreshAvailableAd;
        public void CleanUp() => _adsService.rewardedVideoReady -= RefreshAvailableAd;

        private void OnShowAdClicked() => _adsService.ShowRewardedVideo(OnVideoFinished);

        private void OnVideoFinished() => _manaManager.RestoreMana(_manaUser, _adsService.Reward);

        private void RefreshAvailableAd()
        {
            bool videoReady = _adsService.IsRewardedVideoReady();

            foreach(GameObject adActiveObject in AdActiveObjects)
            {
                adActiveObject.SetActive(videoReady);
            }
            foreach (GameObject adInactiveObjects in AdInactiveObjects)
            {
                adInactiveObjects.SetActive(!videoReady);
            }
        }
    }
}
