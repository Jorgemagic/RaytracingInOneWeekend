using System.IO;

namespace _01_PPMImageFormat
{
    public class Program
    {
        static void Main(string[] args)
        {
            int image_width = 256;
            int image_height = 256;

            string filePath = "image.ppm";

            using (var file = new StreamWriter(filePath))
            {
                file.WriteLine($"P3\n{image_width} {image_height}\n255");

                for (int j = image_height - 1; j >= 0; --j)
                {
                    for (int i = 0; i < image_width; ++i)
                    {
                        var r = (double)i / (image_width - 1);
                        var g = (double)j / (image_height - 1);
                        var b = 0.25;

                        int ir = (int)(255.999 * r);
                        int ig = (int)(255.999 * g);
                        int ib = (int)(255.999 * b);

                        file.WriteLine($"{ir} {ig} {ib}");
                    }
                }
            }
        }
    }
}
