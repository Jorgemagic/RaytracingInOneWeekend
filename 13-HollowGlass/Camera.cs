using System.Numerics;

namespace _13_HollowGlass
{
    public class Camera
    {
        private Vector3 origin;
        private Vector3 lower_left_corner;
        private Vector3 horizontal;
        private Vector3 vertical;

        public Camera()
        {
            var aspect_ratio = 16.0f / 9.0f;
            var viewport_height = 2.0f;
            var viewport_width = aspect_ratio * viewport_height;
            var focal_length = 1.0f;

            origin = Vector3.Zero;
            horizontal = new Vector3(viewport_width, 0.0f, 0.0f);
            vertical = new Vector3(0.0f, viewport_height, 0.0f);
            lower_left_corner = origin - horizontal / 2 - vertical / 2 - new Vector3(0, 0, focal_length);
        }

        public Ray Get_Ray(float u, float v)
        {
            return new Ray(origin, lower_left_corner + u * horizontal + v * vertical - origin);
        }
    }
}
