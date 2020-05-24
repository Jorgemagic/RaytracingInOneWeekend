using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace _08_GammaCorrection
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

        public static Vector3 RandomVector3(float min, float max)
        {
            return new Vector3(RandomFloat(min, max), RandomFloat(min, max), RandomFloat(min, max));
        }

        public static Vector3 Random_in_unit_sphere()
        {
            while (true)
            {
                Vector3 p = RandomVector3(-1, 1);
                if (p.LengthSquared() >= 1)
                    continue;
                return p;
            }
        }
    }
}
