using Newtonsoft.Json;

namespace PixivWallpaperHelper.Pixiv.Objects
{
    public partial class IllustPage
    {
        [JsonProperty("image_urls")]
        public ImageUrls ImageUrls { get; set; }
    }
}
