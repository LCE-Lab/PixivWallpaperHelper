using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixivWallpaperHelper.Pixiv.Objects
{
    public partial class IllustSinglePage
    {
        [JsonProperty("original_image_url")]
        public string OriginalImageUrl { get; set; }
    }
}
