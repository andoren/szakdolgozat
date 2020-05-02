using Caliburn.Micro;
using Google.Protobuf;
using Iktato;
using IktatogRPCClient.Models;
using IktatogRPCClient.Models.Managers;
using Microsoft.Win32;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace IktatogRPCClient.ViewModels
{
	class DocumentHandlerViewModel : Screen
	{
		public DocumentHandlerViewModel(int IkonyvId)
		{
			this.ikonyvid = IkonyvId;
			LoadData(IkonyvId);
		}
		private async void LoadData(int IkonyvId) {
		
			Log.Debug("{Class} Adatok betöltése a szerverről. Id: {IkonyvId}", GetType(),IkonyvId);
			IkonyvDocuments = await serverHelper.GetDocumentInfoByIkonyvIdAsync(IkonyvId);
		}
		private int ikonyvid;
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
			Log.Debug("{Class} Dokumentum törlése gomb megnyomva. Doc: {SelectedDocument}", GetType(), SelectedDocument);
			if ( await serverHelper.RemoveDocumentAsync(SelectedDocument))
			{
				ModificationHappend = true;
				IkonyvDocuments.Remove(SelectedDocument);
				NotifyOfPropertyChange(() => IkonyvDocuments);
				Log.Debug("{Class} Sikeres törlés", GetType());
			}
			else {

				Log.Debug("{Class} Sikertelen törlés.", GetType());
			}
		}
		public bool CanDownloadDocument {
			get {
				return SelectedDocument != null;
			}
		}

		public object Logger { get; private set; }

		public async void DownloadDocument() {
			try
			{
				LoaderIsVisible = true;
				Log.Debug("{Class} Adatok letöltése a szerverről. Document: {SelectedDocument}", GetType(), SelectedDocument);
				Document rawdata = await serverHelper.GetDocumentByIdAsync(SelectedDocument);
				if (rawdata.Id != -1)
				{
					Log.Debug("{Class} Sikeres letöltés", GetType(), SelectedDocument);
					byte[] bytes = rawdata.Doc.ToByteArray();
					string temppath = Path.GetTempPath();
					string fullpath = $"{temppath}{rawdata.Name}.{rawdata.Type}";
					Log.Debug("{Class} Adatok mentése", GetType(), SelectedDocument);
					if (File.Exists(fullpath))
					{
						File.Delete(fullpath);
						File.WriteAllBytes(fullpath, bytes);
					}
					else File.WriteAllBytes(fullpath, bytes);
					Log.Debug("{Class} Sikeres adatmentés.", GetType(), SelectedDocument);
					try
					{
						Log.Debug("{Class} Letöltött documentum megnyitása.", GetType(), SelectedDocument);
						Process.Start(fullpath);
					}
					catch (Exception e)
					{
						InformationBox.ShowError(e);
					}
				}
				else
				{
					MessageBox.Show("Hiba a dokumentum letöltése közben.");
				}

				LoaderIsVisible = false;
			}
			catch (Exception e)
			{
				InformationBox.ShowError(e);
			}
		}
		public async Task UploadDocument() {
			Log.Debug("{Class} Feltöltés gomb megnyomva.", GetType());
			Log.Debug("{Class} Adatok beszerzése", GetType());
			string[] FileInfo = ChooseDataToUpload();
			if (string.IsNullOrWhiteSpace(FileInfo[2])) return;
			Log.Debug("{Class} Fájl feltöltés megkezdése.", GetType());
			LoaderIsVisible = true;
			Document document = new Document();
			document.Name = FileInfo[0];
			document.Type = FileInfo[1];
			Log.Debug("{Class} ByteArray to BysteString", GetType());
			document.Doc = ByteString.CopyFrom(GetBytesFromFile(FileInfo[2]));
			document.IkonyvId = ikonyvid;
			Log.Debug("{Class} Adatok feltöltése. Doc: {Name}", GetType(), document.Name);
			DocumentInfo uploadedDocument = await serverHelper.UploadDocumentAsync(document);
			if (uploadedDocument.Id != -1)
			{
				Log.Debug("{Class} Sikeres feltöltése. ", GetType());
				IkonyvDocuments.Add(uploadedDocument);
				NotifyOfPropertyChange(() => IkonyvDocuments);

				ModificationHappend = true;
			}
			else {
				Log.Debug("{Class} Sikertelen feltöltése. ", GetType());
				MessageBox.Show("Sikertelen feltöltés.");
			}
			LoaderIsVisible = false;
		}
		public void CancelButton() {
			Log.Debug("{Class} Mégse gomb megnyomva. ", GetType());
			eventAggregator.PublishOnUIThread(new DocumentHandlerClosed(ModificationHappend, IkonyvDocuments.Count > 0));
			TryClose();
		}
		private string[] ChooseDataToUpload() {
			
			string[] fileInfo = new string[3];
			try
			{
				Log.Debug("{Class} Dialog megnyitása ", GetType());
				OpenFileDialog dialog = new OpenFileDialog();
				dialog.Title = "Válaszd ki a dokumentumot.";
				dialog.FileName = "";
				dialog.Multiselect = false;
				dialog.Filter = "Feltölthető dokumentumok |*.doc;*.xls;*.ppt;*.pdf;*.PDF;*.docx;*.xlsx";
				dialog.ReadOnlyChecked = true;
				if (dialog.ShowDialog() == true)
				{
					string dokname = dialog.SafeFileName;
					int lastDotIndex = dokname.LastIndexOf('.');
					Log.Debug("{Class} Sikeres fájl kiválasztás. Name: {Dokname}", GetType(), dokname);
					fileInfo[0] = dokname.Substring(0, lastDotIndex);
					fileInfo[1] = dokname.Substring(lastDotIndex + 1);
					fileInfo[2] = dialog.FileName;
					return fileInfo;
				}
				else
				{
					Log.Debug("{Class} Sikertelen fájl kiválasztás.", GetType());
					return fileInfo;
				}
					
			}
			catch (Exception e) {
				InformationBox.ShowError(e);
			}
			return fileInfo;
	}
		private byte[] GetBytesFromFile(string FileName) {
			byte[] ba1 = new byte[1];
			FileStream fs;
			BinaryReader br;
			try
			{
				Log.Debug("{Class} Fájl megnyitása beolvasásra. ", GetType());
				fs = new FileStream(FileName, FileMode.Open);
				long lFileSize = fs.Length;
				br = new BinaryReader(fs);
				ba1 = br.ReadBytes((Int32)lFileSize);
				Log.Debug("{Class} Fájl beolvasva. ", GetType());
				br.Close();
				fs.Close();
			}
			catch (UnauthorizedAccessException)
			{
				Log.Debug("{Class} Nincs jogosultság. A fájl átmásolása tempbe. ", GetType());
				string temppath = Path.GetTempPath();
				string fullpath = temppath + $"{FileName}";
				if (File.Exists(fullpath))
				{
					Log.Debug("{Class} Ilyen nevu fájl már létezett. Törlése... ", GetType());
					File.Delete(fullpath);
					File.Copy(FileName, fullpath);
				}
				else File.Copy(FileName, fullpath);
				Log.Debug("{Class} Fájl másolása sikeres. ", GetType());
				fs = new FileStream(fullpath, FileMode.Open);
				long lFileSize = fs.Length;
				Log.Debug("{Class} Fájl beolvasása. ", GetType());
				br = new BinaryReader(fs);
				ba1 = br.ReadBytes((Int32)lFileSize);
				Log.Debug("{Class} Fájl beolvasva. ", GetType());
				br.Close();
				fs.Close();

			}
			catch (Exception e)
			{
				InformationBox.ShowError(e);
			}
			return (ba1);
		}
	}
}
