using System.Text;

namespace PocketWallet.Nagad.Http;
internal static class HttpProxy
{
    internal static async Task<HttpResponse<TOut>> GetAsync<TOut>(
        this HttpClient httpClient,
        string endpoint,
        Dictionary<string, string>? headers = null) where TOut : BaseNagadResponse
    {
        return await Request<TOut>(
            httpClient: httpClient,
            method: HttpMethod.Get,
            endpoint: endpoint,
            headers: headers);
    }

    internal static async Task<HttpResponse<TOut>> PostAsync<TIn, TOut>(
        this HttpClient httpClient,
        string endpoint,
        TIn body,
        Dictionary<string, string>? headers = null) where TOut : BaseNagadResponse
    {
        return await Request<TIn, TOut>(
            httpClient: httpClient,
            method: HttpMethod.Post,
            endpoint: endpoint,
            body: body,
            headers: headers);
    }

    internal static async Task<HttpResponse<TOut>> PutAsync<TIn, TOut>(
        this HttpClient httpClient,
        string endpoint,
        TIn body,
        Dictionary<string, string>? headers = null) where TOut : BaseNagadResponse
    {
        return await Request<TIn, TOut>(
            httpClient: httpClient,
            method: HttpMethod.Put,
            endpoint: endpoint,
            body: body,
            headers: headers);
    }

    internal static async Task<HttpResponse<TOut>> DeleteAsync<TOut>(
        this HttpClient httpClient,
        string endpoint,
        Dictionary<string, string>? headers = null) where TOut : BaseNagadResponse
    {
        return await Request<TOut>(
            httpClient: httpClient,
            method: HttpMethod.Delete,
            endpoint: endpoint,
            headers: headers);
    }

    internal static async Task<HttpResponse<TOut>> DeleteAsync<TIn, TOut>(
        this HttpClient httpClient,
        string endpoint,
        TIn body,
        Dictionary<string, string>? headers = null) where TOut : BaseNagadResponse
    {
        return await Request<TIn, TOut>(
            httpClient: httpClient,
            method: HttpMethod.Delete,
            endpoint: endpoint,
            body: body,
            headers: headers);
    }

    private static async Task<HttpResponse<TOut>> Request<TIn, TOut>(
        HttpClient httpClient,
        HttpMethod method,
        string endpoint,
        TIn body,
        Dictionary<string, string>? headers = null) where TOut : BaseNagadResponse
    {
        var requestMessage = ProcessRequestMessage(httpClient, method, endpoint, headers);

        if (body is not null)
        {
            var jsonPayload = JsonConvert.SerializeObject(body);
            requestMessage.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        }

        return await HandleRequest<TOut>(httpClient, requestMessage);
    }

    private static async Task<HttpResponse<TOut>> Request<TOut>(
        HttpClient httpClient,
        HttpMethod method,
        string endpoint,
        Dictionary<string, string>? headers = null) where TOut : BaseNagadResponse
    {
        var requestMessage = ProcessRequestMessage(httpClient, method, endpoint, headers);
        return await HandleRequest<TOut>(httpClient, requestMessage);
    }

    private static HttpRequestMessage ProcessRequestMessage(
        HttpClient httpClient,
        HttpMethod method,
        string endpoint,
        Dictionary<string, string>? headers)
    {
        var requestMessage = new HttpRequestMessage
        {
            RequestUri = new Uri(httpClient.BaseAddress!, endpoint),
            Method = method
        };

        foreach (var header in httpClient.DefaultRequestHeaders)
        {
            requestMessage.Headers.Add(header.Key, header.Value);
        }

        if (headers is not null)
        {
            foreach (KeyValuePair<string, string> header in headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value);
            }
        }

        return requestMessage;
    }

    private static async Task<HttpResponse<TOut>> HandleRequest<TOut>(HttpClient httpClient, HttpRequestMessage requestMessage)
        where TOut : BaseNagadResponse
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
