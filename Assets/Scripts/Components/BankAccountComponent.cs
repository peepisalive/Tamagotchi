using Core;

namespace Components
{
    public struct BankAccountComponent
    {
        public BankAccount BankAccount;
    }

    public struct ChangeBankAccountValueEvent
    {
        public int Value;
    }
}