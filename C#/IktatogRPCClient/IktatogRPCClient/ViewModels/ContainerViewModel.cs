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
        }
        SceneManager sceneManager = new SceneManager();
        private string _currentMenu;
        private User user;
        public string CurrentMenu
        {
            get { return _currentMenu; }
            set { _currentMenu = value; }
        }

        public void Fooldal() {
            ChangeScene(Scenes.Fooldal);
            ChangeCurrentMenuLabel("Főoldal");

        }
        public void Iktatas() {
            ChangeScene(Scenes.Iktato);
            ChangeCurrentMenuLabel("Iktató");
        }
        public void Kereses() {
            ChangeScene(Scenes.Kereses);
            ChangeCurrentMenuLabel("Keresés");
        }
        public void Torzs() {
            ChangeScene(Scenes.Torzs);
            ChangeCurrentMenuLabel("Törzs");
        }

        public void Kijelentkezes() {
            var manager = new WindowManager();
            manager.ShowWindow(new LoginViewModel(), null, null);
            TryClose();
        }
        public void Kilepes() {
            TryClose();
        }


        private void ChangeCurrentMenuLabel(string label)
        {
            CurrentMenu = label;
        }
        private void ChangeScene(Scenes scene) {
            ActivateItem(sceneManager.CreateScene(scene));
        }
    }
}
