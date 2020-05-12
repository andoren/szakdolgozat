using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace IktatogRPCClient.ViewModels
{
    public class AddCsoportViewModel : TorzsDataView<Csoport>,IHandle<BindableCollection<Telephely>>
    {
        public AddCsoportViewModel()
        {
            
        }

        private BindableCollection<Telephely> _valaszthatoTelephely;
        private Telephely _valasztottTelephely;
        private string _csoportKod="";

        private string _csoportName="";

        public string CsoportName
        {
            get { return _csoportName; }
            set { 
                _csoportName = value;
                NotifyOfPropertyChange(()=>CanDoAction);
            }
        }

        public string CsoportKod
        {
            get { return _csoportKod; }
            set { _csoportKod = value;
                NotifyOfPropertyChange(() => CanDoAction);
            }
        }

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
                NotifyOfPropertyChange(() => CanDoAction);
            }
        }


        protected override bool ValidateDataInForm()
        {
            
            bool isValid = true;
            if (ValasztottTelephely == null) isValid = false;
            if (string.IsNullOrWhiteSpace(CsoportKod) || string.IsNullOrWhiteSpace(CsoportName))
            {
                isValid = false;
            }
            if (CsoportName.Length < 5 || CsoportName.Length > 100 || CsoportKod.Length < 1 || CsoportKod.Length > 3) isValid = false;
            return isValid;
        }
        public async override void DoAction()
        {
            Log.Debug("{Class} Csoport hozzáadása gomb megnyomva.", GetType());
            Log.Debug("{Class} Új csoport készítése...szerverre várás.", GetType());       
            Csoport NewCsoport = await serverHelper.AddCsoportToTelephelyAsync(ValasztottTelephely, CsoportName, CsoportKod);
            if (NewCsoport.Id != -1)
            {
                Log.Debug("{Class} Sikeres hozzáadás", GetType());
                Log.Debug("{Class} ÚJ csoport hírdetése.", GetType());
                eventAggregator.PublishOnUIThread((ValasztottTelephely, NewCsoport));
            }
            else {
                Log.Debug("{Class} Sikertelen hozzáadás", GetType());
            }
            Log.Debug("{Class} Addcsoport bezárása.", GetType());
            
            TryClose();
        }

        public void Handle(BindableCollection<Telephely> message)
        {
            Log.Debug("{Class} Telephelyek event lekezleve és hozzáadva.", GetType());
            ValaszthatoTelephely = message;
        }
    }
}
