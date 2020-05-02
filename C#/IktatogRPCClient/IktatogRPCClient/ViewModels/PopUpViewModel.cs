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
using System.Windows;
using System.Windows.Media;

namespace IktatogRPCClient.ViewModels
{
   
    public class PopUpViewModel:Conductor<Screen>
    {
        public PopUpViewModel(PopUpChildModel ScreenToShow)
        {
            this.ScreenToShow = ScreenToShow;
            ScreenToShow.SetParent(this);
            ActivateItem(ScreenToShow);
            Application.Current.Resources["GridVisibility"] = Visibility.Visible;
        }
        private Screen _screenToShow;

        public Screen ScreenToShow
        {
            get { return _screenToShow; }
            set { _screenToShow = value; }
        }
        public void CloseScreen(Screen screen, bool? result) {
            Log.Debug("{Class} View bezárása.", GetType());
            DeactivateItem(screen,(bool)result);
            Application.Current.Resources["GridVisibility"] = Visibility.Hidden;
            TryClose();
        }
    }
}
