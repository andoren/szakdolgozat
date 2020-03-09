using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Models;
using IktatogRPCClient.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.ViewModels
{
    class AddUgyintezoViewModel:TorzsDataView<Ugyintezo>
    {
        public AddUgyintezoViewModel()
        {
            LoadData();
        }
        private void LoadData() {

            ValaszthatoTelephely = serverHelper.GetTelephelyek();
            
        }
        private BindableCollection<Telephely> _valaszthatoTelephely = new BindableCollection<Telephely>();
        private Telephely _valasztottTelephely;

        public BindableCollection<Telephely> ValaszthatoTelephely
        {
            get { return _valaszthatoTelephely; }
            set
            {
                _valaszthatoTelephely = value;
                NotifyOfPropertyChange(() => ValaszthatoTelephely);
            }
        }
        public Telephely ValasztottTelephely
        {
            get { return _valasztottTelephely; }
            set
            {
                _valasztottTelephely = value;
                NotifyOfPropertyChange(() => ValaszthatoTelephely);
                NotifyOfPropertyChange(() => CanUgyintezoNeve);
            }
        }
        private string _ugyintezoNeve;

        public string UgyintezoNeve
        {
            get { return _ugyintezoNeve; }
            set { 
                _ugyintezoNeve = value;
                NotifyOfPropertyChange(()=>UgyintezoNeve);
                NotifyOfPropertyChange(()=>CanCreateUgyintezo);
            }
        }
        public bool CanUgyintezoNeve {
            get { return ValasztottTelephely != null; }
        }
        public bool CanCreateUgyintezo
        {
            get
            {
                return ValidDataInView(UgyintezoNeve);
            }
        }
        private bool ValidDataInView(string ugyintezoNeve) {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(ugyintezoNeve))
            {
                isValid = false;
            }
            else if (ugyintezoNeve.Length < 5 || ugyintezoNeve.Length > 100) isValid = false;
            return isValid;
        }
        public override void DoAction() {
            Ugyintezo NewUgyintezo = serverHelper.AddUgyintezoToTelephely(ValasztottTelephely,UgyintezoNeve);
            eventAggregator.PublishOnUIThread((ValasztottTelephely,NewUgyintezo));
            TryClose();
        }

    }
}
