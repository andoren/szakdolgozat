using Caliburn.Micro;
using IktatogRPCClient.Models;
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
            //var c = CloseMySelf();
        }
        private Screen _screenToShow;

        public Screen ScreenToShow
        {
            get { return _screenToShow; }
            set { _screenToShow = value; }
        }
        public void CloseScreen(Screen screen, bool result) {
            ChildResult = result;
            DeactivateItem(screen,result);
            TryClose(ChildResult);
        }
        public bool ChildResult { get; set; }
        //public async Task CloseMySelf() { 
        //    await Task.Run(()=> {
        //        while (ActiveItem != null) {
        //            Thread.Sleep(10);
        //        }
        //        TryClose(ChildResult);
        //    });
        //}

       
    }
}
