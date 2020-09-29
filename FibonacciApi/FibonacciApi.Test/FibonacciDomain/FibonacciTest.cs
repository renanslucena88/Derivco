using FibonacciApi.Controllers;
using FibonacciApi.Core.Domain.Interfaces;
using FibonacciApi.Core.Domain.Services;
using FibonacciApi.Core.Infrasctructure;
using FibonacciApi.Test.Builder;
using FibonacciApi.Test.Extentions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace FibonacciApi.Test.FibonacciDomain
{
    public class FibonacciTest : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private static readonly ICacheProvider _cacheProvider = new CacheProvider(GetMemoryCache());
        private static readonly ICacheFibonacciService _cache = new CacheFibonacciService(_cacheProvider);
        private static readonly IFibonacciService _service = new FibonacciService(_cache);

        public static IMemoryCache GetMemoryCache()
        {
            var memoryCacheOptions = new MemoryCacheOptions();
            return new MemoryCache(memoryCacheOptions);
        }

        public FibonacciTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Start the tests");
        }

        public void Dispose()
        {
            _output.WriteLine("Dispose Executed");
        }

        #region Check Negative numbers (Argument Exception)

        [Theory(DisplayName = "Last Index must not be negative")]
        [InlineData(-3)]
        [InlineData(-4)]
        public async Task LastIndexmustnotbenegativeReturnBadRequest(int lastIndex)
        {
            var tmp = ControllerBuilder.New().ChangeLastIndex(lastIndex);
            await Assert.IsType<BadRequestObjectResult>(await tmp.BuildToGetException().ConfigureAwait(false)).CheckBadRequest("Last Index cannot be less than 0").ConfigureAwait(false);
        }

        [Theory(DisplayName = "First Index must not be negative")]
        [InlineData(-3)]
        [InlineData(-4)]
        public async Task FirstIndexmustnotbenegativeReturnBadRequest(int firsIndex)
        {
            var tmp = ControllerBuilder.New().ChangeFirstIndex(firsIndex);
            await Assert.IsType<BadRequestObjectResult>(await tmp.BuildToGetException().ConfigureAwait(false)).CheckBadRequest("First Index cannot be less than 0").ConfigureAwait(false);
        }

        [Theory(DisplayName = "Time Of Execution cannot be less or equals than 0")]
        [InlineData(0)]
        [InlineData(-4)]
        public async Task TimeOfExecutioncannotbelessorequalsthan0(int timeout)
        {
            var tmp = ControllerBuilder.New().ChangeTimeOfExecution(timeout);
            await Assert.IsType<BadRequestObjectResult>(await tmp.BuildToGetException().ConfigureAwait(false)).CheckBadRequest("Time Of Execution cannot be less or equals than 0").ConfigureAwait(false);
        }

        [Theory(DisplayName = "Amount Memory cannot be less or equals than 0")]
        [InlineData(0)]
        [InlineData(-4)]
        public async Task AmountMemoryCannotbelessorequalsthan0(int amountMemory)
        {
            var tmp = ControllerBuilder.New().ChangeAmountMemory(amountMemory);
            await Assert.IsType<BadRequestObjectResult>(await tmp.BuildToGetException().ConfigureAwait(false)).CheckBadRequest("Amount of Memory cannot be less or equals than 0").ConfigureAwait(false);
        }

        #endregion

        #region Check Subsequence

        [Fact(DisplayName = "Check Service.GetFibonacci Subsequence: Return OK")]
        public async Task CheckServiceGetFibonacciSubsequenceReturnOK()
        {
            FibonacciServiceRequestBuilder builder = new FibonacciServiceRequestBuilder();
            var request = builder.GetRequestInstance();
            var result = await _service.Fibonacci(request).ConfigureAwait(false);
            var correctResponse = FibonacciServiceResponseBuilder.BaseResponseCorrect();
            correctResponse.ResultWithIndex.Should().BeEquivalentTo(result.ResultWithIndex);
        }

        #endregion

        #region Check Json ErrorsMessages

        [Theory(DisplayName = "Check TimeOutMessage Service.GetFibonacci")]
        [InlineData(10)]
        public async Task CheckServiceGetFibonacciSubsequenceTimeOut(int timeout)
        {
            FibonacciServiceRequestBuilder builder = new FibonacciServiceRequestBuilder();
            var correctResponse = FibonacciServiceResponseBuilder.ErrorTimeOut(timeout);
            var tmp = builder.New().ChangeTimeOfExecution(timeout);
            var result = await tmp.GetFibonacciErrorResponse().ConfigureAwait(false);
            correctResponse.Description.Should().BeEquivalentTo(result.Description);
        }

        [Fact(DisplayName = "Check MemoryExceeded Service.GetFibonacci")]
        public async Task CheckServiceGetFibonacciSubsequenceMemoryExceeded()
        {
            FibonacciServiceRequestBuilder builder = new FibonacciServiceRequestBuilder();
            var tmp = builder.New().ChangeAmountMemory(20);
            var result = await tmp.GetFibonacciErrorResponse().ConfigureAwait(false);
            result.MemoryExceeded.Should().BeTrue();
        }
#endregion
    }
}