using Iktato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Interfaces
{
    interface IManageEv
    {
        List<Year> GetEvek();
        Answer CloseOldYearAndActivateNewOne(Year modifiedObject);
    }
}
