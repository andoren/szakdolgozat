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
    class AddPartnerViewModel : TorzsDataView<Partner>, IHandle<BindableCollection<Telephely>>
    {
       

        public override void DoAction()
        {
            throw new NotImplementedException();
        }

        public void Handle(BindableCollection<Telephely> message)
        {
            throw new NotImplementedException();
        }

        protected override bool ValidateDataInForm()
        {
            throw new NotImplementedException();
        }
    }
}
