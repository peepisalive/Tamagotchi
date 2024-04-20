using System.Collections.Generic;
using Events.Popups;
using UnityEngine;
using UI.Settings;
using UI.Popups;
using Core.Job;
using UI.View;
using Modules;

namespace UI.Controller
{
    [RequireComponent(typeof(JobButtonView), typeof(UnityEngine.UI.Button))]
    public sealed class JobButtonController : MonoBehaviour
    {
        [SerializeField] private JobButtonView _view;
        [SerializeField] private UnityEngine.UI.Button _button;

        private Job _job;

        private string _title;
        private string _content;

        public void Setup(Job job, Sprite icon, string title, string content)
        {
            _job = job;
            _title = title;
            _content = content;

            _view.SetIcon(icon);
            _view.SetTitle(_title);
            _view.SetDescription(_content);
        }

        private void OnClick()
        {
            var dropdownSettings = GetDropdownSettings();

            EventSystem.Send(new ShowPopupEvent
            {
                Settings = new PopupToShow<DefaultPopup>(new DefaultPopup
                {
                    Title = _title,
                    DropdownSettings = dropdownSettings,
                    ButtonSettings = new List<TextButtonSettings>
                    {

                    }
                })
            });


            List<DropdownSettings> GetDropdownSettings()
            {
                var dropdownSettings = (List<DropdownSettings>)null;

                if (_job is FullTimeJob fullTimeJob)
                {
                    var settings = new DropdownSettings
                    {
                        Title = "[test]", // to do: use localization system
                        DropdownContent = new List<DropdownContent>()
                    };

                    foreach (var item in fullTimeJob.WorkingHours)
                    {
                        settings.DropdownContent.Add(new DropdownContent<int>
                        {
                            Title = item.ToString(), // to do: edit this (add hours)
                            Value = item
                        });
                    }

                    dropdownSettings = new List<DropdownSettings>
                    {
                        settings
                    };
                }

                return dropdownSettings;
            }
        }

        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _button.onClick?.RemoveListener(OnClick);
        }
    }
}