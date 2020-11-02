using Newtonsoft.Json;

namespace PixivWallpaperHelper.Pixiv.Objects
{
    public partial class IllustSinglePage
    {
        [JsonProperty("original_image_url")]
        public string OriginalImageUrl { get; set; }
    }
}
