using Caliburn.Micro;
using IktatogRPCClient.Models.Scenes;
using IktatogRPCClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.Managers
{
    class SceneManager
    {
        public static Screen CreateScene(Scenes scene)
        {
            Screen screen = null;
            switch (scene) {
                case Scenes.Fooldal:
                    {
                        screen = new FooldalViewModel();
                        break;
                    }
                    
                case Scenes.Iktato:
                    {
                        screen = new IktatasViewModel();
                        break;
                    }
                case Scenes.Kereses:
                    {
                        screen = new KeresesViewModel();
                        break;
                    }
                case Scenes.Torzs:
                    {
                        screen = new TorzsViewModel();
                        break;
                    }
                case Scenes.Ugyintezok:
                    {
                        screen = new UgyintezokViewModel();
                        break;
                    }
                case Scenes.AddUgyintezo:
                    {
                        screen = new AddUgyintezoViewModel();
                        break;
                    }
                case Scenes.ModifyUgyintezo:
                    {
                        screen = new ModifyUgyintezoViewModel();
                        break;
                    }
                case Scenes.Telephelyek:
                    {
                        screen = new TelephelyekViewModel();
                        break;
                    }
                case Scenes.AddTelephely:
                    {
                        screen = new AddTelephelyViewModel();
                        break;
                    }
                case Scenes.ModifyTelephely:
                    {
                        screen = new ModifyTelephelyViewModel();
                        break;
                    }
                case Scenes.Csoportok:
                    {
                        screen = new CsoportokViewModel();
                        break;
                    }
                case Scenes.AddCsoport:
                    {
                        screen = new AddCsoportViewModel();
                        break;
                    }
                case Scenes.ModifyCsoport:
                    {
                        screen = new ModifyCsoportViewModel();
                        break;
                    }
                case Scenes.Jellegek:
                    {
                        screen = new JellegekViewModel();
                        break;
                    }
                case Scenes.AddJelleg:
                    {
                        screen = new AddJellegViewModel();
                        break;
                    }
                case Scenes.ModifyJelleg:
                    {
                        screen = new ModifyJellegViewModel();
                        break;
                    }
                case Scenes.Partnerek:
                    {
                        screen = new PartnerekViewModel();
                        break;
                    }
                case Scenes.PartnerekUgyintezoi:
                    {
                        screen = new PartnerekUgyintezoiViewModel();
                        break;
                    }
                case Scenes.AddPartner:
                    {
                        screen = new AddPartnerViewModel();
                        break;
                    }
                case Scenes.ModifyPartner:
                    {
                        screen = new ModifyPartnerViewModel();
                        break;
                    }
                case Scenes.AddPartnerUgyintezo:
                    {
                        screen = new AddPartnerUgyintezoViewModel();
                        break;
                    }
                case Scenes.ModifyPartnerUgyintezo:
                    {
                        screen = new ModifyPartnerUgyintezoViewModel();
                        break;
                    }
                case Scenes.Felhasznalok:
                    {
                        screen = new FelhasznalokViewModel();
                        break;
                    }
                case Scenes.AddFelhasznalo:
                    {
                        screen = new AddFelhasznaloViewModel();
                        break;
                    }
                case Scenes.ModifyFelhasznalo:
                    {
                        screen = new ModifyFelhasznaloViewModel();
                        break;
                    }
                default:
                    throw new InvalidSceneExeption();
                    
            }
            return screen;
        }
    }
}
