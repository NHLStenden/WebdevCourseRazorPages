# Algemene uitleg van de opdrachten

**Voor alle opdrachten geldt kijk goed naar de voorbeelden, uitleg, de bronnen (er staan linkjes naar deze bronnen in de opdrachten) & de [sheets](https://slides.com/jorislops/corerazorpages).** 
Mijn voorbeelden van de technieken zijn te vinden in de directory: Examples\Pages\Lesson1.

Als je het Examples project start dan kun je de voorbeelden in je browser proberen. Per techniek/concept zijn er voorbeelden. Onderaan de opdracht staan de relevante voorbeelden, maar wees vrij om gerust eens door de voorbeelden te grasduinen. Tip: plaats breakpoints zodat je kunt zien wat er gebeurt.

De startpunten voor de opdrachten staan al voor je klaar in de directory Exercises\Pages\Lesson1. 
Voor de meeste opdrachten staat er al een Content Page (.cshtml) i.c.m. een Page Model (.cs.cshtml) klaar die aangevuld moet worden. Overal waar ### moet Razor en/of html code komen te staan. Voel je vrij om de opdracht anders op te lossen.

Je kan gebruik maken van de Tests voor de opdrachten van Lesson1, zie directory (Exercies.Tests).  
Je kan de opdrachten ook maken zonder te testen, let op: een test kan helpen om te kijken of je goed bezig bent, echter een test is erg beperkend en kan je ook tegenwerken. Daarom is er ook voor gekozen om alleen voor week1 testen beschikbaar te stellen (de opdrachten worden steeds vrijer en dus lastiger te testen).

Eventueel kan je de test bekijken in (`Exercises.Test/Lesson1.cs/Assignment<AssignmentNumber>`). Het nadeel van test is dat ze precies goed moeten zijn, je kan ook gewoon proberen de opdracht te maken (zonder al te veel de testen te gebruiken). Het gaat er tenslotte om dat je de technieken leert! En niet per se de test laat werken!

**State Management** is erg belangrijk voor websiteontwikkelaars. Daarom gaan we er uitgebreid mee oefenen.
We maken gebruik van een Content Page (.cshtml) i.c.m. een Page Model (.cs.cshtml), zodat we de html en logica kunnen scheiden.
In de Content Page (view) gebruiken we Razor. Razor is C# gecombineerd met HTML.     
- [Razor Pages](https://www.learnrazorpages.com/razor-pages)    
- [Razor Syntax Overview](https://www.mikesdotnetting.com/article/153/inline-razor-syntax-overview)  
- [State Management In Razor Pages](https://www.learnrazorpages.com/razor-pages/state-management)    

*Voor alle opdrachten geldt kijk goed naar de voorbeelden, uitleg, de bronnen & de sheets.*

Overal waar ``###`` moet Razor en/of html code komen te staan.
Voor de meeste testen staat al een Content Page (.cshtml) i.c.m een Page Model (.cs.cshtml) klaar die aangevuld moet worden.
Eventueel kan je de test bekijken in (`Exercises.Test/Lesson1.cs/Assignment<AssignmentNumber>`). 
Het nadeel van tests is dat je code exacte moet en precies het resultaat oplevert wat de test verwacht, je kan ook gewoon proberen de opdracht te maken (zonder al te veel de testen te gebruiken).
Het gaat er tenslotte om dat je de technieken leert! En niet per se de test laat werken!

## Opdracht 1 QueryString - Scorebord 

Het idee is om een simpel scorebord te maken. 
De toestand (state) wordt bijgehouden in de QueryString.
De state in deze opdracht is: `scoreHome` & `scoreAway`.

De cshtml structuur (Content Page) om mee te beginnen staat al voor je klaar. 

Er zijn drie manier om te werken met querystring in Razor Pages, pas elke manier toe:    
1. `Request.Query["someKey"]`  vergeet niet te controleren of de key bestaat! Deze manier wordt niet aangeraden!  
2. Het meegeven van de input als Parameter in de methode die aangeroepen wordt, zoals in:   
```c#
public void OnGet([FromQuery] int scoreHome, [FromQuery] int scoreAway) { ... }`
```  
Het is niet per se noodzakelijk om het `[FromQuery]` attribuut (annotatie) te gebruiken, maar het geeft wel duidelijk de intentie weer!  
3. Het gebruik van het `[BindProperty]` attribuut (annotatie). Let op: als je een GET request doet, moet `SupportGet = true` staan in het attribute.
```c#
[BindProperty(SupportsGet = true, Name = "awayScore")]
public int Away { get; set; }
```

Relevante voorbeelden:  
1. QueryStringsRequest  
2. QueryStringsMethodParameter.cshtml.cs  
3. QueryStringsRequest.cshtml.cs  

## Opdracht 2 QueryString - Gaan we Links of gaan we Rechts

Het idee is dat we een route gaan bijhouden die we hebben afgelegd. 
Iedere keer als we afslaan klikken we op de desbetreffende link.
We willen dus de gehele route bijhouden, dus meerdere waarden per query parameter.
De truc om meerdere waarden door te geven is als volgt: `<a href='?directions=Left&directions=Right>...'`.
Tip: gebruik een lijst van directions (`List<Direction> direction`), zie methode 2 of 3 van de vorige opdracht. 

Er staat al een Page Model (cs.cshtml) en Content Page (.cshtml) voor je klaar als mogelijk startpunt.

Relevante voorbeelden:
Zelfde als van de vorige opdracht.


## Opdracht 3 Rekenmachine

Maak een simpele rekenmachine. Dit keer gaan we een form met `method="POST"` gebruiken, met daarin een hidden input om het tussenresultaat te onthouden.   
Een rekenmachine heeft meerdere knoppen daarvoor zijn handlers handig in gebruikt.

- [Hidden form fields](https://www.learnrazorpages.com/razor-pages/state-management#hidden-form-fields)    
- [Handlers](https://www.learnrazorpages.com/razor-pages/handler-methods)  

*Let op*, een mogelijke valkuil is dat `[BindProperty]` gebruik maakt van Model Binding, 
en het aanpassen van de waarde in de Page Model heeft dan geen effect in een POST! 
In een GET Request werkt het wel! 
:-(. Dit heeft mij erg veel tijd (frustratie) gekost om hierachter te komen, voor uitleg zie:  
[uitleg + slechte workaround, liever niet gebruiken](https://stackoverflow.com/questions/53669863/change-bound-property-in-onpost-in-model-is-invalid/53675887#53675887).  
**Zorgt er dus voor dat je zelf de value zet van de input's!**
Model validatie is handig, maar dat is een onderwerp voor volgende week!

Indien we delen door 0 willen we een ``BadRequest("Delen door nul is niet toegestaan")`` response teruggeven en anders de ``Page()``.  
* [Action Result](https://www.learnrazorpages.com/razor-pages/action-results)  
* [IActionResult Type](https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-5.0#iactionresult-type)  

Relavante voorbeelden:  
- HiddenFormFields  
- PageHandler  
- PostRequest

## Opdracht 4 - Route Data 

We kunnen gegevens meegeven als Route Data.
We willen een pagina kunnen aanroepen met de volgende url structuur: `/category/subcategory/productId`.  
- subcategory is optioneel. Als deze leeg is druk dan `"Geen subcategory"` af in de `<h2 id="subCategoryHeading">`  
- productId is optioneel. Als deze leeg is druk dan `"Geen productId"` af in de `<h3 id="productIdHeading">`.  
Category & subcategory zijn strings, en productId moet een getal zijn groter dan 0.    
- [Route Data](https://www.learnrazorpages.com/razor-pages/routing#route-data)

Maak een *Custom route constraints* die controleert of category en subcategory de volgende structuur hebben "cat{number}" of "sub{nunber}".
B.v. cat23, subcat100.
Zie voor een voorbeeld van Custom Route Constrain:  
- [Custom route constraints](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-5.0#route-template-reference) (even naar beneden scrollen).

Relevante voorbeelden:   
- RouteParameters

## Opdracht 5 - Cookies

Er zijn drie soorten knoppen die een gebruiker kan indrukken die zijn gemoedstoestand representeren, namelijk: blij, teleurgesteld, boos. Hoe vaak een bepaalde knop is ingedrukt willen we graag bijhouden in een Cookie en natuurlijk weergeven aan de gebruiker.
Uitleg over cookies: 
- [How cookies can track you (Simply Explained)](https://www.youtube.com/watch?v=QWw7Wd2gUJk) 
- [Using Cookies in Razor Pages](https://www.learnrazorpages.com/razor-pages/cookies)  

Probeer alles in 1 cookie op te slaan d.m.v. een object.
```c#
public class MoodCounter {
  public int Happy { get; set; }
  public int Disappointed { get; set; }
  public int Angry { get; set; }
}
```
  
Opmerking: de delete cookie knop verwijder de cookie en initialiseert (`new`) een nieuwe MoodCounter instantie.
De truc is om de instantie van het `Moodcounter` object plat te slaan als JSON. Dit platslaan (serialize) en terughalen (deserialize) kan als volgt:
```c#
  string json = JsonConvert.SerializeObject(moodCounter); //serialze

  MoodCounter deserializedMoodCounter = JsonConvert.DeserializeObject<MoodCounter>(value); //deserialize

```
De MoodCounter instantie platgeslagen (serialize) als JSON ziet er zo uit:

```json
{
"Happy":6,
"Disappointed":2,
"Angry":1
}
```

Relevante voorbeelden:   
- Cookies

*Off-topic:* Tracking cookies zijn speciale cookies die worden gebruikt door meerdere website, dit wordt o.a. gebruikt voor het aanmaken van een profiel van de gebruikers zodat bedrijven gericht reclame kunnen sturen. Mocht je het interessant vinden om te weten hoe dit werkt, zie [How cookies can track you (Simply Explained)](https://www.youtube.com/watch?v=QWw7Wd2gUJk).

## Opdracht 6 - NHLStenden Café - Login & Registratie voor obers m.b.v. sessie.
Voor deze eindopdracht (NHLStenden Café) is het handig om een nieuwe Razor applicatie aan te maken *zonder authenticatie*.

Hier wordt uitgelegd hoe je een Razor Pages project kan aanmaken:  
- [Learnrazorpages - First Look](https://www.learnrazorpages.com/first-look)  
- [Razor Pages for ASP.NET Core - Full Course (.NET 6)](https://www.youtube.com/watch?v=eru2emiqow0), zie tijdstip 14:50.  

Controleer eerst of je de juiste versie hebt van .NET met `dotnet --version`, 6 of hoger. 
De commando's voor het aanmaken van een nieuw project vanuit de console (terminal):

```
dotnet --version 
mkdir NHLCafe
cd NHLCafe
dotnet new razor --au none
```

Het idee is om een simpel login systeem te maken, voor de eindopdracht (NHLStenden Café).
Een ober moet kunnen inloggen. Daarna kan hij pas bestellingen opnemen.
 
Het idee is dat we een Sessie aanmaken met daarin de userid die een unieke code bevat.  
Voor uitleg wat een sessie is, zie [Life Cycle Of A Session (HTTP Session)](https://www.youtube.com/watch?v=mzEwSlKMxzw).
We gaan uit van een type gebruiker, namelijk de ober. 

**Opmerking: gebruik niet de klasse User! User is in b.v. de database een gereserveerd keyword en kan veel problemen opleveren!**

Het idee is om gebruik te maken van een session state (sessie), zie [Session State in Razor Pages](https://www.learnrazorpages.com/razor-pages/session-state).

Maak de volgende webpagina's (Razor Pages):  

- Register.cshtml - Registratie pagina (username, password). 
Een ober (CafeUser) kunnen we opslaan in een database, echter dit hebben we nog niet behandeld.
Een truc is een `static variabele` te gebruiken, dan blijven de gegevens bestaan zolang de server draait.
Om dit te faciliteren is er de klasse `StaticUserRepopository` gemaakt (zie `Exercises\Pages\Lesson1\StaticUserRepository.cs`, die je kan gebruiken! 
Een ober (CafeUser) toevoegen kan als volgt: `StaticUserRepopository.AddUser(user)`.

- Login.cshtml - Inlog Pagina.  
Als een gebruiker een correcte username & password combinatie invult dan wordt hij ingelogd.
Deze pagina zet de userid sessie dan, als je succesvol bent ingelogd word je doorverwezen ([redirect](https://www.learnrazorpages.com/razor-pages/action-results)) naar de AccountOverview.cshtml pagina.

- Logout.cshtml - Uitlog Pagina.  
 Deze verwijdert de sessie en laat zien dat het uitloggen succesvol is gelukt.

- AccountOverview.cshtml - Overzichtspagina van Account.  
    Hier kan je de username afdrukken van de ingelogde ober.
    Als je niet bent ingelogd moet de gebruiker worden doorverwezen ([redirect](https://www.learnrazorpages.com/razor-pages/action-results))  naar de Login.cshtml pagina, dit kan b.v. met van de `RedirectToPage(...)` of `Redirect(...)` methode. 
    Ophalen van de gebruikersgegevens kan als volgt: `StaticUserRepo.GetUser(userid)`.

Relevante voorbeelden:  
- Redirect  
- Sessions  
- SessionStoreObject  