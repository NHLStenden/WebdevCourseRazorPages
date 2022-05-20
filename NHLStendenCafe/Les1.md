# Opdracht Les 1 - NHLStendenCafé Sessions

Het is verstandig om de opdracht voor het NHLStenden Café door te lezen, deze staat op Blackboard. 

YouTube video:
* [Sessies](https://www.youtube.com/watch?v=ClPteZ12mAw&list=PLQ3zAu75nbTGDqP-jmu0JURMEG9dLiX9I&index=14)

Het idee is om een simpel login systeem te maken, voor de eindopdracht (NHLStenden Café).
Een ober moet kunnen inloggen. Daarna kan hij pas bestellingen opnemen.

Het idee is dat we een Sessie aanmaken met daarin de userid die een unieke code bevat.  
Voor uitleg wat een sessie is, zie [Life Cycle Of A Session (HTTP Session)](https://www.youtube.com/watch?v=mzEwSlKMxzw).
We gaan uit van een type gebruiker, namelijk de ober.

**Opmerking: gebruik niet de klasse User! User is in b.v. de database een gereserveerd keyword en kan veel problemen opleveren! Daarom gebruik ik `CafeUser`.**

Het idee is om gebruik te maken van een session state (sessie), zie [Session State in Razor Pages](https://www.learnrazorpages.com/razor-pages/session-state).

Maak de volgende webpagina's (Razor Pages):

- Register.cshtml - Registratie pagina (username, password).
  Een ober (`CafeUser` staat in de directory `Models`) kunnen we opslaan in een database, echter dit hebben we nog niet behandeld.
  Een truc is een `static variabele` te gebruiken, dan blijven de gegevens bestaan zolang de server draait.
  Om dit te faciliteren is er de klasse `StaticUserRepopository` gemaakt (zie `Repository/StaticUserRepository.cs`, die je kan gebruiken!
  Een ober (CafeUser) toevoegen kan als volgt: `StaticUserRepopository.AddUser(user)`. Dit is handig voor het registeren.
  Om in te loggen kan de volgende methode worden aangeroepen `StaticUserRepopository.GetUser(username, password)`.
  Als de username en password combinatie niet bestaat of ongeldig is, dan wordt er `null` geretourneerd.

Er wordt een Guid als unieke identificatie van de `CafeUser` m.b.v. de property `UserId`.
In het onderstaande voorbeeld is te zien hoe je een `Guid` kan gebruiken. Een `Guid` kan je zien als een soort van "getal" dat altijd uniek is!
```C#
    //create new Guid
    var guid = Guid.NewGuid();
    
    //convert Guid to String
    var guisAsString = guid.ToString();
    
    //convert string to Guid
    var guidFromSTring = new Guid(guidAsString);
```

- Login.cshtml - Inlog Pagina.  
  Als een gebruiker een correcte username & password combinatie invult dan wordt hij ingelogd. 
  Dit betekent dat we in een sessie variable de UserId opslaan. 
  Daarna word je doorverwezen ([redirect](https://www.learnrazorpages.com/razor-pages/action-results)) naar de AccountOverview.cshtml pagina.

- Logout.cshtml - Uitlog Pagina.  
  Deze verwijdert de sessie en laat zien dat het uitloggen succesvol is gelukt.

- AccountOverview.cshtml - Overzichtspagina van Account.  
  Haal de waarde van de UserId op uit de sessievariable die is aangemaakt met de Inlog Pagina. Met deze UserId kan je de `CafeUser` ophalen uit het repository met de methode: `GetUser(Guid guid)`.
  "Print" de naam van de ingelogde ober op het scherm. 
  Als je niet bent ingelogd (`userId == null`) moet de gebruiker worden doorverwezen ([redirect](https://www.learnrazorpages.com/razor-pages/action-results)) naar de Login.cshtml pagina, dit kan b.v. met van de `RedirectToPage(...)` of `Redirect(...)` methode.
  

Relevante voorbeelden:
- Redirect
- Sessions
- SessionStoreObject 