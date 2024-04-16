using Application = Tamagotchi.Application;
using System.Collections.Generic;
using Modules.Navigation;
using Settings.Job;
using UnityEngine;
using System.Linq;
using Core.Job;
using Settings;

namespace UI.Controller.Screen
{
    public sealed class JobScreenController : ScreenController
    {
        [Header("Controller")]
        [SerializeField] private RectTransform _layoutsParent;

        [Space]
        [SerializeField] private JobButtonController _buttonPrefab;
        [SerializeField] private RectTransform _layoutPrefab;

        private NavigationBlock _navigationBlock;
        private NavigationPoint _navigationPoint;

        private JobSettings _settings;

        public override void Setup()
        {
            base.Setup();

            if (_navigationBlock == null || _navigationPoint == null)
                return;

            var jobList = Application.Model.GetAvailableJob().ToArray();

            if (jobList == null)
                return;

            var layoutRectList = new List<RectTransform>();
            var layoutRectIdx = 0;

            for (var i = 0; i < jobList.Length; ++i)
            {
                if (layoutRectList.Count - 1 < layoutRectIdx)
                    layoutRectList.Add(Instantiate(_layoutPrefab, _layoutsParent));

                var icon = (Sprite)default;
                var title = string.Empty;
                var job = jobList[i];

                if (job is FullTimeJob fullTimeJob)
                {
                    icon = _settings.GetFullTimeJobSettings(fullTimeJob.JobType).Icon;
                    title = _settings.Localization.GetFulltimeJobName(fullTimeJob.JobType);
                }
                else if (job is PartTimeJob partTimeJob)
                {
                    icon = _settings.GetPartTimeJobSettings(partTimeJob.JobType).Icon;
                    title = _settings.Localization.GetParttimeJobName(partTimeJob.JobType);
                }

                Instantiate(_buttonPrefab, layoutRectList[layoutRectIdx]).Setup(job, icon, title);

                if (i % 2 != 0)
                    ++layoutRectIdx;
            }
        }

        private void Awake()
        {
            _navigationBlock = Application.Model.GetCurrentNavigationBlock();
            _navigationPoint = Application.Model.GetCurrentNavigationPoint();

            _settings = SettingsProvider.Get<JobSettings>();
        }
    }
}