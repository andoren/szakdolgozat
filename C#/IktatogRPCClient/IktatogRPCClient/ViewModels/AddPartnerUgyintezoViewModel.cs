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
    class AddPartnerUgyintezoViewModel : TorzsDataView<PartnerUgyintezo>,IHandle<BindableCollection<Partner>>
    {
        private BindableCollection<Partner> _availablePartnerek;
        private Partner _selectedPartner;
        private string _ugyintezoNeve;

        public string UgyintezoNeve
        {
            get { return _ugyintezoNeve; }
            set { _ugyintezoNeve = value;
                NotifyOfPropertyChange(()=>CanDoAction);
            }
        }
        public bool CanWriteUgyintezoNeve {
            get {
                return SelectedPartner != null;
            }
        }
        public Partner SelectedPartner
        {
            get { return _selectedPartner; }
            set { _selectedPartner = value;
                NotifyOfPropertyChange(()=>SelectedPartner);
                NotifyOfPropertyChange(() => CanWriteUgyintezoNeve);
            }
        }

        public BindableCollection<Partner> AvailablePartnerek
        {
            get { return _availablePartnerek; }
            set { _availablePartnerek = value;
                NotifyOfPropertyChange(() => AvailablePartnerek);
            }
        }

        public override void DoAction()
        {
            PartnerUgyintezo partner = serverHelper.AddPartnerUgyintezoToPartner(SelectedPartner,UgyintezoNeve);
            eventAggregator.PublishOnUIThread((SelectedPartner, partner));
            TryClose();
        }

        public void Handle(BindableCollection<Partner> message)
        {
            AvailablePartnerek = message;
        }

        protected override bool ValidateDataInForm()
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(UgyintezoNeve))
            {
                isValid = false;
            }
            else if (UgyintezoNeve.Length < 5 || UgyintezoNeve.Length > 100) isValid = false;
            return isValid;
        }
    }
}
