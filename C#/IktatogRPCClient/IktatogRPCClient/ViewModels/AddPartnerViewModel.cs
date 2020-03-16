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
            Partner partner = await serverHelper.AddPartnerToTelephelyAsync(SelectedTelephely,PartnerNeve) ;
            eventAggregator.BeginPublishOnUIThread((SelectedTelephely,partner));
            TryClose();
        }

        public void Handle(BindableCollection<Telephely> message)
        {
            AvailableTelephelyek = message;
        }

        protected override bool ValidateDataInForm()
        {
            return !(PartnerNeve.Length < 3 || PartnerNeve.Length > 50);
        }
    }
}
