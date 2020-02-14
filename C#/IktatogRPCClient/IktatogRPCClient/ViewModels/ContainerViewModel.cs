using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Managers;
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
       
        public ContainerViewModel(User user)
        {
            Fooldal();
            this.user = user;
            CurrentMenu = "Főoldal";
            ChangeCurrentMenuLabel(user.Fullname,user.Privilege.Name);
        }
        SceneManager sceneManager = new SceneManager();
        private string _currentMenu;
        private User user;
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
            LoaderIsVisible = false;
        }
        public void Iktatas() {
            ChangeScene(Scenes.Iktato);
            LoaderIsVisible = true;
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
            ActivateItem(sceneManager.CreateScene(scene));
        }

    }
}
