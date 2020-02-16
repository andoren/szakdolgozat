using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.ViewModels
{
    class KeresesViewModel:Screen
    {
		public KeresesViewModel()
		{
			SearchList = new BindableCollection<string>() { "Iktatószám","Tárgy","Partner", "Jelleg", "Ügyintéző", "Csoport" };
			Iranyok = new BindableCollection<string>() { "Bejövő", "Kimenő","Összes" };
			SelectedSearchParameter = SearchList[0];
			SelectedIranyParameter = Iranyok[0];
		}
		private string _selectedSearchParameter;

		public string SelectedSearchParameter
		{
			get { return _selectedSearchParameter; }
			set {
				_selectedSearchParameter = value;
				NotifyOfPropertyChange(() => SelectedSearchParameter);
			}
		}
		private string _selectedIranyParameter;

		public string SelectedIranyParameter
		{
			get { return _selectedIranyParameter; }
			set { 
				_selectedIranyParameter = value;
				NotifyOfPropertyChange(() => SelectedIranyParameter);
			}
		}

		private BindableCollection<string> _searchList;

		public BindableCollection<string> SearchList
		{
			get { return _searchList; }
			set {
				_searchList = value;
				NotifyOfPropertyChange(() => SearchList);
			}
		}
		private BindableCollection<string> _iranyok;

		public BindableCollection<string> Iranyok
		{
			get { return _iranyok; }
			set {
				_iranyok = value;
				NotifyOfPropertyChange(() => Iranyok);
			}
		}

	}
}
