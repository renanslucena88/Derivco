using System.Numerics;
using System.Text.Json.Serialization;

namespace DerivcoTest.FibonacciApi.Core.Models.Response
{
    public class BaseResponse
    {
        private static BaseResponse _instance = null;

        public static BaseResponse Instance
        {
            get
            {
                if (_instance == null)
                {
                    return _instance = new BaseResponse();
                }
                return _instance;
            }
        }

        [JsonPropertyName("Timeout")]
        public bool TimeExeeded { get; set; }

        [JsonPropertyName("TimeExecutedMilliseconds")]
        public double TimeExecuted { get; set; }

        [JsonIgnore]
        public BigInteger[] Result { get; set; }

        [JsonPropertyName("FibonacciSubSequence")]
        public string[] ResultWithIndex { get; set; }
    }
}