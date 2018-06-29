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


        public static async Task<T> SendAsync<T>(this HttpClient client, HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = await client.SendAsync(request, cancellationToken);
            if (!result.IsSuccessStatusCode) return default(T);
            using (var stream = await result.Content.ReadAsStreamAsync())
            {
                return JSON.Parse<T>(stream);
            }
                
        }

        static Uri CreateUri(string value)
        {
            if (String.IsNullOrEmpty(value)) return null;
            return new Uri(value, UriKind.RelativeOrAbsolute);
        }

        #region PostAsync

        public static Task<T> PostAsync<T>(this HttpClient client, string requestUri, HttpContent content)
        {
            return PostAsync<T>(client, CreateUri(requestUri), content, CancellationToken.None);
        }


        public static Task<T> PostAsync<T>(this HttpClient client, Uri requestUri, HttpContent content)
        {
            return PostAsync<T>(client, requestUri, content, CancellationToken.None);
        }


        public static Task<T> PostAsync<T>(this HttpClient client, string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return PostAsync<T>(client, CreateUri(requestUri), content, cancellationToken);
        }


        public static Task<T> PostAsync<T>(this HttpClient client, Uri requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = content
            };
            return SendAsync<T>(client, httpRequestMessage, cancellationToken);
        }

        #endregion


        #region PostForm


        public static Task<T> PostFormAsync<T>(this HttpClient client, string requestUri, IEnumerable<KeyValuePair<string, object>> value)
        {
            return PostFormAsync<T>(client, CreateUri(requestUri), value, CancellationToken.None);
        }

        public static Task<T> PostFormAsync<T>(this HttpClient client, Uri requestUri, IEnumerable<KeyValuePair<string, object>> value)
        {
            return PostFormAsync<T>(client, requestUri, value, CancellationToken.None);
        }

        public static Task<T> PostFormAsync<T>(this HttpClient client, string requestUri, IEnumerable<KeyValuePair<string, object>> value, CancellationToken cancellationToken)
        {
            return PostFormAsync<T>(client, CreateUri(requestUri), value, cancellationToken);
        }

        public static Task<T> PostFormAsync<T>(this HttpClient client, Uri requestUri, IEnumerable<KeyValuePair<string, object>> value, CancellationToken cancellationToken)
        {
            return PostAsync<T>(client, requestUri, ParseFormContent(value), cancellationToken);
        }

        public static Task<T> PostFormAsync<T>(this HttpClient client, string requestUri, dynamic value)
        {
            return PostFormAsync<T>(client, CreateUri(requestUri), value, CancellationToken.None);
        }

        public static Task<T> PostFormAsync<T>(this HttpClient client, Uri requestUri, dynamic value)
        {
            return PostFormAsync<T>(client, requestUri, value, CancellationToken.None);
        }

        public static Task<T> PostFormAsync<T>(this HttpClient client, string requestUri, dynamic value, CancellationToken cancellationToken)
        {
            return PostFormAsync<T>(client, CreateUri(requestUri), value, cancellationToken);
        }

        public static Task<T> PostFormAsync<T>(this HttpClient client, Uri requestUri, dynamic value, CancellationToken cancellationToken)
        {
            return PostAsync<T>(client, requestUri, ParseFormContent(value), cancellationToken);
        }

        #endregion

        #region ParseFormContent


        static FormUrlEncodedContent ParseUrlEncodedContent(IEnumerable<KeyValuePair<string, string>> value)
        {
            return new FormUrlEncodedContent(value);
        }

        static MultipartFormDataContent ParseMultipartFormContent(IEnumerable<KeyValuePair<string, object>> value)
        {
            var result = new MultipartFormDataContent();
            foreach (var item in value)
            {
                switch (item.Value)
                {
                    case Stream stream:
                        result.Add(new StreamContent(stream));
                        break;
                    case null:
                        break;
                    default:
                        result.Add(new StringContent(item.Value.ToString()), item.Key);
                        break;
                }
            }
            return result;
        }

        public static HttpContent ParseFormContent(IEnumerable<KeyValuePair<string, object>> value)
        {
            value.Select(e => e);
            if (value.Any(e => e.Value != null && e.Value is Stream))
                return ParseMultipartFormContent(value);
            else
                return ParseUrlEncodedContent(value);
        }

        public static FormUrlEncodedContent ParseUrlEncodedContent(dynamic value)
        {
            return new FormUrlEncodedContent(ObjectConverter.AsDictionary(value));
        }

        public static MultipartFormDataContent ParseMultipartFormContent(dynamic value)
        {
            return ParseMultipartFormContent(ObjectConverter.AsDictionary(value));
        }

        public static HttpContent ParseFormContent(dynamic value)
        {
            return ParseFormContent(ObjectConverter.AsDictionary(value));
        }

        #endregion

    }
}
