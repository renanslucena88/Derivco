using System;
using System.Threading;
using System.Threading.Tasks;

namespace DerivcoTest.FibonacciApi.Core.Domain.Extensions
{
    public static class TaskExtension
    {
        public static async Task TimeoutAfter(this Task task, int millisecondsTimeout, CancellationToken ct)
        {
            var completedTask = await Task.WhenAny(
                task, Task.Delay(millisecondsTimeout, ct));
            if (completedTask == task)
                return;
            throw new TimeoutException(string.Format("The operation exceeded {0} milliseconds", millisecondsTimeout.ToString()));
        }
    }
}