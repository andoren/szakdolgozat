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
    class AddPartnerViewModel : TorzsDataView<Partner>, IHandle<BindableCollection<Telephely>>
    {

        private BindableCollection<Telephely> _availableTelephelyek;
        private Telephely _selectedTelephely;
        private string _partnerNeve="";

        public string PartnerNeve
        {
            get { return _partnerNeve; }
            
            set { _partnerNeve = value;
                NotifyOfPropertyChange(()=>PartnerNeve);
                NotifyOfPropertyChange(()=>CanDoAction);
            }
        }

        public Telephely SelectedTelephely
        {
            get { return _selectedTelephely; }
            
            set { _selectedTelephely = value;
                NotifyOfPropertyChange(() => SelectedTelephely);
                NotifyOfPropertyChange(()=>CanWritePartnerNeve);
            }
        }

        public BindableCollection<Telephely> AvailableTelephelyek
        {
            get { return _availableTelephelyek; }
            set { _availableTelephelyek = value;
                NotifyOfPropertyChange(() => AvailableTelephelyek);
            }
        }
        public bool CanWritePartnerNeve { 
            get {
                return SelectedTelephely != null;
            }
        }
        public async override void DoAction()
        {
            Log.Debug("{Class} hozzáadás gomb megnyomva.", GetType());
            Log.Debug("{Class} Várakozás a szerverre... Adat: {PartnerNeve}", GetType(),PartnerNeve);
            Partner partner = await serverHelper.AddPartnerToTelephelyAsync(SelectedTelephely, PartnerNeve);
            if (partner.Id != -1)
            {
                Log.Debug("{Class} Partner sikeresen hozzáadva.", GetType());
                Log.Debug("{Class} Partner hírdetése.", GetType());
                eventAggregator.BeginPublishOnUIThread((SelectedTelephely, partner));
            }
            else {
                Log.Debug("{Class} Sikertelen hozzáadás.", GetType());
            }
            Log.Debug("{Class} Partner hozzáadás bezárása.", GetType());
            TryClose();
        }

        public void Handle(BindableCollection<Telephely> message)
        {
            Log.Debug("{Class} Telephelyek hozzáadva.", GetType());
            AvailableTelephelyek = message;
        }

        protected override bool ValidateDataInForm()
        {
            return !(PartnerNeve.Length < 3 || PartnerNeve.Length > 50);
        }
    }
}
