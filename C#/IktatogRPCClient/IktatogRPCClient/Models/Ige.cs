using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.Models
{
    class Ige:PropertyChangedBase
    {
		public Ige()
		{
			
		}
		private DateTime _letoltottIgeDatuma = DateTime.Parse("1991.02.24");

		public DateTime LetoltottIgeDatuma
		{
			get { return _letoltottIgeDatuma; }
			set { _letoltottIgeDatuma = value; }
		}

		public string IgeTitleWithDate
		{
			get { return $"Napi ige\n({DateTime.Today.Date.ToShortDateString()})"; }			
		}
		private string _napiIge = $"Örvendezni [fogok] népemmel. Nem hallatszik ott többé sírás és jajkiáltás hangja. (Ézs 65,19)\n\nJézus mondja: Így most ti is szomorúak vagytok, de ismét meglátlak majd titeket, és örülni fog a szívetek, és örömötöket senki sem veheti el tőletek… (Jn 16,22)";

		public string NapiIge
		{
			get { return _napiIge; }
			set { _napiIge = value;			
			}
		}
		
	}
}
