using IktatogRPCServer.Database;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using MySql.Data.MySqlClient;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;



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



        private async void SaveSqlButton_Click(object sender, RoutedEventArgs e)
        {
            SaveProgressBar.IsIndeterminate = true;
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
                await Task.Run(() =>
                {
                    try
                    {

                        file = dlg.FileName + $"\\{DateTime.Now.ToString().Replace(".", "").Replace(":", "")}IktatoSQLBackup.sql";
                        
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
                                    mb.ExportToFile(file);
                                    conn.Close();
                                }
                            }
                        }
                        Dispatcher.Invoke(()=>{
                            PathToSave.Text = file;
                            SaveProgressBar.IsIndeterminate = false;
                            SaveStatus.Text = $"Az adatbázis mentése sikeresen befejeződött.";
                            if (!IsErrorOccured) MessageBox.Show("Sikeres mentés.");
                            else
                            {
                                SaveStatus.Text = $"Hiba az adatbázis mentése közben.";

                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Hiba történt az adatbázis mentése során. {Message}", ex.Message);
                        MessageBox.Show("Hiba történt.");
                    }
                });
            }


        }


        private async void ImportSqlButton_Click(object sender, RoutedEventArgs e)
        {
            ImportProgressBar.IsIndeterminate = true;
            OpenFileDialog dialog = new OpenFileDialog();
            string file;
            string constring = connectionManager.ConnectionString;
            dialog.Filter = "SQL script| *sql;";
            dialog.Multiselect = false;
            dialog.CheckFileExists = true;
            dialog.Title = "Script kiválasztása";
            if ((bool)dialog.ShowDialog())
            {
                await Task.Run(() =>
                {
                    try
                    {
                        IsErrorOccured = false;
                        file = dialog.FileName;                      
                        using (MySqlConnection conn = new MySqlConnection(constring))
                        {
                            using (MySqlCommand cmd = new MySqlCommand())
                            {
                                using (MySqlBackup mb = new MySqlBackup(cmd))
                                {
                                    cmd.Connection = conn;
                                    conn.Open();                                 
                                    mb.ImportFromFile(file);
                                    conn.Close();
                                }
                            }
                        }
                        Dispatcher.Invoke(()=> {
                            ImportPath.Text = file;
                            ImportProgressBar.IsIndeterminate = false;
                            ImportStatus.Text = $"Az adatbázis importálása sikeresen befejeződött.";
                            if (!IsErrorOccured) MessageBox.Show("Sikeres importálás.");
                            else
                            {
                                SaveStatus.Text = $"Hiba az adatbázis importálása közben.";
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Hiba történt az adatbázis importálása során. {Message}", ex.Message);
                        MessageBox.Show("Hiba történt.");
                    }
                });
            }
        }
    }
}
