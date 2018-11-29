using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Shared
{
    public class InputAwaiter : INotifyCompletion
    {
        private readonly string pattern;

        public InputAwaiter(string pattern)
        {
            this.pattern = pattern;
        }

        public void OnCompleted(Action continuation)
        {
            continuation();
        }

        public bool IsCompleted { get; private set; }

        public void GetResult()
        {
            string input = null;
            while (input != pattern)
            {
                Console.WriteLine($"Type '{pattern}' to finish");
                input = Console.ReadLine();
            }

            IsCompleted = true;
        }

        public InputAwaiter GetAwaiter()
        {
            return this;
        }
    }
}
