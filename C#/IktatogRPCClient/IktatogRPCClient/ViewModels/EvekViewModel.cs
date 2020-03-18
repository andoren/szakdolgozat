using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Managers;
using IktatogRPCClient.Models.Managers;
using IktatogRPCClient.Models.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.ViewModels
{
    class EvekViewModel : Screen
    {

        public EvekViewModel()
        {
            Initalize();
            
        }
        private async void Initalize() {
            Years = await serverHelper.GetYears();
            eventAggregator.Subscribe(this);
        }
        private ServerHelperSingleton serverHelper = ServerHelperSingleton.GetInstance();
        private BindableCollection<Year> _years = new BindableCollection<Year>();
        private EventAggregatorSingleton eventAggregator = EventAggregatorSingleton.GetInstance();

        public BindableCollection<Year> Years
        {
            get { 
                return _years; }
            set { 
                _years = value;
                NotifyOfPropertyChange(()=>Years);
            }
        }
        public  async void DoAction() {
            if (await serverHelper.AddYearAndActivateAsync()) {
                int maxYear = Years.Max(x => x.Year_);
                Years.Add(new Year() { Year_ = maxYear + 1 });
            }
        }
        public void Handle(Year message)
        {
            Years.Add(message);
            NotifyOfPropertyChange(()=>Years);
        }
    }
}
