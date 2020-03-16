using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Managers;
using IktatogRPCClient.Models;
using IktatogRPCClient.Models.Managers;
using IktatogRPCClient.Models.Managers.Helpers.Client;
using IktatogRPCClient.Models.Scenes;
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
        private void InitializeData(Ikonyv IkonyvToModify) {
            this.IkonyvToModify = new Ikonyv(IkonyvToModify);
            AvailablePartners = serverHelper.GetPartnerekByTelephely(IkonyvToModify.Telephely);
            SelectedPartner = AvailablePartners.Count > 0 ? AvailablePartners[0] : new Partner();
            AvailableJellegek = serverHelper.GetJellegekByTelephely(IkonyvToModify.Telephely);
            AvailableUgyintezok = serverHelper.GetUgyintezokByTelephely(IkonyvToModify.Telephely);
            AvailablePartnerUgyintezok = new BindableCollection<PartnerUgyintezo>(SelectedPartner.Ugyintezok);
            SelectedJelleg = IkonyvToModify.Jelleg;
            SelectedPartnerUgyintezo = IkonyvToModify.Partner.Ugyintezok.Count > 0 ? IkonyvToModify.Partner.Ugyintezok[0] : null;
            SelectedUgyintezo = IkonyvToModify.Ugyintezo;
            IkonyvHasDocument = IkonyvToModify.HasDoc;
            eventAggregator.Subscribe(this);
        }
        private ServerHelperSingleton serverHelper = ServerHelperSingleton.GetInstance();
        private Ikonyv _ikonyvToModify;
        private Partner _selectedPartner;
        private BindableCollection<Partner> _availablePartners;
        private PartnerUgyintezo _selectedPartnerUgyintezo;
        private BindableCollection<PartnerUgyintezo> _availablePartnerUgyintezok;
        private Jelleg _selectedJelleg;
        private BindableCollection<Jelleg> _availableJellegek;
        private Ugyintezo _selectedUgyintezo;
        private BindableCollection<Ugyintezo> _availableUgyintezok;
        private bool _modificationHappend = false;
        private bool _modificationIsVisible = true;
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
                _selectedPartnerUgyintezo = value;
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
                NotifyOfPropertyChange(()=>SelectedPartner);
                AvailablePartnerUgyintezok = new BindableCollection<PartnerUgyintezo>(SelectedPartner.Ugyintezok);
                NotifyOfPropertyChange(() => AvailablePartnerUgyintezok);
            }
        }

        public string Title
        {
            get { return $"{IkonyvToModify.Iktatoszam} módosítása"; }
            
        }

        public Ikonyv IkonyvToModify
        {
            get { return _ikonyvToModify; }
            set { _ikonyvToModify = value; }
        }

        public void CancelButton() {
            MyParent.CloseScreen(this, ModificationHappend);
        }
        public void ModifyButton() {

            IkonyvToModify.Jelleg = SelectedJelleg;
            IkonyvToModify.Partner = SelectedPartner;
            IkonyvToModify.Partner.Ugyintezok.Clear();
            IkonyvToModify.Partner.Ugyintezok.Add(SelectedPartnerUgyintezo);
            IkonyvToModify.Ugyintezo = SelectedUgyintezo;
            IkonyvToModify.HasDoc = IkonyvHasDocument;
            IkonyvToModify.Szoveg = Szoveg;
            eventAggregator.PublishOnUIThread(IkonyvToModify);
            ModificationHappend = true;
            MyParent.CloseScreen(this, ModificationHappend);
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
            else if (IkonyvToModify.Targy.Length < 4 ||IkonyvToModify.Targy.Length> 100) IsValid = false;
            return IsValid;
        }
        public void RemoveButton() {
            ModificationHappend = true;
            if (serverHelper.RemoveIkonyvById(IkonyvToModify.Id)) {
                ModificationHappend = true;
            }
            eventAggregator.PublishOnUIThread(new RemovedItem(IkonyvToModify));
            MyParent.CloseScreen(this, ModificationHappend);
        }
        public bool CanRemoveButton {
            get {
                return UserHelperSingleton.CurrentUser.Privilege.Name == "Admin";
            }
        }

        public void DocumentView() {
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
