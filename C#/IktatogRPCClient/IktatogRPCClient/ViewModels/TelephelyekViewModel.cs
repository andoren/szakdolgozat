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
    class TelephelyekViewModel : Conductor<Screen>, IHandle<Telephely>
    {
        public TelephelyekViewModel()
        {
            LoadData();
        }
        private  void LoadData() {
            Telephelyek = serverHelper.GetTelephelyek();
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
            ActivateItem(SceneManager.CreateScene(Scenes.AddTelephely));
        }
        public void ModifyTelephely() {
            TelephelyekIsVisible = false;
            Screen modifyTelephelyScreen = SceneManager.CreateScene(Scenes.ModifyTelephely);
            eventAggregator.Subscribe(modifyTelephelyScreen);
            eventAggregator.PublishOnUIThread(SelectedTelephely);
            ActivateItem(modifyTelephelyScreen);
        }
 

        public async void RemoveTelephely() {
            if ( await serverHelper.RemoveTelephelyAsync(SelectedTelephely)) {
                eventAggregator.PublishOnUIThread(new RemovedItem(SelectedTelephely));
                Telephelyek.Remove(SelectedTelephely);
                NotifyOfPropertyChange(()=>Telephelyek);
            }
        }



        public void Handle(Telephely message)
        {
            if(Telephelyek.Where(x=>x.Name == message.Name).FirstOrDefault() == null) { 
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
    }
}
