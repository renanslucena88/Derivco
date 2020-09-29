using FibonacciApi.Core.Models.Response;

namespace FibonacciApi.Test.Builder
{
    public class FibonacciServiceResponseBuilder
    {
        private static readonly BaseResponse response = BaseResponse.Instance;
        private static ErrorResponse errorResponse = new ErrorResponse();

        public static BaseResponse BaseResponseCorrect()
        {
            string[] arrFibo = new string[3];
            arrFibo[0] = "F(28) = 196418";
            arrFibo[1] = "F(29) = 317811";
            arrFibo[2] = "F(30) = 514229";

            response.ResultWithIndex = arrFibo;
            response.TimeExecuted = 500;

            return response;
        }

        public static ErrorResponse ErrorTimeOut(int timeout)
        {
            errorResponse = new ErrorResponse
            {
                Description = string.Format("The operation exceeded {0} milliseconds", timeout.ToString()),
                TimeExceeded = true
            };

            return errorResponse;
        }

        public static ErrorResponse ErrorMemory()
        {
            errorResponse = new ErrorResponse
            {
                MemoryExceeded = true
            };

            return errorResponse;
        }

        public FibonacciServiceResponseBuilder ChangeTimeExecuted(int timeExecuted)
        {
            response.TimeExecuted = timeExecuted;
            return this;
        }
    }
}