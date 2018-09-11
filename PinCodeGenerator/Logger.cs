using System;

namespace PinCodeGenerator
{
    public class Logger : ILogger
    {
        public void Info(string message)
        {
            Console.WriteLine(message);
        }
        
        public void Warn(string message)
        {
            Console.WriteLine(message);
        }
    }
}