using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;

namespace RaytracingWeekend
{
    class Program
    {
        public static Hitable_List Random_Scene()
        {
            Hitable_List world = new Hitable_List();

            var ground_material = new Lambertian(new Vector3(0.5f, 0.5f, 0.5f));
            world.Add(new Sphere(new Vector3(0, -1000, 0), 1000, ground_material));

            for (int a = -11; a < 11; a++)
            {
                for (int b = -11; b < 11; b++)
                {
                    float choose_mat = (float)Helpers.random.NextDouble();
                    Vector3 center = new Vector3(a + 0.9f * (float)Helpers.random.NextDouble(), 0.2f, b + 0.9f * (float)Helpers.random.NextDouble());

                    if ((center - new Vector3(4,0.2f,0)).Length() > 0.9f)
                    {
                        Material sphere_material;

                        if (choose_mat < 0.8f)
                        {
                            // diffuse
                            Vector3 albedo = Helpers.RandomVector3(0, 1) * Helpers.RandomVector3(0, 1);
                            sphere_material = new Lambertian(albedo);
                            world.Add(new Sphere(center, 0.2f, sphere_material));
                        } 
                        else if (choose_mat < 0.95f)
                        {
                            // metal
                            Vector3 albedo = Helpers.RandomVector3(0.5f, 1.0f);
                            float fuzz = Helpers.RandomFloat(0, 0.5f);
                            sphere_material = new Metal(albedo, fuzz);
                            world.Add(new Sphere(center, 0.2f, sphere_material));
                        }
                        else
                        {
                            // glass
                            sphere_material = new Dielectric(1.5f);
                            world.Add(new Sphere(center, 0.2f, sphere_material));
                        }
                    }
                }
            }

            var material1 = new Dielectric(1.5f);
            world.Add(new Sphere(new Vector3(0, 1, 0), 1.0f, material1));

            var material2 = new Lambertian(new Vector3(0.4f, 0.2f, 0.1f));
            world.Add(new Sphere(new Vector3(-4, 1, 0), 1.0f, material2));

            var material3 = new Metal(new Vector3(0.7f, 0.6f, 0.5f), 0.0f);
            world.Add(new Sphere(new Vector3(4, 1, 0), 1.0f, material3));

            return world;
        }


        static void Main(string[] args)
        {            
            const float aspect_ratio = 16.0f / 9.0f;
            const int image_width =  384;
            const int image_height = (int)(image_width / aspect_ratio);
            const int samples_per_pixel = 100;
            const int max_depth = 50;

            string filePath = "image.ppm";

            using (var file = new StreamWriter(filePath))
            {
                file.WriteLine($"P3\n{image_width} {image_height}\n255");

                float R = (float)Math.Cos(Math.PI / 4);

                Hitable_List world = Random_Scene();

                /*Hitable_List world = new Hitable_List();
                world.Add(new Sphere(new Vector3(0, 0, -1), 0.5f, new Lambertian(new Vector3(0.1f,0.2f,0.5f))));
                world.Add(new Sphere(new Vector3(0, -100.5f, -1), 100, new Lambertian(new Vector3(0.8f,0.8f,0.0f))));

                world.Add(new Sphere(new Vector3(1, 0, -1), 0.5f, new Metal(new Vector3(0.8f, 0.6f, 0.2f), 0.3f)));
                world.Add(new Sphere(new Vector3(-1, 0, -1), 0.5f, new Metal(new Vector3(0.8f, 0.8f, 0.8f), 1.0f)));
                world.Add(new Sphere(new Vector3(-1, 0, -1), 0.5f, new Dielectric(1.5f)));
                world.Add(new Sphere(new Vector3(-1, 0, -1), -0.45f, new Dielectric(1.5f)));*/

                Vector3 lookfrom = new Vector3(13, 3, 2);
                Vector3 lookat = new Vector3(0, 0, 0);
                Vector3 vup = new Vector3(0, 1, 0);
                float dist_to_focus = 10;
                float aperture = 0.1f;
                Camera cam = new Camera(lookfrom, lookat, Vector3.UnitY, 20, aspect_ratio, aperture, dist_to_focus);

                //Vector3 lookfrom = new Vector3(0,0,0);
                //Vector3 lookat = new Vector3(0, 0, -1);
                //Vector3 vup = new Vector3(0, 1, 0);
                //float dist_to_focus = 1;
                //float aperture = 0.0f;

                //Camera cam = new Camera(lookfrom, lookat, Vector3.UnitY, 90, aspect_ratio, aperture, dist_to_focus);

                for (int j = image_height - 1; j >= 0; --j)
                {
                    Console.WriteLine($"\rScanlines remaining: {j}");
                    for (int i = 0; i < image_width; ++i)
                    {
                        Vector3 pixel_color = Vector3.Zero;
                        for (int s = 0; s < samples_per_pixel; ++s)
                        {
                            float u = (float)(i + Helpers.random.NextDouble()) / (image_width - 1);
                            float v = (float)(j + Helpers.random.NextDouble()) / (image_height - 1);

                            Ray r = cam.Get_Ray(u, v);
                            pixel_color += Helpers.Ray_Color(r, world, max_depth);

                        }
                        Helpers.Write_Color(file, pixel_color, samples_per_pixel);
                    }
                }

                Console.WriteLine("Done.");
            }
        }
    }
}
