using System.ComponentModel;
using System.Net;
using System.Text.Json;

namespace PocketWallet.Bkash.Http;

/// <summary>
/// Represents a generic class to create an interpreted object of Bkash response.
/// </summary>
/// <typeparam name="TOut">Expected output object type.</typeparam>
internal class HttpResponse<TOut> where TOut : BaseBkashResponse
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never,
        NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString
    };

    /// <summary>
    /// Initiates <see cref="HttpResponse{T}"/> object.
    /// </summary>
    /// <param name="httpResponse">Network response.</param>
    private HttpResponse(HttpResponseMessage httpResponse)
    {
        StatusCode = httpResponse.StatusCode;
        Response = httpResponse.Content.ReadAsStringAsync().Result;

        if (httpResponse.IsSuccessStatusCode)
        {
            Data = JsonSerializer.Deserialize<TOut>(Response, JsonOptions);
        }

        Success = CheckIfOk(httpResponse.IsSuccessStatusCode, Data);
    }

    /// <summary>
    /// Initiates <see cref="HttpResponse{T}"/> object.
    /// </summary>
    /// <param name="isSuccessStatusCode">Network response success status.</param>
    /// <param name="responseString">Network response string.</param>
    /// <param name="httpStatusCode">Network response status code.</param>
    private HttpResponse(bool isSuccessStatusCode, string responseString, HttpStatusCode? httpStatusCode)
    {
        StatusCode = httpStatusCode;
        Response = responseString;

        if (isSuccessStatusCode)
        {
            Data = JsonSerializer.Deserialize<TOut>(Response, JsonOptions);
        }

        Success = CheckIfOk(isSuccessStatusCode, Data);
    }

    /// <summary>
    /// Response success status.
    /// </summary>
    [DefaultValue(false)]
    internal bool Success { get; }

    /// <summary>
    /// Response status code.
    /// </summary>
    [DefaultValue(null)]
    internal HttpStatusCode? StatusCode { get; }

    /// <summary>
    /// Response string.
    /// </summary>
    internal string Response { get; } = string.Empty;

    /// <summary>
    /// Response data object.
    /// </summary>
    internal TOut? Data { get; }

    /// <summary>
    /// Creates final HTTP response result based on network response.
    /// </summary>
    /// <param name="httpResponse">Network response.</param>
    /// <returns>An interpreted object of network response.</returns>
    internal static HttpResponse<TOut> Create(HttpResponseMessage httpResponse) => new(httpResponse);

    /// <summary>
    /// Creates final HTTP response result based on network response.
    /// </summary>
    /// <param name="isSuccessStatusCode">Network response success status.</param>
    /// <param name="responseString">Network response string.</param>
    /// <param name="httpStatusCode">Network response status code.</param>
    /// <returns>An interpreted object of network response.</returns>
    internal static HttpResponse<TOut> Create(bool isSuccessStatusCode, string responseString, HttpStatusCode? httpStatusCode = null) =>
        new(isSuccessStatusCode, responseString, httpStatusCode);

    /// <summary>
    /// Checks if request is overall indicates fine or not.
    /// </summary>
    /// <param name="isSuccessStatusCode">Network success status.</param>
    /// <param name="responseData">Bkash Response.</param>
    /// <returns>Overall response status.</returns>
    private static bool CheckIfOk(bool isSuccessStatusCode, TOut? responseData) => isSuccessStatusCode
            && responseData is not null
            && !string.IsNullOrWhiteSpace(responseData.StatusCode)
            && responseData.StatusCode is CONSTANTS.BKASH_SUCCESS_RESPONSE_CODE
            && string.IsNullOrWhiteSpace(responseData.ErrorCode);
}
