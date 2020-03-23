using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Managers;
using IktatogRPCClient.Models;
using IktatogRPCClient.Models.Managers;
using IktatogRPCClient.Models.Managers.Helpers.Client;
using IktatogRPCClient.Models.Scenes;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.ViewModels
{
    class TorzsViewModel:Conductor<Screen>.Collection.AllActive
    {
        
        public TorzsViewModel()
        {
            Initialize();
            GetTelephelyekAsync();
        }
        private UserProxy user = new UserProxy(UserHelperSingleton.CurrentUser);
        private  void Initialize() {
            Log.Debug("{Class} UserControllerek inicializációja.", GetType());
            UgyintezokViewModel = SceneManager.CreateScene(Scenes.Ugyintezok);
            JellegekViewModel = SceneManager.CreateScene(Scenes.Jellegek);
            PartnersViewModel = SceneManager.CreateScene(Scenes.Partnerek);
            PartnerekUgyintezoiViewModel = SceneManager.CreateScene(Scenes.PartnerekUgyintezoi);
            if (user.IsAdmin)
            {
                FelhasznalokViewModel = SceneManager.CreateScene(Scenes.Felhasznalok);
                TelephelyekViewModel = SceneManager.CreateScene(Scenes.Telephelyek);
                CsoportokViewModel = SceneManager.CreateScene(Scenes.Csoportok);
                EvekViewModel = SceneManager.CreateScene(Scenes.Evek);
 
            }
            AddScreensToEventAggregator();
      

        }
        EventAggregatorSingleton eventAggregator = EventAggregatorSingleton.GetInstance();
        private async void GetTelephelyekAsync() {
            Log.Debug("{Class} Telephelyek letöltése.", GetType());
            LoaderIsVisible = true;
            BindableCollection<Telephely> telephelyek = await ServerHelperSingleton.GetInstance().GetTelephelyekAsync();
            await eventAggregator.PublishOnUIThreadAsync(telephelyek);

            LoaderIsVisible = false;
        }
        public Screen PartnerekUgyintezoiViewModel { get; set; }
        public Screen UgyintezokViewModel { get; private set; }
        public Screen TelephelyekViewModel { get; private set; }

        public Screen CsoportokViewModel { get; private set; }
        public Screen JellegekViewModel { get; private set; }
        public Screen FelhasznalokViewModel { get; private set; }
        public Screen EvekViewModel { get; private set; }
        public Screen PartnersViewModel { get; private set; }
        

        public bool LoaderIsVisible { get; private set; }

        private void AddScreensToEventAggregator() {
           
            foreach (var screen in Items)
            {
                eventAggregator.Subscribe(screen);
            }
        }
        private void AddScreensToShowIt(params Screen[] screens) {
            foreach (var screen in screens)
            {
                Items.Add(screen);
            }
            
        }

    }
}
