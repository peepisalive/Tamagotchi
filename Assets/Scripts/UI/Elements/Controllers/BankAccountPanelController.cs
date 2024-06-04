using Application = Tamagotchi.Application;
using UnityEngine;
using System.Text;
using UI.View;

namespace UI.Controller
{
    [RequireComponent(typeof(BankAccountPanelView))]
    public sealed class BankAccountPanelController : MonoBehaviour
    {
        [SerializeField] private BankAccountPanelView _view;
        private StringBuilder _stringBuilder;

        private void OnValueChangedEvent(int previousValue, int value)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(value);

            _view.SetValue(_stringBuilder.ToString());
        }

        private void Start ()
        {
            var bankAccount = Application.Model.GetBankAccount();
            _stringBuilder = new StringBuilder();

            bankAccount.OnValueChangedEvent += OnValueChangedEvent;

            _stringBuilder.Append(bankAccount.Value);
            _view.SetValue(_stringBuilder.ToString());
        }

        private void OnDestroy()
        {
            var bankAccount = Application.Model.GetBankAccount();

            if (bankAccount == null)
                return;

            bankAccount.OnValueChangedEvent -= OnValueChangedEvent;
        }
    }
}