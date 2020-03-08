using Caliburn.Micro;
using IktatogRPCClient.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.ViewModels
{
    class TorzsViewModel:Conductor<Screen>.Collection.AllActive
    {
        
        public TorzsViewModel()
        {
            EventAggregatorSingleton.GetInstance().Subscribe(this);
        }

    }
}
