using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iktato;
using IktatogRPCClient.Models;
using IktatogRPCClient.Models.Managers;
using Serilog;
using System.Windows;

namespace IktatogRPCClient.ViewModels
{
    class IktatasViewModel : IkonyvHandler
    {
        public IktatasViewModel()
        {
            LoadData();
        }
        private async void LoadData()
        {
            Log.Debug("{Class} Adatok betöltése a szerverről.", GetType());
            SelectedIrany = Iranyok.First();
            eventAggregator.Subscribe(this);
            AvailableTelephelyek = await serverHelper.GetTelephelyekAsync();
            if (AvailableTelephelyek.Count > 0) SelectedTelephely = AvailableTelephelyek.First();
            Log.Debug("{Class} Sikeres adat letöltés.", GetType());
        }
        #region mezők
        private EventAggregatorSingleton eventAggregator = EventAggregatorSingleton.GetInstance();
        public static BindableCollection<Ikonyv> _recentlyAddedIkonyvek = new BindableCollection<Ikonyv>();
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
        #endregion
        #region propetries
        public string Hivatkozasiszam
        {
            get { return _hivatkozasiszam; }
            set
            {
                _hivatkozasiszam = value;
                NotifyOfPropertyChange(() => Hivatkozasiszam);
            }
        }
        private BindableCollection<Irany> _iranyok = new BindableCollection<Irany>() { new Irany("Bejövő", 0), new Irany("Kimenő", 1) };

        public BindableCollection<Irany> Iranyok
        {
            get { return _iranyok; }
            set { _iranyok = value; }
        }
        private Irany _selectedIrany = new Irany("Bejövő", 0);

        public Irany SelectedIrany
        {
            get { return _selectedIrany; }
            set
            {
                _selectedIrany = value;
                NotifyOfPropertyChange(() => CanIktatButton);
                NotifyOfPropertyChange(() => SelectedIrany);
            }
        }
        public string Szoveg
        {
            get { return _szoveg; }
            set
            {
                _szoveg = value;
                NotifyOfPropertyChange(() => Szoveg);
                NotifyOfPropertyChange(() => CanIktatButton);
            }
        }
        public RovidIkonyv SelectedIktSzam
        {
            get { return _selectedIktSzam; }
            set
            {
                _selectedIktSzam = value;
                NotifyOfPropertyChange(() => SelectedIktSzam);
                NotifyOfPropertyChange(() => CanIktatButton);
            }
        }
        public BindableCollection<RovidIkonyv> IktSzamok
        {
            get { return _iktSzamok; }
            set
            {
                _iktSzamok = value;
                NotifyOfPropertyChange(() => IktSzamok);
                if (value != null && value.Count > 0) SelectedIktSzam = IktSzamok[0];
            }
        }
        public string ErkezettDatum
        {
            get { return _erkezettDatum; }
            set { _erkezettDatum = value;
                NotifyOfPropertyChange(() => CanIktatButton);
            }
        }
        private string _hatidoDatum = DateTime.Now.ToShortDateString();
        public string HatidoDatum
        {
            get { return _hatidoDatum; }
            
            set { _hatidoDatum = value;
                NotifyOfPropertyChange(() => CanIktatButton);
            }
        }
        public Ugyintezo SelectedUgyintezo
        {
            get { return _selectedUgyintezo; }
            set
            {
                _selectedUgyintezo = value;
                NotifyOfPropertyChange(() => SelectedUgyintezo);
                NotifyOfPropertyChange(() => CanIktatButton);
            }
        }

        public BindableCollection<Ugyintezo> AvailableUgyintezok
        {
            get { return _availableUgyintezok; }
            set
            {
                _availableUgyintezok = value;
                NotifyOfPropertyChange(() => AvailableUgyintezok);
                if (value != null && value.Count > 0) SelectedUgyintezo = AvailableUgyintezok[0];
            }
        }

        public Jelleg SelectedJelleg
        {
            get { return _selectedJelleg; }
            set
            {
                _selectedJelleg = value;
                NotifyOfPropertyChange(() => SelectedJelleg);
                NotifyOfPropertyChange(() => CanIktatButton);
            }
        }

        public BindableCollection<Jelleg> AvailableJellegek

        {
            get { return _availableJellegek; }
            set
            {
                _availableJellegek = value;
                NotifyOfPropertyChange(() => AvailableJellegek);
                if (value != null && value.Count > 0) SelectedJelleg = AvailableJellegek[0];
            }
        }

        public Partner SelectedPartner

        {
            get { return _selectedPartner; }
            set
            {
                _selectedPartner = value;
                NotifyOfPropertyChange(() => SelectedPartner);
                if (value != null) LoadPartnerUgyintezo();
                NotifyOfPropertyChange(() => CanIktatButton);
            }
        }
        private async void LoadPartnerUgyintezo()
        {
            Log.Debug("{Class} Partnerügyintézők letöltése. Partner: {SelectedPartner}", GetType(), SelectedPartner);
            AvailablePartnerUgyintezok = await serverHelper.GetPartnerUgyintezoByPartnerAsync(SelectedPartner);
            AvailablePartnerUgyintezok.Insert(0, EmptyPartnerUgyintezo);
            SelectedPartnerUgyintezo = AvailablePartnerUgyintezok.First();
            Log.Debug("{Class} Sikeres letöltés.", GetType(), SelectedPartner);
        }
        public PartnerUgyintezo SelectedPartnerUgyintezo

        {
            get { return _selectedPartnerUgyintezo; }
            set
            {
                _selectedPartnerUgyintezo = value;
                NotifyOfPropertyChange(() => SelectedPartnerUgyintezo);
                NotifyOfPropertyChange(() => CanIktatButton);
            }
        }
        private PartnerUgyintezo _emptyPartnerUgyintezo = new PartnerUgyintezo() { Id = -1, Name = "" };

        public PartnerUgyintezo EmptyPartnerUgyintezo
        {
            get { return _emptyPartnerUgyintezo; }

        }

        public BindableCollection<PartnerUgyintezo> AvailablePartnerUgyintezok

        {
            get { return _availablePartnerUgyintezok; }
            set
            {
                _availablePartnerUgyintezok = value;

                NotifyOfPropertyChange(() => AvailablePartnerUgyintezok);
            }
        }

        public BindableCollection<Partner> AvailablePartnerek

        {
            get { return _availablePartnerek; }
            set
            {
                _availablePartnerek = value;
                NotifyOfPropertyChange(() => AvailablePartnerek);
                if (value != null && value.Count > 0) SelectedPartner = AvailablePartnerek[0];
            }
        }

        public string Targy
        {
            get { return _targy; }
            set
            {
                _targy = value;
                NotifyOfPropertyChange(() => Targy);
                NotifyOfPropertyChange(() => CanIktatButton);
            }
        }

        public BindableCollection<Csoport> AvailableCsoportok

        {
            get { return _availableCsoportok; }
            set
            {
                _availableCsoportok = value;
                NotifyOfPropertyChange(() => AvailableCsoportok);
                if (value != null && value.Count > 0) SelectedCsoport = AvailableCsoportok[0];
            }
        }

        public Csoport SelectedCsoport
        {
            get { return _selectedCsoport; }
            set
            {
                _selectedCsoport = value;
                NotifyOfPropertyChange(() => SelectedCsoport);
                NotifyOfPropertyChange(() => CanIktatButton);
            }
        }

        public Telephely SelectedTelephely
        {
            get { return _selectedTelephely; }
            set
            {
                _selectedTelephely = value;
                NotifyOfPropertyChange(() => SelectedTelephely);
                NotifyOfPropertyChange(() => CanIktatButton);
                GetTelephelyData();
            }
        }
        private async void GetTelephelyData()
        {
            SetLoader();
            Log.Debug("{Class} A kiválasztott telephely által adatok betöltése. Telephely: {SelectedTelephely}", GetType(), SelectedTelephely);
            AvailableCsoportok.Clear();
            AvailablePartnerek.Clear();
            AvailableJellegek.Clear();
            AvailableUgyintezok.Clear();
            Log.Debug("{Class} Csoportok letöltésének megkezdése.", GetType());
            AvailableCsoportok = await serverHelper.GetCsoportokByTelephelyAsync(SelectedTelephely);
            Log.Debug("{Class} Partnerek letöltésének megkezdése.", GetType());
            AvailablePartnerek = await serverHelper.GetPartnerekByTelephelyAsync(SelectedTelephely);
            Log.Debug("{Class} Jellegek letöltésének megkezdése.", GetType());
            AvailableJellegek = await serverHelper.GetJellegekByTelephelyAsync(SelectedTelephely);
            Log.Debug("{Class} Ügyintézők letöltésének megkezdése. ", GetType());
            AvailableUgyintezok = await serverHelper.GetUgyintezokByTelephelyAsync(SelectedTelephely);
            Log.Debug("{Class} Letöltés kész.", GetType());
            SetLoader();
        }
        public BindableCollection<Telephely> AvailableTelephelyek
        {
            get { return _availableTelephelyek; }
            set
            {
                _availableTelephelyek = value;
                NotifyOfPropertyChange(() => AvailableTelephelyek);
            }
        }

        public BindableCollection<Ikonyv> RecentlyAddedIkonyvek
        {
            get { return _recentlyAddedIkonyvek; }
            set
            {
                _recentlyAddedIkonyvek = value;
                NotifyOfPropertyChange(() => RecentlyAddedIkonyvek);
            }
        }

        private bool _valaszIsChecked;
        private bool _loaderIsVisible = false;

        public bool LoaderIsVisible
        {
            get { return _loaderIsVisible; }
            set
            {
                _loaderIsVisible = value;
                NotifyOfPropertyChange(() => LoaderIsVisible);
            }
        }

        public bool ValaszIsChecked
        {
            get { return _valaszIsChecked; }
            set
            {
                _valaszIsChecked = value;
                NotifyOfPropertyChange(() => ValaszIsChecked);
                if (value)
                {
                    Log.Debug("{Class} Rövid ikönyvek letöltése.", GetType());
                    LoadRovidIkonyvek();
                }
                else { IktSzamok.Clear(); }
                NotifyOfPropertyChange(() => CanIktatButton);
            }
        }

        private async void LoadRovidIkonyvek()
        {
            IktSzamok = await serverHelper.GetShortIktSzamokByTelephelyAsync(SelectedTelephely);
            if (IktSzamok.Count > 0) SelectedIktSzam = IktSzamok[0];
        }
        public async void IktatButton()
        {
            Log.Debug("{Class} Iktatás gomb megnyomva.", GetType());

            LoaderIsVisible = true;
            if (SelectedPartnerUgyintezo != EmptyPartnerUgyintezo) SelectedPartner.Ugyintezok.Add(SelectedPartnerUgyintezo);
            Ikonyv newIkonyv = new Ikonyv()
            {
                Csoport = SelectedCsoport,
                Erkezett = this.ErkezettDatum,
                HatIdo = this.HatidoDatum,
                Hivszam = this.Hivatkozasiszam,
                Irany = SelectedIrany.Way,
                Jelleg = SelectedJelleg,
                Partner = SelectedPartner,
                Szoveg = this.Szoveg,
                Targy = this.Targy,
                Telephely = SelectedTelephely,
                Ugyintezo = SelectedUgyintezo,
                ValaszId = -1
            };
            Log.Debug("{Class} Az elő állított iktatás: {NewIkonyv}", GetType(), newIkonyv);
            RovidIkonyv rovidIkonyv;
            if (ValaszIsChecked)
            {
                Log.Debug("{Class} Parent hozzáadása. Parent.Id: {Id}", GetType(), SelectedIktSzam.Id);
                newIkonyv.ValaszId = SelectedIktSzam.Id;
                rovidIkonyv = await serverHelper.AddIktatasWithValaszAsync(newIkonyv);
            }
            else
            rovidIkonyv = await serverHelper.AddIktatas(newIkonyv);

            if (rovidIkonyv.Id == 0)
            {
                LoaderIsVisible = false;
                MessageBox.Show($"Az iktatás sikertlen.");
                Log.Debug("{Class} Iktatás sikertelen.", GetType());
                return;
            }

            newIkonyv.Id = rovidIkonyv.Id;
            newIkonyv.Iktatoszam = rovidIkonyv.Iktatoszam;
            _recentlyAddedIkonyvek.Add(newIkonyv);
            LoaderIsVisible = false;
            MessageBox.Show($"Az új iktatásiszám: {newIkonyv.Iktatoszam}.");
            Log.Debug("{Class} Iktatás sikeres", GetType());
        }
        public bool CanIktatButton
        {
            get{
                return FormValidation();
            }
         
        }
        #endregion
        #region methods and functions
        private bool FormValidation() {
            bool IsValid = true;
            if (IsTorzsDataInFormNull()) IsValid = false;
            else if (string.IsNullOrWhiteSpace(Targy)) IsValid = false;
            else if (Targy.Length > 100) IsValid = false;
            else if (DateTime.Parse(HatidoDatum) < DateTime.Parse(ErkezettDatum)) IsValid = false;
            else if (ValaszIsChecked && SelectedIktSzam == null) IsValid = false;
            else if (Szoveg.Length > 500) IsValid = false;
            else if (Hivatkozasiszam.Length > 50) IsValid = false;
            return IsValid;
        }
        private bool IsTorzsDataInFormNull() {
            return (SelectedJelleg == null || SelectedPartner == null ||
                    SelectedPartnerUgyintezo == null || SelectedTelephely == null ||
                    SelectedUgyintezo == null || SelectedCsoport == null || SelectedIrany == null);
        }
        public override void Handle(RemovedItem message)
        {
            if (message.Item is Ikonyv)
            {
                Ikonyv ikonyv = message.Item as Ikonyv;
                for (int i = 0; i < RecentlyAddedIkonyvek.Count; i++)
                {
                    if (RecentlyAddedIkonyvek[i].Id == ikonyv.Id)
                    {
                        RecentlyAddedIkonyvek.RemoveAt(i);
                        NotifyOfPropertyChange(() => RecentlyAddedIkonyvek);
                    }
                }
            }

        }

        public override void Handle(Ikonyv message)
        {
            
            for (int i = 0; i < RecentlyAddedIkonyvek.Count; i++)
            {
                if (RecentlyAddedIkonyvek[i].Id == message.Id)
                {
                    RecentlyAddedIkonyvek.RemoveAt(i);
                    RecentlyAddedIkonyvek.Add(message);
                }
            }
            NotifyOfPropertyChange(()=> RecentlyAddedIkonyvek);
           
        }
        private void SetLoader()
        {
            LoaderIsVisible = !LoaderIsVisible;
        }


        #endregion
    }
}
