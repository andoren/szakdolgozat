using Iktato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Interfaces
{
    interface IManageIkonyv
    {
        RovidIkonyv AddRootIkonyv(Ikonyv newObject, User user);
        RovidIkonyv AddSubIkonyv(Ikonyv newObject, User user);
        Answer DeleteIkonyv(int id, User user);
        List<Ikonyv> GetIkonyvek(SearchIkonyvData filter);
        Answer ModifyIkonyv(Ikonyv modifiedObject);
        List<RovidIkonyv> GetRovidIkonyvek(Telephely filter);
    }
}
