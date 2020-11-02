using Newtonsoft.Json;

namespace PixivWallpaperHelper.Pixiv.Objects
{
    public partial class Tag
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("translated_name")]
        public string TranslatedName { get; set; }
    }
}
