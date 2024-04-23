using YandexMobileAds.Base;
using YandexMobileAds;
using UnityEngine;
using Core;

namespace Modules
{
    public sealed class RewardedAdManager : MonoBehaviourSingleton<RewardedAdManager>
    {
        private RewardedAdLoader _rewardedAdLoader;
        private RewardedAd _rewardedAd;

        public void ShowRewardedAd()
        {
            if (_rewardedAd == null)
                return;

            _rewardedAd.Show();
        }

        private void DestroyRewardedAd()
        {
            if (_rewardedAd == null)
                return;

            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        private void SetupLoader()
        {
            _rewardedAdLoader = new RewardedAdLoader();

            _rewardedAdLoader.OnAdLoaded += OnAdLoaded;
            _rewardedAdLoader.OnAdFailedToLoad += OnAdFailedToLoad;
        }

        private void RequestRewardedAd()
        {
            var adUnitId = "demo-rewarded-yandex"; // to do: replace it with your ID
            var adRequestConfiguration = new AdRequestConfiguration.Builder(adUnitId).Build();

            _rewardedAdLoader.LoadAd(adRequestConfiguration);
        }

        private void OnAdLoaded(object sender, RewardedAdLoadedEventArgs args)
        {
            _rewardedAd = args.RewardedAd;

            _rewardedAd.OnAdFailedToShow += OnAdFailedToShow;
            _rewardedAd.OnAdImpression += OnAdImpression;
            _rewardedAd.OnRewarded += OnRewarded;

            Debug.Log("Ad loaded");
        }

        private void OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            // Ad {args.AdUnitId} failed for to load with {args.Message}
            // Attempting to load new ad from the OnAdFailedToLoad event is strongly discouraged.

            Debug.Log("Ad failed to load");
        }

        public void OnAdFailedToShow(object sender, AdFailureEventArgs args)
        {
            DestroyRewardedAd();
            RequestRewardedAd();

            Debug.Log("Ad failed to show");
        }

        public void OnAdImpression(object sender, ImpressionData impressionData)
        {
            DestroyRewardedAd();
            RequestRewardedAd();

            Debug.Log("Ad impression");
        }

        private void OnRewarded(object sender, Reward e)
        {
            Debug.Log("Rewarded");
        }

        private void Awake()
        {
            Instance = this;
            
            SetupLoader();
            RequestRewardedAd();
        }
    }
}