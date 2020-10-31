using Newtonsoft.Json;

namespace PixivWallpaperHelper.Pixiv.Objects
{
    public class ProfileImageUrls
    {
        [JsonProperty("px_16x16")]
        public string Px16X16 { get; set; }

        [JsonProperty("px_50x50")]
        public string Px50X50 { get; set; }

        [JsonProperty("px_170x170")]
        public string Px170X170 { get; set; }
    }
}
