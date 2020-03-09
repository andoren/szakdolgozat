﻿using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Managers;
using IktatogRPCClient.Models.Managers.Helpers.Client;
using IktatogRPCClient.Models.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IktatogRPCClient.ViewModels
{
    class ContainerViewModel : Conductor<Screen>
    {
       
        public ContainerViewModel()
        {
            Fooldal();
       
            CurrentMenu = "Főoldal";
            ChangeCurrentMenuLabel(userHelper.CurrentUser.Fullname, userHelper.CurrentUser.Privilege.Name);
        }
        private string _currentMenu;

        private UserHelperSingleton userHelper = UserHelperSingleton.GetInstance();
        public string CurrentMenu
        {
            get { return _currentMenu; }
            set { _currentMenu = value; }
        }
        private bool _loaderIsVisible;

        public bool LoaderIsVisible
        {
            get { return _loaderIsVisible; }
            set { _loaderIsVisible = value;
                NotifyOfPropertyChange(()=>LoaderIsVisible);
            }
        }

        public void Fooldal() {
            ChangeScene(Scenes.Fooldal);
            
        }
        public void Iktatas() {
            ChangeScene(Scenes.Iktato);
           
        }
        public void Kereses() {
            ChangeScene(Scenes.Kereses);
         
        }
        public void Torzs() {
            ChangeScene(Scenes.Torzs);
        
        }

        public void Kijelentkezes() {
            var manager = new WindowManager();
            manager.ShowWindow(new LoginViewModel(), null, null);
            TryClose();
        }
        public void Kilepes() {
            TryClose();
        }


        private void ChangeCurrentMenuLabel(string name, string Role)
        {
            CurrentMenu = $"{name} - {Role}";
        }
        private void ChangeScene(Scenes scene) {
            ActivateItem(SceneManager.CreateScene(scene));
        }

    }
}
