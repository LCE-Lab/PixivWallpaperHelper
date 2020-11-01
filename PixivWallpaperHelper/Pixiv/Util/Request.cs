using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PixivWallpaperHelper.Pixiv.Util
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
            this.Source = source;
        }

        public HttpResponseMessage Source { get; }

        public Task<Stream> GetResponseStreamAsync()
        {
            return this.Source.Content.ReadAsStreamAsync();
        }

        public Task<string> GetResponseStringAsync()
        {
            return this.Source.Content.ReadAsStringAsync();
        }

        public Task<byte[]> GetResponseByteArrayAsync()
        {
            return this.Source.Content.ReadAsByteArrayAsync();
        }

        public void Dispose()
        {
            this.Source?.Dispose();
        }
    }
    public class Request
    {
        public async static Task<AsyncResponse> CreateRequest(MethodType type, string url, IDictionary<string, string> param = null, IDictionary<string, string> headers = null)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Referer", "https://www.pixiv.net");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "PixivAndroidApp/5.0.64 (Android 6.0)");

            if (headers != null)
            {
                foreach (var header in headers)
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

            AsyncResponse asyncResponse = null;

            if (type == MethodType.POST)
            {
                if (param != null)
                {
                    var reqParam = new FormUrlEncodedContent(param);
                    var response = await httpClient.PostAsync(url, reqParam);
                    asyncResponse = new AsyncResponse(response);
                }
            }
            else
            {
                string uri = url;

                if (param != null)
                {
                    var query_string = "";
                    foreach (KeyValuePair<string, string> kvp in param)
                    {
                        if (query_string == "")
                            query_string += "?";
                        else
                            query_string += "&";

                        query_string += kvp.Key + "=" + WebUtility.UrlEncode(kvp.Value);
                    }
                    uri += query_string;
                }

                var response = await httpClient.GetAsync(uri);
                asyncResponse = new AsyncResponse(response);
            }

            return asyncResponse;
        }
    }
}
