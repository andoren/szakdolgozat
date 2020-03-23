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
            serverHelper =  ServerHelperSingleton.GetInstance();
        }
        private static UserHelperSingleton userHelper;
        private ServerHelperSingleton serverHelper  ;
        private AuthToken _token;
        private static User _currentUser;
        public static User CurrentUser {
            get {
                return _currentUser;
            }
            set {
                _currentUser = value;
            }
        }

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
        public async  Task<bool> Login(LoginMessage message)
        {
            bool success = false;
            try
            {
                CurrentUser = await new IktatoService.IktatoServiceClient(serverHelper.GetChannel()).LoginAsync(message);

                if (CurrentUser != null)
                {
                    Token = CurrentUser.AuthToken;
                    serverHelper.InitializeConnection();
                    success = true;
                }
            }
            catch (RpcException re)
            {
                InformationBox.ShowError(re);
            }
            catch (Exception e )
            {
                MessageBox.Show($"Sima Exception: {e.Message}");           
            }
            return success;
        }
        public bool IsAdmin {
            get {
                return CurrentUser.Privilege.Name == "admin" ? true : false;
            }
        }
    }
}
