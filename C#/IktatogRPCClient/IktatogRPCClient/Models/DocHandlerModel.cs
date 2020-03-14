using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Models.Managers.Helpers.Client;
using IktatogRPCClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IktatogRPCClient.Models
{
    public abstract class DocHandlerModel:Screen
    {
		private Ikonyv _selectedIkonyv;

		public Ikonyv SelectedIkonyv
		{
			get { return _selectedIkonyv; }
			set {
				_selectedIkonyv = value;
				NotifyOfPropertyChange(()=>SelectedIkonyv);
			}
		}
		public void ModifyIkonyv() {
			WindowManager windowManager = new WindowManager();
			Screen screen = new PopUpViewModel(new ModifyIkonyvViewModel(_selectedIkonyv));
			var result = windowManager.ShowDialog(screen, null, null);
			if ((bool)result == true)
			{
				GetIkonyvek();
			}
	

		}
		public abstract void GetIkonyvek();
	}
}
