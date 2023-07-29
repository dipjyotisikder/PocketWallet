using System.Text;

namespace PocketWallet.Bkash.Http;
internal static class HttpProxy
{
    internal static async Task<HttpResponse<TOut>> GetAsync<TOut>(
        this HttpClient httpClient,
        string endpoint,
        Dictionary<string, string>? headers = null) where TOut : BaseBkashResponse
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
        Dictionary<string, string>? headers = null) where TOut : BaseBkashResponse
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
        Dictionary<string, string>? headers = null) where TOut : BaseBkashResponse
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
        Dictionary<string, string>? headers = null) where TOut : BaseBkashResponse
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
        Dictionary<string, string>? headers = null) where TOut : BaseBkashResponse
    {
        var requestMessage = new HttpRequestMessage
        {
            RequestUri = new Uri(httpClient.BaseAddress!, endpoint),
            Method = method
        };

        foreach (var x in httpClient.DefaultRequestHeaders)
        {
            requestMessage.Headers.Add(x.Key, x.Value);
        }

        if (body is not null)
        {
            var jsonPayload = JsonConvert.SerializeObject(body);
            requestMessage.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        }

        if (headers is not null)
        {
            foreach (KeyValuePair<string, string> header in headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value);
            }
        }

        return await HandleRequest<TOut>(httpClient, requestMessage);
    }

    private static async Task<HttpResponse<TOut>> HandleRequest<TOut>(HttpClient httpClient, HttpRequestMessage requestMessage) where TOut : BaseBkashResponse
    {
        try
        {
            return HttpResponse<TOut>.Create(await httpClient.SendAsync(requestMessage));
        }
        catch (Exception e)
        {
            var message = string.Format("Exception Message: {0} | Inner Exception Message: {1}", e.Message, e.InnerException?.Message);
            return HttpResponse<TOut>.Create(success: false, response: message);
        }
    }
}
