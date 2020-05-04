using Iktato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Interfaces
{
    interface IManageJelleg
    {
        Jelleg AddJelleg(NewTorzsData newObject, User user);
        Answer DeleteJelleg(int id, User user);
        List<Jelleg> GetJellegek(Telephely filter);
        Answer ModifyJelleg(Jelleg modifiedObject);
    }
}
