using Iktato;
using IktatogRPCServer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Interfaces
{
    interface IManageDocument
    {
        Document GetDocumentByInfo(DocumentInfo documentInfo);
        List<DocumentInfo> GetDocumentInfosByIkonyv(Ikonyv ikonyv);
        Document AddDocument(Document newObject, User user);
        Answer DeleteDocument(int id, User user);
        UploadedFileHandler FileHandler { get;}
    }
}
