using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using Xunit;

namespace FibonacciApi.Test.Extentions
{
    public static class Extensions
    {
        public static async Task CheckBadRequest(this BadRequestObjectResult badRequest, string message)
        {
            if (badRequest.Value.ToString() == message)
            {
                await Task.Run(()=>Assert.True(true, $"Message expected '{badRequest.Value}'")).ConfigureAwait(false);
            }
            else
            {
                await Task.Run(() => Assert.False(true, $"Message expected '{badRequest.Value}'  Message received: {message}")).ConfigureAwait(false);
            }
        }
    }
}