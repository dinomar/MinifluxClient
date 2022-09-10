using Miniflux.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Miniflux.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task EnsureClientResponseOk(this HttpResponseMessage response)
        {
            try
            {
                if (!response.IsSuccessStatusCode)
                {
                    string contentString = await response.Content.ReadAsStringAsync();
                    ErrorResponseModel errorResponse = JsonConvert.DeserializeObject<ErrorResponseModel>(contentString);
                    throw new ClientException(errorResponse != null ? errorResponse.ErrorMessage : "Error", (int)response.StatusCode);
                }
            }
            catch (JsonSerializationException)
            {
                throw new ClientException("Client Error", (int)response.StatusCode);
            }
        }
    }
}
