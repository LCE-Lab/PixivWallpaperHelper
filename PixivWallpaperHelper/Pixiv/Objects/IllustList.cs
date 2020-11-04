using Newtonsoft.Json;

namespace PixivWallpaperHelper.Pixiv.Objects
{
    public class IllustList
    {
        [JsonProperty("illusts")]
        public Illust[] Illusts { get; set; }

        [JsonProperty("next_url")]
        public string NextUrl { get; set; }
    }
}
