# Iktat� program adatb�zis	


# User

A programot haszn�l� dolgoz�k adatait tartalmazza.
Felhaszn�l�n�v aminek a maximum m�rete 45 karakterhossz�.
Jelsz� ami SHA1-ben lesz elt�rolva.
A felhaszn�l� teljes neve.
## �vek:
* Meghat�rozz�k az iktat�sz�m �v�t illetve, hogy melyik �vbe lehet m�g iktatni. Az �vz�r�st Admin jogosults�ggal rendelkez�k v�gezhetik majd el.
## Privilege
A felhaszn�l�knak a jogosults�gi szintje a programban.
### Admin:
* Az admin jogosults�ggal rendelkez�k a t�rzseket szabadon szerkeszthetik.
* �vet z�rhatnak.
* Felhaszn�l�kat is  hozz� tudnak adni a rendszerhez. 	
* T�r�lhetnek iktat�sokat.
### User:
* Iktat�s a programban.
* �gyint�z�ket hozz�adhat a t�rzsh�z.
	
## Felh_telephely

Minden felhaszn�l�hoz tartozik egy vagy t�bb telephely ahova tud iktatni. Illetve ahol a t�rzs adatokat tudja r�gz�teni. A jogosults�g az vagy az �sszesn�l admin vagy user.

# Telephely

Ez jel�li, hogy az adat az melyik telephelyhez tartozik az adatb�zisban. Ez vonatkozik az iktat�sra �s a t�rzsadatokra is.

# Ikonyv

Ez maga az iktat� k�nyv. Ha bej�n egy irat vagy kimegy azt itt kell  ebben lesz r�gzitve. Az iktat�sz�mot storedprocedure-val fogom el��ll�tani a megadott adatok alapj�n �s az id-je alapj�n, illetve hogy van-e iktatottid-je ami arra utal, hogy ez egy v�lasz irat egy el�z�leg m�r iktatott iratra.
## Fel�p�t�se:
* id: ikonyv azonos�t�sa.
* T�rgy: Iktat�s t�rgya. Egy r�vid le�r�s arr�l a dokumentumr�l ami be�rkezett.
* Sz�veg: Ami az iktat�snak egy hosszabb le�r�sa ha sz�ks�ges, de �ltal�ban csak a t�rgy lesz megadva meg a beszkennelt dokumentum hozz�.
* Hivsz�m: Ez akkor fordulhat el� ha egy be�rkez� dokumentumot iktatunk �s annak van hivatkoz�si vagy iktat�si sz�ma.
* �gyintez�: Ez a szervezeten bel�li dolgoz� koll�g�ra utal, hogy ezt az �gyet vagy iratot ki int�zi. 
* Partner: Az a szem�ly, int�zm�ny vagy c�g aki k�lde az iratot. Munkaszerz�d�sekn�l a partner a dolgoz� nev�t jel�li. Lehet p�ld�ul E-on, J�r�si hivatal....  Ide a partnerugyintezokapcsolot fogom becsatolni miut�n adok neki egy primary key id-t.
* Csoport: Az iratok azon t�pusait jel�li, amely egys�ghez kapcsol�dik az iktatand� anyag. P�ld�ul Ell�totti, F�z�konyha, Munka�gy.
* Telephely: Jel�li, hogy melyik telephelyhez tartozik az iktat�s.
* Jelleg: A dokumentum formai megjelen�s�nek megad�sa. Ez lehet e-mail, k�ldem�ny, fax, lev�l, munka�gyi irat.
* Ir�nya: Az irat az bej�v� vagy kimen� volt-e.
* �rkezett: Az irat �rkez�s�nek a d�tuma vagy ha kimen� akkor a kimenet�nek a d�tuma.
* Hatid�: Meddig kell az iratra reag�lni a lej�rathoz k�zel a programban jelezni fogjuk.
* V�laszid: Amit besz�lt�nk, hogy ha v�lasz az iktat�s akkor nem null hivatkozik az el�z� iktat�s�ra. Lehet null hogyha nem v�lasz. 
* iktatottID: Pl hanyadik v�lasz vagy csak sim�n hanyadik kimen� vagy bej�v� iktat�s.
* iktatoszam: ezt gener�lni fogjuk az adatokb�l. Egy p�lda: B-SZ/R/3/2019 
	 * Az els� karakteret az hat�rozza meg, hogy K - kimen� vagy B  - bej�v�
	* A m�sodik karakter a Csoport hat�rozza meg SZ pl szerz�d�s
	 * A harmadik karakter a telephely jel�li pl. R - R�k�czi, V- Vajda  stb..
	* A negyedik karakter sorozat a sorsz�m ami lehet k�t�jeles Pl. B-SZ/R/3-1/2019 vagy B-SZ/R/3-1-1/2019 a v�laszokhoz m�rve
	 * Az utols� r�sz pedig az �vet jel�li.
	 * Az iktatosz�mot elt�rolom gener�l�sa ut�n.
## Ikonyv docs

Az iktat�k�nyvh�z tartoz� dokumentumok
## Doc
Maga a dokumentum ahol mediumblob-ban t�rolom az adatokat. Illetve elt�rolom a kiterjeszt�s�t �s a nev�t is a doksinak. Lehet Pdf, Excel, Word, JPG.

