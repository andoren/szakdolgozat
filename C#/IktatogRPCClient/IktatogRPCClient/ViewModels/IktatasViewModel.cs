using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iktato;
using IktatogRPCClient.Models;
using IktatogRPCClient.Models.Managers;

namespace IktatogRPCClient.ViewModels
{
    class IktatasViewModel:IkonyvHandlerModel,IHandle<RemovedItem>,IHandle<Ikonyv>
    {
		public IktatasViewModel()
		{
            AvailableTelephelyek = serverHelper.GetTelephelyek();
            SelectedTelephely = AvailableTelephelyek.First();
            SelectedIrany = Iranyok.First();
            eventAggregator.Subscribe(this);
          
        }
        private EventAggregatorSingleton eventAggregator = EventAggregatorSingleton.GetInstance();
        private static BindableCollection<Ikonyv> _recentlyAddedIkonyvek = new BindableCollection<Ikonyv>();
        private BindableCollection<Telephely> _availableTelephelyek;
        private Telephely _selectedTelephely;
        private Csoport _selectedCsoport;
        private BindableCollection<Csoport> _availableCsoportok = new BindableCollection<Csoport>();
        private string _targy;
        private ServerHelperSingleton serverHelper = ServerHelperSingleton.GetInstance();
        private BindableCollection<Partner> _availablePartnerek = new BindableCollection<Partner>();
        private BindableCollection<PartnerUgyintezo> _availablePartnerUgyintezok = new BindableCollection<PartnerUgyintezo>();
        private PartnerUgyintezo _selectedPartnerUgyintezo;
        private Partner _selectedPartner;
        private BindableCollection<Jelleg> _availableJellegek = new BindableCollection<Jelleg>();
        private Jelleg _selectedJelleg;
        private BindableCollection<Ugyintezo> _availableUgyintezok = new BindableCollection<Ugyintezo>();
        private Ugyintezo _selectedUgyintezo;
        private string _erkezettDatum = DateTime.Now.ToShortDateString();
        private BindableCollection<RovidIkonyv> _iktSzamok = new BindableCollection<RovidIkonyv>();
        private RovidIkonyv _selectedIktSzam;
        private string _szoveg = "";
        private string _hivatkozasiszam = "";

        public string Hivatkozasiszam
        {
            get { return _hivatkozasiszam; }
            set { _hivatkozasiszam = value;
                NotifyOfPropertyChange(()=>Hivatkozasiszam);
            }
        }
        private BindableCollection<Irany> _iranyok = new BindableCollection<Irany>() { new Irany("Bejövő", 0), new Irany("Kimenő", 1) };

        public BindableCollection<Irany> Iranyok
        {
            get { return _iranyok; }
            set { _iranyok = value; }
        }
        private Irany _selectedIrany = new Irany("Bejövő",0);

        public Irany SelectedIrany
        {
            get { return _selectedIrany; }
            set { 
                _selectedIrany = value;
                NotifyOfPropertyChange(()=>SelectedIrany);
            }
        }

        public string Szoveg
        {
            get { return _szoveg; }
            set { _szoveg = value;
                NotifyOfPropertyChange(()=>Szoveg);
            }
        }

        public RovidIkonyv SelectedIktSzam
        {
            get { return _selectedIktSzam; }
            set { _selectedIktSzam = value;
                NotifyOfPropertyChange(()=>SelectedIktSzam);
            }
        }

        public BindableCollection<RovidIkonyv> IktSzamok
        {
            get { return _iktSzamok; }
            set { _iktSzamok = value;
                NotifyOfPropertyChange(()=>IktSzamok);
                if (value != null && value.Count > 0) SelectedIktSzam = IktSzamok[0];
            }
        }

        public string ErkezettDatum 
        {
            get { return _erkezettDatum; }
            set { _erkezettDatum = value; }
        }
        private string _hatidoDatum = DateTime.Now.ToShortDateString();

        public string HatidoDatum
        {
            get { return _hatidoDatum; }
            set { _hatidoDatum = value; }
        }


        public Ugyintezo SelectedUgyintezo
        {
            get { return _selectedUgyintezo; }
            set { _selectedUgyintezo = value;
                NotifyOfPropertyChange(()=>SelectedUgyintezo);
            }
        }

        public BindableCollection<Ugyintezo> AvailableUgyintezok
        {
            get { return _availableUgyintezok; }
            set {
                _availableUgyintezok = value;
                NotifyOfPropertyChange(()=>AvailableUgyintezok);
                if (value != null && value.Count > 0) SelectedUgyintezo = AvailableUgyintezok[0];
            }
        }

        public Jelleg SelectedJelleg
        {
            get { return _selectedJelleg; }
            set { _selectedJelleg = value;
                NotifyOfPropertyChange(()=>SelectedJelleg);
            }
        }

        public BindableCollection<Jelleg> AvailableJellegek

        {
            get { return _availableJellegek; }
            set {
                _availableJellegek = value;
                NotifyOfPropertyChange(()=> AvailableJellegek);
                if (value != null && value.Count > 0) SelectedJelleg = AvailableJellegek[0];
            }
        }

        public Partner SelectedPartner

        {
            get { return _selectedPartner; }
            set { _selectedPartner = value;
                NotifyOfPropertyChange(()=>SelectedPartner);
                if (value != null) LoadPartnerUgyintezo();
            }
        }
        private async void LoadPartnerUgyintezo() {
            AvailablePartnerUgyintezok = await serverHelper.GetPartnerUgyintezoByPartnerAsync(SelectedPartner);
        }
        public PartnerUgyintezo SelectedPartnerUgyintezo

        {
            get { return _selectedPartnerUgyintezo; }
            set { _selectedPartnerUgyintezo = value;
                NotifyOfPropertyChange(()=>SelectedPartnerUgyintezo);
            }
        }

        public BindableCollection<PartnerUgyintezo> AvailablePartnerUgyintezok

        {
            get { return _availablePartnerUgyintezok; }
            set { _availablePartnerUgyintezok = value;
                if (value != null && value.Count > 0) SelectedPartnerUgyintezo = value[0];
                NotifyOfPropertyChange(()=>AvailablePartnerUgyintezok);
            }
        }

        public BindableCollection<Partner> AvailablePartnerek

        {
            get { return _availablePartnerek; }
            set { _availablePartnerek = value;
                NotifyOfPropertyChange(()=>AvailablePartnerek);
                if (value != null && value.Count > 0) SelectedPartner = AvailablePartnerek[0];
            }
        }

        public string Targy
        {
            get { return _targy; }
            set
            {
                _targy = value;
                NotifyOfPropertyChange(()=>Targy);
            }
        }

        public BindableCollection<Csoport> AvailableCsoportok

        {
            get { return _availableCsoportok; }
            set { _availableCsoportok = value;
                NotifyOfPropertyChange(() => AvailableCsoportok);
                if(value != null && value.Count >0)SelectedCsoport = AvailableCsoportok[0];
            }
        }

        public Csoport SelectedCsoport
        {
            get { return _selectedCsoport; }
            set { _selectedCsoport = value;
                NotifyOfPropertyChange(()=>SelectedCsoport);
            }
        }

        public Telephely SelectedTelephely
        {
            get { return _selectedTelephely; }
            set { 
                _selectedTelephely = value;
                NotifyOfPropertyChange(()=>SelectedTelephely);
                GetTelephelyData();
            }
        }
        private async void GetTelephelyData() {
            SetLoader();
                AvailableCsoportok.Clear();
                AvailablePartnerek.Clear();
                AvailableJellegek.Clear();
                AvailableUgyintezok.Clear();
                AvailableCsoportok  = await serverHelper.GetCsoportokByTelephelyAsync(SelectedTelephely);
                AvailablePartnerek  = await serverHelper.GetPartnerekByTelephelyAsync(SelectedTelephely);
                AvailableJellegek   = await serverHelper.GetJellegekByTelephelyAsync(SelectedTelephely);
                AvailableUgyintezok = await serverHelper.GetUgyintezokByTelephelyAsync(SelectedTelephely);
            SetLoader();
        }
        public BindableCollection<Telephely> AvailableTelephelyek
        {
            get { return _availableTelephelyek; }
            set { _availableTelephelyek = value;
                NotifyOfPropertyChange(()=>AvailableTelephelyek);
            }
        }

        public BindableCollection<Ikonyv> RecentlyAddedIkonyvek
		{
			get { return _recentlyAddedIkonyvek; }
			set {
				_recentlyAddedIkonyvek = value;
				NotifyOfPropertyChange(()=> RecentlyAddedIkonyvek);
			}
		}

        private bool _valaszIsChecked;
        private bool _loaderIsVisible =false;

        public bool LoaderIsVisible
        {
            get { return _loaderIsVisible; }
            set { _loaderIsVisible = value;
                NotifyOfPropertyChange(()=>LoaderIsVisible);
            }
        }

        public bool ValaszIsChecked
        {
            get { return _valaszIsChecked; }
            set { _valaszIsChecked = value;
                NotifyOfPropertyChange(() => ValaszIsChecked);
                if (value) {
                    LoadRovidIkonyvek();
                }
                else { IktSzamok.Clear(); }
            }
        }

        private async void LoadRovidIkonyvek() {
            IktSzamok = await serverHelper.GetShortIktSzamokByTelephelyAsync(SelectedTelephely);
            if (IktSzamok.Count > 0) SelectedIktSzam = IktSzamok[0];
        }
        public async void IktatButton(string targy,string erkezettDatum,string hatidoDatum, string hivatkozasiszam, string szoveg)
        {
            LoaderIsVisible = true;
            Ikonyv newIkonyv = new Ikonyv()
            {
                Csoport = SelectedCsoport,
                Erkezett = erkezettDatum,
                HatIdo = hatidoDatum,
                Hivszam = hivatkozasiszam,
                Irany = SelectedIrany.Way,
                Jelleg = SelectedJelleg,
                Partner = SelectedPartner,
                Szoveg = szoveg,
                Targy = targy,
                Telephely = SelectedTelephely,
                Ugyintezo = SelectedUgyintezo
            };
            RovidIkonyv rovidIkonyv;
            if (ValaszIsChecked)
            {
                newIkonyv.ValaszId = SelectedIktSzam.Id;
                rovidIkonyv = await serverHelper.AddIktatasWithValaszAsync(newIkonyv);
            }
            else {
                rovidIkonyv = await serverHelper.AddIktatas(newIkonyv);
            }
            if (rovidIkonyv.Id == 0) {
                LoaderIsVisible = false;
                return;
            } 
            newIkonyv.Id = rovidIkonyv.Id;
            newIkonyv.Iktatoszam = rovidIkonyv.Iktatoszam;
            _recentlyAddedIkonyvek.Add(newIkonyv);
            LoaderIsVisible = false;
        }
        public bool CanIktatButton(string targy, string erkezettDatum, string hatidoDatum, string hivatkozasiszam, string szoveg) {
            if(string.IsNullOrWhiteSpace(targy) 
                || string.IsNullOrWhiteSpace(erkezettDatum)
                || string.IsNullOrWhiteSpace(hatidoDatum)) return false;
            return true;
        }


        public override void Handle(RemovedItem message)
        {
            if (message.Item is Ikonyv)
            {
                _recentlyAddedIkonyvek.Remove(message.Item as Ikonyv);
                NotifyOfPropertyChange(() => RecentlyAddedIkonyvek);
            }
            
        }

        public override void Handle(Ikonyv message)
        {
            _recentlyAddedIkonyvek.Remove(SelectedIkonyv);
            _recentlyAddedIkonyvek.Add(message);
            NotifyOfPropertyChange(()=>message);
        }
        private void SetLoader() {
            LoaderIsVisible = !LoaderIsVisible;
        }
    }
}
