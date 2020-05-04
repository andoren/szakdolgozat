using IktatogRPCServer.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Services
{
    class DocumentService:IManageDocument
    {
        IManageDocument dbManager;
        public DocumentService(IManageDocument dbManager)
        {
            this.dbManager = dbManager;
        }
    }
}
