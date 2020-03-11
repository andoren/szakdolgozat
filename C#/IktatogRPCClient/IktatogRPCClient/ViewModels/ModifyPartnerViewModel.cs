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
    class ModifyPartnerViewModel : TorzsDataView<Partner>, IHandle<Telephely>,IHandle<Partner>
    {
        private Telephely _selectedTelephely;
        private string _newName;
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

        public override void DoAction()
        {
            SelectedPartner.Name = NewName;
            bool success = serverHelper.ModifyPartner(SelectedPartner);
            eventAggregator.BeginPublishOnUIThread((SelectedTelephely,SelectedPartner));
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
            return!(NewName.Length < 3 || NewName.Length > 50);
        }
    }
}
