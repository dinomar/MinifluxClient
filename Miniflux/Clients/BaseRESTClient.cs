using Miniflux.Extensions;
using Miniflux.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Miniflux.Clients
{
    public class BaseRESTClient
    {
        private readonly Uri _baseUri;
        private int _timeout;


        public int Timeout { get { return _timeout; } }
        public string BaseUrl { get { return _baseUri.ToString(); } }

        public Dictionary<string, string> Headers { get; set; }


        public BaseRESTClient(Uri baseUri, int timeout = 30, Dictionary<string, string> headers = null)
        {
            if (baseUri == null)
            {
                throw new ArgumentNullException(nameof(baseUri));
            }

            _baseUri = baseUri;
            _timeout = timeout;

            Headers = (headers != null) ? headers : new Dictionary<string, string>();
        }

        public BaseRESTClient(string baseUrl, int timeout = 30, Dictionary<string, string> headers = null)
            : this(new Uri(baseUrl), timeout: timeout, headers: headers)
        {
            if (baseUrl == null)
            {
                throw new ArgumentNullException(nameof(baseUrl));
            }
        }


        public async Task<T> GetAsync<T>(string path) where T : class
        {
            string response = await GetAsync(path);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public async Task<string> GetAsync(string path)
        {
            using (HttpClient client = GetConfiguredClient())
            {
                var response = await client.GetAsync(getRequestUri(path));

                await response.EnsureClientResponseOk();

                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<T> PostAsync<T>(string path, string json) where T : class
        {
            string response = await PostAsync(path, json);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public async Task<string> PostAsync(string path, string json)
        {
            using (HttpClient client = GetConfiguredClient())
            {
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(getRequestUri(path), data);

                await response.EnsureClientResponseOk();

                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<T> PostAsync<T>(string path, KeyValuePair<string, string>[] form) where T : class
        {
            string response = await PostAsync(path, form);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public async Task<string> PostAsync(string path, KeyValuePair<string, string>[] form)
        {
            using (HttpClient client = GetConfiguredClient())
            {
                var data = new FormUrlEncodedContent(form);
                var response = await client.PostAsync(getRequestUri(path), data);

                await response.EnsureClientResponseOk();

                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<T> PostAsync<T>(string path, byte[] file) where T : class
        {
            string response = await PostAsync(path, file);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public async Task<string> PostAsync(string path, byte[] file)
        {
            using (HttpClient client = GetConfiguredClient())
            {
                HttpResponseMessage response = null;

                using (var data = new MultipartFormDataContent())
                {
                    data.Add(new StreamContent(new MemoryStream(file)));

                    response = await client.PostAsync(getRequestUri(path), data);
                }

                await response.EnsureClientResponseOk();

                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<T> PutAsync<T>(string path, string json = null) where T : class
        {
            string response = await PutAsync(path, json);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public async Task<string> PutAsync(string path, string json = null)
        {
            using (HttpClient client = GetConfiguredClient())
            {
                HttpResponseMessage response = null;
                if (!string.IsNullOrEmpty(json))
                {
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await client.PutAsync(getRequestUri(path), data);
                }
                else
                {
                    response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Put, getRequestUri(path)));
                }

                await response.EnsureClientResponseOk();

                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> DeleteAsync(string path)
        {
            using (HttpClient client = GetConfiguredClient())
            {
                var response = await client.DeleteAsync(getRequestUri(path));

                await response.EnsureClientResponseOk();

                return await response.Content.ReadAsStringAsync();
            }
        }

        protected HttpClient GetConfiguredClient()
        {
            HttpClient client = new HttpClient();

            client.Timeout = TimeSpan.FromSeconds(_timeout);

            if (Headers != null)
            {
                foreach (var header in Headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            return client;
        }

        private Uri getRequestUri(string path)
        {
            return (string.IsNullOrEmpty(path) || path.Length == 1 && path.StartsWith("/")) ? _baseUri : new Uri(_baseUri, path);
        }
    }
}
