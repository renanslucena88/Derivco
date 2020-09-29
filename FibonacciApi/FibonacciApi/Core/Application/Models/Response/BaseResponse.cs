using System.Numerics;
using System.Text.Json.Serialization;

namespace FibonacciApi.Core.Models.Response
{
    public class BaseResponse
    {
        private static BaseResponse _instance = null;

        public static BaseResponse Instance
        {
            get
            {
                return _instance ??= new BaseResponse();
            }
        }

        [JsonPropertyName("TimeExecutedMilliseconds")]
        public double TimeExecuted { get; set; }

        [JsonIgnore]
        public BigInteger[] Result { get; set; }

        [JsonPropertyName("FibonacciSubSequence")]
        public string[] ResultWithIndex { get; set; }
    }
}