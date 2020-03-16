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
    class ModifyUgyintezoViewModel:TorzsDataView<Ugyintezo>,IHandle<Ugyintezo>
    {

        public ModifyUgyintezoViewModel()
        {
            
        }
        private Ugyintezo _modifiedUgyintezo = new Ugyintezo();

        public Ugyintezo ModifiedUgyintezo
        {
            get { return _modifiedUgyintezo; }
            set {
                _modifiedUgyintezo = value;
                NotifyOfPropertyChange(() => ModifiedUgyintezo);
            }
        }

        private string _newName;

        public string NewName
        {
            get { return _newName; }
            set { 
                _newName = value;
                NotifyOfPropertyChange(() => NewName);
                NotifyOfPropertyChange(() => CanDoAction);
            }
        }


        public void Handle(Ugyintezo message)
        {
            ModifiedUgyintezo = message;
            NewName = message.Name ;
        }

        public async override void DoAction()
        {
            Ugyintezo modifiedUgyintezo = new Ugyintezo() { Id = ModifiedUgyintezo.Id, Name = NewName };
            if (await serverHelper.ModifyUgyintezoAsync(modifiedUgyintezo)) {
                eventAggregator.PublishOnUIThread(modifiedUgyintezo);
                TryClose();
            }
 
        }

        protected override bool ValidateDataInForm()
        {
            return (NewName.Length > 4 && NewName.Length < 100);
        }
    }
}
