using UnityEngine;
using Modules;
using System;
using Events;
using Utils;

namespace Core
{
    [Serializable]
    public sealed class Accessory : AccessElement, IAdRewardable
    {
        public AccessoryType Type { get; private set; }
        public int Value { get; private set; }

        public GameObject Model { get; private set; }
        public Color Color { get; private set; }

        public Accessory(AccessoryType type, AccessType accessType, int value) : base(accessType, false, false)
        {
            Type = type;
            Value = value;
        }

        public void SetModel(GameObject model)
        {
            if (Model != null)
                return;

            Model = model;
        }

        public void SetColor(Color color)
        {
            Color = color;
        }

        public bool TryPurchase()
        {
            if (AccessType == AccessType.Ads)
            {
                RewardedAdManager.Instance.OnAdFailedToShowCallback += OnAdFailedToShow;
                RewardedAdManager.Instance.OnRewardedCallback += OnRewarded;
                RewardedAdManager.Instance.ShowRewardedAd();
            }

            return AccessType switch
            {
                AccessType.Free => true,
                AccessType.Money => CurrencyUtils.TrySpendMoney(Value),
                _ => false
            };
        }

        public void OnAdFailedToShow()
        {
            PopupUtils.ShowNoAdsAvailablePopup();
        }

        public void OnRewarded()
        {
            EventSystem.Send(new UnlockAccessoryEvent());
        }
    }
}