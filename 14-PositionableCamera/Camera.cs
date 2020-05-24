using System;
using System.Numerics;

namespace _14_PositionableCamera
{
    public class Camera
    {
        private Vector3 origin;
        private Vector3 lower_left_corner;
        private Vector3 horizontal;
        private Vector3 vertical;

        public Camera(Vector3 lookfrom,
                      Vector3 lookat,
                      Vector3 vup,
                      float vfov, // vertical field-of-view in degress
                      float aspect_ratio)
        {
            float theta = Helpers.Degress_to_radians(vfov);
            float h = (float)Math.Tan(theta / 2);
            float viewport_height = 2.0f * h;
            float viewport_width = aspect_ratio * viewport_height;

            var w = Vector3.Normalize(lookfrom - lookat);
            var u = Vector3.Normalize(Vector3.Cross(vup, w));
            var v = Vector3.Cross(w, u);

            origin = lookfrom;
            horizontal = viewport_width * u;
            vertical = viewport_height * v;
            lower_left_corner = origin - horizontal / 2 - vertical / 2 - w;
        }

        public Ray Get_Ray(float s, float t)
        {
            return new Ray(this.origin, this.lower_left_corner + s * horizontal + t * vertical - origin);
        }
    }
}