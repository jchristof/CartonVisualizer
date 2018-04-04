

using System;
using UnityEngine;

namespace Assets.Scripts {
    public static class VisualizationServices {

        // Convert a cube iq color string (#000#000#000#000) into a Unity Color
        public static Color ToColor(this string color, Color? defaultColor = null) {
            color = color.Remove(0, 1);
            var rgba = color.Split('#');

            if (rgba.Length != 4) {
                Console.WriteLine("Incorrect color component count");
                return defaultColor ?? Color.magenta;
            }

            if (string.IsNullOrEmpty(rgba[0]) || string.IsNullOrEmpty(rgba[1]) || string.IsNullOrEmpty(rgba[2])) {
                Console.WriteLine("Empty color component value");
                return defaultColor ?? Color.magenta;
            }

            int r,g,b;
            if (!int.TryParse(rgba[0], out r) || !int.TryParse(rgba[1], out g) || !int.TryParse(rgba[2], out b)) {
                Console.WriteLine("Invalid color component value");
                return defaultColor ?? Color.magenta;
            }

            return new Color(r/255f, g/255f, b/255f);
        }

        // Convert a cube iq color string (#000#000#000#000) into a Unity Color with alpha value
        public static Color ToColor(this string color, float alpha, Color? defaultColor = null) {
            var c = color.ToColor(defaultColor);
            c.a = alpha;
            return c;
        }

        // Convert a set of string dimensions into a Vector3 volume
        public static Vector3 ToVolume(string width, string height, string depth) {
            float w = 0, h = 0, d = 0;

            if (!float.TryParse(width, out w) || !float.TryParse(height, out h) || !float.TryParse(depth, out d)) {
                Console.WriteLine("Invalid volume dimension");
                return Vector3.one;
            }

            return new Vector3(w, h, d);
        } 
    }
}
