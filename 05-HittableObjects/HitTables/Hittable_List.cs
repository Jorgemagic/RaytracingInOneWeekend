using System.Collections.Generic;

namespace _05_HittableObjects
{
    public class Hitable_List : HitTable
    {
        public List<HitTable> Objects;

        public Hitable_List()
        {
            this.Objects = new List<HitTable>();
        }

        public Hitable_List(HitTable hitTable)
            : this()
        {
            this.Objects.Add(hitTable);
        }

        public void Clear() { this.Objects.Clear(); }
        public void Add(HitTable o) { this.Objects.Add(o); }

        public bool Hit(Ray r, float t_min, float t_max, ref Hit_Record rec)
        {
            Hit_Record temp_rec = default;
            bool hit_anything = false;
            float closest_so_far = t_max;

            foreach (var o in this.Objects)
            {
                if (o.Hit(r, t_min, closest_so_far, ref temp_rec))
                {
                    hit_anything = true;
                    closest_so_far = temp_rec.T;
                    rec = temp_rec;
                }
            }

            return hit_anything;
        }
    }
}
