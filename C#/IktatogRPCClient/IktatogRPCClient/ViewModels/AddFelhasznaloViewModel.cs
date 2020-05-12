using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Models;
using IktatogRPCClient.Models.Managers.Helpers.Client;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IktatogRPCClient.ViewModels
{
    public class AddFelhasznaloViewModel : TorzsDataView<UserProxy>,IHandle<BindableCollection<Telephely>>
    {
        public AddFelhasznaloViewModel()
        {
            LoadData();
        }
        public AddFelhasznaloViewModel(bool test)
        {
            
        }
        private  void LoadData()
        {

            AvailablePrivileges = serverHelper.GetPrivileges();
            AvailableTelephelyek =  serverHelper.GetAllTelephely();
        }

        private string _newUsername;
        private string _newFullname;
        private string _newPassword;
        private BindableCollection<Privilege> _availablePrivileges;
        private Privilege _selectedPrivilege;
        private BindableCollection<Telephely> _availableTelephelyek;
        private Telephely _selectedTelephelyToAdd;
        private BindableCollection<Telephely> _selectedTelephelyek = new BindableCollection<Telephely>();
        private Telephely _selectedTelephelyToRemove;
        public Telephely SelectedTelephelyToRemove
        {
            get { return _selectedTelephelyToRemove; }
            set { _selectedTelephelyToRemove = value;
                NotifyOfPropertyChange(() => SelectedTelephelyToRemove);
                NotifyOfPropertyChange(() => CanRemoveTelephely);
            }
        }

        public BindableCollection<Telephely> SelectedTelephelyek
        {
            get { return _selectedTelephelyek; }
            set { _selectedTelephelyek = value;
                NotifyOfPropertyChange(() => SelectedTelephelyek);
                NotifyOfPropertyChange(() => CanDoAction);
            }
        }

        public Telephely SelectedTelephelyToAdd
        {
            get { return _selectedTelephelyToAdd; }
            set { 
                _selectedTelephelyToAdd = value;
                NotifyOfPropertyChange(()=>SelectedTelephelyToAdd);
                NotifyOfPropertyChange(() => CanAddTelephely);
            }
        }

        public BindableCollection<Telephely> AvailableTelephelyek
        {
            get { return _availableTelephelyek; }
            set { _availableTelephelyek = value;
                NotifyOfPropertyChange(() => AvailableTelephelyek);
            }
        }

        public Privilege SelectedPrivilege
        {
            get { return _selectedPrivilege; }
            set { _selectedPrivilege = value;
                NotifyOfPropertyChange(()=>SelectedPrivilege);
                NotifyOfPropertyChange(() => CanDoAction);
            }
        }

        public BindableCollection<Privilege> AvailablePrivileges
        {
            get { return _availablePrivileges; }
            set { _availablePrivileges = value;
                NotifyOfPropertyChange(()=> AvailablePrivileges);
            }
        }

        public string NewPassword
        {
            get { return _newPassword; }
            set { _newPassword = value;
                NotifyOfPropertyChange(() => NewPassword);
                NotifyOfPropertyChange(() => CanDoAction);
            }
        }

        public string NewFullname
        {
            get { return _newFullname; }
            set { _newFullname = value;
                NotifyOfPropertyChange(()=>NewFullname);
                NotifyOfPropertyChange(() => CanDoAction);
            }
        }

        public string NewUsername
        {
            get { return _newUsername; }
            set { _newUsername = value;
                NotifyOfPropertyChange(()=>NewUsername);
                NotifyOfPropertyChange(()=>CanDoAction);
            }

        }
        public void RemoveTelephely() {
            Log.Debug("{Class} Telephely törlése gomb megnyomva", GetType());
            Log.Debug("{Class} Az elérhető telephelyekhez hozzáadjuk a {SelectedTelephelyToRemove}", GetType(), SelectedTelephelyToRemove);
            AvailableTelephelyek.Add(SelectedTelephelyToRemove);
            Log.Debug("{Class} Aa választott telephelykből kitöröljük a {SelectedTelephelyToRemove}", GetType(), SelectedTelephelyToRemove);
            SelectedTelephelyek.Remove(SelectedTelephelyToRemove);
            NotifyOfPropertyChange(() => SelectedTelephelyek);
            NotifyOfPropertyChange(() => CanDoAction);
            Log.Debug("{Class} Telephely törlése vége.", GetType());
        }
        public void AddTelephely() {
            Log.Debug("{Class} Telephely hozzáadása gomb megnyomva", GetType());
            Log.Debug("{Class} A kiválasztott telephely {SelectedTelephelyToAdd} hozzáadva a választott telephelyekhez.", GetType(), SelectedTelephelyToAdd);
            SelectedTelephelyek.Add(SelectedTelephelyToAdd);
            Log.Debug("{Class} A kiválasztott telephely {SelectedTelephelyToAdd} kitörlése az elérhető telephelyek közül.", GetType());
            AvailableTelephelyek.Remove(SelectedTelephelyToAdd);
            NotifyOfPropertyChange(()=>SelectedTelephelyek);
            NotifyOfPropertyChange(() => CanDoAction);
            Log.Debug("{Class} Telephely hozzáadása vége.", GetType());
        }
        public bool CanRemoveTelephely {
            get {
                return SelectedTelephelyToRemove != null;
            }
        }
        public bool CanAddTelephely
        {
            get { return SelectedTelephelyToAdd != null; }
        }
        public async override void DoAction()
        {
            Log.Debug("{Class} Felhasználó hozzáadása gomb megnyomva.", GetType());
            Log.Debug("{Class} Várakozás a szerverre... Adat: {Username} , {NewFullname} , SelectedPrivilege, SelectedTelephelyek", GetType(), NewUsername, NewFullname, NewPassword, SelectedPrivilege, SelectedTelephelyek);
            UserProxy NewUser = await serverHelper.AddUserAsync(NewUsername,NewFullname, EncryptionHelper.EncryptSha1(NewPassword),SelectedPrivilege,SelectedTelephelyek);
            if (NewUser.Id != -1) {
                Log.Debug("{Class} Sikeres hozzáadás. Felhasználó hírdetése.", GetType());
                eventAggregator.PublishOnUIThread(NewUser);
            }
            else {
                Log.Debug("{Class} Sikertelen hozzáadás", GetType());
            }
            Log.Debug("{Class} bezárása.", GetType());
            TryClose();
           
        }

 

        protected override bool ValidateDataInForm()
        {
            bool IsValid = true;
            if (SelectedPrivilege == null) IsValid = false;
            else if (SelectedTelephelyek.Count == 0) IsValid = false;
            else if (string.IsNullOrWhiteSpace(NewFullname) ||
                string.IsNullOrWhiteSpace(NewUsername) ||
                string.IsNullOrWhiteSpace(NewPassword)) IsValid = false;
            else if (!UserProxy.IsValidUsername(NewUsername)) IsValid = false;
            else if (!UserProxy.IsValidFullname(NewFullname)) IsValid = false;
            else if (!UserProxy.IsValidPassword(NewPassword)) IsValid = false;
            return IsValid;
        }
        public void OnPasswordChanged(PasswordBox source)
        {
            NewPassword = source.Password;
        }

        public void Handle(BindableCollection<Telephely> message)
        {
            Log.Debug("{Class} Telephelyek hozzáadva.", GetType());
            AvailableTelephelyek = new BindableCollection<Telephely>(message);
        }
    }
}
