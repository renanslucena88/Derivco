using System.Text.Json.Serialization;

namespace FibonacciApi.Core.Models.Response
{
    public class ErrorResponse : BaseResponse
    {
        private static ErrorResponse _instance = null;

        new public static ErrorResponse Instance
        {
            get
            {
                return _instance ??= new ErrorResponse();
            }
        }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("MemoryExceeded")]
        public bool MemoryExceeded { get; set; }

        [JsonPropertyName("Timeout")]
        public bool TimeExceeded { get; set; }
    }
}