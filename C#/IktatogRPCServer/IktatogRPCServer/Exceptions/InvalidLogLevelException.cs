using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Exceptions
{
   public class InvalidLogLevelException : Exception
    {
        public InvalidLogLevelException(string message) : base(message)
        {
        }
    }
}
