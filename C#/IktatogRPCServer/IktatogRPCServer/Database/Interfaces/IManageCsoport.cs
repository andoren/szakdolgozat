using Iktato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Interfaces
{
    interface IManageCsoport
    {
        Csoport AddCsoport(NewTorzsData data, User user);
        Answer DeleteCsoport(int id,User user);
        List<Csoport> GetCsoportok(Telephely filter);
        Answer ModifyCsoport(Csoport modifiedObject);

    }
}
