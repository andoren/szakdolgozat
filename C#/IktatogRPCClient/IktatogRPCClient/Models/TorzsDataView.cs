using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.Models
{
    abstract class TorzsDataView<T> : Screen where T : new()
    {
        public TorzsDataView()
        {

        }
        protected EventAggregatorSingleton eventAggregator = EventAggregatorSingleton.GetInstance();
        protected ServerHelper serverHelper = ServerHelper.GetInstance();
        protected override void OnActivate()
        {
            eventAggregator.Subscribe(this);
            base.OnActivate();
        }

        protected override void OnDeactivate(bool close)
        {
            eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }

        public void CancelAction() {
            eventAggregator.PublishOnUIThread(new T());
            TryClose();
        }
        public abstract void DoAction();
    }
}
