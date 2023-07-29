using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Net;

namespace PocketWallet.Bkash.Http;
internal class HttpResponse<TOut> where TOut : BaseBkashResponse
{
    private HttpResponse(HttpResponseMessage httpResponse)
    {
        StatusCode = httpResponse.StatusCode;
        Response = httpResponse.Content.ReadAsStringAsync().Result;

        if (httpResponse.IsSuccessStatusCode)
        {
            Data = JObject.Parse(Response).ToObject<TOut>();
        }

        if (httpResponse.IsSuccessStatusCode
            && Data is not null
            && !string.IsNullOrWhiteSpace(Data.StatusCode)
            && Data.StatusCode is CONSTANTS.SUCCESS_RESPONSE_CODE
            && string.IsNullOrWhiteSpace(Data.ErrorCode))
        {
            Success = true;
        }
    }

    private HttpResponse(bool success, string response, HttpStatusCode? httpStatusCode)
    {
        StatusCode = httpStatusCode;
        Response = response;

        if (success)
        {
            Data = JObject.Parse(Response).ToObject<TOut>();
        }

        if (success
            && Data is not null
            && !string.IsNullOrWhiteSpace(Data.StatusCode)
            && Data.StatusCode is CONSTANTS.SUCCESS_RESPONSE_CODE
            && string.IsNullOrWhiteSpace(Data.ErrorCode))
        {
            Success = true;
        }
    }

    [DefaultValue(false)]
    internal bool Success { get; }

    [DefaultValue(null)]
    internal HttpStatusCode? StatusCode { get; }

    internal string Response { get; } = string.Empty;

    internal TOut? Data { get; }

    internal static HttpResponse<TOut> Create(HttpResponseMessage httpResponse)
    {
        return new(httpResponse);
    }

    internal static HttpResponse<TOut> Create(
        bool success,
        string response,
        HttpStatusCode? httpStatusCode = null)
    {
        return new(success, response, httpStatusCode);
    }
}
