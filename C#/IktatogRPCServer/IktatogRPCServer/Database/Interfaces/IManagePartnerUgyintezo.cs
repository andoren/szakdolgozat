using Iktato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Interfaces
{
    interface IManagePartnerUgyintezo
    {
        PartnerUgyintezo AddPartnerUgyintezo(NewTorzsData newObject, User user);
        Answer DeletePartnerUgyintezo(int id, User user);
        List<PartnerUgyintezo> GetPartnerUgyintezok(Partner filter);
        Answer ModifyPartnerUgyintezo(PartnerUgyintezo modifiedObject);
    }
}
