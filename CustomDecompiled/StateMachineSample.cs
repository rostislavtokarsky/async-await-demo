using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CustomDecompiled
{
    struct AsyncStateMachine : IAsyncStateMachine
    {
        public int state;
        public AsyncTaskMethodBuilder builder;
        public Program program;
        private TaskAwaiter<HttpResponseMessage> awaiter;

        public void MoveNext()
        {
            try
            {
                //if (state == 0)
                //{
                //    state = -1;
                //}
                if (state == 1)
                {
                    //state = -1;
                    goto TR_0005;
                }
                else
                {
                    Console.WriteLine("We're calling asynchronous operation next");
                    awaiter = program.httpClient.GetAsync("https://exoft.net/").GetAwaiter();
                    if (!awaiter.IsCompleted)
                    {
                        state = 0;
                        builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
                        return;
                    }
                }
                HttpResponseMessage result = awaiter.GetResult();
                Console.WriteLine($"Exoft response: {result.StatusCode}");
                awaiter = program.httpClient.GetAsync("https://www.google.com/").GetAwaiter();
                if (awaiter.IsCompleted)
                {
                    goto TR_0005;
                }
                else
                {
                    state = 1;
                    builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
                }
                return;
                TR_0005:
                awaiter.GetResult();
                state = -2;
                builder.SetResult();
            }
            catch (Exception exception)
            {
                state = -2;
                builder.SetException(exception);
            }
        }

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            this.builder.SetStateMachine(stateMachine);
        }
    }
}
