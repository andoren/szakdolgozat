using Caliburn.Micro;
using Grpc.Core;
using Iktato;
using IktatogRPCClient.Models;
using IktatogRPCClient.Models.Managers.Helpers.Client;
using Microsoft.Win32;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Serilog;

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

        async Task<bool> ConnectToServerAndLogin()
        {
            CheckUsername();
            CheckPassword();
            bool success = await userHelper.Login(GetDataFromLoginTextBoxes());
            if(SaveUsernameIsChecked)Registry.SetValue(keyName, "Felhasználónév", UsernameBox);
            return success;
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
                if (await ConnectToServerAndLogin())
                {
                    var manager = new WindowManager();
                    manager.ShowWindow(new ContainerViewModel(), null, null);
                    TryClose();
                }
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
             
            }
            catch (Exception e)
            {

                InformationBox.ShowError(e);
                
            }
            finally {
                LoaderIsVisible = false;
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
