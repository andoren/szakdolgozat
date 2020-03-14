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
    class ModifyIkonyvViewModel:PopUpChildModel
    {
        public ModifyIkonyvViewModel(Ikonyv IkonyvToModify)
        {
            
            this.IkonyvToModify = new Ikonyv(IkonyvToModify);
            AvailablePartners = serverHelper.GetPartnerekByTelephely(IkonyvToModify.Telephely);
            SelectedPartner = IkonyvToModify.Partner;
            AvailableJellegek = serverHelper.GetJellegekByTelephely(IkonyvToModify.Telephely);
            AvailableUgyintezok = serverHelper.GetUgyintezokByTelephely(IkonyvToModify.Telephely);
            AvailablePartnerUgyintezok = new BindableCollection<PartnerUgyintezo>(SelectedPartner.Ugyintezok);
            SelectedJelleg = IkonyvToModify.Jelleg;
            
            SelectedPartnerUgyintezo = IkonyvToModify.Partner.Ugyintezok[0];
            SelectedUgyintezo = IkonyvToModify.Ugyintezo;
        }
        private ServerHelperSingleton serverHelper = ServerHelperSingleton.GetInstance();
        private Ikonyv _ikonyvToModify;
        private Partner _selectedPartner;
        private BindableCollection<Partner> _availablePartners;
        private PartnerUgyintezo _selectedPartnerUgyintezo;
        private BindableCollection<PartnerUgyintezo> _availablePartnerUgyintezok;
        private Jelleg _selectedJelleg;
        private BindableCollection<Jelleg> _availableJellegek;

        private Ugyintezo _selectedUgyintezo;
        private BindableCollection<Ugyintezo> _availableUgyintezok;

        public BindableCollection<Ugyintezo> AvailableUgyintezok
        {
            get { return _availableUgyintezok; }
            set { _availableUgyintezok = value;
                NotifyOfPropertyChange(()=>AvailableUgyintezok);
            }
        }

        public Ugyintezo SelectedUgyintezo
        {
            get { return _selectedUgyintezo; }
            set { _selectedUgyintezo = value;
                NotifyOfPropertyChange(()=>SelectedUgyintezo);
            }
        }

        public BindableCollection<Jelleg> AvailableJellegek
        {
            get { return _availableJellegek; }
            set { _availableJellegek = value;
                NotifyOfPropertyChange(()=> AvailableJellegek);
            }
        }

        public Jelleg SelectedJelleg
        {
            get { return _selectedJelleg; }
            set { _selectedJelleg = value;
                NotifyOfPropertyChange(()=> SelectedJelleg);
            }
        }
        

        public BindableCollection<PartnerUgyintezo> AvailablePartnerUgyintezok
        {
            get { return _availablePartnerUgyintezok; }
            set { _availablePartnerUgyintezok = value;
                NotifyOfPropertyChange(()=>AvailablePartnerUgyintezok);
            }
        }

        public PartnerUgyintezo SelectedPartnerUgyintezo
        {
            get { return _selectedPartnerUgyintezo; }
            set
            {
                _selectedPartnerUgyintezo = value;
                NotifyOfPropertyChange(() => SelectedPartnerUgyintezo);
            }
        }


        public BindableCollection<Partner> AvailablePartners
        {
            get { return _availablePartners; }
            set { _availablePartners = value;
                NotifyOfPropertyChange(()=>AvailablePartners);
            }
        }

        public Partner SelectedPartner
        {
            get { return _selectedPartner; }
            set {
                _selectedPartner = value;
                NotifyOfPropertyChange(()=>SelectedPartner);
            }
        }

        public string Title
        {
            get { return $"{IkonyvToModify.Iktatoszam} módosítása"; }
            
        }

        public Ikonyv IkonyvToModify
        {
            get { return _ikonyvToModify; }
            set { _ikonyvToModify = value; }
        }

        public void CancelButton() {
            MyParent.CloseScreen(this,false);
        }
        public void ModifyButton() {
            MyParent.CloseScreen(this, true);
        }

    }
}
