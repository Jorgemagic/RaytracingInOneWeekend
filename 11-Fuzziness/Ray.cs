using System.Numerics;

namespace _11_Fuzziness
{
    public class Ray
    {
        private Vector3 orig;
        private Vector3 dir;

        public Vector3 Origin => this.orig;
        public Vector3 Direction => this.dir;

        public Ray(Vector3 origin, Vector3 direction)
        {
            this.orig = origin;
            this.dir = direction;
        }

        public Vector3 At(float t)
        {
            return this.orig + t * this.dir; //P(t) = A + tb
        }
    }
}
