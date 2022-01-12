# Opdrachten Les 1 - State Management met Razor Pages

State Management is erg belangrijk voor websiteontwikkelaars. Daarom gaan we er uitgebreid mee oefenen.
We maken gebruik van een Content Page (.cshtml) i.c.m. een Page Model (.cs.cshtml), zodat we de html en logica kunnen scheiden.
In de Content Page (view) gebruiken we Razor. Razor is C# gecombineerd met html.   
* [Razor Pages](https://www.learnrazorpages.com/razor-pages)  
* [Razor Syntax Overview](https://www.mikesdotnetting.com/article/153/inline-razor-syntax-overview)  
* [State Management In Razor Pages](https://www.learnrazorpages.com/razor-pages/state-management)  

*Voor alle opdrachten geldt kijk goed naar de voorbeelden, uitleg, de bronnen & de sheets.*

Overal waar ``###`` moet Razor en/of html code komen te staan.
Voor de meeste testen staat al een Content Page (.cshtml) i.c.m een Page Model (.cs.cshtml) klaar die aangevult moet worden.
Eventueel kan je de test bekijken in (`Exercises.Test/Lesson1.cs/Assignment<AssignmentNumber>`). 
Het nadeel van test is dat ze precies goed moeten zijn, je kan ook gewoon proberen de opdracht te maken (zonder al te veel de testen te gebruiken).
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
public void OnGet([FromQuery] string scoreHome, [FromQuery] string scoreAway) { ... }`
```  
Het is niet per se noodzakelijk om het `[FromQuery]` attribuut (annotatie) te gebruiken, maar het geeft wel duidelijk de intentie weer!  
3. Het gebruik van het `[BindProperty]` attribuut (annotatie). Let op: als je een GET request doet, moet `SupportGet = true` staan in het attribute.
```c#
[BindProperty(SupportsGet = true, Name = "awayScore")]
public int Away { get; set; }
```

## Opdracht 2 QueryString - Gaan we Links of gaan we Rechts

Het idee is dat we een route gaan bijhouden die we hebben afgelegd. 
Iedere keer als we afslaan klikken we op de desbetreffende link.
We willen dus de gehele route bijhouden, dus meerdere waarden per query parameter.
De truc om meerdere waarden door te geven is als volgt: ``<a href='?directions=Left&directions=Right>...''``.
Tip: gebruik een lijst van directions (`List<Direction> direction`), zie methode 2 of 3 van de vorige opdracht. 

## Opdracht 3 Rekenmachine

Maak een simpele rekenmachine. Dit keer gaan we een form (POST) gebruiken 
met hidden input om het tussenresultaat te onthouden. 
Een rekenmachine heeft meerdere knoppen daarvoor zijn handlers handig in gebruikt. 
De "startwaarde" is 0.

* [Hidden form fields](https://www.learnrazorpages.com/razor-pages/state-management#hidden-form-fields)
* [Handlers](https://www.learnrazorpages.com/razor-pages/handler-methods)

*Let op*, een mogelijke valkuil is dat `BindProperty` gebruik maakt van Model Binding, 
en het aanpassen van de waarde in de Page Model heeft dan geen effect in een POST! 
In een GET Request werkt het wel! 
:-(. Dit heeft mij erg veel tijd (fustratie) gekost om hier achter te komen, voor uitleg zie:  
[uitleg + slechte workaround, liever niet gebruiken](https://stackoverflow.com/questions/53669863/change-bound-property-in-onpost-in-model-is-invalid/53675887#53675887).  
**Zorgt er dus voor dat je zelf de value zet van de input's!**
Model validatie is handig, maar dat is een onderwerp voor volgende week!

Indien we delen door 0 willen we een ``BadRequest("Delen door nul is niet toegestaan")`` response teruggeven en anders de ``Page()``.
* [Action Result](https://www.learnrazorpages.com/razor-pages/action-results)  
* [IActionResult Type](https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-5.0#iactionresult-type)  
 
## Opdracht 4 - Route Data 

We kunnen gegevens meegeven als Route Data.
We willen een pagina kunnen aanroepen met de volgende url: `/category/subcategory/productId` structuur.  
* subcategory is optioneel. Als deze leeg is druk dan `"Geen subcategory"` af in de `<h2 id="subCategoryHeading">`  
* productId is optioneel. Als deze leeg is druk dan `"Geen productId"` af in de `<h3 id="productIdHeading">`.  
Category & subcategory zijn strings, en productId moet een getal zijn groter dan 0.  
* [Route Data](https://www.learnrazorpages.com/razor-pages/routing#route-data)

Maak een *Custom route constraints* die controleert of category en subcategory de volgende structuur hebben "cat{number}" of "sub{nunber}".
B.v. cat23, subcat100.
Zie voor een voorbeeld van Custom Route Constrain:  
* [Custom route constraints](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-5.0#route-template-reference) (even naar beneden scrollen).

## Opdracht 5 - Cookies

Er zijn drie soorten knoppen die een gebruiker kan indrukken die zijn gemoedstoestand representeren, namelijk:
blij, teleurgesteld, boos.  Hoe vaak een bepaalde knop is ingedrukt willen we graag bijhouden in een Cookie en natuurlijk weergeven aan de gebruiker.   
* [Cookies](https://www.learnrazorpages.com/razor-pages/cookies)  
* [What Are Cookies? And How They Work | Explained for Beginners!](https://www.youtube.com/watch?v=rdVPflECed8&ab_channel=CreateaProWebsite)  

Probeer alles in 1 cookie op te slaan d.m.v. een object.
```c#
public class MoodCounter {
    public int Happy { get; set; }
    public int Disappointed { get; set; }
    public int Angry { get; set; }
}
```

Opmerking: de delete cookie knop verwijder de cookie en initialiseert (`new`) een nieuwe `MoodCounter` instantie, de counter komen op 0 te staan.

De truc is om het `Moodcounter` object plat te slaan als Json. Dit platslaan (serialize) en terughalen (deserialize) kan als volgt (zie het antwoord):
[Serialize Cookie](https://stackoverflow.com/questions/54437384/is-there-a-way-to-use-session-for-objects-in-asp-net-core-like-in-web-forms)

## NHL Café - Sessie en Cookies
Voor deze eindopdracht (NHL Café) is het handig om een nieuwe Razor applicatie aan te maken *zonder authenticatie*.

Hier wordt uitgelegd hoe je een Razor Pages project kan aanmaken:
* [Learnrazorpages - First Look](https://www.learnrazorpages.com/first-look)
* [Razor Pages for ASP.NET Core - Full Course (.NET 6)](https://www.youtube.com/watch?v=eru2emiqow0), zie tijdstip 14:50.

Controleer eerst of je de juiste versie hebt van .net met `dotnet --version`, 6 of hoger. 
De commando's voor het aanmaken van een nieuwe project vanuit de console (terminal):

```
dotnet --version 
mkdir NHLCafe
cd NHLCafe
dotnet new razor --au none
```

Het idee is om een simpel login systeem te maken, voor eindopdracht (NHL Café).
Een ober moet kunnen inloggen. 
 
Het idee is dat we een Cookie aanmaken met de userid die een unieke code bevat.  
We gaan uit van een type gebruiker, namelijk de ober. 

Maak de volgende webpagina's (Razor Pages):  
* Register.cshtml - Registratie pagina (username, password). 
Een ober (CafeUser) kunnen we opslaan in een database, echter dit hebben we nog niet behandeld.
Een truc is een `static variabele` te gebruiken, dan blijven de gegevens bestaan zolang de server draait.
Om dit te faciliteren is er de klasse `StaticUserRepopository` gemaakt, die je kan gebruiken! 
Een ober (CafeUser) toevoegen kan als volgt: `StaticUserRepopository.AddUser(userid, user)`.

* Login.cshtml - Inlog Pagina.  
Als een gebruiker een correcte username & password combinatie invult dan wordt hij ingelogd.
Deze pagina zet de userid cookie dan, als je succesvol bent ingelogd word je geredirect naar de AccountOverview.cshtml pagina.

* Logout.cshtml - Uitlog Pagina.  
 Deze verwijdert de cookie en laat zien dat het uitloggen succesvol is gelukt.

* AccountOverview.cshtml - Overzichtspagina van Account.  
    Hier kan je de username afdrukken van de ingelogde ober.
    Als je niet bent ingelogd moet de gebruiker worden [geredirect](https://www.learnrazorpages.com/razor-pages/action-results) naar de Login.cshtml pagina, dit kan b.v. met van de `RedirectToPage(...)` of `Redirect(...)` methode. 
    Ophalen van de gebruikersgevens kan als volgt: `StaticUserRepo.GetUser(userid)`.

