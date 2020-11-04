using Newtonsoft.Json;

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
