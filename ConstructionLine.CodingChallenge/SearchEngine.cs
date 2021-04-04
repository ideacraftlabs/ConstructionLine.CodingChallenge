using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly HashSet<Shirt> _shirts;

        public SearchEngine(List<Shirt> shirts)
        {
            // data preparation and initialisation of additional data structures to improve performance goes here.
            //converting to hashset for better lookup and set operations
            _shirts = shirts.ToHashSet();

        }


        public SearchResults Search(SearchOptions options)
        {
            // search logic goes here.
            _ = options ?? throw new ArgumentNullException(nameof(options));

            //convert to dictionory for fastere lookup query
            var sizes = options.Sizes.ToDictionary(key => key.Id, value => value.Name);
            var colors = options.Colors.ToDictionary(key => key.Id, value => value.Name);

            var sizeMatchedShirtsLazy = options.Sizes.Count == 0 ? _shirts : _shirts.Where(shirt => sizes.ContainsKey(shirt.Size.Id));

            var matchedShirts = options.Colors.Count == 0 ? sizeMatchedShirtsLazy : sizeMatchedShirtsLazy.Where(shirt => colors.ContainsKey(shirt.Color.Id));

            //SizeCount overrides GetHashCode and Equals method hence union will return expected counts
            var allSizeCounts = matchedShirts.GroupBy(g => g.Size).Select(s=> new SizeCount { Size = s.Key, Count = s.Count() })
                                    .Union(Size.All.Select(s=>new SizeCount { Size = s, Count = 0 }));

            //ColorCount overrides GetHashCode and Equals method hence union will return expected counts
            var allColorCounts = matchedShirts.GroupBy(g => g.Color).Select(s => new ColorCount { Color = s.Key, Count = s.Count() })
                                    .Union(Color.All.Select(s => new ColorCount { Color  = s, Count = 0 }));

            return new SearchResults
            {
                Shirts = matchedShirts.ToList(),
                SizeCounts = allSizeCounts.ToList(),
                ColorCounts = allColorCounts.ToList()
            };
        }
    }
}