# ToiduHind.ee

## TALLINNA TÖÖSTUSHARIDUSKESKUS
### Projekti dokumentatsioon

- **Juhendaja**: Irina Merkulova & Marina Oleinik
- **Koostajad**: Vsevolod Tsarev & Bogdan Sergachev  
- **Kursus**: TARpv23  
- **Asukoht**: TALLINN  
- **Aasta**: 2025  

---

## Sisukord

1. [Eesmärk](#eesmärk)  
2. [Sihtrühm](#sihtrühm)  
3. [Projekti Verbaalne Määratlus](#projekti-verbaalne-määratlus)  
4. [Tarkvara Funktionaalsuse Nõuded](#tarkvara-funktionaalsuse-nõuded)  
5. [Andmehalduspõhised Nõuded](#andmehalduspõhised-nõuded)  

---

## Eesmärk

Projekti **ToiduHind.ee** eesmärk on luua **kasutajasõbralik ja funktsionaalne rakendus**, mis võimaldab kasutajatel võrrelda erinevate poodide toidukaupade hindu. Rakenduse funktsioonid aitavad säästa nii **aega** kui ka **raha**:

- **Reaalajas hinnavõrdlus**: kasutajatel on võimalus näha erinevate poodide hindu ja jälgida nende muutusi.  
- **Ostunimekirjade haldus**: võimalus koostada ostunimekirju ja jagada neid teistega.  
- **Hinnateavitused**: saad märguandeid sooduspakkumistest ja hinnalangustest.  
- **Kohaletoimetamisteenuste integratsioon**: rakendus muudab ostuprotsessi veelgi lihtsamaks.  

---

## Sihtrühm

ToiduHind.ee teenus on loodud kolmele peamisele kasutajagrupile:

| **Tüüp kasutaja**   | **Kirjeldus**                                                                             | **Eesmärgid**                                                                                     |
|---------------------|-----------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------|
| **Administraator**  | Rakenduse haldaja, kellel on täielik ligipääs süsteemile.                               | Kasutajate ja toodete haldamine, turvalisuse tagamine.                                           |
| **Tavakasutaja**    | Registreerimata kasutaja, kes võrdleb hindu ja koostab ostunimekirju.                   | Hindade võrdlemine, ostunimekirjade haldamine, teavituste saamine.                               |
| **Kinnitatud kasutaja** | Kasutaja, kes on registreerinud ja kinnitanud oma e-posti.                            | Täiustatud funktsioonid: hinnateavitused, jagatud nimekirjad, isiklikud eelistused.              |

---

## Projekti Verbaalne Määratlus

Rakendus sisaldab järgmisi **peamisi objekte** ja nende omadusi:

| **Objekt**           | **Kirjeldus**                                                                      |
|----------------------|------------------------------------------------------------------------------------|
| **Kasutaja**         | Kasutaja, kes otsib ja võrdleb toidukaupade hindu.                                |
| **Verifitseeritud kasutaja** | Registreeritud kasutaja, kellele on saadaval lisafunktsioonid.               |
| **Administraator**   | Vastutab rakenduse andmebaasi ja süsteemi haldamise eest.                          |
| **Toode**            | Konkreetne kaup, mille hindu võrreldakse.                                         |
| **Pood**             | Kauplus või e-pood, mille hinnad on rakenduses esindatud.                         |
| **Hinnavõrdlus**     | Funktsioon, mis kuvab toodete hinnad erinevates poodides.                         |
| **Ostunimekiri**     | Kasutaja koostatud nimekiri soovitud toodetest.                                   |
| **Sooduspakkumine**  | Allahindlus või eripakkumine, mis on seotud konkreetse tootega.                   |

---

## Tarkvara Funktionaalsuse Nõuded

**Kasutajaliidese nõuded**:  
- Selge otsingufunktsioon toodete kiireks leidmiseks.  
- Reaalajas hinnavõrdlus ilma lehe uuendamiseta.  

**Põhifunktsioonid**:
1. Toodete hindade võrdlemine reaalajas.  
2. Sooduspakkumiste jälgimine ja teavitused.  
3. Ostunimekirjade loomine ja jagamine.  
4. Administraatori tööriistad (haldus, andmete lisamine ja uuendamine).  

---

## Andmehalduspõhised Nõuded

- **Andmebaasi struktuur**:  
  - Kasutajad (Users), Tooted (Products), Poodide info (Stores), Hinnad (Prices), Ostunimekirjad (Wishlists), Teavitused (Notifications).  

- **Andmete valideerimine ja turvalisus**:  
  - Paroolide krüpteerimine.  
  - Rollipõhine juurdepääs (RBAC).  
  - HTTPS ühendused.  

- **Seosed**:  
  - **Users ↔ Wishlists**: iga kasutaja saab omada ühte ostunimekirja.  
  - **Products ↔ Prices ↔ Stores**: iga toode võib olla saadaval mitmes poes erineva hinnaga.  

---

## Lisainfo

Rakendus **ToiduHind.ee** on loodud eesmärgiga pakkuda läbipaistvat ja kiiret ligipääsu toidukaupade hindadele ning aidata kasutajatel teha **teadlikumaid ostuotsuseid**.  

Projekti pikaajalised eesmärgid:
- Laiendada andmebaasi kohalike ja rahvusvaheliste poodide hindadega.  
- Integreerida rohkem funktsioone, nagu isikupärastatud pakkumised ja tehisintellekti soovitused.  

---
