using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Shared
{
    public class Example
    {
        public static void Main()
        {
            new Example().MainAsync();
        }

        async Task MainAsync()
        {
            Console.WriteLine($"MainAsync started on {Environment.CurrentManagedThreadId} thread");
            try
            {
                var x = TaskDelayWithValue().Result;
                Console.WriteLine($"Value is {x}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine($"MainAsync ended on {Environment.CurrentManagedThreadId} thread");
        }

        void SimpleVoidM()
        {
            //throw new Exception($"I'm thown on {Environment.CurrentManagedThreadId} thread");
            Console.WriteLine($"I'm SimpleVoidM running on {Environment.CurrentManagedThreadId} thread");
        }

        async void SimpleAsyncVoidM()
        {
            //throw new Exception($"I'm thown on {Environment.CurrentManagedThreadId} thread");
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

        async Task<int> TaskDelayWithValue()
        {
            Console.WriteLine($"TaskDelayWithValue before delay {Environment.CurrentManagedThreadId}");
            await Task.Delay(10);//.ConfigureAwait(false);
            Console.WriteLine($"TaskDelayWithValue after await {Environment.CurrentManagedThreadId}");
            return 10;
        }
    }
}
