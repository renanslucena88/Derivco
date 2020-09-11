using DerivcoTest.FibonacciApi.Core.Models.Request;
using DerivcoTest.FibonacciApi.Core.Models.Response;
using System.Numerics;
using System.Threading.Tasks;

namespace DerivcoTest.FibonacciApi.Core.Domain.Interfaces
{
    public interface IFibonacciService
    {
        void ValidadeRequest(FibonacciRequest request);

        Task<BaseResponse> Fibonacci(FibonacciRequest request);
    }
}