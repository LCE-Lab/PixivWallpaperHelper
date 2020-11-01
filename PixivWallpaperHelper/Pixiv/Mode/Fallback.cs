using Newtonsoft.Json;
using PixivWallpaperHelper.Pixiv.Objects;
using PixivWallpaperHelper.Pixiv.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixivWallpaperHelper.Pixiv.Mode
{
    public class Fallback
    {
        private static readonly string API = "https://app-api.pixiv.net/v1/walkthrough/illusts";
        public static async Task<IllustList> GetFallback()
        {
            var json = await (await Request.CreateRequest(MethodType.GET, API)).GetResponseStringAsync();
            var fallback = JsonConvert.DeserializeObject<IllustList>(json, Converter.Settings);

            return fallback;
        }
    }
}
