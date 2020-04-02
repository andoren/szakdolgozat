using Caliburn.Micro;
using IktatogRPCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IktatogRPCClient.ViewModels
{
    class FooldalViewModel : Screen
    {
        public FooldalViewModel()
        {
            
            if(_ige.LetoltottIgeDatuma < DateTime.Today.Date)DownloadIge();
        }
        private static Ige _ige = new Ige();

       
        public string IgeTitleWithDate
        {
            get
            {
                return _ige.IgeTitleWithDate;
            }
        }
        
        public string NapiIge
        {
            get
            {
                return _ige.NapiIge;
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
                _ige.NapiIge = BuildDailyIge(result);
                _ige.LetoltottIgeDatuma = DateTime.Today.Date;
                NotifyOfPropertyChange(()=>NapiIge);
            }
            catch (WebException e)
            {
                InformationBox.ShowError(e);
            }
        }
        private string BuildDailyIge(string rawstring) {
            StringBuilder stringBuilder = new StringBuilder();
            Regex rxText = new Regex(@"<\s*p[^>]*>(.*?)\s*\(",RegexOptions.Multiline);
            Regex rxFrom = new Regex(@"<a [^>]+>(.*?)<\/a>", RegexOptions.Multiline);
            MatchCollection TextMatches = rxText.Matches(rawstring);
            MatchCollection FromMatches = rxFrom.Matches(rawstring);
            string firstText = TextMatches[0].Value.Replace("<p>","");
            string secondText = TextMatches[1].Value.Replace("<p>", "");
            string firstFromText = FromMatches[0].Value.Substring(FromMatches[0].Value.IndexOf(">")+1).Replace("</a>", "");
            string secondFromText = FromMatches[1].Value.Substring(FromMatches[1].Value.IndexOf(">") + 1).Replace("</a>", "");
            return
                stringBuilder
                .Append(firstText)
                .Append(firstFromText)
                .Append(")")
                .Append("\n\n")
                .Append(secondText)
                .Append(secondFromText)
                .Append(")")
                .ToString();
        }
    }
}
