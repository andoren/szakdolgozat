using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Models;
using Serilog;
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
            Log.Debug("{Class} hozzáadása gomb megnyomva.", GetType());
            Log.Debug("{Class} Várakozás a szerverre... Adat:{TelephelyNeve}", GetType(),TelephelyNeve);
            Telephely newTelephely = await serverHelper.AddTelephelyAsync(TelephelyNeve);
            if (newTelephely.Id != -1)
            {
                Log.Debug("{Class} Sikeres hozzáadás.", GetType());
                Log.Debug("{Class} Telephely hírdetése.", GetType());
                eventAggregator.PublishOnUIThread(newTelephely);
            }
            else {
                Log.Debug("{Class} Sikertelen hozzáadás.", GetType());
            }
     
            Log.Debug("{Class} bezárása.", GetType());
            TryClose();
        }

        protected override bool ValidateDataInForm()
        {
            return !(TelephelyNeve.Length < 5 || TelephelyNeve.Length > 50);
        }
    }
}
