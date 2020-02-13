using System;
using System.Runtime.Serialization;

namespace IktatogRPCClient.Models.Managers
{
    [Serializable]
    internal class InvalidCallOperation : Exception
    {
        public InvalidCallOperation()
        {
        }

        public InvalidCallOperation(string message) : base(message)
        {
        }

        public InvalidCallOperation(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidCallOperation(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}