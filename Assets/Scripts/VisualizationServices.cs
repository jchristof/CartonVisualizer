

using UnityEngine;

namespace Assets.Scripts {
    public static class VisualizationServices {
        public static Color ToColor(this string color, Color? defaultColor = null) {
            color = color.Remove(0, 1);
            var rgba = color.Split('#');

            if (rgba.Length != 4)
                return defaultColor ?? Color.white;

            if(string.IsNullOrEmpty(rgba[0]) || string.IsNullOrEmpty(rgba[1]) || string.IsNullOrEmpty(rgba[2]))
                return defaultColor ?? Color.white;

            int r,g,b;
            if (!int.TryParse(rgba[0], out r) || !int.TryParse(rgba[1], out g) || !int.TryParse(rgba[2], out b))
                return defaultColor ?? Color.white;

            return new Color(r/255f, g/255f, b/255f);
        }

        public static Color ToColor(this string color, float alpha, Color? defaultColor = null) {
            var c = color.ToColor(defaultColor);
            c.a = alpha;
            return c;
        }

        public static Vector3 ToVolume(string width, string height, string depth) {
            float w = 0, h = 0, d = 0;

            if (!float.TryParse(width, out w) || !float.TryParse(height, out h) || !float.TryParse(depth, out d))
                return Vector3.one;

            return new Vector3(w, h, d);
        } 
    }
}
