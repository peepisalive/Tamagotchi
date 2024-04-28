namespace Core
{
    public interface IAdRewardable
    {
        public void OnAdFailedToShow();
        public void OnRewarded();
    }
}