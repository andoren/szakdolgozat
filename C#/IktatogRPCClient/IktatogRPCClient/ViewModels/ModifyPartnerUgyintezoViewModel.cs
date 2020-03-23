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
    class ModifyPartnerUgyintezoViewModel : TorzsDataView<PartnerUgyintezo>,IHandle<(Telephely,Partner,PartnerUgyintezo)>
    {
        private string _ugyintezoNeve;
        

        private Telephely _selectedTelephely;

        private Partner _selectedPartner;

        private PartnerUgyintezo _selectedPartnerUgyintezo;

        public PartnerUgyintezo SelectedPartnerUgyintezo
        {
            get { return _selectedPartnerUgyintezo; }
            set { _selectedPartnerUgyintezo = value; 
                
            }
        }

        public Partner SelectedPartner
        {
            get { return _selectedPartner; }
            set { _selectedPartner = value; }
        }

        public Telephely SelectedTelephely
        {
            get { return _selectedTelephely; }
            set { _selectedTelephely = value; }
        }

        public string UgyintezoNeve
        {
            get { return _ugyintezoNeve; }
            set
            {
                _ugyintezoNeve = value;
                NotifyOfPropertyChange(() => UgyintezoNeve);
                NotifyOfPropertyChange(() => CanDoAction);
            }
        }
        public async override void DoAction()
        {
            Log.Debug("{Class} módosítás gomb megnyomva", GetType());
            Log.Debug("{Class} a módosított ügyintéző: {SelectedPartnerUgyintezo}", GetType(), SelectedPartnerUgyintezo);
            bool success =  await serverHelper.ModifyPartnerUgyintezoAsync(SelectedPartnerUgyintezo, UgyintezoNeve);
            PartnerUgyintezo NewUgyintezo = new PartnerUgyintezo() { Id = SelectedPartnerUgyintezo.Id, Name = UgyintezoNeve };
            if (success)
            {
                Log.Debug("{Class} sikeres módosítás", GetType());
               
                eventAggregator.PublishOnUIThread((SelectedTelephely, SelectedPartner, NewUgyintezo));
            }
            else {
                Log.Debug("{Class} sikertelen módosítás", GetType());
                eventAggregator.PublishOnUIThread((SelectedTelephely, SelectedPartner, new PartnerUgyintezo()));
            }
          
        }

        public void Handle((Telephely, Partner, PartnerUgyintezo) message)
        {
            SelectedTelephely = message.Item1;
            SelectedPartner = message.Item2;
            SelectedPartnerUgyintezo = message.Item3;
            UgyintezoNeve = SelectedPartnerUgyintezo.Name;
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
