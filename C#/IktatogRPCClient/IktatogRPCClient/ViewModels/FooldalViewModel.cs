using Caliburn.Micro;
using IktatogRPCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.ViewModels
{
    class FooldalViewModel : Screen
    {
        public FooldalViewModel()
        {
            Ige = new Ige();
            DownloadIge();
        }
        private Ige _ige;

        public Ige Ige
        {
            get
            {
                return _ige;
            }
            set
            {
                _ige = value;
            }
        }
        public string IgeTitleWithDate
        {
            get
            {
                return Ige.IgeTitleWithDate;
            }
        }
        
        public string NapiIge
        {
            get
            {
                return Ige.NapiIge;
            }
        }
        private async void DownloadIge()
        {
            try
            {
                string result = "";
                using (WebClient client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    result = await client.DownloadStringTaskAsync("https://napiige.lutheran.hu/igek.php");
                }
                Ige.NapiIge = result;
                NotifyOfPropertyChange(()=>NapiIge);
            }
            catch (WebException e)
            {
                InformationBox.ShowError(e);
            }
        }

    }
}
