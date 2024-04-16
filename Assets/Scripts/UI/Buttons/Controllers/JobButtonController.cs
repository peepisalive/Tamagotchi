using UnityEngine;
using Core.Job;
using UI.View;

namespace UI.Controller
{
    [RequireComponent(typeof(JobButtonView))]
    public sealed class JobButtonController : MonoBehaviour
    {
        [SerializeField] private JobButtonView _view;
        private Job _job;

        public void Setup(Job job, Sprite icon, string title)
        {
            _job = job;

            _view.SetIcon(icon);
            _view.SetTitle(title);
        }
    }
}