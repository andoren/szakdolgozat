using Caliburn.Micro;
using IktatogRPCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.ViewModels
{
    class FooldalViewModel:Screen
    {
        public FooldalViewModel()
        {
            NapiIge = new Ige();
        }
        private Ige _napiIge;

        public Ige NapiIge
        {
            get { 
                return _napiIge;
            }
            set { 
                _napiIge = value;
            }
        }
        public string IgeTitleWithDate { get {
                return NapiIge.IgeTitleWithDate;    
            }
        }

    }
}
