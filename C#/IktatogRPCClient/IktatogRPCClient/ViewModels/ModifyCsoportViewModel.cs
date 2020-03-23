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
    class ModifyCsoportViewModel : TorzsDataView<Csoport>,IHandle<Csoport>
    {

        private string _newName;

        private string _newKod;
        private Csoport _modifiedCsoport;

        public Csoport ModifiedCsoport
        {
            get { return _modifiedCsoport; }
            set { _modifiedCsoport = value; }
        }

        public string NewKod
        {
            get { return _newKod; }
            set { _newKod = value;
                NotifyOfPropertyChange(() => CanDoAction);
                NotifyOfPropertyChange(() => NewKod);
            }
        }

        public string NewName
        {
            get { return _newName; }
            set { 
                _newName = value;
                NotifyOfPropertyChange(() => CanDoAction);
                NotifyOfPropertyChange(() => NewName);
            }
        }

        public async override void DoAction()
        {
            Log.Debug("{Class} módosítás gomb megnyomva", GetType());
            Csoport modifiedCsoport = new Csoport() { Id = ModifiedCsoport.Id, Name = NewName, Shortname = NewKod };
            Log.Debug("{Class} módosítás. Új csoport: {ModifiedCsoport}", GetType(), modifiedCsoport);
            if (await serverHelper.ModifyCsoportAsync(modifiedCsoport))
            {
                Log.Debug("{Class} Sikeres módosítás", GetType());
                eventAggregator.PublishOnUIThread(modifiedCsoport);
                TryClose();
            }
            else {
                Log.Debug("{Class} Sikertlen módosítás", GetType());
            }
        }

        public void Handle(Csoport message)
        {
            ModifiedCsoport = message;
            NewName = message.Name;
            NewKod = message.Shortname;
        }

        protected override bool ValidateDataInForm()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(NewName) || string.IsNullOrWhiteSpace(NewKod))
            {
                isValid = false;
            }
            else if (NewName.Length < 5 || NewName.Length > 100 || NewKod.Length < 1 || NewKod.Length > 3) isValid = false;
            return isValid;
        }
    }
}
