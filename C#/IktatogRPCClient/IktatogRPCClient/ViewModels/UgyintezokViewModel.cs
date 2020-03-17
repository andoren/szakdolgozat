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
    class UgyintezokViewModel:Conductor<Screen>,IHandle<Ugyintezo>,IHandle<(Telephely,Ugyintezo)>,IHandle<Telephely>,IHandle<RemovedItem>
    {
        public UgyintezokViewModel()
        {
            
            LoadData();
        }
        private void LoadData() {
            eventAggregator.Subscribe(this);
            ValaszthatoTelephely = serverHelper.GetTelephelyek();
            ValasztottTelephely = ValaszthatoTelephely.First();
      
        }
        private EventAggregatorSingleton eventAggregator = EventAggregatorSingleton.GetInstance(); 
        private ServerHelperSingleton serverHelper = ServerHelperSingleton.GetInstance();
        private BindableCollection<Telephely> _valaszthatoTelephely;
        private BindableCollection<Ugyintezo> _telephelyUgyintezoi;
        private Telephely _valasztottTelephely;
        private bool _ugyintezokIsVisible = true;
        public BindableCollection<Telephely> ValaszthatoTelephely
        {
            get { return _valaszthatoTelephely; }
            set {
                _valaszthatoTelephely = value;
                NotifyOfPropertyChange(() => ValaszthatoTelephely);
            }
        }
        public Telephely ValasztottTelephely
        {
            get { return _valasztottTelephely; }
            set {
                _valasztottTelephely = value;
                NotifyOfPropertyChange(() => ValasztottTelephely);
                GetUgyintezokAsync();
            }
        }
        private async void GetUgyintezokAsync() {
            TelephelyUgyintezoi = await serverHelper.GetUgyintezokByTelephelyAsync(ValasztottTelephely);
        }
        public bool UgyintezokIsVisible
        {
            get {
                return _ugyintezokIsVisible;
            }
            set {

                _ugyintezokIsVisible = value;
                NotifyOfPropertyChange(() => UgyintezokIsVisible);
                NotifyOfPropertyChange(() => CreationIsVisible);
               
            }
        }
        public bool CreationIsVisible
        {
            get {
                
                return !UgyintezokIsVisible;
            }

        }
        public BindableCollection<Ugyintezo> TelephelyUgyintezoi
        {
            get { return _telephelyUgyintezoi; }
            set {
                _telephelyUgyintezoi = value;
                NotifyOfPropertyChange(()=>TelephelyUgyintezoi);
            }
        }

        private Ugyintezo _valasztottUgyintezo;

        public Ugyintezo ValasztottUgyintezo
        {
            get { return _valasztottUgyintezo; }
            set
            {
                _valasztottUgyintezo = value;
                NotifyOfPropertyChange(() => ValasztottUgyintezo);
                NotifyOfPropertyChange(() => CanRemoveUgyintezo);
                NotifyOfPropertyChange(() => CanModifyUgyintezo);
            }
        }
        public void CreateUgyintezo() {
            UgyintezokIsVisible = false;
            Screen createScreen = SceneManager.CreateScene(Scenes.AddUgyintezo);
            eventAggregator.Subscribe(createScreen);
            ActivateItem(createScreen);
            eventAggregator.PublishOnUIThread(ValaszthatoTelephely);
        }
        public async void RemoveUgyintezo() {
            if (await serverHelper.RemoveUgyintezoFromTelephelyAsync(ValasztottUgyintezo))
            {
                TelephelyUgyintezoi.Remove(ValasztottUgyintezo);
                NotifyOfPropertyChange(() => TelephelyUgyintezoi);
            }
        
        }
        public bool CanRemoveUgyintezo {
            get {
                return ValasztottUgyintezo != null;
            }
        }
        public void ModifyUgyintezo() {
            UgyintezokIsVisible = false;
            Screen modifyScreen = SceneManager.CreateScene(Scenes.ModifyUgyintezo);
            eventAggregator.Subscribe(modifyScreen);
            ActivateItem(modifyScreen);
            eventAggregator.PublishOnUIThread(ValasztottUgyintezo);
            eventAggregator.PublishOnUIThread(ValaszthatoTelephely);
        }
        public bool CanModifyUgyintezo {
            get {
                return ValasztottUgyintezo != null;
            }
        }
        public void Handle(Ugyintezo message)
        {
            if (message != ValasztottUgyintezo) {
                UgyintezokIsVisible = true;
                if (!string.IsNullOrWhiteSpace(message.Name)) {

                    TelephelyUgyintezoi.Remove(ValasztottUgyintezo);
                    TelephelyUgyintezoi.Add(message);
                    
                    NotifyOfPropertyChange(() => TelephelyUgyintezoi);
                }
            }
        }

        public void Handle((Telephely, Ugyintezo) message)
        {
            UgyintezokIsVisible = true;
            if (message.Item1.Name == ValasztottTelephely.Name)
            {             
                if (!string.IsNullOrWhiteSpace(message.Item2.Name))
                {
                    TelephelyUgyintezoi.Add(message.Item2);
                }
                NotifyOfPropertyChange(() => TelephelyUgyintezoi);
            }
        }
        public void Handle(Telephely message)
        {
            if (message != ValasztottTelephely && message != null)
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
