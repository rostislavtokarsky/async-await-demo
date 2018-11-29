using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CustomDecompiled
{
    public class Program
    {
        static void Main(string[] args)
        {
           
        }

        public HttpClient httpClient = new HttpClient();

        //async Task SampleMethod()
        //{
        //    Console.WriteLine("We're calling asynchronous operation next");
        //    var responseExoft = await httpClient.GetAsync("https://exoft.net/");
        //    Console.WriteLine($"Exoft response: {responseExoft.StatusCode}");
        //    var responseGoogle = await httpClient.GetAsync("https://www.google.com/");
        //}

        private Task SampleMethod()
        {
            AsyncStateMachine stateMachine = new AsyncStateMachine
            {
                program = this,
                builder = AsyncTaskMethodBuilder.Create(),
                state = -1,
            };
            AsyncTaskMethodBuilder builder = stateMachine.builder;
            builder.Start(ref stateMachine);
            return stateMachine.builder.Task;
        }
    }
}
