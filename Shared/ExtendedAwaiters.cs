using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared
{
    public static class ExtendedAwaiters
    {
        //await TimeSpan.FromMinutes(15);
        public static TaskAwaiter GetAwaiter(this TimeSpan timeSpan)
        {
            return Task.Delay(timeSpan).GetAwaiter();
        }

        //await 15000; // in milliseconds
        public static TaskAwaiter GetAwaiter(this int millisecondsDue)
        {
            return TimeSpan.FromMilliseconds(millisecondsDue).GetAwaiter();
        }

        //await DateTimeOffset.UtcNow.AddMinutes(1);
        public static TaskAwaiter GetAwaiter(this DateTimeOffset dateTimeOffset)
        {
            return (dateTimeOffset.Subtract(DateTimeOffset.UtcNow)).GetAwaiter();
        }

        //await from url in urls select DownloadAsync(url);
        public static TaskAwaiter GetAwaiter(this IEnumerable<Task> tasks)
        {
            return Task.WhenAll(tasks).GetAwaiter();
        }

        //await Process.Start(“Foo.exe”);
        public static TaskAwaiter<int> GetAwaiter(this Process process)
        {
            var tcs = new TaskCompletionSource<int>();
            process.EnableRaisingEvents = true;
            process.Exited += (s, e) => tcs.TrySetResult(process.ExitCode);
            if (process.HasExited) tcs.TrySetResult(process.ExitCode);
            return tcs.Task.GetAwaiter();
        }

        //await cancellationToken;
        public static TaskAwaiter GetAwaiter(this CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();
            Task t = tcs.Task;
            if (cancellationToken.IsCancellationRequested) tcs.SetResult(true);
            else cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).SetResult(true), tcs);
            return t.GetAwaiter();
        }
    }
}
