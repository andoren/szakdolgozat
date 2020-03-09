﻿using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Managers;
using IktatogRPCClient.Models.Managers;
using IktatogRPCClient.Models.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.ViewModels
{
    class UgyintezokViewModel:Conductor<Screen>,IHandle<Ugyintezo>,IHandle<(Telephely,Ugyintezo)>
    {
        public UgyintezokViewModel()
        {
            eventAggergator.Subscribe(this);
            LoadData();
        }
        private void LoadData() {
            ValaszthatoTelephely = serverHelper.GetTelephelyek();
            ValasztottTelephely = ValaszthatoTelephely.First();
      
        }
        private EventAggregatorSingleton eventAggergator = EventAggregatorSingleton.GetInstance(); 
        private ServerHelper serverHelper = ServerHelper.GetInstance();
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
                TelephelyUgyintezoi = serverHelper.GetUgyintezokByTelephely(value);
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
            UgyintezokIsVisible = false;
            ActivateItem(SceneManager.CreateScene(Scenes.AddUgyintezo));
        }
        public void RemoveUgyintezo() {
            if (serverHelper.RemoveUgyintezoFromTelephely(ValasztottUgyintezo))
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
            eventAggergator.Subscribe(modifyScreen);
            ActivateItem(modifyScreen);
            eventAggergator.PublishOnUIThread(ValasztottUgyintezo);               
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

        ~UgyintezokViewModel() {
            eventAggergator.Unsubscribe(this);
        }
    }
}
