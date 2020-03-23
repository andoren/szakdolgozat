using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Models;
using IktatogRPCClient.Models.Managers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.ViewModels
{
    class AddUgyintezoViewModel:TorzsDataView<Ugyintezo>,IHandle<BindableCollection<Telephely>>
    {
        public AddUgyintezoViewModel()
        {
           
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
                NotifyOfPropertyChange(() => CanWriteUgyintezoNeve);
            }
        }
        private string _ugyintezoNeve;

        public string UgyintezoNeve
        {
            get { return _ugyintezoNeve; }
            set { 
                _ugyintezoNeve = value;
                NotifyOfPropertyChange(()=>UgyintezoNeve);
                NotifyOfPropertyChange(()=> CanDoAction);
            }
        }
        public bool CanWriteUgyintezoNeve
        {
            get { return ValasztottTelephely != null; }
        }

        protected override bool ValidateDataInForm() {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(UgyintezoNeve))
            {
                isValid = false;
            }
            else if (UgyintezoNeve.Length < 5 || UgyintezoNeve.Length > 100) isValid = false;
            return isValid;
        }
        public async override void DoAction() {
            Log.Debug("{Class} hozzáadás gomb megnyomva", GetType());
            Log.Debug("{Class} Várakozás a szerverre... Adat: {ValasztottTelephely, UgyintezoNeve}", GetType(),ValaszthatoTelephely,UgyintezoNeve);
            Ugyintezo NewUgyintezo = await serverHelper.AddUgyintezoToTelephelyAsync(ValasztottTelephely,UgyintezoNeve);
            if(NewUgyintezo.Id != -1) {
                Log.Debug("{Class} Sikeres hozzáadás.", GetType());
                Log.Debug("{Class} Ugyintezo hirdetése", GetType());
                eventAggregator.PublishOnUIThread((ValasztottTelephely, NewUgyintezo));
            }
            else {
                Log.Debug("{Class} Sikertelen hozzáadás.", GetType());
            }
         
            
            Log.Debug("{Class} Sikeres hozzáadás.", GetType());
            TryClose();
        }

        public void Handle(BindableCollection<Telephely> message)
        {
            Log.Debug("{Class} Telephelyek hozzáadva", GetType());
            ValaszthatoTelephely = message;
        }
    }
}
