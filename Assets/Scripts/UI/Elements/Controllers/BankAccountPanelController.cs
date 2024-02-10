using Application = Tamagotchi.Application;
using UnityEngine;
using UI.View;

namespace UI.Controller
{
    [RequireComponent(typeof(BankAccountPanelView))]
    public sealed class BankAccountPanelController : MonoBehaviour
    {
        [SerializeField] private BankAccountPanelView _view;

        private void Start ()
        {
            var bankAccount = Application.Model.GetBankAccount();

            bankAccount.OnValueChangedEvent += _view.SetValue;
            _view.SetValue(bankAccount.Value);
        }

        private void OnDestroy()
        {
            var bankAccount = Application.Model.GetBankAccount();

            if (bankAccount == null)
                return;

            bankAccount.OnValueChangedEvent -= _view.SetValue;
        }
    }
}