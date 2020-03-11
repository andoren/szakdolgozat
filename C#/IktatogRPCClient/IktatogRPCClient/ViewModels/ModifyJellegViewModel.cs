using Caliburn.Micro;
using Google.Protobuf.Collections;
using Iktato;
using IktatogRPCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.ViewModels
{
    class ModifyJellegViewModel : TorzsDataView<Jelleg>,IHandle<Jelleg>
    {
        private Jelleg _modifiedJelleg;
        private string _newName;
        public Jelleg ModifiedJelleg
        {
            get { return _modifiedJelleg; }
            set
            {
                _modifiedJelleg = value;
                NotifyOfPropertyChange(() => ModifiedJelleg);
            }
        }


     

        public string NewName
        {
            get { return _newName; }
            set
            {
                _newName = value;
                NotifyOfPropertyChange(() => NewName);
                NotifyOfPropertyChange(() => CanDoAction);
            }
        }
        public void Handle(Jelleg message)
        {
            ModifiedJelleg =  message;
            NewName = message.Name;
        }

        public override void DoAction()
        {
            Jelleg modifiedJelleg= new Jelleg() { Id = ModifiedJelleg.Id, Name = NewName };
            if (serverHelper.ModifyJelleg(modifiedJelleg))
            {
                eventAggregator.PublishOnUIThread(modifiedJelleg);
                TryClose();
            }

        }

        protected override bool ValidateDataInForm()
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(NewName))
            {
                isValid = false;

         
            }
           
            else if (NewName.Length < 3 || NewName.Length > 100) isValid = false;
            return isValid;
        }
    }
}
