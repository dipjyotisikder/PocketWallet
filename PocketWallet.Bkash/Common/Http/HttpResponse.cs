using PocketWallet.Bkash.Common.Models;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace PocketWallet.Bkash.Common.Http
{
    /// <summary>
    /// Represents a generic class to create an interpreted object of Bkash response.
    /// </summary>
    /// <typeparam name="TOut">Expected output object type.</typeparam>
    internal class HttpResponse<TOut> where TOut : BaseBkashResponse
    {
        private static readonly JsonSerializerSettings JsonSettings = new()
        {
            NullValueHandling = NullValueHandling.Include,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        /// <summary>
        /// Initiates <see cref="HttpResponse{T}"/> object.
        /// </summary>
        /// <param name="httpResponse">Network response.</param>
        private HttpResponse(HttpResponse httpResponse)
        {
            StatusCode = httpResponse.StatusCode;
            Response = httpResponse.ResponseString;

            if (httpResponse.IsSuccessStatusCode)
            {
                Data = JsonConvert.DeserializeObject<TOut>(Response, JsonSettings)!;
            }

            Success = CheckIfOk(httpResponse.IsSuccessStatusCode, Data);
        }

        /// <summary>
        /// Response success status.
        /// /// </summary>
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
        internal TOut Data { get; } = null!;

        /// <summary>
        /// Creates final HTTP response result based on network response.
        /// </summary>
        /// <param name="httpResponse">Network response.</param>
        /// <returns>An interpreted object of network response.</returns>
        internal static HttpResponse<TOut> Create(HttpResponse httpResponse) => new(httpResponse);

        /// <summary>
        /// Checks if request is overall indicates fine or not.
        /// </summary>
        /// <param name="isSuccessStatusCode">Network success status.</param>
        /// <param name="responseData">Bkash Response.</param>
        /// <returns>Overall response status.</returns>
        private static bool CheckIfOk(bool isSuccessStatusCode, TOut? responseData = null) => isSuccessStatusCode
                && responseData != null
                && !string.IsNullOrWhiteSpace(responseData.StatusCode)
                && responseData.StatusCode is Constants.Constants.BKASH_SUCCESS_RESPONSE_CODE
                && string.IsNullOrWhiteSpace(responseData.ErrorCode);
    }

    /// <summary>
    /// Represents the result of an HTTP request, including the response content, status code, and success indicator.
    /// </summary>
    /// <remarks>Use this class to access the details of an HTTP response, such as the raw response body, the
    /// HTTP status code, and whether the response indicates a successful status. The definition of success is based on
    /// the status code, typically any code in the 2xx range.</remarks>
    public class HttpResponse
    {
        /// <summary>
        /// Gets or sets the raw response string from the HTTP request.
        /// </summary>
        public string ResponseString { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the HTTP status code returned by the response.
        /// </summary>
        public HttpStatusCode? StatusCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the HTTP response was successful.
        /// </summary>
        public bool IsSuccessStatusCode { get; set; }
    }
}