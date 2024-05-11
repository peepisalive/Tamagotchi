using Application = Tamagotchi.Application;
using Settings.Job;
using UnityEngine;
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

        public void Setup()
        {
            var currentJob = Application.Model.GetCurrentFullTimeJob();

            gameObject.SetActive(currentJob != null);

            if (currentJob == null)
                return;

            var jobSettings = SettingsProvider.Get<JobSettings>();
            var jobIcon = jobSettings.GetFullTimeJobSettings(currentJob.Job.JobType).Icon;
            var seconds = InGameTimeManager.Instance.FullTimeJobRemainingSeconds;

            _view.SetIcon(jobIcon);
            _view.SetTime(TimeSpan.FromSeconds(seconds).ToString());

            InGameTimeManager.Instance.OnCountFullTimeJobTimeEvent += OnCountFullTimeJobTime;
        }

        public void UpdateState(EndOfFullTimeJobEvent data)
        {
            InGameTimeManager.Instance.OnCountFullTimeJobTimeEvent -= OnCountFullTimeJobTime;
            gameObject.SetActive(false);
        }

        private void OnCountFullTimeJobTime(int seconds)
        {
            _view.SetTime(TimeSpan.FromSeconds(seconds).ToString());
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