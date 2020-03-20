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
    class ModifyUgyintezoViewModel:TorzsDataView<Ugyintezo>,IHandle<(Telephely,Ugyintezo)>
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
        public Telephely SelectedTelephely { get; set; }
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



        public async override void DoAction()
        {
            Ugyintezo modifiedUgyintezo = new Ugyintezo() { Id = ModifiedUgyintezo.Id, Name = NewName };
            if (await serverHelper.ModifyUgyintezoAsync(modifiedUgyintezo)) {
                eventAggregator.PublishOnUIThread((SelectedTelephely, modifiedUgyintezo));
                TryClose();
            }
 
        }

        protected override bool ValidateDataInForm()
        {
            return (NewName.Length > 4 && NewName.Length < 100);
        }

        public void Handle((Telephely, Ugyintezo) message)
        {
            SelectedTelephely = new Telephely(message.Item1);
            ModifiedUgyintezo = new Ugyintezo(message.Item2);
            NewName = message.Item2.Name;
        }
    }
}
