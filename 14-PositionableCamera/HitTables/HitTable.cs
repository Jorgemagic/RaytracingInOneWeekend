using System.Numerics;

namespace _14_PositionableCamera
{
    public struct Hit_Record
    {
        public Vector3 P;
        public Vector3 Normal;
        public Material Mat_ptr;
        public float T;
        public bool Front_face;

        public void Set_Face_Normal(Ray r, Vector3 outward_normal)
        {
            this.Front_face = Vector3.Dot(r.Direction, outward_normal) < 0;
            this.Normal = Front_face ? outward_normal : -outward_normal;
        }
    }

    public interface HitTable
    {
        public bool Hit(Ray r, float t_min, float t_max, ref Hit_Record rec);
    }
}
