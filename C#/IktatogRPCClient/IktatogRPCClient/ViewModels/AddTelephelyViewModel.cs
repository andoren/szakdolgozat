using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.ViewModels
{
    class AddTelephelyViewModel : TorzsDataView<Telephely>
    {
        private string _telephelyNeve = "";

        public string TelephelyNeve
        {
            get { return _telephelyNeve; }
            set { _telephelyNeve = value;
                NotifyOfPropertyChange(() => TelephelyNeve);
                NotifyOfPropertyChange(() => CanDoAction);
            }
        }


        public override void DoAction()
        {
            Telephely newTelephely = serverHelper.AddTelephely(TelephelyNeve);
            eventAggregator.PublishOnUIThread(newTelephely);
            TryClose();
        }
        public bool CanDoAction {
            get{ 
                if (TelephelyNeve.Length > 4 && TelephelyNeve.Length < 100) return true;
                else return false;
            }
        }
    }
}
