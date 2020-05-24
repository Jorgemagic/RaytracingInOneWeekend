using System;
using System.Collections.Generic;
using System.Text;

namespace _05_HittableObjects
{
    public class Helpers
    {
        // Constants
        public static float Infinity = float.MaxValue;

        // Utility Functions
        public static float Degress_to_radians(float degrees)
        {
            return degrees * (float)Math.PI / 180.0f;
        }
    }
}
