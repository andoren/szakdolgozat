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
    class ModifyTelephelyViewModel : TorzsDataView<Telephely>, IHandle<Telephely>
    {

        private Telephely _telephely;

        public Telephely Telephely
        {
            get { return _telephely; }
            set { _telephely = value; }
        }

        private string _telephelyNeve = "";

        public string TelephelyNeve
        {
            get { return _telephelyNeve; }
            set { _telephelyNeve = value;
                NotifyOfPropertyChange(()=>TelephelyNeve);
                NotifyOfPropertyChange(() => CanDoAction);
            }
        }

        public async override void DoAction()
        {
            Log.Debug("{Class} módosítás gomb megnyomva.", GetType());
            Telephely.Name = TelephelyNeve;
            Log.Debug("{Class} a szerver meghívása... Adat: {TelephelyNeve}", GetType(), TelephelyNeve);
            bool success = await serverHelper.ModifyTelephelyAsync(Telephely);
            if (success)
            {
                Log.Debug("{Class} Sikeres módosítás.", GetType());
                eventAggregator.PublishOnUIThread(Telephely);
            }
            else {
                eventAggregator.PublishOnUIThread(new Telephely());
                Log.Debug("{Class} Sikertelen módosítás.", GetType());
            }
            TryClose();
        }
        
        public void Handle(Telephely message)
        {
            Telephely = new Telephely(message);
            TelephelyNeve = message.Name;
        }

        protected override bool ValidateDataInForm()
        {
            return !(TelephelyNeve.Length < 5 || TelephelyNeve.Length > 50);
        }
    }
}
