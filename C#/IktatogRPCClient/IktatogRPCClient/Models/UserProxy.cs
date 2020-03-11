using Iktato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.Models
{
    class UserProxy
    {
        public UserProxy(User _user)
        {
           
            this._user = _user;
            
        }
        private User _user;

        public int PasswordLength {
            get {
                return _user.Password.Length;
            }
        }
      
        public void SetPassword(string password) {
            if (IsValidPassword(password)) _user.Password = password;
        }
        private bool IsValidPassword(string password)
        {
            //TODO password strength checker
            return true;
        }

        public Privilege Privilege { get {
                return _user.Privilege;
            } set {
                _user.Privilege = value;
            } }


        public int Id { get {
                return _user.Id;
            } set {
                _user.Id = value;
            } }
        public string Username
        {
            get { return _user.Username; }
            set { _user.Username = value; }
        }

        public string Fullname
        {
            get { return _user.Fullname; }
            set { _user.Fullname = value; }
        }

        public string DisplayName {
            get {
                return $"{Fullname} - {Username}";
            }
        }
        public User GetUser {
            get {
                return new User(_user);
            }
        }
    }
}
