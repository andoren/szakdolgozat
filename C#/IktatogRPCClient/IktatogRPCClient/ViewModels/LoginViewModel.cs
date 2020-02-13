using Caliburn.Micro;
using Grpc.Core;
using Iktato;
using IktatogRPCClient.Models.Managers;
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
        User user;
        private string _usernameBox;

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

        }
        async Task connectToServer()
        {
            CheckUsername();
            CheckPassword();
            user = await ServerHelper.Login(GetDataFromLoginTextBoxes());
            
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
                await connectToServer();
                var manager = new WindowManager();
                manager.ShowWindow(new ContainerViewModel(user), null, null);
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
