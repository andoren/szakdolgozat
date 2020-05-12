using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.Models
{
   public class Irany
    {
        public Irany(string Name, int Way)
        {
            this.Way = Way;
            this.Name = Name;
        }

        public int Way { get; }
        public string Name { get; }
    }
}
