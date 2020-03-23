using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Managers;
using IktatogRPCClient.Models.Managers;
using IktatogRPCClient.Models.Managers.Helpers.Client;
using IktatogRPCClient.Models.Scenes;
using Serilog;
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
            Log.Debug("{Class} Adatok letöltése a szerverről... ", GetType());
            Years = await serverHelper.GetYears();
            if(Years.Count > 0)Log.Debug("{Class} Sikeres adat letöltés", GetType());
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
            Log.Warning("{Class} Év hozzáadása és aktiválása. User: {User}", GetType(),UserHelperSingleton.CurrentUser);
            if (await serverHelper.AddYearAndActivateAsync()) {
                Log.Warning("{Class}Sikeres év hozzáadás.", GetType());
                int maxYear = Years.Max(x => x.Year_) +1;
                Log.Warning("{Class} Az uj év: {MaxYear}", GetType(), maxYear);
                Years.Add(new Year() { Year_ = maxYear});
            }
        }
        public void Handle(Year message)
        {
            Years.Add(message);
            NotifyOfPropertyChange(()=>Years);
        }
    }
}
