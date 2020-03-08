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
                default:
                    throw new InvalidSceneExeption();
                    
            }
            return screen;
        }
    }
}
