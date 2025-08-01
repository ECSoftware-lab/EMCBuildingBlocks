using System.Diagnostics;

namespace EMC.BuildingBlocks.Static
{
    public static class PerformanceHelper
    {
     
      
        public static async Task<(T result, long elapsedMilliseconds)> MeasureExecutionTimeAsync<T>(Func<Task<T>> action)
        {
            var stopwatch = Stopwatch.StartNew();
            var result = await action();
            stopwatch.Stop();
            return (result, stopwatch.ElapsedMilliseconds);
        }

        public static async Task<long> MeasureExecutionTimeAsync(Func<Task> action)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            await action();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
    }
}
