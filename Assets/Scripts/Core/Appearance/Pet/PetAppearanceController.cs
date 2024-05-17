using System.Collections.Generic;
using Core.Animation;
using UnityEngine;
using Utils;

namespace Core
{
    public sealed class PetAppearanceController : MonoBehaviour
    {
        [field: Header("Data")]
        [field: SerializeField] public PetType Type { get; private set; }
        [field: SerializeField] public List<AccessoryAppearance> AccessoriesAppearances { get; private set; }

        [Header("Other")]
        [SerializeField] private Animator _animator;

        public void SetEyesAnimation(EyesAnimationType currentType, EyesAnimationType futureType)
        {
            SetAnimationTrigger(AnimationUtils.GetKey(currentType), AnimationUtils.GetKey(futureType));
        }

        public void SetAnimation(AnimationType currentType, AnimationType futureType)
        {
            SetAnimationTrigger(currentType.ToString(), futureType.ToString());
        }

        private void SetAnimationTrigger(string currentKey, string futureKey)
        {
            _animator.ResetTrigger(currentKey);
            _animator.SetTrigger(futureKey);
        }
    }
}