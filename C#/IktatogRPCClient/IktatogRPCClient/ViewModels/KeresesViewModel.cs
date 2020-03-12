using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Models.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace IktatogRPCClient.ViewModels
{
    class KeresesViewModel:Screen
    {
		public KeresesViewModel()
		{
			SearchList = new BindableCollection<string>() { "Iktatószám","Tárgy","Partner", "Jelleg", "Ügyintéző", "Csoport" };
			SelectedSearchParameter = SearchList[0];
			Iranyok = new BindableCollection<string>() { "Bejövő", "Kimenő","Összes" };
			AvailabelYears = new BindableCollection<int>() { 2020,2019 };
			ItemsPerPage = new BindableCollection<int>() { 10, 20, 50, 100, 200, 500 };
			SelectedItemsPerPage = ItemsPerPage[2];
			
		}
		private string _selectedSearchParameter;
		private ServerHelperSingleton serverHelper = ServerHelperSingleton.GetInstance();
		public string SelectedSearchParameter
		{
			get { return _selectedSearchParameter; }
			set {
				_selectedSearchParameter = value;
				NotifyOfPropertyChange(() => SelectedSearchParameter);
				
			}
		}
		private string _selectedIranyParameter;
		private bool _loaderIsVisible=false;

		public bool LoaderIsVisible
		{
			get { return _loaderIsVisible; }
			set { _loaderIsVisible= value;
				NotifyOfPropertyChange(()=>LoaderIsVisible);
			}
		}
		
		public string SelectedIranyParameter
		{
			get { return _selectedIranyParameter; }
			set { 
				_selectedIranyParameter = value;
				NotifyOfPropertyChange(() => SelectedIranyParameter);
				var waiting = GetIkonyvek();
			}
		}


		private int _currentPage=1;

		public int CurrentPage
		{
			get { return _currentPage; }
			set { _currentPage = value; }
		}


		public string MaxItemNumber {
			get {
				return $"Találatok száma: {FoundIkonyvek.Count} db iktatás";
			}
		}

		public int MaxPage
		{
			get { 
				int count = FoundIkonyvek.Count / SelectedItemsPerPage;
				if (FoundIkonyvek.Count % SelectedItemsPerPage != 0) count++;		
				return count;
			}

		}
		private BindableCollection<int> _availabelYears;

		public BindableCollection<int> AvailabelYears
		{
			get { return _availabelYears; }
			set { _availabelYears = value;
				NotifyOfPropertyChange(()=>AvailabelYears);
			}
		}
		private int _selectedYear;

		public int SelectedYear
		{
			get { return _selectedYear; }
			set { _selectedYear = value;
				NotifyOfPropertyChange(()=>SelectedYear);
				var waiting = GetIkonyvek();
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

		private BindableCollection<Ikonyv> _foundIkonyvek = new BindableCollection<Ikonyv>();	

		public BindableCollection<Ikonyv> FoundIkonyvek
		{
			get { return _foundIkonyvek; }
			set { _foundIkonyvek = value;
				NotifyOfPropertyChange(()=>FoundIkonyvek);
			}
		}
		private BindableCollection<int> _itemsPerPage;

		public BindableCollection<int> ItemsPerPage
		{
			get { return _itemsPerPage; }
			set { _itemsPerPage = value;

				
			}
		}
		private int _selectedItemsPerPage;

		public int SelectedItemsPerPage
		{
			get { return _selectedItemsPerPage; }
			set { _selectedItemsPerPage = value;
				NotifyOfPropertyChange(()=>SelectedItemsPerPage);
			}
		}
		private ObservableCollection<Button> _pagingButtons = new ObservableCollection<Button>() { };

		public ObservableCollection<Button> PagingButtons
		{
			get { return _pagingButtons; }
			set { _pagingButtons = value;
				NotifyOfPropertyChange(()=>PagingButtons);
			}
		}

		public async Task GetIkonyvek() {
			if (SelectedIranyParameter == null || SelectedYear == default) return;
			else {
				LoaderIsVisible = true;
				FoundIkonyvek.Clear();
				int choosenIrany;
				if (SelectedIranyParameter == "Bejövő") choosenIrany = 0;
				else if (SelectedIranyParameter == "Kimenyő") choosenIrany = 1;
				else choosenIrany = 2;
				SearchIkonyvData searchData = new SearchIkonyvData()
				{
					 Irany = choosenIrany,
					 Year = SelectedYear,
					 From = CurrentPage,
					 To = CurrentPage* SelectedItemsPerPage
				};

				await foreach (var ikonyv in serverHelper.GetIkonyvekAsync(searchData))
				{
					FoundIkonyvek.Add(ikonyv);
					NotifyOfPropertyChange(()=>FoundIkonyvek);
				}
				LoaderIsVisible = false;
				NotifyOfPropertyChange(() => MaxItemNumber);
				SetButtons();
			}
		}
		/*
		            <Button Width="35" Height="35"  BorderBrush="Black" Margin="0" Padding="0"  Background="#FFF3E9E9" Foreground="black" FontSize="22">1</Button>
                    <Button Width="35" Height="35"  BorderBrush="Black" Margin="0" Padding="0"  Background="White" Foreground="black" FontSize="22">2</Button>
                    <Button Width="35" Height="35"  BorderBrush="Black" Margin="0" Padding="0"  Background="White" Foreground="black" FontSize="22">3</Button>
                    <Button Width="35" Height="35"  BorderBrush="Black" Margin="0" Padding="0"  Background="White" Foreground="black" FontSize="22">...</Button>
                    <Button Width="35" Height="35"  BorderBrush="Black" Margin="0" Padding="0"  Background="White" Foreground="black" FontSize="22">10</Button>
		 */
		public void Proba() {
			MessageBox.Show("Gomb megnyomva");
		}
		private void SetButtons() {
			PagingButtons.Clear();
			CurrentPage = 1;
			Style btnStlye;
			btnStlye = Application.Current.Resources["SearchViewNumberButtonStyle"] as Style;
			for (int i = 0; i < 4 && i+CurrentPage < MaxPage+1; i++)
			{
				
				Button btn = new Button();
				btn.Style = btnStlye;
				btn.Content = CurrentPage + i;
				btn.Click += (object sender, RoutedEventArgs e ) =>
				{ 
						MessageBox.Show($"Ez egy hozzáadott gomb {(sender as Button).Content}"); 
				};
		
				PagingButtons.Add(btn);
				if (i == 2 && i + 1 + CurrentPage < MaxPage + 1)
				{
					Button btn2 = new Button();
					btn2.Style = btnStlye;
					btn2.Content = "...";
					btn2.Click += (object sender, RoutedEventArgs e) =>
					{
						MessageBox.Show($"Pöttyözött {(sender as Button).Content}");
					};
					PagingButtons.Add(btn2);
				}
			}
	
			NotifyOfPropertyChange(()=>PagingButtons);
		}
	}
}
