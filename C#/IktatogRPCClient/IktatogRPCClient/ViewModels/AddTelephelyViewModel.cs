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


        public async override void DoAction()
        {
            Telephely newTelephely = await serverHelper.AddTelephelyAsync(TelephelyNeve);
            eventAggregator.PublishOnUIThread(newTelephely);
            TryClose();
        }

        protected override bool ValidateDataInForm()
        {
            return !(TelephelyNeve.Length < 5 || TelephelyNeve.Length > 50);
        }
    }
}
