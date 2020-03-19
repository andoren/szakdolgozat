using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Managers;
using IktatogRPCClient.Models;
using IktatogRPCClient.Models.Managers;
using IktatogRPCClient.Models.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.ViewModels
{
    class PartnerekUgyintezoiViewModel:Conductor<Screen>,IHandle<Partner>,IHandle<(Telephely,Partner)>
        ,IHandle<RemovedItem>,IHandle<Telephely>,IHandle<(Partner,PartnerUgyintezo)>, IHandle<PartnerUgyintezo>,
        IHandle<(Telephely, Partner, PartnerUgyintezo)>, IHandle<BindableCollection<Telephely>>
    {
        public PartnerekUgyintezoiViewModel()
        {
            LoadData();
        }
        private  void LoadData()
        {
            serverHelper = ServerHelperSingleton.GetInstance();
            eventAggregator = EventAggregatorSingleton.GetInstance();
            eventAggregator.Subscribe(this);

        }
        private bool _partnerekIsVisible=true;
        private BindableCollection<Telephely> _availableTelephelyek = new BindableCollection<Telephely>();
        private Telephely _selectedTelephely;
        private ServerHelperSingleton serverHelper;
        private EventAggregatorSingleton eventAggregator;
        private BindableCollection<Partner> _availablePartnerek = new BindableCollection<Partner>();
        private Partner _selectedPartner;
        private BindableCollection<PartnerUgyintezo> _availableUgyintezok = new BindableCollection<PartnerUgyintezo>();
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
                NotifyOfPropertyChange(() => SelectedPartner);
                if (value != null) GetPartnerUgyintezokAsync();
                else AvailableUgyintezok.Clear();
            }
        }
        private async void GetPartnerUgyintezokAsync() {
            AvailableUgyintezok = await serverHelper.GetPartnerUgyintezoByPartnerAsync(SelectedPartner);
            
        }
        public BindableCollection<Partner> AvailablePartnerek
        {
            get { return _availablePartnerek; }
            set {
                _availablePartnerek = value;               
                if(value.Count !=0)SelectedPartner = value.First();
                NotifyOfPropertyChange(() => AvailablePartnerek);
            }
        }

        public Telephely SelectedTelephely
        {
            get { return _selectedTelephely; }
            set { _selectedTelephely = value;
                NotifyOfPropertyChange(()=>SelectedTelephely);
                GetPartnerekAsync();


            }
        }
        private async void GetPartnerekAsync() {
            AvailablePartnerek = await serverHelper.GetPartnerekByTelephelyAsync(SelectedTelephely);
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
        public async void RemoveUgyintezo() {
            bool success = await serverHelper.RemovePartnerUgyintezoAsync(SelectedUgyintezo);
            if (success)AvailableUgyintezok.Remove(SelectedUgyintezo);
        }
        public void ModifyUgyintezo() {
            PartnerekIsVisible = false;
            Screen modifyScreen = SceneManager.CreateScene(Scenes.ModifyPartnerUgyintezo);
            eventAggregator.Subscribe(modifyScreen);
            ActivateItem(modifyScreen);
            eventAggregator.PublishOnUIThread((SelectedTelephely,SelectedPartner,SelectedUgyintezo));
        }
        public void CreateUgyintezo() {
            PartnerekIsVisible = false;
            Screen creatScreen = SceneManager.CreateScene(Scenes.AddPartnerUgyintezo);
            eventAggregator.Subscribe(creatScreen);
            ActivateItem(creatScreen);
            eventAggregator.PublishOnUIThread(AvailablePartnerek);
        }
        public void Handle((Telephely, Partner) message)
        {
            if (message.Item1.Name == SelectedTelephely.Name) {
                Partner partner = AvailablePartnerek.Where(x => x.Id == message.Item2.Id).FirstOrDefault();
                if (partner!=null) {
                    AvailablePartnerek.Remove(partner);
                    AvailablePartnerek.Add(message.Item2);
                    NotifyOfPropertyChange(()=>AvailablePartnerek);
                }
                else {
                    AvailablePartnerek.Add(message.Item2);
                    NotifyOfPropertyChange(() => AvailablePartnerek);
                }
  
            }
        }

        public void Handle(Partner message)
        {
            PartnerekIsVisible = true;
            Partner partner = AvailablePartnerek.Where(x => x.Id == message.Id).FirstOrDefault();
            if (partner == null)
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
            else if (partner.Name != message.Name)
            {
                AvailablePartnerek.Remove(partner);
                AvailablePartnerek.Add(message);
                NotifyOfPropertyChange(() => AvailablePartnerek);

            }
        }


        public void Handle(RemovedItem message)
        {
            if (message.Item is Partner) {
                Partner partner = AvailablePartnerek.Where(x => x.Id == (message.Item as Partner).Id).FirstOrDefault();
                AvailablePartnerek.Remove(partner);
                NotifyOfPropertyChange(()=>AvailablePartnerek);
            }
            else if (message.Item is Telephely)
            {
                Telephely telephely = AvailableTelephelyek.Where(x => x.Id == (message.Item as Telephely).Id).FirstOrDefault();
                AvailableTelephelyek.Remove(telephely);
                NotifyOfPropertyChange(() => AvailableTelephelyek);
            }
        }

        public void Handle(Telephely message)
        {
            if (message != SelectedTelephely && message != null)
            {
                Telephely telephely = AvailableTelephelyek.Where(x => x.Id == message.Id).FirstOrDefault();
                if (telephely == null)
                {
                    AvailableTelephelyek.Add(message);
                    NotifyOfPropertyChange(() => AvailableTelephelyek);
                }
                else if(telephely.Name != message.Name)
                {
                    AvailableTelephelyek.Remove(telephely);
                    AvailableTelephelyek.Add(message);
                    NotifyOfPropertyChange(() => AvailableTelephelyek);
                    
                }
            }
        }

        public void Handle((Partner, PartnerUgyintezo) message)
        {
            PartnerekIsVisible = true;
            if (message.Item1.Name == SelectedPartner.Name) {
                AvailableUgyintezok.Add(message.Item2);
                NotifyOfPropertyChange(()=>AvailableUgyintezok);
            }
        }

        public void Handle(PartnerUgyintezo message)
        {
            if (SelectedUgyintezo != message) PartnerekIsVisible = true;
        }

        public void Handle((Telephely, Partner, PartnerUgyintezo) message)
        {
            if (message.Item3 != SelectedUgyintezo) {
                PartnerekIsVisible = true;
                if (message.Item1 == SelectedTelephely) {
                    if (message.Item2 == SelectedPartner) {
                        AvailableUgyintezok.Remove(SelectedUgyintezo);
                        AvailableUgyintezok.Add(message.Item3);
                    }
                }
            }
        }

        public void Handle(BindableCollection<Telephely> message)
        {
            AvailableTelephelyek = message ;
            if (AvailableTelephelyek.Count > 0) SelectedTelephely = AvailableTelephelyek.First();
        }
    }
}
