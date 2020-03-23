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
    class TelephelyekViewModel : Conductor<Screen>, IHandle<Telephely>,IHandle<BindableCollection<Telephely>>
    {
        public TelephelyekViewModel()
        {
            eventAggregator.Subscribe(this);
        }


        private ServerHelperSingleton serverHelper = ServerHelperSingleton.GetInstance();
        private BindableCollection<Telephely> _telephelyek;
        private Telephely _selectedTelephely;
        private EventAggregatorSingleton eventAggregator = EventAggregatorSingleton.GetInstance();
        public BindableCollection<Telephely> Telephelyek
        {
            get { return _telephelyek; }
            set { _telephelyek = value;
                NotifyOfPropertyChange(()=>Telephelyek);
            }
        }
        private bool _telephelyekIsVisible = true;

        public bool TelephelyekIsVisible
        {
            get { return _telephelyekIsVisible; }
            set { 
                _telephelyekIsVisible = value;
                NotifyOfPropertyChange(() => TelephelyekIsVisible);
                NotifyOfPropertyChange(() => CreationIsVisible);
            }
        }
        public bool CreationIsVisible {
            get {
                return !TelephelyekIsVisible;
            }
        }
        public bool CanRemoveTelephely
        {
            get
            {
                return SelectedTelephely != null;
            }
        }
        public bool CanModifyTelephely
        {
            get
            {
                return SelectedTelephely != null;
            }
        }
        public Telephely SelectedTelephely
        {
            get { return _selectedTelephely; }
            set { 
                _selectedTelephely = value;
                NotifyOfPropertyChange(() => CanModifyTelephely);
                NotifyOfPropertyChange(() => CanRemoveTelephely);
            }
        }
        public void CreateTelephely() {
            TelephelyekIsVisible = false;
            Log.Debug("{Class} hozzáadás gomb megnyomva.", GetType());
            ActivateItem(SceneManager.CreateScene(Scenes.AddTelephely));
        }
        public void ModifyTelephely() {
            Log.Debug("{Class} módosítás gomb megnyomva.", GetType());
            TelephelyekIsVisible = false;
            Screen modifyTelephelyScreen = SceneManager.CreateScene(Scenes.ModifyTelephely);
            eventAggregator.Subscribe(modifyTelephelyScreen);
            eventAggregator.PublishOnUIThread(SelectedTelephely);
            ActivateItem(modifyTelephelyScreen);
        }
 

        public async void RemoveTelephely() {
            Log.Debug("{Class} törlés gomb megnyomva.", GetType());
            Log.Debug("{Class} törlés a következő adatokkal: {SelectedTelephely}", GetType(), SelectedTelephely);
            if (await serverHelper.RemoveTelephelyAsync(SelectedTelephely))
            {
                Log.Debug("{Class} Sikeres törlés", GetType());
                eventAggregator.PublishOnUIThread(new RemovedItem(SelectedTelephely));
                Telephelyek.Remove(SelectedTelephely);
                NotifyOfPropertyChange(() => Telephelyek);
            }
            else {
                Log.Debug("{Class} Sikertelen törlés", GetType());
            }
        }



        public void Handle(Telephely message)
        {
            Telephely telephely1 = Telephelyek.Where(x => x.Name == message.Name).FirstOrDefault();
            if (telephely1 == null) { 
                if (message != SelectedTelephely) {
                
                    Telephely telephely = Telephelyek.Where(x => x.Id == message.Id).FirstOrDefault();
                    if (telephely != null) {
                        Telephelyek.Remove(SelectedTelephely);
                        Telephelyek.Add(message);
                    }
                    else if (!string.IsNullOrWhiteSpace(message.Name))
                    {

                        Telephelyek.Add(message);
                    }
                    TelephelyekIsVisible = true;
                    NotifyOfPropertyChange(()=>Telephelyek);
                
                }
            }

        }

        public void Handle(BindableCollection<Telephely> message)
        {
            Telephelyek = new BindableCollection<Telephely>(message);
        }
    }
}
