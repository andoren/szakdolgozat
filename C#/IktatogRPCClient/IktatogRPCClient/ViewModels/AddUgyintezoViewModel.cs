using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.ViewModels
{
    class AddUgyintezoViewModel:Screen
    {
        public AddUgyintezoViewModel()
        {
            LoadData();
        }
        private void LoadData() {
            serverHelper = ServerHelper.GetInstance();
            ValaszthatoTelephely = serverHelper.GetTelephelyek();
            ValasztottTelephely = ValaszthatoTelephely.First();
        }
        private BindableCollection<Telephely> _valaszthatoTelephely = new BindableCollection<Telephely>();
        private Telephely _valasztottTelephely;
        private ServerHelper serverHelper;
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
        public void CreateUgyintezo() {
            Ugyintezo NewUgyintezo = serverHelper.AddUgyintezoToTelephely(ValasztottTelephely,UgyintezoNeve);
            EventAggregatorSingleton.GetInstance().PublishOnUIThread(NewUgyintezo);
            TryClose();
        }
        public void CancelUgyintezo() {
            EventAggregatorSingleton.GetInstance().PublishOnUIThread(new Ugyintezo());
            TryClose();
        }
    }
}
