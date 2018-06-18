using System;

namespace IoCContainer
{
    public class Logger : ILogger
    {
        public string Write(string message)
        {
            return DateTime.Now.ToString() + ": " + message;
        }
    }
}