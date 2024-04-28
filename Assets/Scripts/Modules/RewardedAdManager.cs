using YandexMobileAds.Base;
using YandexMobileAds;
using UnityEngine;
using System;
using Core;

namespace Modules
{
    public sealed class RewardedAdManager : MonoBehaviourSingleton<RewardedAdManager>
    {
        public event Action OnAdFailedToShowCallback;
        public event Action OnRewardedCallback;

        private RewardedAdLoader _rewardedAdLoader;
        private RewardedAd _rewardedAd;

        public void ShowRewardedAd()
        {
#if UNITY_EDITOR
            OnRewardedCallback?.Invoke();

            OnAdFailedToShowCallback = null;
            OnRewardedCallback = null;

            return;
#endif
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

            OnAdFailedToShowCallback = null;
            OnRewardedCallback = null;
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
            _rewardedAd.OnRewarded += OnRewarded;

            Debug.Log("Ad loaded");
        }

        private void OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            Debug.Log($"Ad failed to load: {args.Message}");
        }

        public void OnAdFailedToShow(object sender, AdFailureEventArgs args)
        {
            OnAdFailedToShowCallback?.Invoke();

            DestroyRewardedAd();
            RequestRewardedAd();

            Debug.Log("Ad failed to show");
        }

        private void OnRewarded(object sender, Reward e)
        {
            OnRewardedCallback?.Invoke();

            DestroyRewardedAd();
            RequestRewardedAd();

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