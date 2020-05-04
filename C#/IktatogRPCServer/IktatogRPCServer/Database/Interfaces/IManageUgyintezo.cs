using Iktato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Interfaces
{
    interface IManageUgyintezo
    {
        Ugyintezo AddUgyintezo(NewTorzsData newObject, User user);
        Answer DeleteUgyintezo(int id, User user);
        List<Ugyintezo> GetUgyintezok(Telephely filter);
        Answer ModifyUgyintezo(Ugyintezo modifiedObject);
    }
}
