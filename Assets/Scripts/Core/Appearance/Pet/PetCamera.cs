using UnityEngine;
using Modules;
using Events;

namespace Core
{
    public sealed class PetCamera : MonoBehaviour
    {
        [field: SerializeField] public Camera Camera { get; private set; }
        public bool RotateState { get; private set; } = true;

        private Transform _target;
        private Vector3 _offset;
        private float _x;
        private float _y;

        private const float SENSITIVITY = 0.25f;
        private const float Z_OFFSET = 10f;
        private const float LIMIT = 80f;

        public void SetTarget(Transform target)
        {
            if (_target != null)
                return;

            _target = target;
            _offset = new Vector3(0f, 0f, -Z_OFFSET);
            transform.position = _target.position + _offset;
        }

        private void SetState(PetCameraRotateStateEvent e)
        {
            RotateState = e.State;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) && RotateState)
            {
                if (_target == null)
                    return;

                _x = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * SENSITIVITY;
                _y += Input.GetAxis("Mouse Y") * SENSITIVITY;
                _y = Mathf.Clamp(_y, -LIMIT, LIMIT);

                transform.localEulerAngles = new Vector3(-_y, _x, 0);
                transform.position = transform.localRotation * _offset + _target.position;
            }
        }

        private void Start()
        {
            EventSystem.Subscribe<PetCameraRotateStateEvent>(SetState);
        }

        private void OnDestroy()
        {
            EventSystem.Unsubscribe<PetCameraRotateStateEvent>(SetState);
        }
    }
}