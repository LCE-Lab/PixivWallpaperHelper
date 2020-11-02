using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixivWallpaperHelper.Pixiv.Objects
{
    public partial class ImageUrls
    {
        [JsonProperty("square_medium")]
        public string SquareMedium { get; set; }

        [JsonProperty("small")]
        public string Small { get; set; }

        [JsonProperty("medium")]
        public string Medium { get; set; }

        [JsonProperty("large")]
        public string Large { get; set; }

        [JsonProperty("original")]
        public string Original { get; set; }
    }
}
