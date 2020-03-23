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
    class UgyintezokViewModel:Conductor<Screen>,IHandle<Ugyintezo>,
        IHandle<(Telephely,Ugyintezo)>,IHandle<Telephely>,IHandle<RemovedItem>,IHandle<BindableCollection<Telephely>>
    {
        public UgyintezokViewModel()
        {
            
            LoadData();
        }
        private void LoadData() {
            eventAggregator.Subscribe(this);
       
      
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
                if (value != null)
                {
                    _valasztottTelephely = value;
                    NotifyOfPropertyChange(() => ValasztottTelephely);
                    GetUgyintezokAsync();

                }
             
           
            }
        }
        private async void GetUgyintezokAsync() {
            Log.Debug("{Class} Ügyintézők letöltése. Paraméter: {ValasztottTelephely}", GetType(), ValasztottTelephely);
            TelephelyUgyintezoi = await serverHelper.GetUgyintezokByTelephelyAsync(ValasztottTelephely);
            if (TelephelyUgyintezoi.Count > 0)
            {
                Log.Debug("{Class} Sikeres letöltés.", GetType());
            }
            else
            {
                Log.Debug("{Class} Sikertelen letöltés vagy nincs még ügyintéző a telephelyhez.", GetType());
            }
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
            Log.Debug("{Class} Hozzáadás gomb megnyomva.", GetType());
            UgyintezokIsVisible = false;
            Screen createScreen = SceneManager.CreateScene(Scenes.AddUgyintezo);
            eventAggregator.Subscribe(createScreen);
            ActivateItem(createScreen);
            eventAggregator.PublishOnUIThread(ValaszthatoTelephely);
        }
        public async void RemoveUgyintezo() {
            Log.Debug("{Class} Törlés gomb megnyomva.", GetType());
            Log.Debug("{Class} szerver hívása a következő adatokkal: {ValasztottUgyintezo}", GetType(), ValasztottUgyintezo);
            if (await serverHelper.RemoveUgyintezoFromTelephelyAsync(ValasztottUgyintezo))
            {
                Log.Debug("{Class} Sikeres törlés.", GetType());
                TelephelyUgyintezoi.Remove(ValasztottUgyintezo);
                NotifyOfPropertyChange(() => TelephelyUgyintezoi);
            }
            else{
                Log.Debug("{Class} Sikertelen törlés.", GetType());
            }
        
        }
        public bool CanRemoveUgyintezo {
            get {
                return ValasztottUgyintezo != null;
            }
        }
        public void ModifyUgyintezo() {
            Log.Debug("{Class} Módosítás gomb megnyomva.", GetType());
            UgyintezokIsVisible = false;
            Screen modifyScreen = SceneManager.CreateScene(Scenes.ModifyUgyintezo);
            eventAggregator.Subscribe(modifyScreen);
            ActivateItem(modifyScreen);
            eventAggregator.PublishOnUIThread((ValasztottTelephely, ValasztottUgyintezo));
           
        }
        public bool CanModifyUgyintezo {
            get {
                return ValasztottUgyintezo != null;
            }
        }
        public void Handle(Ugyintezo message)
        {
           if(ValasztottUgyintezo != message) UgyintezokIsVisible = true;
        }

        public void Handle((Telephely, Ugyintezo) message)
        {
            
       
                Ugyintezo temp = TelephelyUgyintezoi.Where(x => x.Name == message.Item2.Name).FirstOrDefault();
                if (temp == null) {
                    UgyintezokIsVisible = true;
                    {
                        Ugyintezo tempid = TelephelyUgyintezoi.Where(x => x.Id == message.Item2.Id).FirstOrDefault();
                        if (tempid != null)
                        {

                            TelephelyUgyintezoi.Remove(ValasztottUgyintezo);
                            TelephelyUgyintezoi.Add(message.Item2);


                        }
                        else if(message.Item1.Name == ValasztottTelephely.Name) {
                            TelephelyUgyintezoi.Add(message.Item2);
                        }
                    }
                }
    
                NotifyOfPropertyChange(() => TelephelyUgyintezoi);
            
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

        public void Handle(BindableCollection<Telephely> message)
        {
            ValaszthatoTelephely = message;
            if (ValaszthatoTelephely.Count > 0)ValasztottTelephely = ValaszthatoTelephely.First();
        }
    }
}
