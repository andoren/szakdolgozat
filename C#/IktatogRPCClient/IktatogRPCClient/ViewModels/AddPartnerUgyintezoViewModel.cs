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
    public class AddPartnerUgyintezoViewModel : TorzsDataView<PartnerUgyintezo>,IHandle<BindableCollection<Partner>>
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

        public async override void DoAction()
        {
            Log.Debug("{Class} Partnerügyintéző hozzáadása gomb megnyomva.", GetType());
            Log.Debug("{Class} Várakozás a szerverre...", GetType());
            PartnerUgyintezo partner = await serverHelper.AddPartnerUgyintezoToPartnerAsync(SelectedPartner,UgyintezoNeve);
            if (partner.Id != -1) {
                Log.Debug("{Class} Sikeres hozzáadás...", GetType());
                Log.Debug("{Class} Partnerügyintéző hírdetése...", GetType());
                eventAggregator.PublishOnUIThread((SelectedPartner, partner));
            }
            else{
                Log.Debug("{Class} Sikertelen hozzáadás...", GetType());
            }
            Log.Debug("{Class} bezárása.", GetType());
            TryClose();
        }

        public void Handle(BindableCollection<Partner> message)
        {
            Log.Debug("{Class} Partnerek hozzáadva.", GetType());          
            AvailablePartnerek = message;
        }

        protected override bool ValidateDataInForm()
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(UgyintezoNeve))
            {
                isValid = false;
            }
            else if (UgyintezoNeve.Length < 5 || UgyintezoNeve.Length > 50) isValid = false;
            return isValid;
        }
    }
}
