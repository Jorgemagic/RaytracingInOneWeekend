using System;
using System.IO;
using System.Numerics;

namespace RaytracingWeekend
{
    public class Helpers
    {
        public static float Infinity = float.MaxValue;
        public static Random random = new Random();

        public static void Write_Color(StreamWriter file, Vector3 pixel_color, int samples_per_pixel)
        {
            float r = pixel_color.X;
            float g = pixel_color.Y;
            float b = pixel_color.Z;

            // Divide the color total by the number of samples and gamma-correct for gamma=2.0.
            float scale = 1.0f / samples_per_pixel;
            r = (float)Math.Sqrt(scale * r);
            g = (float)Math.Sqrt(scale * g);
            b = (float)Math.Sqrt(scale * b);

            // Write the translated [0, 255] value of each color component.
            file.WriteLine($"{(int)(256 * Math.Clamp(r, 0.0f, 0.999f))} {(int)(256 * Math.Clamp(g, 0.0f, 0.999f))} {(int)(256 * Math.Clamp(b, 0.0f, 0.999f))}");
        }

        public static Vector3 Ray_Color(Ray r, HitTable world, int depth)
        {
            Hit_Record rec = default;
            // If we've exceeded the ray bounce limit, no more light is gathered.
            if (depth <= 0)
            {
                return Vector3.Zero;
            }

            if (world.Hit(r, 0.001f, Helpers.Infinity, ref rec))
            {
                Ray scattered;
                Vector3 attenuation;
                if (rec.Material.Scatter(r, rec, out attenuation, out scattered))
                {
                    return attenuation * Ray_Color(scattered, world, depth - 1);
                }
                else
                {
                    return Vector3.Zero;
                }                
            }

            Vector3 unit_direction = Vector3.Normalize(r.Direction);
            float t = 0.5f * (unit_direction.Y + 1.0f);
            return (1.0f - t) * Vector3.One + t * new Vector3(0.5f, 0.7f, 1.0f);  // blendedValue = (1 - t) * startValue + t * endValue
        }

        public static float Degress_to_radians(float degrees)
        {
            return degrees * (float)Math.PI / 180.0f;
        }

        public static uint wang_hash(uint seed)
        {
            seed = (seed ^ 61) ^ (seed >> 16);
            seed *= 9;
            seed = seed ^ (seed >> 4);
            seed *= 0x27d4eb2d;
            seed = seed ^ (seed >> 15);
            return seed;
        }

        private static uint indexFactor;

        // Generate a random float in [0, 1)... 
        public static float NextDouble()
        {
            indexFactor++;
            return wang_hash(indexFactor) * (1.0f / 4294967296.0f);
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

        /// <summary>
        /// Lambertian render
        /// </summary>
        /// <returns>Homogeneous unit vector</returns>
        public static Vector3 Random_unit_Vector()
        {
            float a = RandomFloat(0, 2.0f * (float)Math.PI);
            float z = RandomFloat(-1, 1);
            float r = (float)Math.Sqrt(1 - z * z);
            return new Vector3(r * (float)Math.Cos(a), r * (float)Math.Sin(a), z);
        }

        public static Vector3 Reflect(Vector3 v, Vector3 n)
        {
            return v - 2 * Vector3.Dot(v, n) * n;
        }

        public static Vector3 Refract(Vector3 uv, Vector3 n, float etai_over_etat)
        {
            float cos_theta = Vector3.Dot(-uv, n);
            Vector3 r_out_parallel = etai_over_etat * (uv + cos_theta * n);
            Vector3 r_out_perp = -(float)Math.Sqrt(1.0f - r_out_parallel.LengthSquared()) * n;
            return r_out_parallel + r_out_perp;
        }

        public static float Schlick(float cosine, float ref_idx)
        {
            float r0 = (1 - ref_idx) / (1 + ref_idx);
            r0 = r0 * r0;
            return r0 + (1 - r0) * (float)Math.Pow((1 - cosine), 5);
        }

        public static Vector3 Random_in_unit_disk()
        {
            while(true)
            {
                Vector3 p = new Vector3(RandomFloat(-1, 1), RandomFloat(-1, 1), 0);
                if (p.LengthSquared() >= 1)
                    continue;

                return p;
            }
        }

        //public static float Hit_Sphere(Vector3 center, float radius, Ray r)
        //{
        //    Vector3 oc = r.Origin - center;
        //    float a = r.Direction.LengthSquared();
        //    float half_b = Vector3.Dot(oc, r.Direction);
        //    float c = oc.LengthSquared() - radius * radius;
        //    float discriminant = half_b * half_b - a * c;
        //    if (discriminant < 0)
        //    {
        //        return -1.0f;
        //    }
        //    else
        //    {
        //        return (-half_b - (float)Math.Sqrt(discriminant)) / a;
        //    }
        //}
    }
}
