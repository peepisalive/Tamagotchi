using Core.Animation;

namespace Utils
{
    public static class AnimationUtils
    {
        public static string GetKey(EyesAnimationType type)
        {
            return $"Eyes{type}";
        }
    }
}