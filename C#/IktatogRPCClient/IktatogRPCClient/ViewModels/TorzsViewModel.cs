﻿using Caliburn.Micro;
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
            TelephelyekViewModel = SceneManager.CreateScene(Scenes.Telephelyek);
            CsoportokViewModel = SceneManager.CreateScene(Scenes.Csoportok);
            JellegekViewModel = SceneManager.CreateScene(Scenes.Ugyintezok);
            UsersViewModel = SceneManager.CreateScene(Scenes.Telephelyek);
            PartnersViewModel = SceneManager.CreateScene(Scenes.Csoportok);
            Items.Add(UgyintezokViewModel);
            Items.Add(TelephelyekViewModel);
            Items.Add(CsoportokViewModel);
        }
        public Screen UgyintezokViewModel { get; private set; }
        public Screen TelephelyekViewModel { get; private set; }

        public Screen CsoportokViewModel { get; private set; }
        public Screen JellegekViewModel { get; private set; }
        public Screen UsersViewModel { get; private set; }

        public Screen PartnersViewModel { get; private set; }
        private UserHelperSingleton userHelper = UserHelperSingleton.GetInstance();

        public bool AdminSettingsIsVisible {
            get{
                return userHelper.IsAdmin;
            }
        }

    }
}
