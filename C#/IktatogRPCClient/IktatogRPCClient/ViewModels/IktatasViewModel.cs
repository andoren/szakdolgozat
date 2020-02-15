using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iktato;
namespace IktatogRPCClient.ViewModels
{
    class IktatasViewModel:Screen
    {
		public IktatasViewModel()
		{
		    RecentlyAddedIkonyvek = new BindableCollection<Ikonyv>() {
                new Ikonyv() {
                CreatedBy = new User { Id= 1, Username = "misi" },
                Csoport = new Csoport() {Id = 5, Name="Kiscica", Shortname= "KC" },
                Erkezett = "2020.02.15",
                HatIdo = "2020.02.20",
                Id=232,
                Hivszam="1234/412Hivszám",
                Iktatoszam= "B-R/KC/M/2020",
                Jelleg = new Jelleg(){  Id=1 , Name="Levél"},
                Irany = 0,
                Partner = new Partner(){ Id= 1, Name = "MacskaKonzervGyártó", Ugyintezok = new PartnerUgyintezo(){ Id= 1, Name="FluffyBoy" } },
                Ugyintezo = new Ugyintezo(){Id = 1, Name="Brachna Anita"},
                Szoveg="Éhesek a cicám valamit jó volna baszni ennek a dolognak mert ez már nem állapot roar!",
                Targy="Cica éhes",
                Telephely = new Telephely(){ Id = 1 , Name= "Rákóczi"}

                },                new Ikonyv() {
                CreatedBy = new User { Id= 1, Username = "misi" },
                Csoport = new Csoport() {Id = 5, Name="Kiscica2", Shortname= "KC" },
                Erkezett = "2020.02.15",
                HatIdo = "2020.02.20",
                Id=232,
                Hivszam="1234/412Hivszám",
                Iktatoszam= "B-R2/KC/M/2020",
                Jelleg = new Jelleg(){  Id=1 , Name="Levél"},
                Irany = 1,
                Partner = new Partner(){ Id= 1, Name = "MacskaKonzervGyártó", Ugyintezok = new PartnerUgyintezo(){ Id= 1, Name="FluffyBoy" } },
                Ugyintezo = new Ugyintezo(){Id = 1, Name="Brachna Anita"},
                Szoveg="Éhesek a2 cicám valamit jó volna baszni ennek a dolognak mert ez már nem állapot roar!",
                Targy="Cica éhes2",
                Telephely = new Telephely(){ Id = 1 , Name= "Rákóczi"}

                }
            }; 
        }
		private BindableCollection<Ikonyv> _recentlyAddedIkonyvek;

		public BindableCollection<Ikonyv> RecentlyAddedIkonyvek
		{
			get { return _recentlyAddedIkonyvek; }
			set {
				_recentlyAddedIkonyvek = value;
				NotifyOfPropertyChange(()=> RecentlyAddedIkonyvek);
			}
		}

        private bool _valaszIsChecked;

        public bool ValaszIsChecked
        {
            get { return _valaszIsChecked; }
            set { _valaszIsChecked = value;
                NotifyOfPropertyChange(() => ValaszIsChecked);
            }
        }

    }
}
