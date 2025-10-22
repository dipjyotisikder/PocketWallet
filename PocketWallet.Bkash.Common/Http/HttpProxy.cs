using PocketWallet.Bkash.Common.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PocketWallet.Bkash.Common.Http
{
    /// <summary>
    /// Represents a static class that helps to make HTTP request to BKASH APIs.
    /// </summary>
    internal static class HttpProxy
    {
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never
        };

        /// <summary>
        /// Represents a HTTP POST request to Bkash API.
        /// </summary>
        /// <typeparam name="TIn">Input body object type.</typeparam>
        /// <typeparam name="TOut">Output data type.</typeparam>
        /// <param name="httpClient">HTTP client object.</param>
        /// <param name="endpoint">Endpoint URL of Bkash.</param>
        /// <param name="body">Input request body object.</param>
        /// <param name="headers">Additional header parameters.</param>
        /// <returns>HTTP response object.</returns>
        internal static async Task<HttpResponse<TOut>> PostAsync<TIn, TOut>(this HttpClient httpClient,
            string endpoint, TIn body, Dictionary<string, string> headers = null) where TOut : BaseBkashResponse
        {
            return await Request<TIn, TOut>(
                httpClient: httpClient,
                method: HttpMethod.Post,
                endpoint: endpoint,
                body: body,
                headers: headers);
        }

        /// <summary>
        /// Represents a request handler.
        /// </summary>
        /// <typeparam name="TIn">Input body object type.</typeparam>
        /// <typeparam name="TOut">Output data type.</typeparam>
        /// <param name="httpClient">HTTP client object.</param>
        /// <param name="method">HTTP request method. (GET, POST etc.)</param>
        /// <param name="endpoint">Endpoint URL of Bkash.</param>
        /// <param name="body">Input request body object.</param>
        /// <param name="headers">Additional header parameters.</param>
        /// <returns>HTTP response object.</returns>
        private static async Task<HttpResponse<TOut>> Request<TIn, TOut>(
            HttpClient httpClient,
            HttpMethod method,
            string endpoint,
            TIn body,
            Dictionary<string, string> headers = null) where TOut : BaseBkashResponse
        {
            var requestMessage = ProcessRequestMessage(httpClient, method, endpoint, headers);

            if (body != null)
            {
                requestMessage.Content = new StringContent(
                    content: JsonSerializer.Serialize(body, JsonOptions),
                    encoding: Encoding.UTF8,
                    mediaType: "application/json");
            }

            return await HandleRequest<TOut>(httpClient, requestMessage);
        }

        /// <summary>
        /// Represents a request message processor.
        /// </summary>
        /// <param name="httpClient">HTTP client object.</param>
        /// <param name="method">HTTP request method. (GET, POST etc.)</param>
        /// <param name="endpoint">Endpoint URL of Bkash.</param>
        /// <param name="headers">Additional header parameters.</param>
        /// <returns>HTTP request message object.</returns>
        private static HttpRequestMessage ProcessRequestMessage(HttpClient httpClient, HttpMethod method, string endpoint, Dictionary<string, string> headers = null)
        {
            var requestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(httpClient.BaseAddress, endpoint),
                Method = method
            };

            foreach (var header in httpClient.DefaultRequestHeaders)
            {
                requestMessage.Headers.Add(header.Key, header.Value);
            }

            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    requestMessage.Headers.Add(header.Key, header.Value);
                }
            }

            return requestMessage;
        }

        /// <summary>
        /// Represents a request handler.
        /// </summary>
        /// <typeparam name="TOut">Output data type.</typeparam>
        /// <param name="httpClient">HTTP client object.</param>
        /// <param name="requestMessage">HTTP request message object.</param>
        /// <returns>A HTTP response object after calling Bkash API.</returns>
        private static async Task<HttpResponse<TOut>> HandleRequest<TOut>(HttpClient httpClient, HttpRequestMessage requestMessage)
            where TOut : BaseBkashResponse
        {
            try
            {
                return HttpResponse<TOut>.Create(await httpClient.SendAsync(requestMessage));
            }
            catch (Exception e)
            {
                var message = string.Format(format: "Exception Message: {0} | Inner Exception Message: {1}", arg0: e.Message, arg1: e.InnerException?.Message);
                return HttpResponse<TOut>.Create(isSuccessStatusCode: false, responseString: message);
            }
        }
    }
}
