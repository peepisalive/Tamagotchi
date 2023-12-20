using Modules.Navigation;
using UI.Controller;
using UnityEngine;
using UI.Settings;
using Modules;
using Events;
using TMPro;

namespace UI
{
    public sealed class NavigationPanel : MonoBehaviour, IUpdatable<UpdateCurrentScreenEvent>
    {
        [Header("Buttons")]
        [SerializeField] private ImageButtonController _backButton;
        [SerializeField] private ImageButtonController _homeButton;

        [Header("Labels")]
        [SerializeField] private TMP_Text _label;

        private NavigationElementType _type;

        public void Setup()
        {
            _backButton?.Setup(new ImageButtonSettings
            {
                Action = () =>
                {
                    EventSystem.Send(new NavigationPointBackEvent());
                }
            });
            _homeButton?.Setup(new ImageButtonSettings
            {
                Action = () =>
                {
                    EventSystem.Send(new NavigationPointHomeEvent());
                }
            });

            UpdateState();
        }

        public void UpdateState(UpdateCurrentScreenEvent data = null)
        {
            if (_type == default)
                return;

            // to do: set text
        }

        private void Start()
        {
            EventSystem.Subscribe<UpdateCurrentScreenEvent>(UpdateState);
        }

        private void OnDestroy()
        {
            EventSystem.Unsubscribe<UpdateCurrentScreenEvent>(UpdateState);
        }
    }
}