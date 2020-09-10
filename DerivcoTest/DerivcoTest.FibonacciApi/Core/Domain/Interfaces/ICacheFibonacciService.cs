using System.Numerics;

namespace DerivcoTest.FibonacciApi.Core.Domain.Interfaces
{
    public interface ICacheFibonacciService
    {
        BigInteger[] GetFibonacciCached(int key);

        void SetFibonacciCache(int key, BigInteger[] value);

        void ClearCache(int key);
    }
}