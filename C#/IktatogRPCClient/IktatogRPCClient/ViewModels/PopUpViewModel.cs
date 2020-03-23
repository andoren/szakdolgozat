using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Models;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IktatogRPCClient.ViewModels
{
   
    public class PopUpViewModel:Conductor<Screen>
    {
        public PopUpViewModel(PopUpChildModel ScreenToShow)
        {
            this.ScreenToShow = ScreenToShow;
            ScreenToShow.SetParent(this);
            ActivateItem(ScreenToShow);
           
        }
        private Screen _screenToShow;

        public Screen ScreenToShow
        {
            get { return _screenToShow; }
            set { _screenToShow = value; }
        }
        public void CloseScreen(Screen screen, bool? result) {
            Log.Debug("{Class} View bezárása.", GetType());
            ChildResult = result;
            DeactivateItem(screen,(bool)result);
            TryClose(ChildResult);
        }

 
        public bool? ChildResult { get; set; }


       
    }
}
