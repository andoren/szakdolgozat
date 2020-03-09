using Caliburn.Micro;
using Grpc.Core;
using Iktato;
using IktatogRPCClient.Models.Managers;
using IktatogRPCClient.Models.Managers.Helpers.Client;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace IktatogRPCClient.ViewModels
{
    class LoginViewModel:Screen
    {
   
        const string userRoot = "HKEY_CURRENT_USER";
        const string subkey = "OtemplomIktato";
        const string keyName = userRoot + "\\" + subkey;
        private string _usernameBox;
        private UserHelperSingleton userHelper = UserHelperSingleton.GetInstance();

        public string UsernameBox
        {
            get { return _usernameBox; }
            set {
                _usernameBox = value;
                NotifyOfPropertyChange(() => UsernameBox);
            }
        }

        public string PasswordBox { get; set; }

        public LoginViewModel()
        {
            UsernameBox = (string)Registry.GetValue(keyName, "Felhasználónév", "");
            if (!string.IsNullOrWhiteSpace(UsernameBox)) SaveUsernameIsChecked = true;
        }
        private bool _saveUsernameIsChecked;

        public bool SaveUsernameIsChecked
        {
            get { return _saveUsernameIsChecked; }
            set { _saveUsernameIsChecked = value; }
        }

        async Task ConnectToServerAndLogin()
        {
            CheckUsername();
            CheckPassword();
            await userHelper.Login(GetDataFromLoginTextBoxes());
            if(SaveUsernameIsChecked)Registry.SetValue(keyName, "Felhasználónév", UsernameBox);
        }
        private LoginMessage GetDataFromLoginTextBoxes() {            
            return new LoginMessage() { Username = UsernameBox, Password = PasswordBox };
        }
        private void CheckUsername() {
            if (string.IsNullOrWhiteSpace(UsernameBox)) throw new InvalidUserNameExepction("Hibás felhasználónév");  
            else if(UsernameBox.Length < 3 || UsernameBox.Length > 20) throw new InvalidUserNameExepction("Hibás felhasználónév");
        }
        private void CheckPassword() {
            if (string.IsNullOrWhiteSpace(PasswordBox)) throw new InvalidPasswordException("Hibás jelszó");
            else if (PasswordBox.Length < 3 || PasswordBox.Length > 20) throw new InvalidPasswordException("Hibás jelszó");
            
        }
        public async void LoginButton() {
            try
            {
                LoaderIsVisible = true;
                await ConnectToServerAndLogin();
                var manager = new WindowManager();
                manager.ShowWindow(new ContainerViewModel(), null, null);
                TryClose();
            }
            catch (Exception e) {
                LoaderIsVisible = false;
                MessageBox.Show(e.Message);
                
            }
           
        }
        public void ExitButton() {
            TryClose();
        }
        //Password change eventkor ez hajtódik végre.
        public void OnPasswordChanged(PasswordBox source)
        {
            PasswordBox = source.Password;
        }
        //Gomblenyomás event az ablakon
        public void IsEnterPressed(KeyEventArgs keyArgs) {
            if(keyArgs.Key == Key.Enter)LoginButton();
        }
        private bool _loaderIsVisible;
        public bool LoaderIsVisible {
            get {
                return _loaderIsVisible;
            } 
            set {
                _loaderIsVisible = value;
                NotifyOfPropertyChange(() => LoaderIsVisible);    
            }
        } 
        
        
    }
}
