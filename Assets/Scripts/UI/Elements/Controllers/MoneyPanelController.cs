using Application = Tamagotchi.Application;
using UnityEngine;
using UI.View;

namespace UI.Controller
{
    [RequireComponent(typeof(MoneyPanelView))]
    public sealed class MoneyPanelController : MonoBehaviour
    {
        [SerializeField] private MoneyPanelView _view;

        private void Start ()
        {
            var bankAccount = Application.Model.GetBankAccount();

            bankAccount.OnValueChangedEvent += _view.SetValue;
            _view.SetValue(bankAccount.Value);
        }

        //private void OnDisable() // TO DO: null ref ex
        //{
        //    Application.Model.GetBankAccount().OnValueChangedEvent -= _view.SetValue;
        //}
    }
}