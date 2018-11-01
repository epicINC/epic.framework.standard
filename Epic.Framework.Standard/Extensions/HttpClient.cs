using Epic.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

        public static bool IsSuccessStatusCode(this HttpStatusCode? value)
        {
            return (value != null && value >= HttpStatusCode.OK && value <= (HttpStatusCode)299);
        }

        public static bool IsSuccessStatusCode<T>(this (T Data, HttpStatusCode? StatusCode) value)
        {
            return IsSuccessStatusCode(value.StatusCode);
        }

        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent value)
        {
            if (value == null) return default;
            using (var stream = await value.ReadAsStreamAsync())
            {
                return JSON.Parse<T>(stream);
            }
        }

        static Uri CreateUri(string value)
        {
            if (String.IsNullOrEmpty(value)) return null;
            Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out Uri result);
            return result;
        }


        #region TryJsonResult

        public static async Task<T> TryJsonResult<T>(this HttpResponseMessage response)
        {
            if (response == null || !response.IsSuccessStatusCode) return default;
            return await ReadAsJsonAsync<T>(response.Content);
        }

        public static async Task<T> TryJsonResult<T>(this Task<HttpResponseMessage> value)
        {
            HttpResponseMessage response;
            try
            {
                response = await value;
            }
            catch (HttpRequestException)
            {
                response = null;
            }
            catch (Exception)
            {
                response = null;
            }
            return await TryJsonResult<T>(response);
        }

        #endregion

        #region JsonResult

        public static async Task<(T Data, HttpStatusCode? StatusCode)> JsonResult<T>(this HttpResponseMessage response)
        {
            if (response == null) return (default, null);
            if (!response.IsSuccessStatusCode) return (default, response.StatusCode);
            return (await ReadAsJsonAsync<T>(response.Content), response.StatusCode);
        }

        public static async Task<(T Data, HttpStatusCode? StatusCode)> JsonResult<T>(this Task<HttpResponseMessage> value)
        {
            HttpResponseMessage response;
            try
            {
                response = await value;
            }
            catch (HttpRequestException)
            {
                response = null;
            }
            catch (Exception)
            {
                response = null;
            }
            return await JsonResult<T>(response);
        }

        #endregion

        #region Get Json Async



        public static async Task<HttpResponseMessage> GetAsync<T>(this HttpClient client, Uri requestUri, T query) where T : class
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

            try
            {
                return await client.GetAsync(builder.Uri);
            }
            catch (HttpRequestException)
            {

                return null;
            }
            catch (Exception)
            {
                return null;
            }
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

        public static async Task<HttpResponseMessage> PostFormAsync(this HttpClient client, string requestUri, IEnumerable<KeyValuePair<string, object>> value)
        {
            return await PostFormAsync(client, CreateUri(requestUri), value, CancellationToken.None);
        }

        public static async Task<HttpResponseMessage> PostFormAsync(this HttpClient client, Uri requestUri, IEnumerable<KeyValuePair<string, object>> value)
        {
            return await PostFormAsync(client, requestUri, value, CancellationToken.None);
        }

        public static async Task<HttpResponseMessage> PostFormAsync(this HttpClient client, string requestUri, IEnumerable<KeyValuePair<string, object>> value, CancellationToken cancellationToken)
        {
            return await PostFormAsync(client, CreateUri(requestUri), value, cancellationToken);
        }

        public static async Task<HttpResponseMessage> PostFormAsync(this HttpClient client, Uri requestUri, IEnumerable<KeyValuePair<string, object>> value, CancellationToken cancellationToken)
        {
            try
            {
                return await client.PostAsync(requestUri, ParseFormContent(value), cancellationToken);
            }
            catch (HttpRequestException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<HttpResponseMessage> PostFormAsync<T>(this HttpClient client, string requestUri, T value) where T : class
        {
            if (value is IEnumerable<KeyValuePair<string, object>> collection)
                return await PostFormAsync(client, requestUri, collection, CancellationToken.None);
            return await PostFormAsync(client, CreateUri(requestUri), value, CancellationToken.None);
        }

        public static async Task<HttpResponseMessage> PostFormAsync<T>(this HttpClient client, Uri requestUri, T value) where T : class
        {
            if (value is IEnumerable <KeyValuePair<string, object>> collection)
                return await PostFormAsync(client, requestUri, collection, CancellationToken.None);
            return await PostFormAsync(client, requestUri, value, CancellationToken.None);
        }

        public static async Task<HttpResponseMessage> PostFormAsync<T>(this HttpClient client, string requestUri, T value, CancellationToken cancellationToken) where T : class
        {
            if (value is IEnumerable<KeyValuePair<string, object>> collection)
                return await PostFormAsync(client, CreateUri(requestUri), collection, CancellationToken.None);
            return await PostFormAsync(client, CreateUri(requestUri), value, cancellationToken);
        }

        public static async Task<HttpResponseMessage> PostFormAsync<T>(this HttpClient client, Uri requestUri, T value, CancellationToken cancellationToken) where T : class
        {
            if (value is IEnumerable<KeyValuePair<string, object>> collection)
                return await PostFormAsync(client, requestUri, collection, CancellationToken.None);
            try
            {

                return await client.PostAsync(requestUri, ParseFormContent(value), cancellationToken);
            }
            catch (HttpRequestException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
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
