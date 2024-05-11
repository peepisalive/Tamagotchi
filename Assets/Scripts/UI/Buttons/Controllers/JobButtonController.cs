using System.Collections.Generic;
using Events.Popups;
using UnityEngine;
using UI.Settings;
using UI.Popups;
using Core.Job;
using UI.View;
using Modules;
using Events;

namespace UI.Controller
{
    [RequireComponent(typeof(JobButtonView), typeof(UnityEngine.UI.Button))]
    public class JobButtonController : MonoBehaviour
    {
        protected string Title;
        protected string Content;

        protected JobButtonView View;
        protected Job Job;

        protected Sprite Icon;

        [SerializeField] private UnityEngine.UI.Button _button;

        public virtual void Setup(Job job, Sprite icon, string title, string content)
        {
            Job = job;
            Icon = icon;
            Title = title;
            Content = content;

            View.SetIcon(Icon);
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
                    Icon = Icon,
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
                            ActionWithInstance = (popup) =>
                            {
                                var currentPopup = (DefaultPopupView)popup;
                                var workingHours = currentPopup.Dropdowns[0].GetCurrentValue<int>();

                                EventSystem.Send(new GettingJobEvent(Job, workingHours));
                                EventSystem.Send(new HidePopupEvent());
                            }
                        }
                    },
                    UseIcon = true
                })
            });
        }

        protected List<DropdownSettings> GetDropdownSettings()
        {
            var dropdownSettings = (List<DropdownSettings>)null;

            if (Job is FullTimeJob fullTimeJob)
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