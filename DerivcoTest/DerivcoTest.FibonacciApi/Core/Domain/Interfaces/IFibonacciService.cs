using DerivcoTest.FibonacciApi.Core.Models.Request;
using DerivcoTest.FibonacciApi.Core.Models.Response;
using System.Numerics;
using System.Threading.Tasks;

namespace DerivcoTest.FibonacciApi.Core.Domain.Interfaces
{
    public interface IFibonacciService
    {
        Task<BaseResponse> Fibonacci(FibonacciRequest request);

        Task<BigInteger> FibonacciAsync(int n);

        string[] PrepareResult(BigInteger[] arrTmpResult, int firstIndex, int lastIndex);
    }
}