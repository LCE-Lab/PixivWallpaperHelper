using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
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
    public class Request
    {
        public async static Task<AsyncResponse> CreateRequest(MethodType type, string url, IDictionary<string, string> param = null, IDictionary<string, string> headers = null)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Referer", "https://www.pixiv.net");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "PixivAndroidApp/5.0.64 (Android 6.0)");

            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            AsyncResponse asyncResponse = null;

            if (type == MethodType.POST)
            {
                if (param != null)
                {
                    FormUrlEncodedContent reqParam = new FormUrlEncodedContent(param);
                    HttpResponseMessage response = await httpClient.PostAsync(url, reqParam);
                    asyncResponse = new AsyncResponse(response);
                }
            }
            else
            {
                string uri = url;

                if (param != null)
                {
                    string queryString = "";
                    foreach (KeyValuePair<string, string> kvp in param)
                    {
                        if (queryString == "")
                        {
                            queryString += "?";
                        }
                        else
                        {
                            queryString += "&";
                        }

                        queryString += kvp.Key + "=" + WebUtility.UrlEncode(kvp.Value);
                    }
                    uri += queryString;
                }

                HttpResponseMessage response = await httpClient.GetAsync(uri);
                asyncResponse = new AsyncResponse(response);
            }

            return asyncResponse;
        }
    }
}
