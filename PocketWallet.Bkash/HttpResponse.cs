using System.Net;

namespace PocketWallet.Bkash;
internal class HttpResponse<TOut>
{
    private HttpResponse(
        bool isSuccessStatusCode,
        HttpStatusCode statusCode,
        string responseContent)
    {
        IsSuccessStatusCode = isSuccessStatusCode;
        StatusCode = statusCode;
        ResponseString = responseContent;

        try
        {
            Response = JsonConvert.DeserializeObject<TOut>(responseContent)!;
            IsParseSuccess = true;
        }
        catch (Exception)
        {
            Response = default;
            IsParseSuccess = false;
        }
    }

    internal bool IsSuccessStatusCode { get; }

    internal bool IsParseSuccess { get; }

    internal HttpStatusCode StatusCode { get; }

    internal string ResponseString { get; }

    internal TOut Response { get; }

    internal static HttpResponse<TOut> Create(
          bool isSuccessStatusCode,
          HttpStatusCode statusCode,
          string responseContent)
    {
        return new(
            isSuccessStatusCode,
            statusCode,
            responseContent);
    }
}
