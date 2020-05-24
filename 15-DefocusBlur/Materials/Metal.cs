using System.Numerics;

namespace _15_DefocusBlur
{
    public class Metal : Material
    {
        public Vector3 Albedo;
        public float fuzz;

        public Metal(Vector3 a, float f)
        {
            this.Albedo = a;
            this.fuzz = f < 1 ? f : 1;
        }

        public bool Scatter(Ray r_in, Hit_Record rec, out Vector3 attenuation, out Ray scattered)
        {
            Vector3 unit_vector = Vector3.Normalize(r_in.Direction);
            Vector3 reflected = Helpers.Reflect(unit_vector, rec.Normal);
            scattered = new Ray(rec.P, reflected + fuzz * Helpers.Random_in_unit_sphere());

            attenuation = this.Albedo;
            return (Vector3.Dot(scattered.Direction, rec.Normal) > 0);
        }
    }
}
