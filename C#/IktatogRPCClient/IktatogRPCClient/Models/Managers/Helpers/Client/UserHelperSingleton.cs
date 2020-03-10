using Grpc.Core;
using Iktato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IktatogRPCClient.Models.Managers.Helpers.Client
{
    class UserHelperSingleton
    {
        private UserHelperSingleton()
        {

        }
        private static UserHelperSingleton userHelper;
        private ServerHelperSingleton serverHelper = ServerHelperSingleton.GetInstance();
        private AuthToken _token;
        public User CurrentUser;

        public static UserHelperSingleton GetInstance()
        {
            lock (typeof(UserHelperSingleton))
            {
                if (userHelper == null) userHelper = new UserHelperSingleton(); 
                return userHelper;
            }
        }

      

        public AuthToken Token
        {
            get { return _token; }
            private set { _token = value; }
        }
        public async  Task Login(LoginMessage message)
        {
            try
            {
                CurrentUser = await new IktatoService.IktatoServiceClient(serverHelper.GetChannel()).LoginAsync(message);
                Token = CurrentUser.AuthToken;
            }
            catch (RpcException re)
            {
                if (re.StatusCode == StatusCode.Unauthenticated) throw new LoginErrorException("Hibás felhasznlónév vagy jelszó! Próbálja megújra");               
            }
            catch (Exception )
            {
                MessageBox.Show("Sima Exception");           
            }

        }
        public bool IsAdmin {
            get {
                return CurrentUser.Privilege.Name == "Admin" ? true : false;
            }
        }
    }
}
