using Caliburn.Micro;
using Google.Protobuf;
using Iktato;
using IktatogRPCClient.Models;
using IktatogRPCClient.Models.Managers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IktatogRPCClient.ViewModels
{
    class DocumentHandlerViewModel:Screen
    {
		public DocumentHandlerViewModel(int IkonyvId)
		{
			IkonyvDocuments = serverHelper.GetDocumentInfoByIkonyvIdAsync(IkonyvId).Result;
		}
        private EventAggregatorSingleton eventAggregator = EventAggregatorSingleton.GetInstance();
		private ServerHelperSingleton serverHelper = ServerHelperSingleton.GetInstance();
		private BindableCollection<DocumentInfo> _ikonyvDocuments = new BindableCollection<DocumentInfo>();

		private DocumentInfo _selectedDocument;
		public bool ModificationHappend { get; set; } = false;
		public DocumentInfo SelectedDocument
		{
			get { return _selectedDocument; }
			set {
				_selectedDocument = value;
				NotifyOfPropertyChange(()=> CanDownloadDocument);
			}
		}

		public BindableCollection<DocumentInfo> IkonyvDocuments
		{
			get { return _ikonyvDocuments; }
			set { _ikonyvDocuments = value;
				NotifyOfPropertyChange(()=>IkonyvDocuments);
			}
		}
		private bool _loaderIsVisible = false;
		public bool LoaderIsVisible {
			get {
				return _loaderIsVisible;
			}
			set {
				_loaderIsVisible = value;
				NotifyOfPropertyChange(()=>LoaderIsVisible);
			}
		}

		public async void RemoveDocument() {
			if ( await serverHelper.RemoveDocumentAsync(SelectedDocument))
			{
				ModificationHappend = true;
				IkonyvDocuments.Remove(SelectedDocument);
				NotifyOfPropertyChange(() => IkonyvDocuments);
			}
		}
		public bool CanDownloadDocument {
			get {
				return SelectedDocument != null;
			}
		}
		public async Task DownloadDocument() {
		
			LoaderIsVisible = true;
			Document rawdata = await serverHelper.GetDocumentByIdAsync(SelectedDocument);
			byte[] bytes = rawdata.Doc.ToByteArray();
			string temppath = System.IO.Path.GetTempPath();
			string fullpath = $"{temppath}{rawdata.Name}.{rawdata.Type}";
			if (File.Exists(fullpath))
			{
				File.Delete(fullpath);
				File.WriteAllBytes(fullpath, bytes);
			}
			else File.WriteAllBytes(fullpath, bytes);
		
			try
			{
				Process.Start(fullpath);
			}
			catch (Exception)
			{
				throw;
			}
			LoaderIsVisible = false;
		}
		public async Task UploadDocument() {
			string[] FileInfo = ChooseDataToUpload();
			if (FileInfo[2] == "") return;
			LoaderIsVisible = true;
			Document document = new Document();
			document.Name = FileInfo[0];
			document.Type = FileInfo[1];
			document.Doc = ByteString.CopyFrom(GetBytesFromFile(FileInfo[2]));
			DocumentInfo uploadedDocument = await serverHelper.UploadDocumentAsync(document);
			IkonyvDocuments.Add(uploadedDocument);
			NotifyOfPropertyChange(()=>IkonyvDocuments);
			LoaderIsVisible = false;
			ModificationHappend = true;
		}
		public void CancelButton() {
			eventAggregator.PublishOnUIThread(new DocumentHandlerClosed(ModificationHappend, IkonyvDocuments.Count > 0));
			TryClose();
		}
		private string[] ChooseDataToUpload() {
			string[] fileInfo = new string[3];
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Title = "Válaszd ki a dokumentumot.";
			dialog.FileName = "";
			dialog.Multiselect = false;
			dialog.Filter = "Feltölthető dokumentumok |*.doc;*.xls;*.ppt;*.pdf,*.docx;*.xlsx" ;
			dialog.ReadOnlyChecked = true;
			if (dialog.ShowDialog() == true)
			{
				
 				string[] fileName = dialog.SafeFileName.Split('.');
				fileInfo[0] = fileName[0];
				fileInfo[1] = fileName[1];
				fileInfo[2] =  dialog.FileName;
				return fileInfo;
			}
			else
				return fileInfo;

	}
		private byte[] GetBytesFromFile(string FileName) {
			byte[] ba1 = new byte[1];
			FileStream fs;
			BinaryReader br;
			try
			{

				fs = new FileStream(FileName, FileMode.Open);
				long lFileSize = fs.Length;
				br = new BinaryReader(fs);
				ba1 = br.ReadBytes((Int32)lFileSize);

				br.Close();
				fs.Close();
			}
			catch (UnauthorizedAccessException)
			{
				string temppath = Path.GetTempPath();
				string fullpath = temppath + $"{FileName}";
				if (File.Exists(fullpath))
				{
					File.Delete(fullpath);
					File.Copy(FileName, fullpath);
				}
				else File.Copy(FileName, fullpath);
				fs = new FileStream(fullpath, FileMode.Open);
				long lFileSize = fs.Length;

				br = new BinaryReader(fs);
				ba1 = br.ReadBytes((Int32)lFileSize);

				br.Close();
				fs.Close();

			}
			catch (Exception e)
			{
				System.Windows.MessageBox.Show(e.Message);
			}
			return (ba1);
		}
	}
}
