using Iktato;
using IktatogRPCServer.Database.Mysql;
using IktatogRPCServer.Database.Services;
using IktatogRPCServer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IktatogRPCServer
{
    /// <summary>
    /// Interaction logic for UserManage.xaml
    /// </summary>
    public partial class UserManage : UserControl
    {
        public UserManage()
        {
            InitializeComponent();
            LoadDataAndSet();


        }
        UserService userService = new UserService( new UserDatabaseManager());
        TelephelyService telephelyService = new TelephelyService(new TelephelyDatabaseManager());
        PrivilegeService privilegeService = new PrivilegeService(new PrivilegeDatabaseManager());

        private async void LoadDataAndSet() {
            await Task.Run(() => {
                AllUser = userService.GetallUser();
                AvailableTelephelyek = telephelyService.GetTelephelyek();
                AvailablePrivileges = privilegeService.GetPrivileges();
                AvailableTelephelyForModification = new List<Telephely>(AvailableTelephelyek);
            });
            AllUserCombobox.ItemsSource = AllUser;
            AllUserCombobox.DataContext = this;
            AllTelephely.ItemsSource = AvailableTelephelyek;
            AllTelephely.DataContext = this;
            AvailablePrivilegesComboBox.ItemsSource = AvailablePrivileges;
            AvailablePrivilegesComboBox.DataContext = this;
            SelectedTelephelyekListBox.ItemsSource = SelectedTelephelyek;
            SelectedTelephelyekListBox.DataContext = this;
            DoAction.IsEnabled = false;
            PrivilegesForModificationComboBox.ItemsSource = AvailablePrivileges;
            PrivilegesForModificationComboBox.DataContext = this;

        }
        #region Addition
        private Telephely _selectedTelephelyToRemove;

        public Telephely SelectedTelephelyToRemove
        {
            get { return _selectedTelephelyToRemove; }
            set { _selectedTelephelyToRemove = value; }
        }

        private List<Telephely> _selectedTelephelyek = new List<Telephely>();

        public List<Telephely> SelectedTelephelyek
        {
            get { return _selectedTelephelyek; }
            set { _selectedTelephelyek = value; }
        }

        private List<User> _allUser;

        public List<User> AllUser
        {
            get { return _allUser; }
            set { _allUser = value; }
        }
        private User _selectedUser;

        public User SelectedUser
        {
            get { return _selectedUser; }
            set { _selectedUser = value; }
        }
        private Telephely _selectedTelephely;

        public Telephely SelectedTelephely
        {
            get { return _selectedTelephely; }
            set { _selectedTelephely = value; }
        }

        private List<Telephely> _availableTelephelyek ;


        public List<Telephely> AvailableTelephelyek
        {
            get { return _availableTelephelyek; }
            set { _availableTelephelyek = value; }
        }
        private Privilege _selectedPrivilege;

        public Privilege SelectedPrivilege
        {
            get { return _selectedPrivilege; }
            set { _selectedPrivilege = value; }
        }

        private List<Privilege> _availablePrivileges;

        public List<Privilege> AvailablePrivileges
        {
            get { return _availablePrivileges; }
            set { _availablePrivileges = value; }
        }
        private void AddTelephely_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTelephely != null){
                SelectedTelephelyek.Add(SelectedTelephely);
                AvailableTelephelyek.Remove(SelectedTelephely);
                SelectedTelephelyekListBox.Items.Refresh();
                AllTelephely.Items.Refresh();
                ValidateNewUserData();
            }
        }
        private void RemoveTelephely_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTelephelyToRemove != null)
            {
                AvailableTelephelyek.Add(SelectedTelephelyToRemove);
                SelectedTelephelyek.Remove(SelectedTelephelyToRemove);
                SelectedTelephelyekListBox.Items.Refresh();
                AllTelephely.Items.Refresh();
                ValidateNewUserData();
            }
        }
        private void ValidateNewUserData() {
            bool IsValid = true;
            string password = NewUserPassword.Password;
            string fullname = NewFullname.Text;
            string username = NewUsername.Text;
           
            if (SelectedPrivilege == null) IsValid = false;
            else if (SelectedTelephelyek.Count == 0 && SelectedPrivilege.Name.ToLower() != "admin" ) IsValid = false;
            else if (string.IsNullOrWhiteSpace(fullname) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password)) IsValid = false;
            else if (!IsValidUsername(username)) IsValid = false;
            else if (!IsValidFullname(fullname)) IsValid = false;
            else if (!IsValidPassword(password)) IsValid = false;
           
            DoAction.IsEnabled = IsValid;
        }
        private bool IsValidFullname(string newFullname)
        {
            return !(newFullname.Length < 4 || newFullname.Length > 100 || !newFullname.Contains(" "));
        }
        private bool IsValidUsername(string newUsername)
        {
            return !(newUsername.Length < 4 || newUsername.Length > 20);
        }
        private bool IsValidPassword(string newPassword)
        {
            //TODO Password Is Valid method
            return newPassword.Length > 5;
        }

        private void NewUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateNewUserData();
        }

        private void NewFullname_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateNewUserData();
        }

        private void NewUserPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ValidateNewUserData();
        }

        private void DoAction_Click(object sender, RoutedEventArgs e)
        {
            User user = new User() {
                Id = -1,
                Username = NewUsername.Text,
                Fullname = NewFullname.Text,
                Password = EncryptionHelper.EncryptSha1(NewUserPassword.Password),
                Privilege = SelectedPrivilege,

            };
            foreach (Telephely telephely in SelectedTelephelyek) {
                user.Telephelyek.Add(telephely);
            }
            user = userService.AddUser(new NewTorzsData() { User = user }, new User() { Id = 1 });
            if (user.Id != -1)
            {
                SelectedTelephelyek.Clear();
                AvailableTelephelyek = telephelyService.GetTelephelyek();
                SelectedTelephelyekListBox.Items.Refresh();
                AllTelephely.ItemsSource = AvailableTelephelyek;
                AllTelephely.Items.Refresh();
                NewUsername.Text = "";
                NewFullname.Text = "";
                NewUserPassword.Clear();
                MessageBox.Show("Sikeres hozzáadás.");
            }
            else {
                MessageBox.Show("Sikertelen hozzáadás.");
            }
        }
        #endregion
        #region Modification
        private List<Telephely> _availableTelephelyForModicitaion;
        public List<Telephely> AvailableTelephelyForModification
        {
            get { return _availableTelephelyForModicitaion; }
            set { _availableTelephelyForModicitaion = value; }
        }
        private Telephely _selectedTelephelyToAddModification;

        public Telephely SelectedTelephelyToAddModification
        {
            get { return _selectedTelephelyToAddModification; }
            set { _selectedTelephelyToAddModification = value; }
        }
       
        private List<Telephely> _selectedTelephelyekForModification = new List<Telephely>();

        public List<Telephely> SelectedTelephelyekForModification
        {
            get { return _selectedTelephelyekForModification; }
            set { _selectedTelephelyekForModification = value; }
        }
        private Telephely _selectedTelephelyToRemoveModification;

        public Telephely SelectedTelephelyToRemoveModification
        {
            get { return _selectedTelephelyToRemoveModification; }
            set { _selectedTelephelyToRemoveModification = value; }
        }
        private List<Privilege> _privilegesForModification;

        public List<Privilege> PrivilegesForModification
        {
            get { return _privilegesForModification; }
            set { _privilegesForModification = value; }
        }
        private Privilege _selectedPrivilegeForModification;

        public Privilege SelectedPrivilegeForModification
        {
            get { return _selectedPrivilegeForModification; }
            set { _selectedPrivilegeForModification = value; }
        }

        private void AllUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (SelectedUser != null)
            {
                NewFullnameForModification.Text = SelectedUser.Fullname;
                NewUsernameForModification.Text = SelectedUser.Username;
                SelectedPrivilegeForModification = SelectedUser.Privilege;
                PrivilegesForModificationComboBox.SelectedIndex = SelectedUser.Privilege.Id == 1 ? 0 : 1;
                DeleteButton.IsEnabled = true;
                CanModifyUser();
            }
            else {
                DeleteButton.IsEnabled = false;
            }

            SelectedTelephelyekForModification = telephelyService.GetTelephelyek(SelectedUser);
            AvailableTelephelyForModification = new List<Telephely>(AvailableTelephelyek);
            AvailableTelephelyForModification.RemoveAll(item => SelectedTelephelyekForModification.Contains(item));
            AllTelephelyForModification.ItemsSource = AvailableTelephelyForModification;
            AllTelephelyForModification.DataContext = this;
            AllTelephelyForModification.Items.Refresh();
            SelectedTelephelyekListBoxForModification.ItemsSource = SelectedTelephelyekForModification;
            SelectedTelephelyekListBoxForModification.DataContext = this;
            SelectedTelephelyekListBoxForModification.Items.Refresh();


        }
        private void ModifyAddTelephely_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTelephelyToAddModification != null)
            {
                SelectedTelephelyekForModification.Add(SelectedTelephelyToAddModification);
                AvailableTelephelyForModification.Remove(SelectedTelephelyToAddModification);
                SelectedTelephelyekListBoxForModification.Items.Refresh();
                AllTelephelyForModification.Items.Refresh();
                ValidateNewUserData();
            }
        }

        private void ModifyRemoveTelephely_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTelephelyToRemoveModification != null)
            {
                AvailableTelephelyForModification.Add(SelectedTelephelyToRemoveModification);
                SelectedTelephelyekForModification.Remove(SelectedTelephelyToRemoveModification);
                SelectedTelephelyekListBoxForModification.Items.Refresh();
                AllTelephelyForModification.Items.Refresh();
                ValidateNewUserData();
            }
        }

        private void DoActionForModification_Click(object sender, RoutedEventArgs e)
        {
            User user = new User()
            {
                Id = SelectedUser.Id,
                Username = NewUsernameForModification.Text,
                Fullname = NewFullnameForModification.Text,
                Password = EncryptionHelper.EncryptSha1(NewUserPasswordForModification.Password),
                Privilege = SelectedPrivilegeForModification,

            };
            foreach (Telephely telephely in SelectedTelephelyekForModification)
            {
                user.Telephelyek.Add(telephely);
            }     
            Answer answer = userService.ModifyUser(user);
            if (!answer.Error)
            {
                PrivilegesForModificationComboBox.SelectedIndex = -1;
                SelectedTelephelyekForModification.Clear();
                AvailableTelephelyForModification.Clear();
                SelectedTelephelyekListBoxForModification.Items.Refresh();
                AllTelephelyForModification.Items.Refresh();
                NewUsernameForModification.Text = "";
                NewFullnameForModification.Text = "";
                NewUserPasswordForModification.Clear();
                MessageBox.Show("Sikeres módosítás.");
            }
            else
            {
                MessageBox.Show("Sikertelen módosítás.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedUser != null && !(userService.DisableUser(SelectedUser.Id,new User() { Id=1})).Error) MessageBox.Show("Sikeres törlés");
            else MessageBox.Show("Sikertelen törlés.");
        }
        private void NewUserPasswordForModification_PasswordChanged(object sender, RoutedEventArgs e)
        {
            CanModifyUser();
        }

        private void NewFullnameForModification_TextChanged(object sender, TextChangedEventArgs e)
        {
            CanModifyUser();
        }

        private void NewUsernameForModification_TextChanged(object sender, TextChangedEventArgs e)
        {
            CanModifyUser();
        }
        private void CanModifyUser()
        {
            bool IsValid = true;
            string password = NewUserPasswordForModification.Password;
            string fullname = NewFullnameForModification.Text;
            string username = NewUsernameForModification.Text;


            if (password.Length == 0) IsValid = true;
            else if (!IsValidPassword(password)) IsValid = false;
            if (SelectedPrivilegeForModification == null) IsValid = false;
            else if (SelectedTelephelyekForModification.Count == 0) IsValid = false;
            else if (string.IsNullOrWhiteSpace(fullname) ||
                string.IsNullOrWhiteSpace(username)) IsValid = false;
            else if (!IsValidUsername(username)) IsValid = false;
            else if (!IsValidFullname(fullname)) IsValid = false;


            DoActionForModification.IsEnabled = IsValid;
        }


        #endregion

        private void AvailablePrivilegesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidateNewUserData();
        }
    }
}
