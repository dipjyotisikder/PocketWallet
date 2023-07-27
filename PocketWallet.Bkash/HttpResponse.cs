using System.ComponentModel;
using System.Net;

namespace PocketWallet.Bkash;
internal class HttpResponse<TOut>
{
    private HttpResponse(HttpResponseMessage httpResponse)
    {
        Success = httpResponse.IsSuccessStatusCode;
        StatusCode = httpResponse.StatusCode;
        Response = httpResponse.Content.ReadAsStringAsync().Result;

        if (httpResponse.IsSuccessStatusCode)
        {
            try
            {
                var value = JsonConvert.DeserializeObject<dynamic>(Response);
                Data = (TOut)value!;
                Parsed = true;
            }
            catch (Exception)
            {
                Data = default;
                Parsed = false;
            }
        }
    }

    private HttpResponse(bool success, string response, HttpStatusCode? httpStatusCode)
    {
        Success = success;
        StatusCode = httpStatusCode;

        if (!success)
        {
            Data = default;
            Response = response;
        }

        try
        {
            Data = JsonConvert.DeserializeObject<TOut>(Response)!;
            Parsed = true;
        }
        catch (Exception)
        {
            Data = default;
            Parsed = false;
        }
    }

    [DefaultValue(false)]
    internal bool Success { get; }

    [DefaultValue(false)]
    internal bool Parsed { get; }

    [DefaultValue(null)]
    internal HttpStatusCode? StatusCode { get; }

    internal string Response { get; } = string.Empty;

    internal TOut? Data { get; }

    internal static HttpResponse<TOut> Create(HttpResponseMessage httpResponse)
    {
        return new(httpResponse);
    }

    internal static HttpResponse<TOut> Create(bool success, string response, HttpStatusCode? httpStatusCode = null)
    {
        return new(success, response, httpStatusCode);
    }
}
