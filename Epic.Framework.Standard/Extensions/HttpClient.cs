using Epic.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Epic.Extensions
{
    public static class HttpClientExtensions
    {
        #region Authorization

        public static HttpClient AuthBasic(this HttpClient client, string account, string password)
        {
            if (String.IsNullOrWhiteSpace(account) || String.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("account, password is required.");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{account}:{password}")));
            return client;
        }

        public static HttpClient AuthBearer(this HttpClient client, string token)
        {
            if (String.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException("token is required.");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        #endregion


        public static async Task<HttpResponseMessage> SendAsync(this HttpClient client, HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await client.SendAsync(request, cancellationToken);
        }


        public static async Task<T> JsonResult<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode) return default(T);
            return await ReadAsJsonAsync<T>(response.Content);
        }

        public static async Task<T> JsonResult<T>(this Task<HttpResponseMessage> value)
        {
            var response = await value;
            if (!response.IsSuccessStatusCode) return default(T);
            return await ReadAsJsonAsync<T>(response.Content);
        }

        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent value)
        {
            using (var stream = await value.ReadAsStreamAsync())
            {
                return JSON.Parse<T>(stream);
            }
        }

        static Uri CreateUri(string value)
        {
            if (String.IsNullOrEmpty(value)) return null;
            return new Uri(value, UriKind.RelativeOrAbsolute);
        }


        #region Get Json Async



        public static Task<HttpResponseMessage> GetAsync<T>(this HttpClient client, Uri requestUri, T query) where T : class
        {
            var builder = new UriBuilder(requestUri.IsAbsoluteUri ? requestUri : new Uri(client.BaseAddress, requestUri));

            var dictionary = JQueryString.Parse(query);
            if (!String.IsNullOrWhiteSpace(builder.Query))
            {
                var collection = JQueryString.Parse(builder.Query);
                foreach (var key in collection.AllKeys)
                    dictionary.Add(key, collection[key]);
            }

            builder.Query = JQueryString.Stirngify(dictionary);

            return client.GetAsync(builder.Uri);
        }
        
        #endregion

        #region PostJsonAsync

        //public static Task<HttpResponseMessage> PostJsonAsync<T>(this HttpClient client, Uri requestUri, T value, CancellationToken cancellationToken) where T : class
        //{
        //    return client.PostAsync(requestUri, new StringContent(JSON.Stringify(value), Encoding.UTF8), cancellationToken);
        //}

        //public static Task<HttpResponseMessage> PostJsonAsync<T>(this HttpClient client, Uri requestUri, T value) where T : class
        //{
        //    return PostJsonAsync<T>(client, requestUri, value, CancellationToken.None);
        //}

        //public static Task<HttpResponseMessage> PostJsonAsync<T>(this HttpClient client, string requestUri, T value) where T : class
        //{
        //    return PostJsonAsync<T>(client, CreateUri(requestUri), value, CancellationToken.None);
        //}

        #endregion




        #region PostFormAsync


        public static Task<HttpResponseMessage> PostFormAsync(this HttpClient client, string requestUri, IEnumerable<KeyValuePair<string, object>> value)
        {
            return PostFormAsync(client, CreateUri(requestUri), value, CancellationToken.None);
        }

        public static Task<HttpResponseMessage> PostFormAsync(this HttpClient client, Uri requestUri, IEnumerable<KeyValuePair<string, object>> value)
        {
            return PostFormAsync(client, requestUri, value, CancellationToken.None);
        }

        public static Task<HttpResponseMessage> PostFormAsync(this HttpClient client, string requestUri, IEnumerable<KeyValuePair<string, object>> value, CancellationToken cancellationToken)
        {
            return PostFormAsync(client, CreateUri(requestUri), value, cancellationToken);
        }

        public static Task<HttpResponseMessage> PostFormAsync(this HttpClient client, Uri requestUri, IEnumerable<KeyValuePair<string, object>> value, CancellationToken cancellationToken)
        {
            return client.PostAsync(requestUri, ParseFormContent(value), cancellationToken);
        }

        public static Task<HttpResponseMessage> PostFormAsync<T>(this HttpClient client, string requestUri, T value) where T : class
        {
            if (value is IEnumerable<KeyValuePair<string, object>> collection)
                return PostFormAsync(client, requestUri, collection, CancellationToken.None);
            return PostFormAsync(client, CreateUri(requestUri), value, CancellationToken.None);
        }

        public static Task<HttpResponseMessage> PostFormAsync<T>(this HttpClient client, Uri requestUri, T value) where T : class
        {
            if (value is IEnumerable <KeyValuePair<string, object>> collection)
                return PostFormAsync(client, requestUri, collection, CancellationToken.None);
            return PostFormAsync(client, requestUri, value, CancellationToken.None);
        }

        public static Task<HttpResponseMessage> PostFormAsync<T>(this HttpClient client, string requestUri, T value, CancellationToken cancellationToken) where T : class
        {
            if (value is IEnumerable<KeyValuePair<string, object>> collection)
                return PostFormAsync(client, CreateUri(requestUri), collection, CancellationToken.None);
            return PostFormAsync(client, CreateUri(requestUri), value, cancellationToken);
        }

        public static Task<HttpResponseMessage> PostFormAsync<T>(this HttpClient client, Uri requestUri, T value, CancellationToken cancellationToken) where T : class
        {
            if (value is IEnumerable<KeyValuePair<string, object>> collection)
                return PostFormAsync(client, requestUri, collection, CancellationToken.None);
            return client.PostAsync(requestUri, ParseFormContent(value), cancellationToken);
        }

        #endregion

        #region ParseFormContent




        static MultipartFormDataContent ParseMultipartFormContent(IEnumerable<KeyValuePair<string, object>> value)
        {
            var result = new MultipartFormDataContent();
            foreach (var item in value)
            {
                switch (item.Value)
                {
                    case FileStream stream:
                        stream.Position = 0;
                        var fileName = Path.GetFileName(stream.Name);
                        result.Add(new StreamContent(stream), fileName, fileName);
                        break;
                    case Stream stream:
                        stream.Position = 0;
                        result.Add(new StreamContent(stream), item.Key, item.Key);
                        break;
                    case null:
                        break;
                    case string s:
                        result.Add(new StringContent(s), item.Key);
                        break;
                    case byte[] s:
                        result.Add(new ByteArrayContent(s), item.Key);
                        break;
                    default:
                        //result.Add(new ByteArrayContent(Encoding.UTF8.GetBytes(item.Value.ToString())), item.Key);
                        result.Add(new StringContent(item.Value.ToString()), item.Key);
                        break;
                }
            }
            return result;
        }

        public static MultipartFormDataContent ParseMultipartFormContent<T>(T value) where T : class
        {
            return ParseMultipartFormContent(JQueryString.Parse(value));
        }


        public static FormUrlEncodedContent ParseUrlEncodedContent(IEnumerable<KeyValuePair<string, string>> value)
        {
            return new FormUrlEncodedContent(value);
        }

        public static FormUrlEncodedContent ParseUrlEncodedContent(IEnumerable<KeyValuePair<string, object>> value)
        {
            return ParseUrlEncodedContent(value.Select(e => new KeyValuePair<string, string>(e.Key, e.Value?.ToString())));
        }

        public static FormUrlEncodedContent ParseUrlEncodedContent<T>(T value) where T : class
        {
            return ParseUrlEncodedContent(JQueryString.Parse(value));
        }

        public static HttpContent ParseFormContent<T>(T value) where T : class
        {
            return ParseFormContent(JQueryString.Parse(value) as IEnumerable<KeyValuePair<string, object>>);
        }


        public static HttpContent ParseFormContent(IEnumerable<KeyValuePair<string, object>> value)
        {
            if (value.Any(e => e.Value != null && e.Value is Stream))
                return ParseMultipartFormContent(value);
            else
                return ParseUrlEncodedContent(value);
        }

        #endregion

    }
}
