using System;
using System.Collections.Generic;

namespace ImagesOfFoundant.Models
{
    public class Tag
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IList<Image> Images { get; set; }
    }
}
