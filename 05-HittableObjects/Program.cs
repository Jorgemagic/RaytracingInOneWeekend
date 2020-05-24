using RaytracingWeekend;
using System;
using System.IO;
using System.Numerics;

namespace _05_HittableObjects
{
    public class Program
    {
        static Vector3 Ray_color(Ray r, HitTable world)
        {
            Hit_Record rec = default;
            if (world.Hit(r, 0, Helpers.Infinity, ref rec))
            {
                return 0.5f * (rec.Normal + Vector3.One);
            }

            Vector3 unit_direction = Vector3.Normalize(r.Direction);
            var t = 0.5f * (unit_direction.Y + 1.0f);
            return (1.0f - t) * new Vector3(1.0f, 1.0f, 1.0f) + t * new Vector3(0.5f, 0.7f, 1.0f);
        }

        static void Write_color(StreamWriter file, Vector3 pixel_color)
        {
            // Write the translated [0,255] value of each color component.
            file.WriteLine($"{(int)(255.999 * pixel_color.X)} {(int)(255.999 * pixel_color.Y)} {(int)(255.999 * pixel_color.Z)}");
        }

        static void Main(string[] args)
        {
            float aspect_ratio = 16.0f / 9.0f;
            int image_width = 384;
            int image_height = (int)(image_width / aspect_ratio);

            string filePath = "image.ppm";

            using (var file = new StreamWriter(filePath))
            {
                file.WriteLine($"P3\n{image_width} {image_height}\n255");

                float viewport_height = 2.0f;
                float viewport_width = aspect_ratio * viewport_height;
                float focal_length = 1.0f;

                var origin = Vector3.Zero;
                var horizontal = new Vector3(viewport_width, 0, 0);
                var vertical = new Vector3(0, viewport_height, 0);
                var lower_left_corner = origin - horizontal / 2 - vertical / 2 - new Vector3(0, 0, focal_length);

                Hitable_List world = new Hitable_List();
                world.Add(new Sphere(new Vector3(0, 0, -1), 0.5f));
                world.Add(new Sphere(new Vector3(0, -100.5f, -1), 100));

                for (int j = image_height - 1; j >= 0; --j)
                {
                    for (int i = 0; i < image_width; ++i)
                    {
                        var u = (float)i / (image_width - 1);
                        var v = (float)j / (image_height - 1);
                        Ray r = new Ray(origin, lower_left_corner + u * horizontal + v * vertical - origin);
                        Vector3 pixel_color = Ray_color(r, world);
                        Write_color(file, pixel_color);
                    }
                }
            }
        }
    }
}