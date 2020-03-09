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

namespace IktatogRPCClient.ViewModels
{
    class TorzsViewModel:Conductor<Screen>.Collection.AllActive
    {
        
        public TorzsViewModel()
        {
            UgyintezokViewModel = SceneManager.CreateScene(Scenes.Ugyintezok);
            Items.Add(UgyintezokViewModel);
        }
        public Screen UgyintezokViewModel { get; private set; }
        private UserHelperSingleton userHelper = UserHelperSingleton.GetInstance();

        public bool AdminSettingsIsVisible {
            get{
                return userHelper.IsAdmin;
            }
        }

    }
}
