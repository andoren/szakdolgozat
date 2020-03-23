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
    class ModifyPartnerViewModel : TorzsDataView<Partner>, IHandle<Telephely>,IHandle<Partner>
    {
        private Telephely _selectedTelephely;
        private string _newName="" ;
        private Partner _selectedPartner;

        public Partner SelectedPartner
        {
            get { return _selectedPartner; }
            set { _selectedPartner = value; }
        }

        public string NewName   
        {
            get { return _newName; }
            set { _newName = value;
                NotifyOfPropertyChange(() => NewName);
                NotifyOfPropertyChange(() => CanDoAction);
            }
        }

        public Telephely SelectedTelephely
        {
            get { return _selectedTelephely; }
            set { _selectedTelephely = value; }
        }

        public async override void DoAction()
        {
            Log.Debug("{Class} módosítás gomb megnyomva.", GetType());
            SelectedPartner.Name = NewName;
            Log.Debug("{Class} a szerver hívása... Adat: {SelectedPartner}", GetType(), SelectedPartner);
            bool success = await serverHelper.ModifyPartnerAsync(SelectedPartner);
            if (success)
            {
                eventAggregator.BeginPublishOnUIThread((SelectedTelephely, SelectedPartner));
                Log.Debug("{Class} Sikeres módosítás.", GetType());
            }
            else {
                eventAggregator.BeginPublishOnUIThread((SelectedTelephely, new Partner()));
                Log.Debug("{Class} Sikeretelen módosítás.", GetType());
            }
           
            TryClose();
        }

        public void Handle(Telephely message)
        {
            SelectedTelephely = message;
        }

        public void Handle(Partner message)
        {
            SelectedPartner = new Partner(message);
            NewName = message.Name;
        }

        protected override bool ValidateDataInForm()
        {
            return !(NewName.Length < 3 || NewName.Length > 50);
        }
    }
}
