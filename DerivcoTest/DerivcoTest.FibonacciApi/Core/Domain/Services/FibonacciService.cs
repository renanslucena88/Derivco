using DerivcoTest.FibonacciApi.Core.Domain.Extensions;
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
        private Stopwatch watch = new Stopwatch();
        private int timeOut;
        private int amountMemory;
        private bool useCache;
        private BigInteger[] arrFibonacci;

        public FibonacciService(ICacheFibonacciService cache)
        {
            _cache = cache;
        }



        public async Task<BaseResponse> Fibonacci(FibonacciRequest request)
        {
            
            watch.Restart();
            BaseResponse response = BaseResponse.Instance;
            ErrorResponse errorResponse = ErrorResponse.Instance;
            bool error = false;
            string errorMessage = "";
            try
            {
                response.TimeExeeded = false;
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
                        var token = CancellationToken.None;
                        await FibonacciAsync(request.LastIndex).TimeoutAfter(timeOut, token).ConfigureAwait(false);
                    }
                    catch (TimeoutException ex)
                    {
                        error = true;
                        errorMessage = ex.Message;
                    }
                }

                watch.Stop();

                if (error)
                {
                    errorResponse.Description = errorMessage;
                    errorResponse.TimeExeeded = error;
                    errorResponse.TimeExecuted = watch.Elapsed.TotalMilliseconds;
                    return errorResponse;
                }

                response.Result = arrFibonacci;
                if (!error)
                {
                    _cache.SetFibonacciCache(request.LastIndex, arrFibonacci);
                }
                _cache.SetFibonacciCache(request.LastIndex, arrFibonacci);

                response.TimeExeeded = error;
                response.ResultWithIndex = PrepareResult(arrFibonacci, request.FirstIndex, request.LastIndex);
                response.TimeExecuted = watch.Elapsed.TotalMilliseconds;
                return response;
            }
            catch (TaskCanceledException)
            {
                errorResponse.Description = errorMessage;
                errorResponse.TimeExeeded = error;
                errorResponse.TimeExecuted = watch.Elapsed.TotalMilliseconds;
                return errorResponse;
            }
        }

        public async Task<BigInteger> FibonacciAsync(int n)
        {
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
                    tmpResult = await Task.WhenAll(FibonacciAsync(n - 1), FibonacciAsync(n - 2)).ConfigureAwait(false);
                }
            }

            if (!useCache)
            {
                tmpResult = await Task.WhenAll(FibonacciAsync(n - 1), FibonacciAsync(n - 2)).ConfigureAwait(false);
            }
            arrFibonacci[n] = tmpResult[0] + tmpResult[1];

            return arrFibonacci[n];
        }

        public string[] PrepareResult(BigInteger[] arrTmpResult, int firstIndex, int lastIndex)
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