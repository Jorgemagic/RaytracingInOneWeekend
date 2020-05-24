using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace _11_Fuzziness
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

        public static Vector3 Random_unit_Vector()
        {
            float a = RandomFloat(0, 2.0f * (float)Math.PI);
            float z = RandomFloat(-1, 1);
            float r = (float)Math.Sqrt(1 - z * z);
            return new Vector3(r * (float)Math.Cos(a), r * (float)Math.Sin(a), z);
        }

        public static Vector3 Random_in_hemisphere(Vector3 normal)
        {
            Vector3 in_unit_sphere = Random_unit_Vector();
            if (Vector3.Dot(in_unit_sphere, normal) > 0.0) // In the same hemisphere as the normal
                return in_unit_sphere;
            else
                return -in_unit_sphere;
        }

        public static Vector3 Reflect(Vector3 v, Vector3 n)
        {
            return v - 2 * Vector3.Dot(v, n) * n;
        }
    }
}
