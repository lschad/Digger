using UnityEngine;

namespace Assets.Scripts.Util
{
    public static class ColorUtils
    {
        public static Color RandomColor()
        {
            return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
    }
}