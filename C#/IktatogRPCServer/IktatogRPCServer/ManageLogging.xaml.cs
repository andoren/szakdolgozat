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
            ServerCurrentLogLevel.Text = Enum.GetName(typeof(LogEventLevel), RegistryHelper.GetLogLevel());
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
            RegistryHelper.SetLogPath(NewLoggingPath.Text);
            MessageBox.Show("A logolás elérésí útja módosult.");
            InformationText.Visibility = Visibility.Visible;
        }

        private void ModifyServerLogLevel_Click(object sender, RoutedEventArgs e)
        {
            if (ServerLoggingLevelComboBox.SelectedItem == null) return;
            RegistryHelper.SetLogLevel((int)ServerLoggingLevelComboBox.SelectedItem);
            MessageBox.Show("A szerver logolási szintje módosult");
            InformationText.Visibility = Visibility.Visible;
        }

        private void ModifyCurrentLogLevelToShow_Click(object sender, RoutedEventArgs e)
        {
            if (ServerLoggingLevelToShowComboBox.SelectedItem == null) return;
            int currentLogLevel = (int)RegistryHelper.GetLogLevel();
            int setLogLevelToShow = (int)ServerLoggingLevelToShowComboBox.SelectedItem;
            if (currentLogLevel <= setLogLevelToShow)
            {
                RegistryHelper.SetLogLevelToShow(setLogLevelToShow);
                MessageBox.Show("A mutatott logolási szint módosult");
                InformationText.Visibility = Visibility.Visible;
            }
            else {
                MessageBox.Show("A mutatott logolás szintje nem lehet kisebb mint a szerver logolási szintje.");
            }
        }
    }
}
