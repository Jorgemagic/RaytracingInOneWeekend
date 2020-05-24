using System.Numerics;

namespace _10_Metal
{
    public class Metal : Material
    {
        public Vector3 Albedo;

        public Metal(Vector3 a)
        {
            this.Albedo = a;
        }

        public bool Scatter(Ray r_in, Hit_Record rec, out Vector3 attenuation, out Ray scattered)
        {
            Vector3 unit_vector = Vector3.Normalize(r_in.Direction);
            Vector3 reflected = Helpers.Reflect(unit_vector, rec.Normal);
            scattered = new Ray(rec.P, reflected);

            attenuation = this.Albedo;
            return (Vector3.Dot(scattered.Direction, rec.Normal) > 0);
        }
    }
}
