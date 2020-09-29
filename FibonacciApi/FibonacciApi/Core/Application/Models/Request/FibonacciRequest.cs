using System;
using System.Text.Json.Serialization;

namespace FibonacciApi.Core.Models.Request
{
    public class FibonacciRequest
    {
        private static FibonacciRequest _instance = null;

        public static FibonacciRequest Instance
        {
            get
            {
                return _instance ??= new FibonacciRequest();
            }
        }

        public FibonacciRequest()
        {
        }

        public FibonacciRequest(int firstIndex, int lastIndex, bool useCache, int timeOfExecution, int amountMemory)
        {
            if (firstIndex < 0)
            {
                throw new ArgumentException("First Index cannot be less than 0");
            }
            if (lastIndex < 0)
            {
                throw new ArgumentException("Last Index cannot be less than 0");
            }
            if (firstIndex > lastIndex)
            {
                throw new ArgumentException("First Index cannot be bigger than Last Index");
            }
            if (timeOfExecution <= 0)
            {
                throw new ArgumentException("Time Of Execution cannot be less or equals than 0");
            }
            if (amountMemory <=0)
            {
                throw new ArgumentException("Amount of Memory cannot be less or equals than 0");
            }

            FirstIndex = firstIndex;
            LastIndex = lastIndex;
            UseCache = useCache;
            TimeOfExecution = timeOfExecution;
            AmountMemory = amountMemory;
        }

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