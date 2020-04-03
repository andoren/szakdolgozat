using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.Models
{
    class DocumentHandlerClosed
    {
		public DocumentHandlerClosed(bool ModificationHappend,bool HasDocument)
		{
			this.ModificationHappend = ModificationHappend;
			this.HasDocument = HasDocument;
		}
		private bool _hasDocument;

		public bool HasDocument
		{
			get { return _hasDocument; }
			set { _hasDocument = value; }
		}

		private bool _modificationHappend;

		public bool ModificationHappend
		{
			get { return _modificationHappend; }
			private set { _modificationHappend = value; }
		}

	}
}
