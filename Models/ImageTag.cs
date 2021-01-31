using System;
using System.Collections.Generic;

namespace ImagesOfFoundant.Models
{
    public class ImageTag
    {
        public long ImageId { get; set; }
        public Image Image { get; set; }
        public long TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
