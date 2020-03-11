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
    class PartnerekViewModel:Conductor<Screen>, IHandle<Partner>, IHandle<(Telephely, Partner)>
    {
        public PartnerekViewModel()
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

        public Partner SelectedPartner
        {
            get { return _selectedPartner; }
            set
            {
                _selectedPartner = value;
                NotifyOfPropertyChange(() => SelectedPartner);
                NotifyOfPropertyChange(() => CanRemovePartner);
                NotifyOfPropertyChange(() => CanModifyPartner);
            }
        }

        public BindableCollection<Partner> AvailablePartnerek
        {
            get { return _availablePartnerek; }
            set { _availablePartnerek = value;
                NotifyOfPropertyChange(() => AvailablePartnerek);
            }
        }

        public Telephely SelectedTelephely
        {
            get { return _selectedTelephely; }
            set
            {
                _selectedTelephely = value;
                NotifyOfPropertyChange(() => SelectedTelephely);
                AvailablePartnerek = serverHelper.GetPartnerekByTelephely(SelectedTelephely);
            }
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


        public bool CreationIsVisible
        {
            get { return !PartnerekIsVisible; }

        }

        public bool PartnerekIsVisible
        {
            get { return _partnerekIsVisible; }
            set
            {
                _partnerekIsVisible = value;
                NotifyOfPropertyChange(() => PartnerekIsVisible);
                NotifyOfPropertyChange(() => CreationIsVisible);
            }
        }
        public bool CanRemovePartner
        {
            get
            {
                return SelectedPartner != null;
            }
        }
        public bool CanModifyPartner
        {
            get
            {
                return SelectedPartner != null;
            }
        }
        public void CreatePartner() {
            PartnerekIsVisible = false;
        }
        public void RemovePartner() {
            if (serverHelper.RemovePartner(SelectedPartner)) {
                AvailablePartnerek.Remove(SelectedPartner);
            }
        }
        public void ModifyPartner() { 
        
        }

        public void Handle((Telephely, Partner) message)
        {
            throw new NotImplementedException();
        }

        public void Handle(Partner message)
        {
            if (message != SelectedPartner)
            {
                PartnerekIsVisible = true;
                if (!string.IsNullOrWhiteSpace(message.Name))
                {
                    AvailablePartnerek.Add(message);
                }
            }
        }
        protected override void OnDeactivate(bool close)
        {
            eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }
    }
}
