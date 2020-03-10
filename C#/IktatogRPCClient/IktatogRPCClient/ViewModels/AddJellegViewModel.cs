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
    class AddJellegViewModel : TorzsDataView<Jelleg>,IHandle<BindableCollection<Telephely>>,IHandle<Jelleg>
    {
        private BindableCollection<Telephely> _availableTelephelyek;
        private Telephely _selectedTelephely;
        private Jelleg _modifiedJelleg;
        private string _jellegNeve;

        public string JellegNeve
        {
            get { return _jellegNeve; }
            set { 
                _jellegNeve = value;
                NotifyOfPropertyChange(()=>JellegNeve);
                NotifyOfPropertyChange(()=>CanDoAction);
            }
        }

        public Jelleg ModifiedJelleg
        {
            get { return _modifiedJelleg; }
            set {
                _modifiedJelleg = value;
                NotifyOfPropertyChange(() =>ModifiedJelleg);
            }
        }

        public Telephely SelectedTelephely
        {
            get { return _selectedTelephely; }
            set { _selectedTelephely = value;
                NotifyOfPropertyChange(()=>SelectedTelephely);
                NotifyOfPropertyChange(()=>CanWriteJellegNeve);
            }
        }

        public BindableCollection<Telephely> AvailableTelephelyek
        {
            get { return _availableTelephelyek; }
            set { _availableTelephelyek = value;
                NotifyOfPropertyChange(() => AvailableTelephelyek);
            }
        }
        public bool CanWriteJellegNeve
        {
            get { return SelectedTelephely != null; }
        }

        public bool CanDoAction
        {
            get
            {
                return ValidDataInView(JellegNeve);
            }
        }
        private bool ValidDataInView(string jellegNeve)
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(jellegNeve))
            {
                isValid = false;
            }
            else if (jellegNeve.Length < 5 || jellegNeve.Length > 100) isValid = false;
            return isValid;
        }
        public override void DoAction()
        {
            Jelleg NewJelleg = serverHelper.AddJellegToTelephely(SelectedTelephely, JellegNeve);
            eventAggregator.PublishOnUIThread((SelectedTelephely, NewJelleg));
            TryClose();
        }
        public void Handle(BindableCollection<Telephely> message)
        {
            AvailableTelephelyek = message;
        }

        public void Handle(Jelleg message)
        {
            ModifiedJelleg = message;
        }
    }
}
