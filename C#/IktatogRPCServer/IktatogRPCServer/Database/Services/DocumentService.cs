using Iktato;
using IktatogRPCServer.Database.Interfaces;
using IktatogRPCServer.Helpers;
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

        public UploadedFileHandler FileHandler => dbManager.FileHandler;

        public Document AddDocument(Document newObject, User user)
        {
            return dbManager.AddDocument(newObject, user);
        }

        public Answer DeleteDocument(int id, User user)
        {
            return dbManager.DeleteDocument(id, user);
        }

        public Document GetDocumentByInfo(DocumentInfo documentInfo)
        {
            return dbManager.GetDocumentByInfo(documentInfo);
        }

        public List<DocumentInfo> GetDocumentInfosByIkonyv(Ikonyv ikonyv)
        {
            return dbManager.GetDocumentInfosByIkonyv(ikonyv);
        }
    }
}
