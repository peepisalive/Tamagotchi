using Application = Tamagotchi.Application;
using Settings.Job;
using UnityEngine;
using System.Text;
using Settings;
using UI.View;
using Modules;
using Events;
using System;

namespace UI.Controller
{
    [RequireComponent(typeof(FullTimeJobPanelView))]
    public sealed class FullTimeJobPanelController : MonoBehaviour, IUpdatable<EndOfFullTimeJobEvent>
    {
        [SerializeField] private FullTimeJobPanelView _view;
        private StringBuilder _stringBuilder;

        public void Setup()
        {
            var currentJob = Application.Model.GetCurrentFullTimeJob();

            gameObject.SetActive(currentJob != null);

            if (currentJob == null)
                return;

            var jobSettings = SettingsProvider.Get<JobSettings>();
            var jobIcon = jobSettings.GetFullTimeJobSettings(currentJob.Job.JobType).Icon;
            var seconds = InGameTimeManager.Instance.RemainingSeconds;

            _stringBuilder = new StringBuilder(8);
            _stringBuilder.Append(TimeSpan.FromSeconds(seconds));

            _view.SetIcon(jobIcon);
            _view.SetTime(_stringBuilder.ToString());

            InGameTimeManager.Instance.OnCountRemainingTimeEvent += OnCountFullTimeJobTime;
        }

        public void UpdateState(EndOfFullTimeJobEvent data)
        {
            InGameTimeManager.Instance.OnCountRemainingTimeEvent -= OnCountFullTimeJobTime;
            gameObject.SetActive(false);
        }

        private void OnCountFullTimeJobTime(int seconds)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(TimeSpan.FromSeconds(seconds));

            _view.SetTime(_stringBuilder.ToString());
        }

        private void Start()
        {
            EventSystem.Subscribe<EndOfFullTimeJobEvent>(UpdateState);
        }

        private void OnDestroy()
        {
            EventSystem.Unsubscribe<EndOfFullTimeJobEvent>(UpdateState);
        }
    }
}