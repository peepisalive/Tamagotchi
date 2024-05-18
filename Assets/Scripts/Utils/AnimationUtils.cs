using Core.Animation;
using Leopotam.Ecs;
using Components;

namespace Utils
{
    public static class AnimationUtils
    {
        public static void SetEyesAnimation(this EcsFilter<PetComponent> petFilter, EyesAnimationType type)
        {
            foreach (var i in petFilter)
            {
                petFilter.Get1(i).Pet.SetEyesAnimation(type);
            }
        }

        public static void SetAnimation(this EcsFilter<PetComponent> petFilter, AnimationType type)
        {
            foreach (var i in petFilter)
            {
                petFilter.Get1(i).Pet.SetAnimation(type);
            }
        }

        public static string GetKey(EyesAnimationType type)
        {
            return $"Eyes{type}";
        }
    }
}