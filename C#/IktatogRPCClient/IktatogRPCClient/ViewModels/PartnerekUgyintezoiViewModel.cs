using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.ViewModels
{
    class PartnerekUgyintezoiViewModel:Conductor<Screen>,IHandle<Partner>,IHandle<(Telephely,Partner)>
    {
        public PartnerekUgyintezoiViewModel()
        {
            LoadData();
        }
        private void LoadData()
        {
            serverHelper = ServerHelperSingleton.GetInstance();
            eventAggregator = EventAggregatorSingleton.GetInstance();
            eventAggregator.Subscribe(this);
            AvailableTelephelyek = serverHelper.GetTelephelyek();
            SelectedTelephely = AvailableTelephelyek.First();
        }
        private bool _partnerekIsVisible=true;
        private BindableCollection<Telephely> _availableTelephelyek;
        private Telephely _selectedTelephely;
        private ServerHelperSingleton serverHelper;
        private EventAggregatorSingleton eventAggregator;
        private BindableCollection<Partner> _availablePartnerek;
        private Partner _selectedPartner;
        private BindableCollection<PartnerUgyintezo> _availableUgyintezok;
        private PartnerUgyintezo _selectedUgyintezo;

        public PartnerUgyintezo SelectedUgyintezo
        {
            get { return _selectedUgyintezo; }
            set { _selectedUgyintezo = value;
                NotifyOfPropertyChange(() => SelectedUgyintezo);
                NotifyOfPropertyChange(() => CanRemoveUgyintezo);
                NotifyOfPropertyChange(() => CanModifyUgyintezo );
            }
        }

        public BindableCollection<PartnerUgyintezo> AvailableUgyintezok
        {
            get { return _availableUgyintezok; }
            set { _availableUgyintezok = value;
                NotifyOfPropertyChange(()=>AvailableUgyintezok);
            }
        }

        public Partner SelectedPartner
        {
            get { return _selectedPartner; }
            set { 
                _selectedPartner = value;
                NotifyOfPropertyChange(()=>SelectedPartner);
                if(value != null)AvailableUgyintezok = new BindableCollection<PartnerUgyintezo>(SelectedPartner.Ugyintezok);
            }
        }

        public BindableCollection<Partner> AvailablePartnerek
        {
            get { return _availablePartnerek; }
            set {
                _availablePartnerek = value;
                NotifyOfPropertyChange(() =>AvailablePartnerek);
            }
        }

        public Telephely SelectedTelephely
        {
            get { return _selectedTelephely; }
            set { _selectedTelephely = value;
                NotifyOfPropertyChange(()=>SelectedTelephely);
                AvailablePartnerek = serverHelper.GetPartnerekByTelephely(SelectedTelephely);
                SelectedPartner = AvailablePartnerek.First();
            }
        }



        public BindableCollection<Telephely> AvailableTelephelyek
        {
            get { return _availableTelephelyek; }
            set { _availableTelephelyek = value;
                NotifyOfPropertyChange(()=> AvailableTelephelyek);
            }
        }


        public bool CreationIsVisible
        {
            get { return !PartnerekIsVisible; }
      
        }

        public bool PartnerekIsVisible
        {
            get { return _partnerekIsVisible; }
            set { 
                _partnerekIsVisible = value;
                NotifyOfPropertyChange(()=> PartnerekIsVisible);
                NotifyOfPropertyChange(()=> CreationIsVisible);
            }
        }
        public bool CanRemoveUgyintezo {
            get { 
            return SelectedUgyintezo != null;
            }
        }
        public bool CanModifyUgyintezo {
            get {
                return SelectedUgyintezo != null;
            }
        }
        public void RemoveUgyintezo() { }
        public void ModifyUgyintezo() { }
        public void CreateUgyintezo() { }
        public void Handle((Telephely, Partner) message)
        {
            throw new NotImplementedException();
        }

        public void Handle(Partner message)
        {
            if (message != SelectedPartner) {
                PartnerekIsVisible = true;
                if (!string.IsNullOrWhiteSpace(message.Name)){
                    AvailablePartnerek.Add(message);
                }
            }
        }
        ~PartnerekUgyintezoiViewModel() { 
            
        }
    }
}
