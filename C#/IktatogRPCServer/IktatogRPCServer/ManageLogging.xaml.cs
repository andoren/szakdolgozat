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
using Serilog;
using Serilog.Events;
using IktatogRPCServer.Exceptions;

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
            ServerLoggingLevelComboBox.ItemsSource = Enum.GetValues(typeof(LogEventLevel));
            ServerLoggingLevelToShowComboBox.ItemsSource = Enum.GetValues(typeof(LogEventLevel));
            ServerCurrentLogLevel.Text = Enum.GetName(typeof(LogEventLevel),MainWindow.GetLogLevel());
            ServerCurrentLogLevelToShow.Text= Enum.GetName(typeof(LogEventLevel), RegistryHelper.GetLogLevelToShow());
        }
        
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
            try
            {
                RegistryHelper.SetLogPath(NewLoggingPath.Text);
            }
            catch (Exception ex) {
                Log.Error(ex.Message);
                return;
            }
            MessageBox.Show("A logolás elérésí útja módosult.");
            InformationText.Visibility = Visibility.Visible;
        }

        private void ModifyServerLogLevel_Click(object sender, RoutedEventArgs e)
        {
            if (ServerLoggingLevelComboBox.SelectedItem == null) return;
            try
            {
                RegistryHelper.SetLogLevel((int)ServerLoggingLevelComboBox.SelectedItem);
            }
            catch (Exception ex) {
                Log.Warning(ex.Message);
                return;
            }
            MessageBox.Show("A szerver logolási szintje módosult");
            InformationText.Visibility = Visibility.Visible;
        }
        private void ModifyCurrentLogLevelToShow_Click(object sender, RoutedEventArgs e)
        {
            if (ServerLoggingLevelToShowComboBox.SelectedItem == null) return;
            try
            {
                RegistryHelper.SetLogLevelToShow((int)ServerLoggingLevelToShowComboBox.SelectedItem);
                MessageBox.Show("A mutatott logolási szint módosult");
                InformationText.Visibility = Visibility.Visible;
            }
            catch (Exception ex) {
                Log.Warning(ex.Message);
            }
        }
    }
}
