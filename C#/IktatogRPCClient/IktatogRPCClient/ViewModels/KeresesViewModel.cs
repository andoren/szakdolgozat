using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Models;
using IktatogRPCClient.Models.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace IktatogRPCClient.ViewModels
{
	class KeresesViewModel : Screen
	{
		public KeresesViewModel()
		{
			SearchList = new BindableCollection<string>() { "Iktatószám", "Tárgy", "Partner", "Jelleg", "Ügyintéző", "Csoport", "Hivatkozási szám" };
			SelectedSearchParameter = SearchList[0];
			Iranyok = new BindableCollection<Irany>() { new Irany("Bejövő",0),new Irany("Kimenő",1),new Irany("Összes",2) };
			AvailabelYears = new BindableCollection<int>() { 2020, 2019 };
			ItemsPerPage = new BindableCollection<int>() { 10, 20, 50, 100, 200, 500 };
			SelectedItemsPerPage = ItemsPerPage[2];
		}
		private string _selectedSearchParameter;
		private BindableCollection<string> _searchList;
		private ServerHelperSingleton serverHelper = ServerHelperSingleton.GetInstance();
		private Irany _selectedIranyParameter;
		private bool _loaderIsVisible = false;
		private int _currentPage = 1;
		private BindableCollection<int> _availabelYears;
		private int _selectedYear;
		private BindableCollection<Irany> _iranyok;
		private BindableCollection<int> _itemsPerPage;
		private BindableCollection<Ikonyv> _allikonyv = new BindableCollection<Ikonyv>();
		private int _selectedItemsPerPage;
		private ObservableCollection<Button> _pagingButtons = new ObservableCollection<Button>() { };
		private BindableCollection<Ikonyv> _searchedIkonyvek = new BindableCollection<Ikonyv>();

		public BindableCollection<Ikonyv> SearchedIkonyvek
		{
			get { return _searchedIkonyvek; }
			set { _searchedIkonyvek = value; }
		}

		private BindableCollection<Ikonyv> _shownIktatas = new BindableCollection<Ikonyv>();
		private string _searchText;

		public string SearchText
		{
			get { return _searchText; }
			set { _searchText = value;
				NotifyOfPropertyChange(()=> SearchText);
				CurrentPage = 0;
			}
		}

		public string SelectedSearchParameter
		{
			get { return _selectedSearchParameter; }
			set {
				_selectedSearchParameter = value;
				NotifyOfPropertyChange(() => SelectedSearchParameter);

			}
		}
		public bool LoaderIsVisible
		{
			get { return _loaderIsVisible; }
			set { _loaderIsVisible = value;
				NotifyOfPropertyChange(() => LoaderIsVisible);
			}
		}
		public BindableCollection<Irany> Iranyok
		{
			get { return _iranyok; }
			set
			{
				_iranyok = value;
				NotifyOfPropertyChange(() => Iranyok);
			}
		}
		public Irany SelectedIranyParameter
		{
			get { return _selectedIranyParameter; }
			set {
				_selectedIranyParameter = value;
				NotifyOfPropertyChange(() => SelectedIranyParameter);
				var waiting = GetIkonyvek();
			}
		}
		public int CurrentPage
		{
			get { return _currentPage; }
			set {
				_currentPage = value;
				NotifyOfPropertyChange(() => CurrentPage);
				NotifyOfPropertyChange(() => CanToLastPage);
				NotifyOfPropertyChange(() => CanToNextPage);
				NotifyOfPropertyChange(() => CanToPrevPage);
				NotifyOfPropertyChange(() => CanToFirstPage);
				var s = SetVisibleIktatas();
			}
		}
		public bool CanToLastPage
		{
			get
			{
				return CurrentPage != MaxPage;
			}
		}
		public bool CanToNextPage
		{
			get
			{
				return CurrentPage != MaxPage;
			}
		}
		public bool CanToPrevPage
		{
			get
			{
				return CurrentPage != 0;
			}
		}
		public bool CanToFirstPage
		{

			get
			{
				return CurrentPage != 0;
			}
		}
		public string MaxItemNumber {
			get {
				return $"Találatok száma: {ShownIktatas.Count} db iktatás";
			}
		}
		public int MaxPage
		{
			get {
				int count = 0;
				if (string.IsNullOrWhiteSpace(SearchText))
				{
					if (AllIkonyv.Count % SelectedItemsPerPage != 0)
					{
						count++;
					}
					count += AllIkonyv.Count / SelectedItemsPerPage;
				}
				else {
					if (ShownIktatas.Count % SelectedItemsPerPage != 0)
					{
						count++;
					}
					count += ShownIktatas.Count / SelectedItemsPerPage;
				}
				
				return count;
			}

		}
		public BindableCollection<int> AvailabelYears
		{
			get { return _availabelYears; }
			set { _availabelYears = value;
				NotifyOfPropertyChange(() => AvailabelYears);
			}
		}
		public int SelectedYear
		{
			get { return _selectedYear; }
			set { _selectedYear = value;
				NotifyOfPropertyChange(() => SelectedYear);
				var waiting = GetIkonyvek();
			}
		}
		public BindableCollection<string> SearchList
		{
			get { return _searchList; }
			set {
				_searchList = value;
				NotifyOfPropertyChange(() => SearchList);
			}
		}

		public BindableCollection<Ikonyv> AllIkonyv
		{
			get { return _allikonyv; }
			set {
				_allikonyv = value;
				NotifyOfPropertyChange(() => AllIkonyv);
			}
		}
		public BindableCollection<int> ItemsPerPage
		{
			get { return _itemsPerPage; }
			set { _itemsPerPage = value;


			}
		}
		public int SelectedItemsPerPage
		{
			get { 
				return _selectedItemsPerPage;
			}
			set { 
				_selectedItemsPerPage = value;
				NotifyOfPropertyChange(() => SelectedItemsPerPage);
				CurrentPage = 0;
			}
		}
		public ObservableCollection<Button> PagingButtons
		{
			get { return _pagingButtons; }
			set { _pagingButtons = value;
				NotifyOfPropertyChange(() => PagingButtons);
			}
		}
		public BindableCollection<Ikonyv> ShownIktatas
		{
			get { return _shownIktatas; }
			set {
				_shownIktatas = value;
				NotifyOfPropertyChange(() => ShownIktatas);
				NotifyOfPropertyChange(MaxItemNumber);
			}
		}

		public async Task GetIkonyvek() {
			if (SelectedIranyParameter == null || SelectedYear == default) return;
			else {
				LoaderIsVisible = true;
				AllIkonyv = new BindableCollection<Ikonyv>();
				SearchIkonyvData searchData = new SearchIkonyvData()
				{
					Irany = SelectedIranyParameter.Way,
					Year = SelectedYear
				};

				AllIkonyv = await serverHelper.GetIkonyvekAsync(searchData);
				await SetVisibleIktatas();
				LoaderIsVisible = false;
			}
		}

		private async Task SetVisibleIktatas() {
			
			if (AllIkonyv.Count == 0) return;
			await SetSearchData();
			ShownIktatas.Clear();
			if (CurrentPage + SelectedItemsPerPage > SearchedIkonyvek.Count)
				{
					for (int i = CurrentPage; i < SearchedIkonyvek.Count; i++)
					{
						ShownIktatas.Add(SearchedIkonyvek[i]);
						NotifyOfPropertyChange(() => ShownIktatas);
					}
				}
			else {
				for (int i = CurrentPage * SelectedItemsPerPage; i < CurrentPage * SelectedItemsPerPage + SelectedItemsPerPage; i++)
				{
					ShownIktatas.Add(SearchedIkonyvek[i]);
					NotifyOfPropertyChange(() => ShownIktatas);
				}
				}
			int buttonCount = 0;
			if (SearchedIkonyvek.Count % SelectedItemsPerPage != 0)
			{
				buttonCount++;
			}
			buttonCount += SearchedIkonyvek.Count / SelectedItemsPerPage;
			SetButtons(buttonCount);
		}

		public async Task SetSearchData() {
			await Task.Run(() =>
			{
				ICollectionView cv = CollectionViewSource.GetDefaultView(AllIkonyv);
				SearchedIkonyvek.Clear();
				if (!string.IsNullOrEmpty(SearchText))
				{
					cv.Filter = o =>
					{
						Ikonyv p = o as Ikonyv;
						if (SelectedSearchParameter == "Tárgy")
							return (p.Targy.ToUpper().Contains(SearchText.ToUpper()));
						else if (SelectedSearchParameter == "Partner") 
							return (p.Partner.Name.ToUpper().Contains(SearchText.ToUpper()));
						else if (SelectedSearchParameter == "Iktatószám") 
							return (p.Iktatoszam.ToUpper().Contains(SearchText.ToUpper()));
						else if (SelectedSearchParameter == "Hivatkozási szám") 
							return (p.Hivszam.ToUpper().Contains(SearchText.ToUpper()));
						else if (SelectedSearchParameter == "Jelleg") 
							return (p.Jelleg.Name.ToUpper().Contains(SearchText.ToUpper()));
						else if (SelectedSearchParameter == "Csoport") 
							return (p.Csoport.Name.ToUpper().Contains(SearchText.ToUpper()));
						else return (p.Ugyintezo.Name.ToUpper().Contains(SearchText.ToUpper()));

					};
					
				}
				else
				{
					cv.Filter = null;
				}
				SearchedIkonyvek =  new BindableCollection<Ikonyv>(cv.Cast<Ikonyv>().ToList());
			});
		}
        #region GombokGenerálása és gomb muveletei
        private void SetButtons(int buttonCount)
		{
			PagingButtons.Clear();
			
			if (buttonCount == 0) return;
			int delta = 1;
			int left = CurrentPage - delta;
			int right = CurrentPage + delta + 1;
			BindableCollection<Button> Buttons = new BindableCollection<Button>();
			BindableCollection<Button> ButtonsWithDots = new BindableCollection<Button>();
			Buttons.Add(GenerateButton("1"));
			if (buttonCount == 1)
			{
				PagingButtons = Buttons;
				return;
			}
			for (int i = left; i <= right; i++)
			{
				if (i >= left && i <= right && i > 1 && i < buttonCount)
				{
					Buttons.Add(GenerateButton(i.ToString()));
				}
			}
			Buttons.Add(GenerateButton(buttonCount.ToString()));

			if (Buttons.Count > 2)
			{
				if (CurrentPage > 3) Buttons.Insert(1, GenerateButton("..."));
				if (CurrentPage < buttonCount - 2) Buttons.Insert(Buttons.Count - 1, GenerateButton("..."));
			}
			PagingButtons = Buttons;
			NotifyOfPropertyChange(() => PagingButtons);
		}
		private Button GenerateButton(string pageNumber)
		{
			Style btnStlye;
			btnStlye = Application.Current.Resources["SearchViewNumberButtonStyle"] as Style;
			Button btn = new Button();
			btn.Style = btnStlye;
			btn.Width = 35;
			btn.Height = 35;
			btn.Content = pageNumber;
			btn.Click += (object sender, RoutedEventArgs e) =>
			{
				if ((sender as Button).Content.ToString() == "...") return;
				CurrentPage = int.Parse((sender as Button).Content.ToString()) - 1;

			};
			return btn;
		}
       
        public void ToFirstPage() { 
				CurrentPage = 1;
		}

		public void ToPrevPage() {
				CurrentPage--;
		}
	
		public void ToNextPage() {

				CurrentPage++;
		
		}

		public void ToLastPage() {	
				CurrentPage = MaxPage;
		}
		#endregion
	}

}
