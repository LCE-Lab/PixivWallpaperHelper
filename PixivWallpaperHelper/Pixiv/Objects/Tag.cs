using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
