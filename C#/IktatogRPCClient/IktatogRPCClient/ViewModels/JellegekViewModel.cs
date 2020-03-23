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
    class JellegekViewModel : Conductor<Screen>, IHandle<Jelleg>, 
        IHandle<(Telephely, Jelleg)>,IHandle<Telephely>,IHandle<RemovedItem>,IHandle<BindableCollection<Telephely>>
    {
        public JellegekViewModel()
        {
            Initialize();
        }
        private  void Initialize() {
            Log.Debug("{Class} Adatok inicializációja", GetType());
            serverHelper = ServerHelperSingleton.GetInstance();
            eventAggregator = EventAggregatorSingleton.GetInstance();
            eventAggregator.Subscribe(this);
          
        }
        private bool _jellegekIsVisible = true;
        private BindableCollection<Telephely> _availableTelephelyek;
        private Telephely _selectedTelephely;
        private BindableCollection<Jelleg> _availableJellegek;
        private Jelleg _selectedJelleg;
        private ServerHelperSingleton serverHelper;
        private EventAggregatorSingleton eventAggregator;
        public Jelleg SelectedJelleg
        {
            get { return _selectedJelleg; }
            set { 
                _selectedJelleg = value;
                NotifyOfPropertyChange(() => SelectedJelleg);
                NotifyOfPropertyChange(() => CanRemoveJelleg);
                NotifyOfPropertyChange(() => CanModifyJelleg);
            }
        }

        public BindableCollection<Jelleg> AvailableJellegek
        {
            get { return _availableJellegek; }
            set { 
                _availableJellegek = value;
                NotifyOfPropertyChange(() => AvailableJellegek);
            }
        }

        public Telephely SelectedTelephely
        {
            get { return _selectedTelephely; }
            set { _selectedTelephely = value;
                NotifyOfPropertyChange(() => SelectedTelephely);
                GetJellegekAsync();
            }
        }
        private async void GetJellegekAsync() {
            Log.Debug("{Class} Jellegek letöltése. Telephely: {SelectedTelephely}", GetType(),SelectedTelephely);
            AvailableJellegek = await serverHelper.GetJellegekByTelephelyAsync(SelectedTelephely);
            if (AvailableJellegek.Count > 0)
            {
                Log.Debug("{Class} Sikeres letöltés", GetType());
            }
            else {
                Log.Debug("{Class} Sikertelen letöltés vagy nincs még jelleg a telephelyhez adva.", GetType());
            }
        }
        public BindableCollection<Telephely> AvailableTelephelyek
        {
            get { return _availableTelephelyek; }
            set { _availableTelephelyek = value;
                NotifyOfPropertyChange(() => AvailableTelephelyek);
            }
        }

        public bool JellegekIsVisible
        {
            get { return _jellegekIsVisible; }
            set { 
                _jellegekIsVisible = value;
                NotifyOfPropertyChange(() => JellegekIsVisible);
            }
        }
        public bool CanRemoveJelleg
        {
            get
            {
                return SelectedJelleg != null;
            }
        }
        public bool CanModifyJelleg
        {
            get
            {
                return SelectedJelleg != null;
            }
        }
        public void CreateJelleg()
        {
            Log.Debug("{Class} Jelleg létrehozása gomb megnyomva", GetType());
            JellegekIsVisible = false;
            Screen createScreen = SceneManager.CreateScene(Scenes.AddJelleg);
            eventAggregator.Subscribe(createScreen);
            eventAggregator.PublishOnUIThread(AvailableTelephelyek);
            ActivateItem(createScreen);
        }
        public async void RemoveJelleg()
        {
            Log.Debug("{Class} Jelleg törlése gomb megnyomva", GetType());
            if (await serverHelper.RemoveJellegAsync(SelectedJelleg))
            {
                Log.Debug("{Class} Sikeres törlés.", GetType());
                AvailableJellegek.Remove(SelectedJelleg);
                NotifyOfPropertyChange(() => AvailableJellegek);
            }
            else {
                Log.Debug("{Class} Sikertlen törlés.", GetType());
            }

        }

        public void ModifyJelleg()
        {
            Log.Debug("{Class} Jelleg módosítása gomb megnyomva", GetType());
            JellegekIsVisible = false;
            Screen modifyScreen = SceneManager.CreateScene(Scenes.ModifyJelleg);
            eventAggregator.Subscribe(modifyScreen);
            ActivateItem(modifyScreen);
            eventAggregator.PublishOnUIThread(SelectedJelleg);
            
        }

        public void Handle(Jelleg message)
        {
            if (message != SelectedJelleg)
            {
                JellegekIsVisible = true;
                if (!string.IsNullOrWhiteSpace(message.Name))
                {

                    AvailableJellegek.Remove(SelectedJelleg);
                    AvailableJellegek.Add(message);

                    NotifyOfPropertyChange(() => AvailableJellegek);
                }
            }
        }

        public void Handle((Telephely, Jelleg) message)
        {
            JellegekIsVisible = true;
            if (message.Item1.Name == SelectedTelephely.Name)
            {
                if (!string.IsNullOrWhiteSpace(message.Item2.Name))
                {
                    AvailableJellegek.Add(message.Item2);
                }
                NotifyOfPropertyChange(() => AvailableJellegek);
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
                    else if (telephely.Name != message.Name)
                    {
                        AvailableTelephelyek.Remove(telephely);
                        AvailableTelephelyek.Add(message);
                        NotifyOfPropertyChange(() => AvailableTelephelyek);

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

        public void Handle(BindableCollection<Telephely> message)
        {
            AvailableTelephelyek = message;
            if(AvailableTelephelyek.Count > 0)SelectedTelephely = AvailableTelephelyek.First();
        }
    }
}
