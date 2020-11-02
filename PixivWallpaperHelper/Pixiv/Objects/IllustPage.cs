using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixivWallpaperHelper.Pixiv.Objects
{
    public partial class IllustPage
    {
        [JsonProperty("image_urls")]
        public ImageUrls ImageUrls { get; set; }
    }
}
