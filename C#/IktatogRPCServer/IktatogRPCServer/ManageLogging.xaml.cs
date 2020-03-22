using Microsoft.Win32;
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
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;

namespace IktatogRPCServer
{
    /// <summary>
    /// Interaction logic for Logging.xaml
    /// </summary>
    public partial class ManageLogging : UserControl
    {
        public ManageLogging()
        {
            InitializeComponent();
            InformationText.Visibility = Visibility.Hidden;
            CurrentLoggingPath.Text = MainWindow.LogPath;
            NewLoggingPath.Text = MainWindow.LogPath;
            
        }
        const string userRoot = "HKEY_CURRENT_USER";
        const string subkey = "OtemplomIktato";
        const string keyName = userRoot + "\\" + subkey;
        private void ChoosePathButton_Click(object sender, RoutedEventArgs e)
        {

            var dlg = new CommonOpenFileDialog();
            dlg.Title = "Elérési út kiválasztása.";
            dlg.IsFolderPicker = true;
            dlg.InitialDirectory = Directory.GetCurrentDirectory();

            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = false;
            dlg.DefaultDirectory = Directory.GetCurrentDirectory();
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;
            dlg.ShowPlacesList = true;

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                NewLoggingPath.Text = dlg.FileName;
            }

        }

        private void ModifyPathButton_Click(object sender, RoutedEventArgs e)
        {
            RegistryHelper.SetLogPath(NewLoggingPath.Text);
            MessageBox.Show("A logolás elérésí útja módosult.");
        }
    }
}
