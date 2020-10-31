using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PixivWallpaperHelper.Pixiv.Objects;
using PixivWallpaperHelper.Pixiv.Util;
using PixivWallpaperHelper.Utils;

namespace PixivWallpaperHelper.Pixiv.OAuth
{
    public enum MethodType
    {
        GET = 0,
        POST = 1,
        DELETE = 2,
    }

    public class AsyncResponse : IDisposable
    {
        public AsyncResponse(HttpResponseMessage source)
        {
            Source = source;
        }

        public HttpResponseMessage Source { get; }

        public Task<Stream> GetResponseStreamAsync()
        {
            return Source.Content.ReadAsStreamAsync();
        }

        public Task<string> GetResponseStringAsync()
        {
            return Source.Content.ReadAsStringAsync();
        }

        public Task<byte[]> GetResponseByteArrayAsync()
        {
            return Source.Content.ReadAsByteArrayAsync();
        }

        public void Dispose()
        {
            Source?.Dispose();
        }
    }

    public class Auth
    {
        private const string clientId = "MOBrBDS8blbauoSck0ZfDbtuzpyT";
        private const string clientSecret = "lsACyCD94FhDUtGTXi3QzcFE2uU1hqtDaKeqrdwj";
        private const string hashSecret = "28c1fdd170a5204386cb1313c7077b34f83e4aaf4aa829ce78c231e05b0bae2c";

        /// <summary>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> username (required)</para>
        /// <para>- <c>string</c> password (required)</para>
        /// </summary>
        /// <returns>Tokens.</returns>
        public static async Task<Tokens> AuthorizeAsync(string username, string password)
        {
            var localTime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Local)
                .ToString("yyyy'-'MM'-'dd'T'HH':'mm':'sszzz");
            var hash = Md5.Md5Code(localTime + hashSecret);

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "PixivAndroidApp/5.0.64 (Android 6.0)");
            httpClient.DefaultRequestHeaders.Add("Accept-Language", "en_US");
            httpClient.DefaultRequestHeaders.Add("X-Client-Time", localTime);
            httpClient.DefaultRequestHeaders.Add("X-Client-Hash", hash);

            var param = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"username", username},
                {"password", password},
                {"grant_type", "password"},
                {"device_token", "pixiv"},
                {"get_secure_url", "true"},
                {"client_id", clientId},
                {"client_secret", clientSecret},
            });
            var response = await httpClient.PostAsync("https://oauth.secure.pixiv.net/auth/token", param);
            if (!response.IsSuccessStatusCode)
                throw new AuthenticationException();

            var json = await response.Content.ReadAsStringAsync();
            var authorize = JToken.Parse(json).SelectToken("response")?.ToObject<Authorize>();

            Data.SaveAuthData(authorize);

            return new Tokens(authorize?.AccessToken);
        }

        public static Tokens AuthorizeWithAccessToken(string accessToken)
        {
            return new Tokens(accessToken);
        }
    }

    public class Tokens
    {
        public string AccessToken { get; private set; }

        internal Tokens(string accessToken)
        {
            AccessToken = accessToken;
        }

        /// <summary>
        /// <para>Available parameters:</para>
        /// <para>- <c>MethodType</c> type (required) [ GET, POST ]</para>
        /// <para>- <c>string</c> url (required)</para>
        /// <para>- <c>IDictionary</c> param (required)</para>
        /// <para>- <c>IDictionary</c> header (optional)</para>
        /// </summary>
        /// <returns>AsyncResponse.</returns>
        public async Task<AsyncResponse> SendRequestAsync(MethodType type, string url,
            IDictionary<string, string> param, IDictionary<string, string> headers = null)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Referer", "https://www.pixiv.net");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "PixivAndroidApp/5.0.64 (Android 6.0)");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);

            if (headers != null)
            {
                foreach (var header in headers)
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

            AsyncResponse asyncResponse = null;

            if (type == MethodType.POST)
            {
                var reqParam = new FormUrlEncodedContent(param);
                var response = await httpClient.PostAsync(url, reqParam);
                asyncResponse = new AsyncResponse(response);
            }
            else if (type == MethodType.DELETE)
            {
                var uri = url;

                if (param != null)
                {
                    var queryString = "";
                    foreach (KeyValuePair<string, string> kvp in param)
                    {
                        if (queryString == "")
                            queryString += "?";
                        else
                            queryString += "&";

                        queryString += kvp.Key + "=" + WebUtility.UrlEncode(kvp.Value);
                    }

                    uri += queryString;
                }

                var response = await httpClient.DeleteAsync(uri);
                asyncResponse = new AsyncResponse(response);
            }
            else
            {
                var uri = url;

                if (param != null)
                {
                    var queryString = "";
                    foreach (KeyValuePair<string, string> kvp in param)
                    {
                        if (queryString == "")
                            queryString += "?";
                        else
                            queryString += "&";

                        queryString += kvp.Key + "=" + WebUtility.UrlEncode(kvp.Value);
                    }

                    uri += queryString;
                }

                var response = await httpClient.GetAsync(uri);
                asyncResponse = new AsyncResponse(response);
            }

            return asyncResponse;
        }

        private async Task<T> AccessApiAsync<T>(MethodType type, string url, IDictionary<string, string> param,
            IDictionary<string, string> headers = null) where T : class
        {
            using (var response = await SendRequestAsync(type, url, param, headers))
            {
                var json = await response.GetResponseStringAsync();
                var obj = JToken.Parse(json).SelectToken("response")?.ToObject<T>();

                if (obj is IPagenated)
                    ((IPagenated) obj).Pagination =
                        JToken.Parse(json).SelectToken("pagination")?.ToObject<Pagination>();

                return obj;
            }
        }

        /// <summary>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> maxId (optional)</para>
        /// <para>- <c>bool</c> showR18 (optional)</para>
        /// </summary>
        /// <returns>Feeds.</returns>
        public async Task<List<Feed>> GetUserFeedsAsync(long maxId = 0, bool showR18 = true)
        {
            var url = "https://public-api.secure.pixiv.net/v1/me/feeds.json";

            var param = new Dictionary<string, string>
            {
                {"relation", "all"},
                {"type", "touch_nottext"},
                {"show_r18", Convert.ToInt32(showR18).ToString()},
            };

            if (maxId != 0)
                param.Add("max_id", maxId.ToString());

            return await AccessApiAsync<List<Feed>>(MethodType.GET, url, param);
        }

        /// <summary>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> mode (optional) [ daily, weekly, monthly, male, female, rookie, daily_r18, weekly_r18, male_r18, female_r18, r18g ]</para>
        /// <para>- <c>int</c> page (optional)</para>
        /// <para>- <c>int</c> perPage (optional)</para>
        /// <para>- <c>string</c> date (optional) [ 2015-04-01 ]</para>
        /// <para>- <c>bool</c> includeSanityLevel (optional)</para>
        /// </summary>
        /// <returns>RankingAll. (Pagenated)</returns>
        public async Task<Paginated<Rank>> GetRankingAllAsync(string mode = "daily", int page = 1, int perPage = 30,
            string date = "", bool includeSanityLevel = true)
        {
            var url = "https://public-api.secure.pixiv.net/v1/ranking/all";

            var param = new Dictionary<string, string>
            {
                {"mode", mode},
                {"page", page.ToString()},
                {"per_page", perPage.ToString()},
                {"include_stats", "1"},
                {"include_sanity_level", Convert.ToInt32(includeSanityLevel).ToString()},
                {"image_sizes", "px_128x128,small,medium,large,px_480mw"},
                {"profile_image_sizes", "px_170x170,px_50x50"},
            };

            if (!string.IsNullOrWhiteSpace(date))
                param.Add("date", date);

            return await AccessApiAsync<Paginated<Rank>>(MethodType.GET, url, param);
        }
    }

    public static class PublicApi
    {
        public static async Task<IllustList> Fallback()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Referer", "https://www.pixiv.net");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "PixivAndroidApp/5.0.64 (Android 6.0)");

            var response = await httpClient.GetAsync("https://app-api.pixiv.net/v1/walkthrough/illusts");
            var asyncResponse = new AsyncResponse(response);

            var json = await asyncResponse.GetResponseStringAsync();
            var obj = JsonConvert.DeserializeObject<IllustList>(json, Converter.Settings);

            return obj;
        }
    }
}
