using Application = Tamagotchi.Application;
using Leopotam.Ecs;
using Components;

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

        public static bool TrySpendMoney(this EcsFilter<BankAccountComponent> bankAccountFilter, int value)
        {
            var result = false;
            
            foreach (var i in bankAccountFilter)
            {
                var bankAccount = bankAccountFilter.Get1(i).BankAccount;

                if (!bankAccount.IsMoneyAvailable(value))
                {
                    PopupUtils.ShowNotEnoughMoneyPopup();
                    break;
                }

                bankAccount.Add(value);
                result = true;
            }

            return result;
        }
    }
}