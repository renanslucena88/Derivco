using System.Text.Json.Serialization;

namespace DerivcoTest.FibonacciApi.Core.Models.Request
{
    public class FibonacciRequest
    {
        [JsonPropertyName("FirstIndex")]
        public int FirstIndex { get; set; }

        [JsonPropertyName("LastIndex")]
        public int LastIndex { get; set; }

        [JsonPropertyName("UseCache")]
        public bool UseCache { get; set; }

        [JsonPropertyName("TimeOfExecution")]
        public int TimeOfExecution { get; set; }

        [JsonPropertyName("AmountMemory")]
        public int AmountMemory { get; set; }
    }
}