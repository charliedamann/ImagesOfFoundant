using System;
using System.Collections.Generic;

namespace ImagesOfFoundant.Models
{
    public class Image
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public IList<Tag> Tags { get; set; }
    }
}
