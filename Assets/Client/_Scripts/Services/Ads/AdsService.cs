using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Scripts.Services.Ads
{
    public class AdsService : IAdsService, IUnityAdsShowListener, IUnityAdsLoadListener, IUnityAdsInitializationListener
    {
        private const string AndroidGameId = "5732139";
        private const string IOSGameId = "5732138";

        private const string RewardedVideoPlacementId = "rewardedVideo";

        private string _gameId;
        private Action _onVideoFinished;
        private bool _isAdReady = false;

        public int Reward => 5;

        public event Action rewardedVideoReady;

        public AdsService()
        {
            Initialize();
        }
        public void Initialize()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    _gameId = AndroidGameId;
                    break;
                case RuntimePlatform.IPhonePlayer:
                    _gameId = IOSGameId;
                    break;
                case RuntimePlatform.WindowsEditor:
                    _gameId = AndroidGameId;
                    break;
                default:
                    Debug.Log("Unsupported platform");
                    break;
            }

            Advertisement.Initialize(_gameId, true, this);
        }

        public void ShowRewardedVideo(Action onVideoFinished)
        {
            Advertisement.Show(RewardedVideoPlacementId, this);
            _onVideoFinished = onVideoFinished;
        }

        public bool IsRewardedVideoReady() => _isAdReady;
        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log($"OnUnityAdsAdLoaded {placementId}");
            if (placementId == RewardedVideoPlacementId)
            {
                rewardedVideoReady?.Invoke();
                _isAdReady = true;
            }

        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) =>
            Debug.Log("Failed to load ad: " + message);

        public void OnUnityAdsShowClick(string placementId) =>
            Debug.Log($"OnUnityAdsShowClick {placementId}");

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) =>
            Debug.Log($"OnUnityAdsShowFailure {message}");
        public void OnUnityAdsShowStart(string placementId) =>
            Debug.Log($"OnUnityAdsShowStart {placementId}");
        public void OnInitializationComplete() => Debug.Log($"OnInitializationComplete");
        public void OnInitializationFailed(UnityAdsInitializationError error, string message) => Debug.Log($"OnInitializationComplete {message}");


        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            switch (showCompletionState)
            {
                case UnityAdsShowCompletionState.SKIPPED:
                    Debug.LogError($"OnUnityAdsShowComplete {showCompletionState}");
                    break;
                case UnityAdsShowCompletionState.COMPLETED:
                    _onVideoFinished?.Invoke();
                    break;
                case UnityAdsShowCompletionState.UNKNOWN:
                    Debug.LogError($"OnUnityAdsShowComplete {showCompletionState}");
                    break;
                default:
                    Debug.LogError($"OnUnityAdsShowComplete {showCompletionState}");
                    break;
            }
            _onVideoFinished = null;
        }

    }
}