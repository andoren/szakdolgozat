using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.Models
{
    public class RemovedItem
    {
        public RemovedItem(object Item)
        {
            this.Item = Item;
        }
        private object _item;

        public object Item
        {
            get { return _item; }
            private set { _item = value; }
        }

    }
}
