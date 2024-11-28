using System;

namespace Scripts.Services.Ads
{
    public interface IAdsService
    {
        int Reward { get; }

        event Action rewardedVideoReady;

        void Initialize();
        bool IsRewardedVideoReady();
        void ShowRewardedVideo(Action onVideoFinished);
    }
}