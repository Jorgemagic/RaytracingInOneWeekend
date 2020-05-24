using System;
using System.Numerics;

namespace _16_FinalRender
{
    public class Sphere : HitTable
    {
        public Vector3 Center;
        public float Radius;
        public Material Material;

        public Sphere(Vector3 cen, float r, Material m)
        {
            this.Center = cen;
            this.Radius = r;
            this.Material = m;
        }

        public bool Hit(Ray r, float t_min, float t_max, ref Hit_Record rec)
        {
            Vector3 oc = r.Origin - this.Center;
            float a = r.Direction.LengthSquared();
            float half_b = Vector3.Dot(oc, r.Direction);
            float c = oc.LengthSquared() - this.Radius * this.Radius;
            float discriminant = half_b * half_b - a * c;

            if (discriminant > 0)
            {
                float root = (float)Math.Sqrt(discriminant);
                float temp = (-half_b - root) / a;
                if (temp < t_max && temp > t_min)
                {
                    rec.T = temp;
                    rec.P = r.At(rec.T);
                    Vector3 outward_normal = (rec.P - this.Center) / this.Radius;
                    rec.Set_Face_Normal(r, outward_normal);
                    rec.Mat_ptr = Material;
                    return true;
                }
                temp = (-half_b + root) / a;
                if (temp < t_max && temp > t_min)
                {
                    rec.T = temp;
                    rec.P = r.At(rec.T);
                    Vector3 outward_normal = (rec.P - this.Center) / this.Radius;
                    rec.Set_Face_Normal(r, outward_normal);
                    rec.Mat_ptr = Material;
                    return true;
                }
            }

            return false;
        }
    }    
}
