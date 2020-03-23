using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Managers;
using IktatogRPCClient.Models;
using IktatogRPCClient.Models.Managers;
using IktatogRPCClient.Models.Scenes;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.ViewModels
{
    class PartnerekViewModel : Conductor<Screen>, IHandle<Partner>,
        IHandle<(Telephely, Partner)>, IHandle<Telephely>, IHandle<RemovedItem>,
        IHandle<BindableCollection<Telephely>>
    {
        public PartnerekViewModel()
        {
            Initialize();
        }
        private void Initialize()
        {
            Log.Debug("{Class} adatok inicializációja.", GetType());
            serverHelper = ServerHelperSingleton.GetInstance();
            eventAggregator = EventAggregatorSingleton.GetInstance();
            eventAggregator.Subscribe(this);


        }
        private bool _partnerekIsVisible = true;
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
            set
            {
                _availablePartnerek = value;
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
                LoadPartnerAsync();
            }
        }
        private async void LoadPartnerAsync()
        {
            Log.Debug("{Class} partnerek letöltése.", GetType());
            AvailablePartnerek = await serverHelper.GetPartnerekByTelephelyAsync(SelectedTelephely);
            if (AvailablePartnerek.Count > 0)
            {
                Log.Debug("{Class} Sikeres letöltés.", GetType());
            }
            else
            {
                Log.Debug("{Class} Sikertelen letöltés vagy nincs még ügyintéző a partnerhez.", GetType());
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
        public void CreatePartner()
        {
            Log.Debug("{Class} hozzáadás gomb megnyomva.", GetType());
            PartnerekIsVisible = false;
            Screen createScreen = SceneManager.CreateScene(Scenes.AddPartner);
            ActivateItem(createScreen);
            eventAggregator.Subscribe(createScreen);
            eventAggregator.PublishOnUIThread(AvailableTelephelyek);
        }
        public async void RemovePartner()
        {
            Log.Debug("{Class} törlés gomb megnyomva.", GetType());
            Log.Debug("{Class} törlés a kovetkező paraméterrel. {SelectedPartner}", GetType(),SelectedPartner);
            if (await serverHelper.RemovePartnerAsync(SelectedPartner))
            {
                Log.Debug("{Class} Sikeres törlés.", GetType());
                eventAggregator.PublishOnUIThread(new RemovedItem(SelectedPartner));
                AvailablePartnerek.Remove(SelectedPartner);
            }
            else {
                Log.Debug("{Class} Sikertelen törlés.", GetType());
            }
        }
        public void ModifyPartner()
        {
            Log.Debug("{Class} módosítás gomb megnyomva.", GetType());
            PartnerekIsVisible = false;
            Screen modifyScreen = SceneManager.CreateScene(Scenes.ModifyPartner);
            eventAggregator.Subscribe(modifyScreen);
            ActivateItem(modifyScreen);
            eventAggregator.PublishOnUIThread(SelectedTelephely);
            eventAggregator.PublishOnUIThread(SelectedPartner);
        }

        public void Handle((Telephely, Partner) message)
        {
            PartnerekIsVisible = true;
            if (message.Item1.Name == SelectedTelephely.Name)
            {
                Partner partner = AvailablePartnerek.Where(x => x.Id == message.Item2.Id).FirstOrDefault();
                if (partner != null)
                {
                    AvailablePartnerek.Remove(partner);
                    AvailablePartnerek.Add(message.Item2);
                    NotifyOfPropertyChange(() => AvailablePartnerek);
                }
                else
                {
                    AvailablePartnerek.Add(message.Item2);
                    NotifyOfPropertyChange(() => AvailablePartnerek);
                }
            }
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
       
        public void Handle(RemovedItem message)
        {
            if (message.Item is Telephely)
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
                else
                {
                    AvailableTelephelyek.Remove(telephely);
                    AvailableTelephelyek.Add(message);
                    NotifyOfPropertyChange(() => AvailableTelephelyek);
                }
            }
        }

        public void Handle(BindableCollection<Telephely> message)
        {
            if (AvailableTelephelyek == null)
            {
                AvailableTelephelyek = message;
                if (AvailableTelephelyek.Count > 0) SelectedTelephely = AvailableTelephelyek.First();
            }

        }
    }
}
