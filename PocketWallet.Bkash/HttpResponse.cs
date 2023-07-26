using System.Net;

namespace PocketWallet.Bkash;
internal class HttpResponse<TOut>
{
    private HttpResponse(HttpResponseMessage httpResponse)
    {
        IsSuccessStatusCode = httpResponse.IsSuccessStatusCode;
        StatusCode = httpResponse.StatusCode;
        ResponseString = httpResponse.Content.ReadAsStringAsync().Result;

        try
        {
            Data = JsonConvert.DeserializeObject<TOut>(ResponseString)!;
            IsParsedSuccessfully = true;
        }
        catch (Exception)
        {
            Data = default;
            IsParsedSuccessfully = false;
        }
    }

    internal bool IsSuccessStatusCode { get; }

    internal bool IsParsedSuccessfully { get; }

    internal HttpStatusCode StatusCode { get; }

    internal string ResponseString { get; }

    internal TOut? Data { get; }

    internal static HttpResponse<TOut> Create(HttpResponseMessage httpResponse)
    {
        return new(httpResponse);
    }
}
