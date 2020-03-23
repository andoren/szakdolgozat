using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Models;
using IktatogRPCClient.Models.Managers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.ViewModels
{
    class ModifyUgyintezoViewModel:TorzsDataView<Ugyintezo>,IHandle<(Telephely,Ugyintezo)>
    {

        public ModifyUgyintezoViewModel()
        {
            
        }
        private Ugyintezo _modifiedUgyintezo = new Ugyintezo();

        public Ugyintezo ModifiedUgyintezo
        {
            get { return _modifiedUgyintezo; }
            set {
                _modifiedUgyintezo = value;
                NotifyOfPropertyChange(() => ModifiedUgyintezo);
            }
        }
        public Telephely SelectedTelephely { get; set; }
        private string _newName;

        public string NewName
        {
            get { return _newName; }
            set { 
                _newName = value;
                NotifyOfPropertyChange(() => NewName);
                NotifyOfPropertyChange(() => CanDoAction);
            }
        }



        public async override void DoAction()
        {
            Log.Debug("{Class} módosítás gomb megnyomva.", GetType());
            Ugyintezo modifiedUgyintezo = new Ugyintezo() { Id = ModifiedUgyintezo.Id, Name = NewName };
            Log.Debug("{Class} a szerver meghívása.... Adat: {ModifiedUgyintezo}", GetType(), modifiedUgyintezo);
            if (await serverHelper.ModifyUgyintezoAsync(modifiedUgyintezo))
            {
                Log.Debug("{Class} Sikeres módosítás.", GetType());
                eventAggregator.PublishOnUIThread((SelectedTelephely, modifiedUgyintezo));
                
            }
            else {
                eventAggregator.PublishOnUIThread((SelectedTelephely, new Ugyintezo()));
                Log.Debug("{Class} Sikertelen módosítás.", GetType());
            }
            TryClose();
        }

        protected override bool ValidateDataInForm()
        {
            return (NewName.Length > 4 && NewName.Length < 100);
        }

        public void Handle((Telephely, Ugyintezo) message)
        {
            SelectedTelephely = new Telephely(message.Item1);
            ModifiedUgyintezo = new Ugyintezo(message.Item2);
            NewName = message.Item2.Name;
        }
    }
}
