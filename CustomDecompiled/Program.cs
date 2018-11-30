using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CustomDecompiled
{
    public class Program
    {
        static void Main(string[] args)
        {
            MainAsync();
            Console.ReadLine();
            Thread.Sleep(120000);
        }

        static async Task MainAsync()
        {
            await new Program().SampleMethod();
        }

        public HttpClient httpClient = new HttpClient();

        async Task SampleMethod()
        {
            Console.WriteLine("We're calling asynchronous operation next");
            var responseExoft = await httpClient.GetAsync("https://exoft.net/");
            Console.WriteLine($"Exoft response: {responseExoft.StatusCode}");
            var responseGoogle = await httpClient.GetAsync("https://www.google.com/");
            Console.WriteLine($"Google response: {responseGoogle.StatusCode}");
        }

        //private Task SampleMethod()
        //{
        //    AsyncStateMachine stateMachine = new AsyncStateMachine
        //    {
        //        program = this,
        //        builder = AsyncTaskMethodBuilder.Create(),
        //        state = -1,
        //    };
        //    AsyncTaskMethodBuilder builder = stateMachine.builder;
        //    builder.Start(ref stateMachine);
        //    return stateMachine.builder.Task;
        //}
    }
}
