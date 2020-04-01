using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.Models
{
    class Ige
    {


		public string IgeTitleWithDate
		{
			get { return $"Napi ige\n({DateTime.Today.Date.ToShortDateString()})"; }
			
		}

	}
}
