using Iktato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Interfaces
{
    interface IManagePartner
    {
        Partner AddPartner(NewTorzsData newObject, User user);
        Answer DeletePartner(int id, User user);
        List<Partner> GetPartnerek(Telephely filter);
        Answer ModifyPartner(Partner modifiedObject);
    }
}
