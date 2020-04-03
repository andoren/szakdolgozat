using System;
using System.Runtime.Serialization;

namespace IktatogRPCClient.Managers
{
    [Serializable]
    internal class InvalidSceneExeption : Exception
    {
        public InvalidSceneExeption()
        {
        }

        public InvalidSceneExeption(string message) : base(message)
        {
        }

        public InvalidSceneExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidSceneExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}