using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IktatogRPCClient.ViewModels
{
    class ModifyFelhasznaloViewModel : TorzsDataView<UserProxy>, IHandle<UserProxy>
    {
        public ModifyFelhasznaloViewModel()
        {
            LoadData();
        }
        private void LoadData()
        {
            AvailableTelephelyek = serverHelper.GetTelephelyek();
            AvailablePrivileges = serverHelper.GetPrivileges();
        }
        private string _newUsername;
        private string _newFullname;
        private BindableCollection<Privilege> _availablePrivileges;
        private Privilege _selectedPrivilege;
        private BindableCollection<Telephely> _availableTelephelyek;
        private Telephely _selectedTelephelyToAdd;
        private BindableCollection<Telephely> _selectedTelephelyek = new BindableCollection<Telephely>();
        private Telephely _selectedTelephelyToRemove;
        public Telephely SelectedTelephelyToRemove
        {
            get { return _selectedTelephelyToRemove; }
            set
            {
                _selectedTelephelyToRemove = value;
                NotifyOfPropertyChange(() => SelectedTelephelyToRemove);
                NotifyOfPropertyChange(() => CanRemoveTelephely);
            }
        }

        public BindableCollection<Telephely> SelectedTelephelyek
        {
            get { return _selectedTelephelyek; }
            set
            {
                _selectedTelephelyek = value;
                NotifyOfPropertyChange(() => SelectedTelephelyek);
                NotifyOfPropertyChange(() => CanDoAction);
            }
        }

        public Telephely SelectedTelephelyToAdd
        {
            get { return _selectedTelephelyToAdd; }
            set
            {
                _selectedTelephelyToAdd = value;
                NotifyOfPropertyChange(() => SelectedTelephelyToAdd);
                NotifyOfPropertyChange(() => CanAddTelephely);
            }
        }

        public BindableCollection<Telephely> AvailableTelephelyek
        {
            get { return _availableTelephelyek; }
            set
            {
                _availableTelephelyek = value;
                NotifyOfPropertyChange(() => AvailableTelephelyek);
            }
        }

        public Privilege SelectedPrivilege
        {
            get { return _selectedPrivilege; }
            set
            {
                _selectedPrivilege = value;
                NotifyOfPropertyChange(() => SelectedPrivilege);
                NotifyOfPropertyChange(() => CanDoAction);
            }
        }

        public BindableCollection<Privilege> AvailablePrivileges
        {
            get { return _availablePrivileges; }
            set
            {
                _availablePrivileges = value;
                NotifyOfPropertyChange(() => AvailablePrivileges);
            }
        }


        public UserProxy ModifiedUser { get; private set; }

        public string NewFullname
        {
            get { return _newFullname; }
            set
            {
                _newFullname = value;
                NotifyOfPropertyChange(() => NewFullname);
                NotifyOfPropertyChange(() => CanDoAction);
            }
        }

        public string NewUsername
        {
            get { return _newUsername; }
            set
            {
                _newUsername = value;
                NotifyOfPropertyChange(() => NewUsername);
                NotifyOfPropertyChange(() => CanDoAction);
            }

        }
        public void RemoveTelephely()
        {
            AvailableTelephelyek.Add(SelectedTelephelyToRemove);
            SelectedTelephelyek.Remove(SelectedTelephelyToRemove);
            NotifyOfPropertyChange(() => SelectedTelephelyek);
            NotifyOfPropertyChange(() => CanDoAction);
        }
        public void AddTelephely()
        {
            SelectedTelephelyek.Add(SelectedTelephelyToAdd);
            AvailableTelephelyek.Remove(SelectedTelephelyToAdd);
            NotifyOfPropertyChange(() => SelectedTelephelyek);
            NotifyOfPropertyChange(() => CanDoAction);
        }
        public bool CanRemoveTelephely
        {
            get
            {
                return SelectedTelephelyToRemove != null;
            }
        }
        public bool CanAddTelephely
        {
            get { return SelectedTelephelyToAdd != null; }
        }
        public override void DoAction()
        {
            User user = new User() { Id = ModifiedUser.Id, Fullname = NewFullname, Privilege = SelectedPrivilege, Username = NewUsername };
            foreach (var telephely in SelectedTelephelyek)
            {
                user.Telephelyek.Add(telephely);
            }
            UserProxy NewUser = new UserProxy(user);
            bool success = serverHelper.ModifyUser(NewUser.GetUser);
            eventAggregator.PublishOnUIThread(NewUser);
            TryClose();
        }



        protected override bool ValidateDataInForm()
        {
            bool IsValid = true;
            if (SelectedPrivilege == null) IsValid = false;
            else if (SelectedTelephelyek.Count == 0) IsValid = false;
            else if (string.IsNullOrWhiteSpace(NewFullname) ||
                string.IsNullOrWhiteSpace(NewUsername)) IsValid = false;
            else if (!UserProxy.IsValidUsername(NewUsername)) IsValid = false;
            else if (!UserProxy.IsValidFullname(NewFullname)) IsValid = false;
            return IsValid;
        }

        public void Handle(UserProxy message)
        {
            ModifiedUser = message;
            NewFullname = message.Fullname;
            NewUsername = message.Username;
            
            SelectedTelephelyek = message.Telephelyek;
            for (int i = 0; i < AvailableTelephelyek.Count; i++)
            {
                for (int j = 0; j < SelectedTelephelyek.Count; j++)
                {
                    if (AvailableTelephelyek[i].Name == SelectedTelephelyek[j].Name) AvailableTelephelyek.RemoveAt(i);
                }
            }
            SelectedPrivilege = message.Privilege;
            NotifyOfPropertyChange(()=>SelectedTelephelyek);
            NotifyOfPropertyChange(() => AvailableTelephelyek);
        }
    }
}
