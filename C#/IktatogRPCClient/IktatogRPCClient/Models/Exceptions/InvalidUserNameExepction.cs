using System;
using System.Runtime.Serialization;

namespace IktatogRPCClient.ViewModels
{
    [Serializable]
    internal class InvalidUserNameExepction : Exception
    {
        public InvalidUserNameExepction()
        {
        }

        public InvalidUserNameExepction(string message) : base(message)
        {
        }

        public InvalidUserNameExepction(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidUserNameExepction(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}