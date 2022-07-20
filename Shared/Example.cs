using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading;

namespace Shared
{
    public class Example
    {
        public static void Main()
        {
            //456
        //123 on masta
        //789
            TaskScheduler.UnobservedTaskException += TaskSchedulerUnobservedTaskException;

            new Example().MainAsync();
        }


        async Task MainAsync()
        {
            Console.WriteLine($"MainAsync started on {Environment.CurrentManagedThreadId} thread");
            try
            {
                await new InputAwaiter("Hello");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine($"MainAsync ended on {Environment.CurrentManagedThreadId} thread");
        }

        void SimpleVoidM()
        {
            throw new Exception($"I'm thown on {Environment.CurrentManagedThreadId} thread");
            Console.WriteLine($"I'm SimpleVoidM running on {Environment.CurrentManagedThreadId} thread");
        }

        async void SimpleAsyncVoidM()
        {
            throw new Exception($"I'm thown on {Environment.CurrentManagedThreadId} thread");
            Console.WriteLine($"I'm SimpleAsyncVoidM running on {Environment.CurrentManagedThreadId} thread");
        }

        async Task SimpleAsyncTaskM()
        {
            //throw new Exception($"I'm thown on {Environment.CurrentManagedThreadId} thread");
            Console.WriteLine($"I'm SimpleAsyncTaskM running on {Environment.CurrentManagedThreadId} thread");
        }

        async Task TaskWithBgOperation()
        {
            Console.WriteLine($"TaskWithBgOperation before await {Environment.CurrentManagedThreadId}");
            await  Task.Run(() =>
            {
                Console.WriteLine($"I'm in Test.Run on {Environment.CurrentManagedThreadId} thread");
            });
            Console.WriteLine($"TaskWithBgOperation after await {Environment.CurrentManagedThreadId} thread");
        }

        //Test Deadlock on UI app with .Wait for this method call.
        async Task<int> TaskDelayWithValue()
        {
            Console.WriteLine($"TaskDelayWithValue before delay {Environment.CurrentManagedThreadId}");
            await Task.Delay(10);//.ConfigureAwait(false);
            Console.WriteLine($"TaskDelayWithValue after await {Environment.CurrentManagedThreadId}");
            return 10;
        }

        Task FromTCS()
        {
            var tcs = new TaskCompletionSource<bool>();
            bool result = false;
            while (!result)
            {
                Console.WriteLine("Type 'true' to finish");
                var input = Console.ReadLine();
                var success = bool.TryParse(input, out result);
            }
            tcs.TrySetResult(true);
            return tcs.Task;
        }




        static void TaskSchedulerUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Console.WriteLine(e.Exception.Message);
        }


        #region HandleAsyncAction
        async void HandleAsyncAction()
        {
            Task t = null;
            Foo(() => t = TaskWithBgOperation());
            await t;
        }

        static void Foo(Action action) { }
        #endregion
    }
}
