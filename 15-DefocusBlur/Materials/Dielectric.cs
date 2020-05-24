using System;
using System.Numerics;

namespace _15_DefocusBlur
{
    public class Dielectric : Material
    {
        public float Ref_idx;

        public Dielectric(float ri)
        {
            this.Ref_idx = ri;
        }

        public bool Scatter(Ray r_in, Hit_Record rec, out Vector3 attenuation, out Ray scattered)
        {
            attenuation = Vector3.One;
            float etai_over_etat = (rec.Front_face) ? (1.0f / Ref_idx) : (Ref_idx);

            Vector3 unit_direction = Vector3.Normalize(r_in.Direction);
            float cos_theta = Math.Min(Vector3.Dot(-unit_direction, rec.Normal), 1.0f);
            float sin_theta = (float)Math.Sqrt(1.0f - cos_theta * cos_theta);
            if (etai_over_etat * sin_theta > 1.0f)
            {
                Vector3 reflected = Helpers.Reflect(unit_direction, rec.Normal);
                scattered = new Ray(rec.P, reflected);
                return true;
            }
            float reflect_prob = Helpers.Schlick(cos_theta, etai_over_etat);
            if (Helpers.random.NextDouble() < reflect_prob)
            {
                Vector3 reflected = Helpers.Reflect(unit_direction, rec.Normal);
                scattered = new Ray(rec.P, reflected);
                return true;
            }

            Vector3 refracted = Helpers.Refract(unit_direction, rec.Normal, etai_over_etat);
            scattered = new Ray(rec.P, refracted);
            return true;
        }
    }
}
