using UnityEngine;

namespace Utils
{
    public static class ScreenUtils
    {
        private const float FIRST_COLOR_LIMIT = 0.3f;
        private const float SECOND_COLOR_LIMIT = 0.6f;

        public static Color GetBarColor(float value)
        {
            if (value <= FIRST_COLOR_LIMIT)
            {
                return new Color(1, 0.27f, 0.1f);
            }
            else if (value >= SECOND_COLOR_LIMIT)
            {
                return new Color(0.38f, 0.98f, 0f);
            }
            else
            {
                return new Color(1f, 0.84f, 0.31f);
            }
        }
    }
}