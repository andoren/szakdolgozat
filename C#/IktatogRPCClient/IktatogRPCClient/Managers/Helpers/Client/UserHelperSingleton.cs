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
        private static volatile UserHelperSingleton userHelper;
        private static readonly object lockable = new object();
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
            lock (lockable)
            {
                if (userHelper == null) {
                    userHelper = new UserHelperSingleton();
                } 
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
                    userHelper.Token = CurrentUser.AuthToken;
                    
                    success = true;
                }
            }
            catch (RpcException re)
            {
                InformationBox.ShowError(re);
            }
            catch (Exception e )
            {
                InformationBox.ShowError(e);
            }
            return success;
        }
        public bool IsAdmin {
            get {
                return CurrentUser.Privilege.Name.ToLower() == "admin" ? true : false;
            }
        }
    }
}
