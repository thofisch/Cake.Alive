using System;

namespace Cake.Alive
{
    public class NotAliveException : Exception
    {
        public NotAliveException(string message) : base(message)
        {
        }

        public NotAliveException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}