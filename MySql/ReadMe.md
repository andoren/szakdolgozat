# Iktató program adatbázis	


# User

A programot használó dolgozók adatait tartalmazza.
Felhasználónév aminek a maximum mérete 45 karakterhosszú.
Jelszó ami SHA1-ben lesz eltárolva.
A felhasználó teljes neve.
## Évek:
* Meghatározzák az iktatószám évét illetve, hogy melyik évbe lehet még iktatni. Az évzárást Admin jogosultsággal rendelkezõk végezhetik majd el.
## Privilege
A felhasználóknak a jogosultsági szintje a programban.
### Admin:
* Az admin jogosultsággal rendelkezõk a törzseket szabadon szerkeszthetik.
* Évet zárhatnak.
* Felhasználókat is  hozzá tudnak adni a rendszerhez. 	
* Törölhetnek iktatásokat.
### User:
* Iktatás a programban.
* Ügyintézõket hozzáadhat a törzshöz.
	
## Felh_telephely

Minden felhasználóhoz tartozik egy vagy több telephely ahova tud iktatni. Illetve ahol a törzs adatokat tudja rögzíteni. A jogosultság az vagy az összesnél admin vagy user.

# Telephely

Ez jelöli, hogy az adat az melyik telephelyhez tartozik az adatbázisban. Ez vonatkozik az iktatásra és a törzsadatokra is.

# Ikonyv

Ez maga az iktató könyv. Ha bejön egy irat vagy kimegy azt itt kell  ebben lesz rögzitve. Az iktatószámot storedprocedure-val fogom elõállítani a megadott adatok alapján és az id-je alapján, illetve hogy van-e iktatottid-je ami arra utal, hogy ez egy válasz irat egy elõzõleg már iktatott iratra.
## Felépítése:
* id: ikonyv azonosítása.
* Tárgy: Iktatás tárgya. Egy rövid leírás arról a dokumentumról ami beérkezett.
* Szöveg: Ami az iktatásnak egy hosszabb leírása ha szükséges, de általában csak a tárgy lesz megadva meg a beszkennelt dokumentum hozzá.
* Hivszám: Ez akkor fordulhat elõ ha egy beérkezõ dokumentumot iktatunk és annak van hivatkozási vagy iktatási száma.
* Ügyintezõ: Ez a szervezeten belüli dolgozó kollégára utal, hogy ezt az ügyet vagy iratot ki intézi. 
* Partner: Az a személy, intézmény vagy cég aki külde az iratot. Munkaszerzõdéseknél a partner a dolgozó nevét jelöli. Lehet például E-on, Járási hivatal....  Ide a partnerugyintezokapcsolot fogom becsatolni miután adok neki egy primary key id-t.
* Csoport: Az iratok azon típusait jelöli, amely egységhez kapcsolódik az iktatandó anyag. Például Ellátotti, Fõzõkonyha, Munkaügy.
* Telephely: Jelöli, hogy melyik telephelyhez tartozik az iktatás.
* Jelleg: A dokumentum formai megjelenésének megadása. Ez lehet e-mail, küldemény, fax, levél, munkaügyi irat.
* Iránya: Az irat az bejövö vagy kimenõ volt-e.
* Érkezett: Az irat érkezésének a dátuma vagy ha kimenõ akkor a kimenetének a dátuma.
* Hatidõ: Meddig kell az iratra reagálni a lejárathoz közel a programban jelezni fogjuk.
* Válaszid: Amit beszéltünk, hogy ha válasz az iktatás akkor nem null hivatkozik az elõzõ iktatására. Lehet null hogyha nem válasz. 
* iktatottID: Pl hanyadik válasz vagy csak simán hanyadik kimenõ vagy bejövö iktatás.
* iktatoszam: ezt generálni fogjuk az adatokból. Egy példa: B-SZ/R/3/2019 
	 * Az elsõ karakteret az határozza meg, hogy K - kimenõ vagy B  - bejövõ
	* A második karakter a Csoport határozza meg SZ pl szerzõdés
	 * A harmadik karakter a telephely jelöli pl. R - Rákóczi, V- Vajda  stb..
	* A negyedik karakter sorozat a sorszám ami lehet kötöjeles Pl. B-SZ/R/3-1/2019 vagy B-SZ/R/3-1-1/2019 a válaszokhoz mérve
	 * Az utolsó rész pedig az évet jelöli.
	 * Az iktatoszámot eltárolom generálása után.
## Ikonyv docs

Az iktatókönyvhöz tartozó dokumentumok
## Doc
Maga a dokumentum ahol mediumblob-ban tárolom az adatokat. Illetve eltárolom a kiterjesztését és a nevét is a doksinak. Lehet Pdf, Excel, Word, JPG.

