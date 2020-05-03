using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Managers;
using IktatogRPCClient.Models;
using IktatogRPCClient.Models.Managers;
using IktatogRPCClient.Models.Managers.Helpers.Client;
using IktatogRPCClient.Models.Scenes;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.ViewModels
{
    class ModifyIkonyvViewModel:PopUpChildModel,IHandle<DocumentHandlerClosed>
    {
        public ModifyIkonyvViewModel(Ikonyv IkonyvToModify)
        {

            InitializeData(IkonyvToModify);
            
        }
        private async void InitializeData(Ikonyv IkonyvToModify) {
            Log.Debug("{Class} Adatok letöltése a szerverről", GetType());
            this.IkonyvToModify = new Ikonyv(IkonyvToModify);
            AvailablePartners = await serverHelper.GetPartnerekByTelephelyAsync(IkonyvToModify.Telephely);
            AvailableJellegek = await serverHelper.GetJellegekByTelephelyAsync(IkonyvToModify.Telephely);
            AvailableUgyintezok = await serverHelper.GetUgyintezokByTelephelyAsync(IkonyvToModify.Telephely);
            SelectedPartner = AvailablePartners.Where(x=>x.Id == IkonyvToModify.Partner.Id).FirstOrDefault();
            SelectedJelleg = IkonyvToModify.Jelleg;
            SelectedUgyintezo = IkonyvToModify.Ugyintezo;
            IkonyvHasDocument = IkonyvToModify.HasDoc;
            Targy = IkonyvToModify.Targy;
            Iranya = IkonyvToModify.Irany == 0 ? "Bejövő" : "Kimenő";
            NotifyOfPropertyChange(() => CanModifyButton);
            eventAggregator.Subscribe(this);
        }
        private PartnerUgyintezo _emptyPartnerUgyintezo = new PartnerUgyintezo() { Id = -1, Name = "" };

  
        private ServerHelperSingleton serverHelper = ServerHelperSingleton.GetInstance();
        private Ikonyv _ikonyvToModify;
        private Partner _selectedPartner;
        private BindableCollection<Partner> _availablePartners;
        private PartnerUgyintezo _selectedPartnerUgyintezo;
        private BindableCollection<PartnerUgyintezo> _availablePartnerUgyintezok = new BindableCollection<PartnerUgyintezo>();
        private Jelleg _selectedJelleg;
        private BindableCollection<Jelleg> _availableJellegek;
        private Ugyintezo _selectedUgyintezo;
        private BindableCollection<Ugyintezo> _availableUgyintezok;
        private bool _modificationHappend = false;
        private bool _modificationIsVisible = true;
        private string _iranya;

        public string Iranya
        {
            get { return _iranya; }
            set { _iranya = value;
                NotifyOfPropertyChange(()=>Iranya);
            }
        }

        public string Szoveg { get {
                return IkonyvToModify.Szoveg;
            } 
            set {
                IkonyvToModify.Szoveg = value;
            } 
        }
        public string Targy
        {
            get { return IkonyvToModify.Targy; }
            set
            {
                IkonyvToModify.Targy = value;
                NotifyOfPropertyChange(()=>Targy);
                NotifyOfPropertyChange(()=>CanModifyButton);
            }
        }
        public string Hivszam
        {
            get { return IkonyvToModify.Hivszam; }
            set
            {
                if(value!= null)IkonyvToModify.Hivszam = value;
                NotifyOfPropertyChange(()=>Hivszam);
                NotifyOfPropertyChange(() => CanModifyButton);
            }
        }
        public string Hatido
        {
            get { return IkonyvToModify.HatIdo; }
            set
            {
                if (value != null) IkonyvToModify.HatIdo = DateTime.Parse(value).ToShortDateString();
                NotifyOfPropertyChange(()=>Hatido);
                NotifyOfPropertyChange(() => CanModifyButton);
            }
        }
        public string Erkezett
        {
            get { return IkonyvToModify.Erkezett; }
            set {
                IkonyvToModify.Erkezett = DateTime.Parse(value).ToShortDateString(); 
                NotifyOfPropertyChange(()=>Erkezett);
                NotifyOfPropertyChange(() => CanModifyButton);
            }
        }
        public PartnerUgyintezo EmptyPartnerUgyintezo
        {
            get { return _emptyPartnerUgyintezo; }

        }
        public bool ModificationIsVisible
        {
            get { return _modificationIsVisible; }
            set
            {
                _modificationIsVisible = value;
                NotifyOfPropertyChange(() => ModificationIsVisible);
            }
        }
        public bool DocsIsVisible
        {
            get
            {
                return !ModificationIsVisible;
            }
        }
        public bool ModificationHappend
        {
            get { return _modificationHappend; }
            set { _modificationHappend = value; }
        }

        public bool IkonyvHasDocument { get; set; }

        public BindableCollection<Ugyintezo> AvailableUgyintezok
        {
            get { return _availableUgyintezok; }
            set { _availableUgyintezok = value;
                NotifyOfPropertyChange(()=>AvailableUgyintezok);
            }
        }

        public Ugyintezo SelectedUgyintezo
        {
            get { return _selectedUgyintezo; }
            set { _selectedUgyintezo = value;
                NotifyOfPropertyChange(()=>SelectedUgyintezo);
            }
        }

        public BindableCollection<Jelleg> AvailableJellegek
        {
            get { return _availableJellegek; }
            set { _availableJellegek = value;
                NotifyOfPropertyChange(()=> AvailableJellegek);
            }
        }

        public Jelleg SelectedJelleg
        {
            get { return _selectedJelleg; }
            set { _selectedJelleg = value;
                NotifyOfPropertyChange(()=> SelectedJelleg);
            }
        }
        
        
        public BindableCollection<PartnerUgyintezo> AvailablePartnerUgyintezok
        {
            get { return _availablePartnerUgyintezok; }
            set { _availablePartnerUgyintezok = value;
                NotifyOfPropertyChange(()=>AvailablePartnerUgyintezok);
            }
        }

        public PartnerUgyintezo SelectedPartnerUgyintezo
        {
            get { return _selectedPartnerUgyintezo; }
            set
            {
                if (value != null) _selectedPartnerUgyintezo = value;
                else _selectedPartnerUgyintezo = EmptyPartnerUgyintezo;
                NotifyOfPropertyChange(() => SelectedPartnerUgyintezo);
            }
        }


        public BindableCollection<Partner> AvailablePartners
        {
            get { return _availablePartners; }
            set { _availablePartners = value;
                NotifyOfPropertyChange(()=>AvailablePartners);
            }
        }

        public Partner SelectedPartner
        {
            get { return _selectedPartner; }
            set {
                _selectedPartner = value;
                NotifyOfPropertyChange(() => SelectedPartner);     

                SetPartnerUgyintezok();
                NotifyOfPropertyChange(() => AvailablePartnerUgyintezok);
            }
        }
        private async void SetPartnerUgyintezok() {
            Log.Debug("{Class} Partnerügyintézők letöltése. Partner: {SelectedPartner}", GetType(),SelectedPartner);
            AvailablePartnerUgyintezok.Clear();
            AvailablePartnerUgyintezok = await serverHelper.GetPartnerUgyintezoByPartnerAsync(SelectedPartner);
            AvailablePartnerUgyintezok.Insert(0, EmptyPartnerUgyintezo);
            if (IkonyvToModify.Partner.Ugyintezok.Count > 0) SelectedPartnerUgyintezo = AvailablePartnerUgyintezok.Where(x => x.Id == IkonyvToModify.Partner.Ugyintezok[0].Id).FirstOrDefault();
            else
            {
                SelectedPartnerUgyintezo = EmptyPartnerUgyintezo;
            }
        }
        public string Title
        {
            get { return $"{IkonyvToModify.Iktatoszam}"; }
            
        }

        public Ikonyv IkonyvToModify
        {
            get { return _ikonyvToModify; }
            set { _ikonyvToModify = value; }
        }

        public void CancelButton() {
            Log.Debug("{Class} Mégse gomb megnyomva.", GetType(), SelectedPartner);
            MyParent.CloseScreen(this, ModificationHappend);
        }
        public async void ModifyButton() {
            Log.Debug("{Class} Módosítás gomb megnyomva.", GetType(), SelectedPartner);
            LoaderIsVisible = true;
            IkonyvToModify.Jelleg = SelectedJelleg;
            IkonyvToModify.Partner = SelectedPartner;
            IkonyvToModify.Partner.Ugyintezok.Clear();
            IkonyvToModify.Partner.Ugyintezok.Add(SelectedPartnerUgyintezo);
            IkonyvToModify.Ugyintezo = SelectedUgyintezo;
            IkonyvToModify.HasDoc = IkonyvHasDocument;
            IkonyvToModify.Szoveg = Szoveg;
            IkonyvToModify.Targy = Targy;
            Log.Debug("{Class} Módosítás: {IkonyvToModify}.", GetType(), IkonyvToModify);
            bool success = await serverHelper.ModifyIkonyvAsync(IkonyvToModify);
            if (success) {
                eventAggregator.PublishOnUIThread(IkonyvToModify);
                ModificationHappend = true;
                Log.Debug("{Class} Sikeres módosítás", GetType());
            }
          
            MyParent.CloseScreen(this, ModificationHappend);
            LoaderIsVisible = false;
        }
        public bool CanModifyButton {
            get {
                return DataValidation();
            }
        }
        private bool DataValidation() {
            bool IsValid = true;
            if (string.IsNullOrWhiteSpace(IkonyvToModify.Targy) ||
                string.IsNullOrWhiteSpace(IkonyvToModify.Erkezett) ||
                string.IsNullOrWhiteSpace(IkonyvToModify.HatIdo))
            {
                IsValid = false;
            }
            else if (SelectedJelleg == null ||
                SelectedPartner == null ||
                SelectedJelleg == null ||
                SelectedUgyintezo == null)
            {
                IsValid = false;
            }
            else if (IkonyvToModify.Targy.Length < 3 ||IkonyvToModify.Targy.Length> 100) IsValid = false;
            return IsValid;
        }
        public async void RemoveButton() {
            ModificationHappend = true;
            if (await serverHelper.RemoveIkonyvByIdAsync(IkonyvToModify.Id)) {
                ModificationHappend = true;
            }
            eventAggregator.PublishOnUIThread(new RemovedItem(IkonyvToModify));
            MyParent.CloseScreen(this, ModificationHappend);
        }
        public bool CanRemoveButton {
            get {
                return UserHelperSingleton.CurrentUser.Privilege.Name == "admin";
            }
        }

        public bool LoaderIsVisible { get; private set; }

        public void DocumentView() {
            Log.Debug("{Class} Dokumentumok listájának megnyitása.", GetType());
            ModificationIsVisible = false;
            Screen docScreen = new DocumentHandlerViewModel(IkonyvToModify.Id);
            eventAggregator.Subscribe(docScreen);
            ActivateItem(docScreen);
        }

        public void Handle(DocumentHandlerClosed message)
        {
            IkonyvHasDocument = message.HasDocument;
            ModificationHappend = message.ModificationHappend;
            ModificationIsVisible = true;
        }
    }
}
