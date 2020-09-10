using System.Text.Json.Serialization;

namespace DerivcoTest.FibonacciApi.Core.Models.Response
{
    public class ErrorResponse : BaseResponse
    {
        private static ErrorResponse _instance = null;

        public static ErrorResponse Instance
        {
            get
            {
                if (_instance == null)
                {
                    return _instance = new ErrorResponse();
                }
                return _instance;
            }
        }

        //public ErrorResponse(int errorCode)
        //{
        //    this.ErrorCode = errorCode;
        //    this.Description = ErrorCodeResolver.GetErrorDescription(ErrorCode);
        //}

        //[JsonPropertyName("ErrorCode")]
        //public int ErrorCode { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }
    }
}