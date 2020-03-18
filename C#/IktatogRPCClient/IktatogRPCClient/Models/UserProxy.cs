using Caliburn.Micro;
using Iktato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.Models
{
    public class UserProxy
    {
        public UserProxy()
        {
            _user = new User();
        }
        public UserProxy(User _user)
        {
           
            this._user = _user;
            
        }
        private User _user;

        public string Password {
            get {
                return _user.Password;
            }
        }
      
        public void SetPassword(string password) {
            if (IsValidPassword(password)) _user.Password = password;
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

        public BindableCollection<Telephely> Telephelyek { 
            get {
                return new BindableCollection<Telephely>(_user.Telephelyek);
            }
        }

        public static bool IsValidPassword(string newPassword)
        {
            //TODO Password Is Valid method
            return newPassword.Length > 5;
        }

        public static bool IsValidFullname(string newFullname)
        {
            return !(newFullname.Length < 4 || newFullname.Length > 100 || !newFullname.Contains(" "));
        }

        public  static bool IsValidUsername(string newUsername)
        {
            return !(newUsername.Length < 4 || newUsername.Length > 20);
        }
        public bool IsAdmin {
            get {
                return _user.Privilege.Name == "admin";
            }
        }
    }
}
