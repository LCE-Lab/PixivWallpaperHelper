using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
