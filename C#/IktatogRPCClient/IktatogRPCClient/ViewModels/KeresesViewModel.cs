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
using System.Windows.Media;

namespace IktatogRPCClient.ViewModels
{
	class KeresesViewModel : Screen
	{
		public KeresesViewModel()
		{
		
			SelectedSearchParameter = SearchList[0];
			AvailabelYears = new BindableCollection<int>() { 2020, 2019 };
			SelectedItemsPerPage = ItemsPerPage[2];
		}
		#region variables
		private ServerHelperSingleton serverHelper = ServerHelperSingleton.GetInstance();
		private bool _loaderIsVisible = false;
		private int _currentPage = 0;
		private string _searchText;
		private Ikonyv _selectedIkonyv;
		private string _selectedSearchParameter;
		private int _selectedItemsPerPage;
		private int _selectedYear;
		private BindableCollection<int> _availabelYears;
		private Irany _selectedIranyParameter;
		private ObservableCollection<Button> _pagingButtons = new ObservableCollection<Button>() { };
		private BindableCollection<Ikonyv> _searchedIkonyvek = new BindableCollection<Ikonyv>();
		private BindableCollection<Ikonyv> _shownIkonyvek = new BindableCollection<Ikonyv>();
		private BindableCollection<Irany> _iranyok = new BindableCollection<Irany>() { new Irany("Bejövő", 0), new Irany("Kimenő", 1), new Irany("Összes", 2) };
		private BindableCollection<Ikonyv> _allikonyv = new BindableCollection<Ikonyv>();
		private BindableCollection<string> _searchList = new BindableCollection<string>() { "Iktatószám", "Tárgy", "Partner", "Jelleg", "Ügyintéző", "Csoport", "Hivatkozási szám" };

        #endregion
        #region Properties
        public Ikonyv SelectedIkonyv
		{
			get { return _selectedIkonyv; }
			set { 
				_selectedIkonyv = value;
				NotifyOfPropertyChange(()=>SelectedIkonyv);
			}
		}
		public string SearchText
		{
			get { return _searchText; }
			set { _searchText = value;
				NotifyOfPropertyChange(()=> SearchText);
				CurrentPage = 0;
			}
		}
		public BindableCollection<Ikonyv> SearchedIkonyvek
		{
			get { return _searchedIkonyvek; }
			set { _searchedIkonyvek = value; }
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
				GetIkonyvek();
			}
		}
		// Ennek a változása inditja meg a gombok és az ikonyvek megjelenítését a viewban.
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
				SetVisibleIktatas();
			}
		}
		public bool CanToLastPage
		{
			get
			{
				return CurrentPage != MaxPage -1 && MaxPage != 0;
			}
		}
		public bool CanToNextPage
		{
			get
			{
				return CurrentPage != MaxPage -1 && MaxPage !=0;
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
		//Ez a property iratja ki a View-ra a Találatok számát
		public string MaxItemNumber {
			get {
				return $"Találatok száma: {ShownIkonyvek.Count} db";
			}
		}
		/*Kiszámolja, hogy hány lap kell az iktatások megjelenítéséhez. Ha van Keresési paraméter akkor azoknak a számát nézi ha nincs
		akkor az összes iktatásét*/
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
					if (SearchedIkonyvek.Count % SelectedItemsPerPage != 0)
					{
						count++;
					}
					count += SearchedIkonyvek.Count / SelectedItemsPerPage;
				}
				
				return count;
			}

		}
		public BindableCollection<int> AvailabelYears
		{
			get { return _availabelYears; }
			private set { _availabelYears = value;
				NotifyOfPropertyChange(() => AvailabelYears);
			}
		}
		public int SelectedYear
		{
			get { return _selectedYear; }
			set { _selectedYear = value;
				NotifyOfPropertyChange(() => SelectedYear);
				GetIkonyvek();
			}
		}
		//keresési lehetőségek
		public BindableCollection<string> SearchList
		{
			get { return _searchList; }
			private set {
				_searchList = value;
				NotifyOfPropertyChange(() => SearchList);
			}
		}

		// Összes elérhető iktatás a felhasználó számára az irány és az év paraméterével.
		public BindableCollection<Ikonyv> AllIkonyv
		{
			get { return _allikonyv; }
			private set {
				_allikonyv = value;
				NotifyOfPropertyChange(() => AllIkonyv);
			}
		}
        public BindableCollection<int> ItemsPerPage { get; set; } = new BindableCollection<int>() { 10, 20, 50, 100, 200, 500 };
		//Ikonyv/lap
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
			private set { _pagingButtons = value;
				NotifyOfPropertyChange(() => PagingButtons);
			}
		}
		//Datagridben megjelenített Ikönyvek
		public BindableCollection<Ikonyv> ShownIkonyvek
		{
			get { return _shownIkonyvek; }
			set {
				_shownIkonyvek = value;
				NotifyOfPropertyChange(() => ShownIkonyvek);
				NotifyOfPropertyChange(MaxItemNumber);
			}
		}

		#endregion
		//Adatbázisbol szedi le az Ikönyveket
		public async void GetIkonyvek() {
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
				SetVisibleIktatas();
				LoaderIsVisible = false;
			}
		}

		/*Meghívja a szűrést(SetSearchData) azutan annak az adataival dolgozik tovább, ahol megnézni, 
		hogy mennyi dolgot tehet ki a képernyőre végén meghivja a gombok beállítását*/
		private async void SetVisibleIktatas() {
			
			if (AllIkonyv.Count == 0) return;
			await SetSearchData();
			ShownIkonyvek.Clear();
			if ((CurrentPage+1) * SelectedItemsPerPage > SearchedIkonyvek.Count)
				{
					for (int i = (CurrentPage ) * SelectedItemsPerPage; i < SearchedIkonyvek.Count; i++)
					{
						ShownIkonyvek.Add(SearchedIkonyvek[i]);
						NotifyOfPropertyChange(() => ShownIkonyvek);
					}
				}
			else {
				for (int i = CurrentPage * SelectedItemsPerPage; i < CurrentPage * SelectedItemsPerPage + SelectedItemsPerPage; i++)
				{
					ShownIkonyvek.Add(SearchedIkonyvek[i]);
					NotifyOfPropertyChange(() => ShownIkonyvek);
				}
				}
			SetButtons();
		}

		// A keresési adok alapján beállíta a ShownIkonyvek változót --- Csak akkor ha a SearchText nem üres.
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
				NotifyOfPropertyChange(MaxItemNumber);
			});
		}
		#region GombokGenerálása és gomb muveletei
		/// <summary>
		/// Gombok beállítása a SearchIkonyvekhez mérten.
		/// </summary>

		private void SetButtons()
		{
			if (MaxPage == 0) {
				PagingButtons.Clear();
				return;
			} 
			PagingButtons.Clear();
			int delta = 1;
			int left = CurrentPage+1 - delta;
			int right = CurrentPage+1 + delta ;
			//Ne változzon a gombok mennyisége hogyha közel járok az elejéhez
			if (CurrentPage < 2) right = 3;
			//Ne változzon a gombok mennyisége hogyha közel járok az végéhez
			if (CurrentPage > MaxPage-4) left = MaxPage- 2;

			BindableCollection<Button> Buttons = new BindableCollection<Button>();
			Buttons.Add(GenerateButton("1"));
			if (MaxPage == 1)
			{
				PagingButtons = Buttons;
				return;
			}
			//Baloldaltól elkezdek iterálni jobboldalig
			for (int i = left; i <= right; i++)
			{			
				
				if (i >= left && i <= right && i > 1 && i < MaxPage)
				{
					Buttons.Add(GenerateButton(i.ToString()));
				}
			}
			Buttons.Add(GenerateButton(MaxPage.ToString()));
			if (Buttons.Count > 2)
			{
				if (int.Parse(Buttons[Buttons.Count-2].Content.ToString()) != MaxPage - 1 && CurrentPage <MaxPage-3) Buttons.Insert(Buttons.Count - 1, GenerateButton("..."));
				if (int.Parse(Buttons[1].Content.ToString()) >2 ) Buttons.Insert(1, GenerateButton("..."));
		
			}		
			PagingButtons = Buttons;
			NotifyOfPropertyChange(() => PagingButtons);
		}
		/// <summary>
		/// Generálja a gombot a lapozáshoz
		/// </summary>
		/// <param name="pageNumber">A gombban megjeleníteni kívánt szöveg aminek számnak vagy "..." kell lennie</param>
		/// <returns></returns>
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
				Button btn = sender as Button;
				if (btn.Content.ToString() == "...") return;
				CurrentPage = int.Parse((sender as Button).Content.ToString()) - 1;
			};

			if (pageNumber != "..." && CurrentPage == int.Parse(pageNumber) - 1)
			{
				btn.Background = new SolidColorBrush(GetBlueishColor());
				btn.Foreground = new SolidColorBrush(Colors.White);
			}
			return btn;
		}
		//Visszaadja a gomb háttér szinét ami megegyezik azzal a kékszinnel amit a viewban hanszálok
		private Color GetBlueishColor() {
			Color color = new Color();
			color.R = 3;
			color.G = 106;
			color.B = 154;
			color.A = 255;
			return color;
		}

        #region Viewban lévő static Léptető gombok Clickjei
        public void ToFirstPage() { 
				CurrentPage = 0;
			
		}

		public void ToPrevPage() {
				CurrentPage--;
			
		}
	
		public void ToNextPage() {
				CurrentPage++;			
		}

		public void ToLastPage() {	
				CurrentPage = MaxPage -1;
		}
        #endregion
        #endregion
    }

}
