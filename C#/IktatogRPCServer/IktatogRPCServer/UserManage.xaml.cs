using Iktato;
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
        }
        private List<User> _allUser;

        public List<User> AllUser
        {
            get { return _allUser; }
            set { _allUser = value; }
        }

        private List<Telephely> _availableTelephelyek;


        public List<Telephely> AvailableTelephelyek
        {
            get { return _availableTelephelyek; }
            set { _availableTelephelyek = value; }
        }

        private void AllUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NewPassword.IsEnabled = true;
        }
    }
}
