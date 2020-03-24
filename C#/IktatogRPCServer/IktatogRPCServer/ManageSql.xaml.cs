using IktatogRPCServer.Database;
using Microsoft.WindowsAPICodePack.Dialogs;
using MySql.Data.MySqlClient;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ManageSql.xaml
    /// </summary>
    public partial class ManageSql : UserControl
    {
        public ManageSql()
        {
            InitializeComponent();
            connectionManager = new ConnectionManager();
        }
        private bool IsErrorOccured = false;
        private ConnectionManager connectionManager;
        private void SaveSqlButton_Click(object sender, RoutedEventArgs e)
        {
            string constring = connectionManager.ConnectionString;
            string file = $"{Directory.GetCurrentDirectory()}\\Backup\\backup.sql";
            var dlg = new CommonOpenFileDialog();
            dlg.Title = "Adatbázis mentése.";
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
                try
                {
                    file = dlg.FileName +$"\\{DateTime.Now.ToString().Replace(".","").Replace(":","")}IktatoSQLBackup.sql";

                    using (MySqlConnection conn = new MySqlConnection(constring))
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            using (MySqlBackup mb = new MySqlBackup(cmd))
                            {
                                cmd.Connection = conn;
                                conn.Open();
                                mb.ExportInfo.AddCreateDatabase = true;
                                mb.ExportInfo.ExportTableStructure = true;
                                mb.ExportInfo.ExportRows = true;
                                mb.ExportInfo.ExportRoutinesWithoutDefiner = true;
                                mb.ExportInfo.ExportProcedures = true;
                                mb.ExportInfo.ExportFunctions = true;
                                mb.ExportInfo.ExportViews = true;
                                mb.ExportInfo.AddDropDatabase = true;
                                mb.ExportInfo.AddDropTable = true;
                                mb.ExportInfo.IntervalForProgressReport = 50;
                                mb.ExportInfo.GetTotalRowsMode = GetTotalRowsMethod.SelectCount;
                                mb.ExportProgressChanged += Mb_ExportProgressChanged;
                                SaveProgressBar.Value = 0;
                                SavePrecentage.Text = $"0%";
                                SaveStatus.Text = $"";
                                mb.ExportCompleted += Mb_ExportCompleted;
                                mb.ExportToFile(file);
                       
                                conn.Close();
            
                            }
                        }
                    }
                    PathToSave.Text = file;
                    SaveProgressBar.Value = 100;
                    SavePrecentage.Text = $"100%";
                    SaveStatus.Text = $"Az adatbázis mentése sikeresen befejeződött.";
                    if (!IsErrorOccured) MessageBox.Show("Sikeres mentés.");
                    else {
                        SaveStatus.Text = $"Hiba az adatbázis mentése közben.";
                       
                    }
                }
                catch (Exception ex) {
                    Log.Error("Hiba történt az adatbázis mentése során. {Message}",ex.Message);
                    MessageBox.Show("Hiba történt.");
                }
            }

    
        }

        private void Mb_ExportCompleted(object sender, ExportCompleteArgs e)
        {
            IsErrorOccured = e.HasError;
        }

        private long AllRowCount = 0;
        private long SavedRowCount = 0;
        private void Mb_ExportProgressChanged(object sender, ExportProgressArgs e)
        {
            Dispatcher.Invoke(()=> {
                if (SaveProgressBar.Value !=100) { 
                AllRowCount = e.TotalRowsInAllTables;
                SavedRowCount += e.TotalRowsInCurrentTable;
                int percentage = (int)(((double)SavedRowCount / AllRowCount) * 100);
                SaveProgressBar.Value = percentage;
                SavePrecentage.Text = $"{percentage}%";
                SaveStatus.Text = $"{e.CurrentTableName} tábla mentése.";
                }

            });
        }

        private void ImportSqlButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
