using Iktato;
using IktatogRPCServer.Database;
using IktatogRPCServer.Database.Abstract;
using IktatogRPCServer.Database.Mysql;
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
            LoadData();
            
        }




        DatabaseManager<User> userDatabaseManager = new UserDatabaseManager(new ConnectionManager());
        DatabaseManager<Telephely> telephelyDatabaseManager = new TelephelyDatabaseManager(new ConnectionManager());
        DatabaseManager<Privilege> privilegeDatabaseManager = new PrivilegeDatabaseManager(new ConnectionManager());
        private async void LoadData() {
            await Task.Run(() => {
                AllUser = userDatabaseManager.GetAllData(new User());
                AvailableTelephelyek = telephelyDatabaseManager.GetAllData(new object());
                AvailablePrivileges = privilegeDatabaseManager.GetAllData(new object());
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
        }
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

        private void AllUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedUser != null)
            {
                NewPassword.IsEnabled = true;
            }
            else {
                NewPassword.IsEnabled = false;
            }
            
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
            else if (SelectedTelephelyek.Count == 0) IsValid = false;
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
                Password = NewUserPassword.Password,
                Privilege = SelectedPrivilege,

            };
            foreach (Telephely telephely in SelectedTelephelyek) {
                user.Telephelyek.Add(telephely);
            }
            {

            }
            user = userDatabaseManager.Add(new NewTorzsData() { User = user }, new User() { Id = 1 });
            if (user.Id != -1)
            {
                SelectedTelephelyek.Clear();
                AvailableTelephelyek = telephelyDatabaseManager.GetAllData(new object());
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
    }
}
