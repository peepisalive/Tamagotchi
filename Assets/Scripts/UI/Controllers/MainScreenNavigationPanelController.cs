using UnityEngine;

namespace UI.Controller
{
    public sealed class MainScreenNavigationPanelController : MonoBehaviour
    {
        [SerializeField] private FullTimeJobPanelController _fullTimeJobPanelController;

        private void Start()
        {
            _fullTimeJobPanelController.Setup();
        }
    }
}