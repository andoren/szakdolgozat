using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Helpers
{
    class EncryptionHelper
    {
        public static string EncryptSha1(string password)
        {

            byte[] temp2;
            SHA1 sha = new SHA1CryptoServiceProvider();
            temp2 = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < temp2.Length; i++)
            {
                sb.Append(temp2[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
