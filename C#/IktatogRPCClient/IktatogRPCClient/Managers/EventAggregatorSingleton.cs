using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.Models.Managers
{
    public sealed class EventAggregatorSingleton:EventAggregator
    {
        private volatile static EventAggregatorSingleton instance;

        public static EventAggregatorSingleton GetInstance() {
            lock (typeof(EventAggregatorSingleton)){
                if (instance == null) instance = new EventAggregatorSingleton();
                return instance;
            }
        }
    }
}
