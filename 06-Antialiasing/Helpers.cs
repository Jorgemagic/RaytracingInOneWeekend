using System;
using System.Collections.Generic;
using System.Text;

namespace _06_Antialiasing
{
    public class Helpers
    {
        // Constants
        public static float Infinity = float.MaxValue;
        public static Random random = new Random();

        // Utility Functions
        public static float Degress_to_radians(float degrees)
        {
            return degrees * (float)Math.PI / 180.0f;
        }

        public static float RandomFloat(float min, float max)
        {
            return (float)(random.NextDouble() * (max - min) + min);
        }
    }
}
