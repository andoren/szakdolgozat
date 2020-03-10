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
    class AddCsoportViewModel : TorzsDataView<Csoport>
    {
        public AddCsoportViewModel()
        {
            LoadData();
        }
        private void LoadData()
        {

            ValaszthatoTelephely = serverHelper.GetTelephelyek();

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
        public bool CanDoAction
        {
            get { return ValidDataInView(); }
        }

        private bool ValidDataInView()
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
        public override void DoAction()
        {
            Csoport NewCsoport = serverHelper.AddCsoportToTelephely(ValasztottTelephely, CsoportName, CsoportKod);
            eventAggregator.PublishOnUIThread((ValasztottTelephely, NewCsoport));
            TryClose();
        }
    }
}
