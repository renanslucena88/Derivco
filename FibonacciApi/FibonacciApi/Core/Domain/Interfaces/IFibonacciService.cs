using FibonacciApi.Core.Models.Request;
using FibonacciApi.Core.Models.Response;
using System.Numerics;
using System.Threading.Tasks;

namespace FibonacciApi.Core.Domain.Interfaces
{
    public interface IFibonacciService
    {
        void ValidadeRequest(FibonacciRequest request);

        Task<BaseResponse> Fibonacci(FibonacciRequest request);
    }
}