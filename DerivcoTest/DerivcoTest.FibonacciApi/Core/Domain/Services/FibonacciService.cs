using DerivcoTest.FibonacciApi.Core.Domain.Interfaces;
using DerivcoTest.FibonacciApi.Core.Models.Request;
using DerivcoTest.FibonacciApi.Core.Models.Response;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace DerivcoTest.FibonacciApi.Core.Domain.Services
{
    public class FibonacciService : IFibonacciService
    {
        private readonly ICacheFibonacciService _cache;
        private readonly Stopwatch watch = new Stopwatch();
        private int timeOut;
        private int amountMemory;
        private bool useCache;
        private BigInteger[] arrFibonacci;
        private bool error = false;
        private bool errorTimeExeeded = false;
        private bool errorOutOfMemory = false;
        private string errorMessage = string.Empty;

        public FibonacciService(ICacheFibonacciService cache)
        {
            _cache = cache;
        }

        public void ValidadeRequest(FibonacciRequest request)
        {
            new FibonacciRequest(request.FirstIndex, request.LastIndex, request.UseCache, request.TimeOfExecution, request.AmountMemory);
        }

        public async Task<BaseResponse> Fibonacci(FibonacciRequest request)
        {
            watch.Restart();
            BaseResponse response = BaseResponse.Instance;
            ErrorResponse errorResponse = ErrorResponse.Instance;

            try
            {
                arrFibonacci = new BigInteger[request.LastIndex + 1];

                if (request.LastIndex <= 1)
                {
                    for (int i = 0; i < request.LastIndex; i++)
                    {
                        arrFibonacci[i] = request.LastIndex;
                    }
                }
                else
                {
                    try
                    {
                        timeOut = request.TimeOfExecution;
                        amountMemory = request.AmountMemory;
                        useCache = request.UseCache;

                        watch.Start();
                        CancellationTokenSource token = new CancellationTokenSource();
                        token.CancelAfter(timeOut);

                        await FibonacciAsync(request.LastIndex).ConfigureAwait(false);
                    }
                    catch (TimeoutException ex)
                    {
                        error = true;
                        errorTimeExeeded = true;
                        errorMessage = ex.Message;
                    }
                    catch (OutOfMemoryException ex)
                    {
                        error = true;
                        errorOutOfMemory = true;
                        errorMessage = ex.Message;
                    }
                    catch (StackOverflowException ex)
                    {
                        error = true;
                        errorMessage = ex.Message;
                    }
                }

                watch.Stop();

                if (error)
                {
                    errorResponse.Description = errorMessage;
                    errorResponse.TimeExceeded = errorTimeExeeded;
                    errorResponse.TimeExecuted = watch.Elapsed.TotalMilliseconds;
                    errorResponse.MemoryExceeded = errorOutOfMemory;
                    return errorResponse;
                }

                response.Result = arrFibonacci;
                if (!error)
                {
                    _cache.SetFibonacciCache(request.LastIndex, arrFibonacci);
                }
                _cache.SetFibonacciCache(request.LastIndex, arrFibonacci);

                response.ResultWithIndex = PrepareResult(arrFibonacci, request.FirstIndex, request.LastIndex);
                response.TimeExecuted = watch.Elapsed.TotalMilliseconds;
                return response;
            }
            catch (TaskCanceledException)
            {
                errorResponse.Description = errorMessage;
                errorResponse.TimeExceeded = error;
                errorResponse.TimeExecuted = watch.Elapsed.TotalMilliseconds;
                errorResponse.MemoryExceeded = errorOutOfMemory;
                return errorResponse;
            }
        }

        private async Task<BigInteger> FibonacciAsync(int n)
        {
            try
            {
                if (watch.ElapsedMilliseconds >= timeOut)
                {
                    throw new TimeoutException(string.Format("The operation exceeded {0} milliseconds", timeOut.ToString()));
                }

                if (amountMemory <= GC.GetTotalMemory(true) / 1024)
                {
                    throw new OutOfMemoryException(string.Format("Memory Exceeded"));
                }

                BigInteger[] tmpResult = new BigInteger[2];

                if (n <= 2)
                {
                    return 1;
                }

                if (arrFibonacci[n] > 0)
                {
                    return arrFibonacci[n];
                }

                if (useCache)
                {
                    var fibonacciSequenceCached = _cache.GetFibonacciCached(n);
                    if (fibonacciSequenceCached != null)
                    {
                        for (int i = n; i > 0; i--)
                        {
                            arrFibonacci[i] = fibonacciSequenceCached[i];
                        }
                        return arrFibonacci[n];
                    }
                    else
                    {
                        arrFibonacci[n] = await FibonacciAsync(n - 1).ConfigureAwait(false) + await FibonacciAsync(n - 2).ConfigureAwait(false);
                    }
                }
                else
                {
                    arrFibonacci[n] = await FibonacciAsync(n - 1).ConfigureAwait(false) + await FibonacciAsync(n - 2).ConfigureAwait(false);
                }
                return arrFibonacci[n];
            }
            catch (StackOverflowException)
            {
                throw new StackOverflowException("StackOverFlow Error");
            }
        }

        private string[] PrepareResult(BigInteger[] arrTmpResult, int firstIndex, int lastIndex)
        {
            string[] arrResult = new string[lastIndex - firstIndex + 1];
            int index = 0;
            for (int i = firstIndex - 1; i < lastIndex; i++)
            {
                arrResult[index] = string.Format("F({0}) = {1}", (i + 1).ToString(), arrTmpResult[i].ToString());
                index++;
            }
            return arrResult;
        }
    }
}