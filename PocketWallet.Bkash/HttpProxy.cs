using System.Net.Http.Headers;
using System.Text;

namespace PocketWallet.Bkash;
internal static class HttpProxy
{
    internal static async Task<HttpResponse<TOut>> GetAsync<TOut>(
        this HttpClient httpClient,
        string url,
        Dictionary<string, string>? headers = null)
    {
        return await Request<TOut>(httpClient, HttpMethod.Get, url, headers);
    }

    internal static async Task<HttpResponse<TOut>> PostAsync<TOut>(
        this HttpClient httpClient,
        string url,
        object body,
        Dictionary<string, string>? headers = null)
    {
        return await Request<TOut>(httpClient, HttpMethod.Post, url, body, headers);
    }

    internal static async Task<HttpResponse<TOut>> PutAsync<TOut>(
        this HttpClient httpClient,
        string url,
        object body,
        Dictionary<string, string>? headers = null)
    {
        return await Request<TOut>(httpClient, HttpMethod.Put, url, body, headers);
    }

    internal static async Task<HttpResponse<TOut>> DeleteAsync<TOut>(
        this HttpClient httpClient,
        string url,
        object? body = null,
        Dictionary<string, string>? headers = null)
    {
        return await Request<TOut>(httpClient, HttpMethod.Delete, url, body, headers);
    }

    private static async Task<HttpResponse<TOut>> Request<TOut>(
        HttpClient httpClient,
        HttpMethod method,
        string url,
        object? body = null,
        Dictionary<string, string>? headers = null)
    {
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var requestMessage = new HttpRequestMessage()
        {
            RequestUri = new Uri(url),
            Method = method
        };

        if (body is not null)
        {
            var jsonPayload = JsonConvert.SerializeObject(body, Formatting.Indented);
            requestMessage.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        }

        if (headers is not null)
        {
            foreach (KeyValuePair<string, string> header in headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value);
            }
        }

        var httpResponse = await httpClient.SendAsync(requestMessage);
        string content = await httpResponse.Content.ReadAsStringAsync();

        return HttpResponse<TOut>.Create(
            isSuccessStatusCode: httpResponse.IsSuccessStatusCode,
            statusCode: httpResponse.StatusCode,
            responseContent: content);
    }
}
