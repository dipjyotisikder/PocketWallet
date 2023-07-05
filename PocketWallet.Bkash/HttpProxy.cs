using System.Text;

namespace PocketWallet.Bkash;
internal static class HttpProxy
{
    internal static async Task<HttpResponse<TOut>> GetAsync<TOut>(
        this HttpClient httpClient,
        string endpoint,
        Dictionary<string, string>? headers = null)
    {
        return await Request<TOut>(
            httpClient: httpClient,
            method: HttpMethod.Get,
            endpoint: endpoint,
            headers: headers);
    }

    internal static async Task<HttpResponse<TOut>> PostAsync<TOut>(
        this HttpClient httpClient,
        string endpoint,
        object body,
        Dictionary<string, string>? headers = null)
    {
        return await Request<TOut>(
            httpClient: httpClient,
            method: HttpMethod.Post,
            endpoint: endpoint,
            body: body,
            headers: headers);
    }

    internal static async Task<HttpResponse<TOut>> PutAsync<TOut>(
        this HttpClient httpClient,
        string endpoint,
        object body,
        Dictionary<string, string>? headers = null)
    {
        return await Request<TOut>(
            httpClient: httpClient,
            method: HttpMethod.Put,
            endpoint: endpoint,
            body: body,
            headers: headers);
    }

    internal static async Task<HttpResponse<TOut>> DeleteAsync<TOut>(
        this HttpClient httpClient,
        string endpoint,
        object? body = null,
        Dictionary<string, string>? headers = null)
    {
        return await Request<TOut>(
            httpClient: httpClient,
            method: HttpMethod.Delete,
            endpoint: endpoint,
            body: body,
            headers: headers);
    }

    private static async Task<HttpResponse<TOut>> Request<TOut>(
        HttpClient httpClient,
        HttpMethod method,
        string endpoint,
        object? body = null,
        Dictionary<string, string>? headers = null)
    {
        var requestMessage = new HttpRequestMessage
        {
            RequestUri = new Uri(endpoint),
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
