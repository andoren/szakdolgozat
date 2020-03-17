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
    class CsoportokViewModel : Conductor<Screen>, IHandle<Csoport>,IHandle<(Telephely,Csoport)>, IHandle<Telephely>,IHandle<RemovedItem>
    {

        public CsoportokViewModel()
        {
            LoadData();
        }
        ServerHelperSingleton serverHelper;
        EventAggregatorSingleton eventAggregator ;
        private Csoport _selectedCsoport;
        private BindableCollection<Csoport> _telephelyCsoportjai;
        private BindableCollection<Telephely> _valaszthatoTelephely;
        private Telephely _selectedTelephely;

        private bool _csoportokIsVisible = true;

        public bool CsoportokIsVisible
        {
            get { return _csoportokIsVisible; }
            set { _csoportokIsVisible = value;
                NotifyOfPropertyChange(()=>CsoportokIsVisible);
                NotifyOfPropertyChange(() =>CreationIsVisible);
            }
        }
        public bool CreationIsVisible { get {
                return !CsoportokIsVisible;    
            }
        }
        public Telephely SelectedTelephely
        {
            get { return _selectedTelephely; }
            set { 
                _selectedTelephely = value;
                NotifyOfPropertyChange(()=>SelectedTelephely);
                GetCsoportokAsync();
                NotifyOfPropertyChange(()=>TelephelyCsoportjai);
            }
        }
        private async void GetCsoportokAsync() {
            TelephelyCsoportjai = await serverHelper.GetCsoportokByTelephelyAsync(SelectedTelephely);

        }
        public BindableCollection<Telephely> ValaszthatoTelephely
        {
            get { return _valaszthatoTelephely; }
            set { _valaszthatoTelephely = value; }
        }


        public BindableCollection<Csoport> TelephelyCsoportjai
        {
            get { return _telephelyCsoportjai; }
            set {
                _telephelyCsoportjai = value;
                
            }
        }
        public Csoport SelectedCsoport
        {
            get { return _selectedCsoport; }
            set { 
                _selectedCsoport = value;
                NotifyOfPropertyChange(() => SelectedCsoport);
                NotifyOfPropertyChange(() => CanModifyCsoport);
                NotifyOfPropertyChange(() => CanRemoveCsoport);
            }
        }

        public bool CanModifyCsoport {
            get {
                return SelectedCsoport != null;
            }
        }
        public bool CanRemoveCsoport {
            get {
                return SelectedCsoport != null;
               }
        }
        public void CreateCsoport() {
            CsoportokIsVisible = false;
            Screen createScreen = SceneManager.CreateScene(Scenes.AddCsoport);
            eventAggregator.Subscribe(createScreen);
            ActivateItem(createScreen);
            eventAggregator.PublishOnUIThread(ValaszthatoTelephely);
        }
        public void ModifyCsoport() {
            CsoportokIsVisible = false;
            Screen modifyScreen = SceneManager.CreateScene(Scenes.ModifyCsoport);
            eventAggregator.Subscribe(modifyScreen);
            ActivateItem(modifyScreen);
            eventAggregator.PublishOnUIThread(SelectedCsoport);
            
        }
        public async void RemoveCsoport() {
            if ( await serverHelper.RemoveCsoportAsync(SelectedCsoport))
            {
                TelephelyCsoportjai.Remove(SelectedCsoport);
                NotifyOfPropertyChange(() => TelephelyCsoportjai);
            }
        }
        public void Handle(Csoport message)
        {
            if (message != SelectedCsoport)
            {
                CsoportokIsVisible = true;
                Csoport modifiedCsoport = TelephelyCsoportjai.Where(x => x.Id == message.Id).FirstOrDefault();
                if (modifiedCsoport != null) {
                    TelephelyCsoportjai.Remove(SelectedCsoport);
                    TelephelyCsoportjai.Add(message);

                    NotifyOfPropertyChange(() => TelephelyCsoportjai);
                }
                else if (!string.IsNullOrWhiteSpace(message.Name))
                {
                    TelephelyCsoportjai.Add(message);
                    NotifyOfPropertyChange(() => TelephelyCsoportjai);
                }
            }
        }
        private void LoadData()
        {
            eventAggregator = EventAggregatorSingleton.GetInstance();
            eventAggregator.Subscribe(this);
            serverHelper = ServerHelperSingleton.GetInstance();
            ValaszthatoTelephely = serverHelper.GetTelephelyek();
            SelectedTelephely = ValaszthatoTelephely.First();
            
        }

        public void Handle((Telephely, Csoport) message)
        {
            CsoportokIsVisible = true;
            if (message.Item1.Name == SelectedTelephely.Name) {
                TelephelyCsoportjai.Add(message.Item2);
            }
        }

        public void Handle(Telephely message)
        {
            if (message != SelectedTelephely && message != null)
            {
                Telephely telephely = ValaszthatoTelephely.Where(x => x.Id == message.Id).FirstOrDefault();
                if (telephely == null)
                {
                    ValaszthatoTelephely.Add(message);
                    NotifyOfPropertyChange(() => ValaszthatoTelephely);
                }
                else if (telephely.Name != message.Name)
                {
                    ValaszthatoTelephely.Remove(telephely);
                    ValaszthatoTelephely.Add(message);
                    NotifyOfPropertyChange(() => ValaszthatoTelephely);

                }
            }
        }

        public void Handle(RemovedItem message)
        {

             if (message.Item is Telephely)
            {
                Telephely telephely = ValaszthatoTelephely.Where(x => x.Id == (message.Item as Telephely).Id).FirstOrDefault();
                ValaszthatoTelephely.Remove(telephely);
                NotifyOfPropertyChange(() => ValaszthatoTelephely);
            }
        }
    }
}
