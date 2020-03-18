using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Managers;
using IktatogRPCClient.Models.Managers;
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
            ChangeCurrentMenuLabel(UserHelperSingleton.CurrentUser.Fullname, UserHelperSingleton.CurrentUser.Privilege.Name);
        }
        private bool _loaderIsVisible;
        private string _currentUSer;
        private ServerHelperSingleton serverHelper = ServerHelperSingleton.GetInstance();
        public string CurrentUser
        {
            get { return _currentUSer; }
            set { _currentUSer = value; }
        }

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

        public async void Kijelentkezes() {
            LoaderIsVisible = true;
           
            await serverHelper.LogoutAsync();
            LoaderIsVisible = false;
            var manager = new WindowManager();
            manager.ShowWindow(new LoginViewModel(), null, null);
            TryClose();
        }
        public async void Kilepes() {
            LoaderIsVisible = true;
            try
            {


                await serverHelper.LogoutAsync();
            }
            catch (Exception) { }
            LoaderIsVisible = false;
            TryClose();
        }


        private void ChangeCurrentMenuLabel(string name, string Role)
        {
            CurrentUser = $"{name} - {Role}";
        }
        private void ChangeScene(Scenes scene) {
            ActivateItem(SceneManager.CreateScene(scene));
        }

    }
}
