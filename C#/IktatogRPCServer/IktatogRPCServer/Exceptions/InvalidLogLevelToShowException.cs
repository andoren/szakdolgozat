using System;
using System.Runtime.Serialization;

namespace IktatogRPCServer
{
    public class InvalidLogLevelToShowException : Exception
    {
        public InvalidLogLevelToShowException()
        {
        }

        public InvalidLogLevelToShowException(string message) : base(message)
        {
        }

        public InvalidLogLevelToShowException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidLogLevelToShowException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}