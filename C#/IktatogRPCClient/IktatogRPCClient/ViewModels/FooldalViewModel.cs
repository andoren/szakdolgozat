using Caliburn.Micro;
using IktatogRPCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.ViewModels
{
    class FooldalViewModel : Screen
    {
        public FooldalViewModel()
        {
            Ige = new Ige();
        }
        private Ige _ige;

        public Ige Ige
        {
            get
            {
                return _ige;
            }
            set
            {
                _ige = value;
            }
        }
        public string IgeTitleWithDate
        {
            get
            {
                return Ige.IgeTitleWithDate;
            }
        }
        public string NapiIge
        {
            get
            {
                return Ige.NapiIge;
            }
        }

    }
}
