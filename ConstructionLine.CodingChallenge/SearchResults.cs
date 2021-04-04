using System;
using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge
{
    public class SearchResults
    {
        public List<Shirt> Shirts { get; set; }


        public List<SizeCount> SizeCounts { get; set; }


        public List<ColorCount> ColorCounts { get; set; }
    }


    public class SizeCount
    {
        public Size Size { get; set; }

        public int Count { get; set; }

        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is SizeCount))
                return false;
            else
                return Size.Id == ((SizeCount)obj).Size.Id;
        }

        public override int GetHashCode()
        {
            return Size.Id.GetHashCode();
        }
    }


    public class ColorCount
    {
        public Color Color { get; set; }

        public int Count { get; set; }

        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is ColorCount))
                return false;
            else
                return Color.Id == ((ColorCount)obj).Color.Id;
        }

        public override int GetHashCode()
        {
            return Color.Id.GetHashCode();
        }
    }
}