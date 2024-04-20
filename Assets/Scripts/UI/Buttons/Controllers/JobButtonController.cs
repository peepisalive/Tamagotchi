using UnityEngine;
using Core.Job;
using UI.View;

namespace UI.Controller
{
    [RequireComponent(typeof(JobButtonView), typeof(UnityEngine.UI.Button))]
    public sealed class JobButtonController : MonoBehaviour
    {
        [SerializeField] private JobButtonView _view;
        private Job _job;

        public void Setup(Job job, Sprite icon, string title, string description)
        {
            _job = job;

            _view.SetIcon(icon);
            _view.SetTitle(title);
            _view.SetDescription(description);
        }
    }
}