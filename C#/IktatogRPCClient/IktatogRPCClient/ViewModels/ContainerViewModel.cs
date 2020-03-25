using Caliburn.Micro;
using IktatogRPCClient.Managers;
using IktatogRPCClient.Models.Managers;
using IktatogRPCClient.Models.Managers.Helpers.Client;
using IktatogRPCClient.Models.Scenes;
using System;
using Serilog;

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
            Log.Debug("{Class} Fooldal gomb megnyomva", GetType());

        }
        public void Iktatas() {
            ChangeScene(Scenes.Iktato);
            Log.Debug("{Class} Iktatás gomb megnyomva", GetType());
        }
        public void Kereses() {
            ChangeScene(Scenes.Kereses);
            Log.Debug("{Class} Keresés gomb megnyomva", GetType());
        }
        public void Torzs() {
            ChangeScene(Scenes.Torzs);
            Log.Debug("{Class} Törzs gomb megnyomva", GetType());
        }

        public async void Kijelentkezes() {
            LoaderIsVisible = true;
            Log.Debug("{Class} Kijelentkezés gomb megnyomva", GetType());
            Log.Debug("{Class} Várakozás a szerverre....", GetType());
            if (await serverHelper.LogoutAsync())
            {
                Log.Debug("{Class} Sikeres kijelentkezés.", GetType());
            }
            else {
                Log.Debug("{Class} Sikertelen kijelentkezés.", GetType());
            }
            IktatasViewModel._recentlyAddedIkonyvek.Clear();
            LoaderIsVisible = false;
            Log.Debug("{Class} Login ablak megnyitása", GetType());
            var manager = new WindowManager();
            manager.ShowWindow(new LoginViewModel(), null, null);
            Log.Debug("{Class} bezárása", GetType());
            TryClose();
        }
        public async void Kilepes() {
            LoaderIsVisible = true;
            try
            {
                Log.Debug("{Class} Kilépés gomb megnyomva", GetType());
                Log.Debug("{Class} Várakozás a szerverre....", GetType());
                if (await serverHelper.LogoutAsync())
                {
                    Log.Debug("{Class} Sikeres kijelentkezés.", GetType());
                }
                else
                {
                    Log.Debug("{Class} Sikertelen kijelentkezés.", GetType());
                }
            }
            catch (Exception e) {
                Log.Warning("Hiba a kilépéskor. {Message}",e.Message);
            }
            LoaderIsVisible = false;
            Log.Warning("Program bezárása.");
            TryClose();
        }


        private void ChangeCurrentMenuLabel(string name, string Role)
        {
            CurrentUser = $"{name} - {Role}";
        }
        private void ChangeScene(Scenes scene) {
            Log.Debug("{Class} Oldal váltása {Scene}", GetType(),scene);
            ActivateItem(SceneManager.CreateScene(scene));
        }

    }
}
