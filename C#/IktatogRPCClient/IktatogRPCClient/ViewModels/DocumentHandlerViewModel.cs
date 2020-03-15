using Caliburn.Micro;
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
			IkonyvDocuments = serverHelper.GetDocumentInfoByIkonyvId(IkonyvId);
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

		public void RemoveDocument() {
			if (serverHelper.RemoveDocument(SelectedDocument))
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
			Document rawdata = await serverHelper.GetDocumentById(SelectedDocument);
			try
			{
				Process.Start(@"C:\Users\Misi\Desktop\1.pdf");
			}
			catch (Exception )
			{
				
			}
			LoaderIsVisible = false;
		}
		public async Task UploadDocument() {
			string documentPath = ChooseDataToUpload();
			if (documentPath == "") return;
			LoaderIsVisible = true;
			DocumentInfo uploadedDocument = await serverHelper.UploadDocument(GetBytesFromFile(documentPath));
			IkonyvDocuments.Add(uploadedDocument);
			NotifyOfPropertyChange(()=>IkonyvDocuments);
			LoaderIsVisible = false;
			ModificationHappend = true;
		}
		public void CancelButton() {
			eventAggregator.PublishOnUIThread(new DocumentHandlerClosed(ModificationHappend, IkonyvDocuments.Count > 0));
			TryClose();
		}
		private string ChooseDataToUpload() {
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Title = "Válaszd ki a dokumentumot.";
			dialog.FileName = "";
			dialog.Multiselect = false;
			dialog.Filter = "Feltölthető dokumentumok |*.doc;*.xls;*.ppt;*.pdf,*.docx;*.xlsx" ;
			dialog.ReadOnlyChecked = true;
			if (dialog.ShowDialog() == true)
			{
				return dialog.FileName;
			}
			else
				return "";

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
