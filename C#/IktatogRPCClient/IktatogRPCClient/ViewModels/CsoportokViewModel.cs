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
    class CsoportokViewModel : Conductor<Screen>, IHandle<Csoport>
    {

        public CsoportokViewModel()
        {
            LoadData();
        }
        ServerHelperSingleton serverHelper;
        EventAggregatorSingleton eventAggregator ;
        private Csoport _selectedCsoport;
        private BindableCollection<Csoport> _telephelyCsoportjai;
        private BindableCollection<Telephely> _valaszthatoTelephely;
        private Telephely _selectedTelephely;

        public Telephely SelectedTelephely
        {
            get { return _selectedTelephely; }
            set { 
                _selectedTelephely = value;
                NotifyOfPropertyChange(()=>SelectedTelephely);
                TelephelyCsoportjai = serverHelper.GetCsoportokByTelephely(SelectedTelephely);
                NotifyOfPropertyChange(()=>TelephelyCsoportjai);
            }
        }

        public BindableCollection<Telephely> ValaszthatoTelephely
        {
            get { return _valaszthatoTelephely; }
            set { _valaszthatoTelephely = value; }
        }


        public BindableCollection<Csoport> TelephelyCsoportjai
        {
            get { return _telephelyCsoportjai; }
            set {
                _telephelyCsoportjai = value;
                
            }
        }
        public Csoport SelectedCsoport
        {
            get { return _selectedCsoport; }
            set { 
                _selectedCsoport = value;
                NotifyOfPropertyChange(() => SelectedCsoport);
                NotifyOfPropertyChange(() => CanModifyCsoport);
                NotifyOfPropertyChange(() => CanRemoveCsoport);
            }
        }

        public bool CanModifyCsoport {
            get {
                return SelectedCsoport != null;
            }
        }
        public bool CanRemoveCsoport {
            get {
                return SelectedCsoport != null;
               }
        }
        public void CreateCsoport() { 
        
        }
        public void ModifyCsoport() { 
            
        }
        public void RemoveCsoport() { 
        
        }
        public void Handle(Csoport message)
        {
            throw new NotImplementedException();
        }
        private void LoadData()
        {
            eventAggregator = EventAggregatorSingleton.GetInstance();
            eventAggregator.Subscribe(this);
            serverHelper = ServerHelperSingleton.GetInstance();
            ValaszthatoTelephely = serverHelper.GetTelephelyek();
            SelectedTelephely = ValaszthatoTelephely.First();
        }
    }
}
