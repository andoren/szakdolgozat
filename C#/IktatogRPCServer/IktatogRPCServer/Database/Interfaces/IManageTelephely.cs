using Iktato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Interfaces
{
    interface IManageTelephely
    {
        Telephely AddTelephely(NewTorzsData newObject,User user);
        Answer DeleteTelephely(int id, User user);
        List<Telephely> GetTelephelyek(User filter);
        List<Telephely> GetTelephelyek();
        Answer ModifyTelephely(Telephely modifiedObject);
    }
}
