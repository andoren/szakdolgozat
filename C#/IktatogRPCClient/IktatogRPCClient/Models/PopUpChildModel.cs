using Caliburn.Micro;
using IktatogRPCClient.Models.Managers;
using IktatogRPCClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.Models
{
    public abstract class PopUpChildModel:Conductor<Screen>
    {
        public PopUpViewModel MyParent { get;  set; }
        public void SetParent(PopUpViewModel popUpViewModel)
        {
            this.MyParent = popUpViewModel;
        }
        protected EventAggregatorSingleton eventAggregator = EventAggregatorSingleton.GetInstance();
    }
}
