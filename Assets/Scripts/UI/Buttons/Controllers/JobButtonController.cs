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
    public class JobButtonController : MonoBehaviour
    {
        protected string Title;
        protected string Content;
        protected JobButtonView View;

        [SerializeField] private UnityEngine.UI.Button _button;

        private Job _job;

        public virtual void Setup(Job job, Sprite icon, string title, string content)
        {
            _job = job;
            Title = title;
            Content = content;

            View.SetIcon(icon);
            View.SetTitle(Title);
            View.SetDescription(Content);
        }

        protected virtual void OnClick()
        {
            EventSystem.Send(new ShowPopupEvent
            {
                Settings = new PopupToShow<DefaultPopup>(new DefaultPopup
                {
                    Title = Title,
                    DropdownSettings = GetDropdownSettings(),
                    ButtonSettings = new List<TextButtonSettings>
                    {
                        new TextButtonSettings
                        {
                            Action = () =>
                            {
                                EventSystem.Send(new HidePopupEvent());
                            }
                        },
                        new TextButtonSettings
                        {
                            Action = () =>
                            {

                            }
                        }
                    }
                })
            });
        }

        protected List<DropdownSettings> GetDropdownSettings()
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

        private void Awake()
        {
            View = GetComponent<JobButtonView>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _button.onClick?.RemoveListener(OnClick);
        }
    }
}