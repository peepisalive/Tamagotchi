using System.Collections.Generic;
using Core.Animation;
using System;

namespace Core
{
    public sealed class Pet : BaseObject
    {
        public event Action<EyesAnimationType, EyesAnimationType> OnEyesAnimationChangeEvent;
        public event Action<AnimationType, AnimationType> OnAnimationChangeEvent;

        public EyesAnimationType EyesAnimation { get; private set; }
        public AnimationType Animation { get; private set; }

        public readonly string Name;
        public readonly PetType Type;
        public readonly Parameters Parameters;
        public readonly List<Accessory> Accessories;

        public Pet(string id) : base(id) { }

        public Pet(string name, PetType type, Parameters parameters, List<Accessory> accessories, string id) : base(id)
        {
            Name = name;
            Type = type;
            Parameters = parameters;
            Accessories = accessories;
        }

        public void SetEyesAnimation(EyesAnimationType type)
        {
            if (type == EyesAnimation)
                return;

            OnEyesAnimationChangeEvent?.Invoke(EyesAnimation, type);
            EyesAnimation = type;
        }

        public void SetAnimation(AnimationType type)
        {
            if (type == Animation)
                return;

            OnAnimationChangeEvent?.Invoke(Animation, type);
            Animation = type;
        }
    }
}