using Application = Tamagotchi.Application;
using System.Collections.Generic;
using Modules.Navigation;
using Settings.Job;
using UnityEngine;
using System.Linq;
using Core.Job;
using Settings;
using Modules;
using Events;

namespace UI.Controller.Screen
{
    public sealed class JobScreenController : ScreenController, IUpdatable<UpdateCurrentScreenEvent>
    {
        [Header("Controller")]
        [SerializeField] private RectTransform _layoutsParent;

        [Header("Prefabs")]
        [SerializeField] private PartTimeJobButtonController _partTimeJobButtonPrefab;
        [SerializeField] private JobButtonController _jobButtonPrefab;
        [SerializeField] private RectTransform _layoutPrefab;

        private NavigationBlock _navigationBlock;
        private NavigationPoint _navigationPoint;

        private JobSettings _settings;

        public override void Setup()
        {
            base.Setup();

            if (_navigationBlock == null || _navigationPoint == null)
                return;

            UpdateState();
        }

        public void UpdateState(UpdateCurrentScreenEvent data = null)
        {
            var jobList = Application.Model.GetAvailableJob().ToList();

            if (!Application.Model.PartTimeIsAvailable())
                jobList = jobList?.Where(j => j is not PartTimeJob)?.ToList();

            if (jobList == null || !jobList.Any())
                return;

            foreach (Transform child in _layoutsParent)
            {
                Destroy(child.gameObject);
            }

            var layoutRectList = new List<RectTransform>();
            var layoutRectIdx = 0;

            for (var i = 0; i < jobList.Count; ++i)
            {
                if (layoutRectList.Count - 1 < layoutRectIdx)
                    layoutRectList.Add(Instantiate(_layoutPrefab, _layoutsParent));

                var job = jobList[i];
                var content = string.Empty;

                if (job is FullTimeJob fullTimeJob)
                {
                    var settings = _settings.GetFullTimeJobSettings(fullTimeJob.JobType);
                    var title = _settings.Localization.GetFulltimeJobName(fullTimeJob.JobType);

                    for (int j = 0; j < settings.WorkingHours.Count; ++j)
                    {
                        content += (j == settings.WorkingHours.Count - 1)
                            ? $"{settings.WorkingHours[j]} {_settings.Localization.HoursText}"
                            : $"{settings.WorkingHours[j]}, ";
                    }

                    Instantiate(_jobButtonPrefab, layoutRectList[layoutRectIdx]).Setup(job, settings.Icon, title, content);
                }
                else if (job is PartTimeJob partTimeJob)
                {
                    var icon = _settings.GetPartTimeJobSettings(partTimeJob.JobType).Icon;
                    var title = _settings.Localization.GetPartTimeJobName(partTimeJob.JobType);

                    Instantiate(_partTimeJobButtonPrefab, layoutRectList[layoutRectIdx]).Setup(job, icon, title, content);
                }

                if (i % 2 != 0)
                    ++layoutRectIdx;
            }
        }

        private void Awake()
        {
            _navigationBlock = Application.Model.GetCurrentNavigationBlock();
            _navigationPoint = Application.Model.GetCurrentNavigationPoint();
            _settings = SettingsProvider.Get<JobSettings>();

            EventSystem.Subscribe<UpdateCurrentScreenEvent>(UpdateState);
        }

        private void OnDestroy()
        {
            EventSystem.Unsubscribe<UpdateCurrentScreenEvent>(UpdateState);
        }
    }
}