using Caliburn.Micro;
using IktatogRPCClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.Models
{
    public abstract class PopUpChildModel:Screen
    {
        public PopUpViewModel MyParent { get;  set; }
        public void SetParent(PopUpViewModel popUpViewModel)
        {
            this.MyParent = popUpViewModel;
        }
    }
}
