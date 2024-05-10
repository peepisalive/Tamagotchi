using Application = Tamagotchi.Application;

namespace Utils
{
    public static class CurrencyUtils
    {
        public static bool TrySpendMoney(int value)
        {
            var bankAccount = Application.Model.GetBankAccount();

            if (!bankAccount.IsMoneyAvailable(value))
            {
                PopupUtils.ShowNotEnoughMoneyPopup();
                return false;
            }

            bankAccount.Add(value);

            return true;
        }
    }
}