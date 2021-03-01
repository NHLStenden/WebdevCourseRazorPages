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

## Opdracht 1 QueryString - Scorebord 

Het idee is om een simpel scorebord te maken. 
De toestand (state) wordt bijgehouden in de QueryString.
De state in deze opdracht is: `scoreHome` & `scoreAway`.

De cshtml structuur (Content Page) om mee te beginnen staat al voor je klaar. 

Er zijn drie manier om te werken met querystring in Razor Pages, pas elke manier toe:  
1. `Request.Query["someKey"]`  vergeet niet te controleren of de key bestaat!  
2. Het meegeven van de input als Parameter in de methode die aangeroepen wordt, zoals in:   
```c#
public void OnGet([FromQuery] string a, [FromQuery] string b)`
```  
Het is niet per se noodzakelijk om het `[FromQuery]` attribuut (annotatie) te gebruiken, maar het geeft wel duidelijk de intentie weer!  
3. Het gebruik van het `[BindProperty]` attribuut (annotatie).   
```c#
[BindProperty(SupportsGet = true, Name = "awayScore")]
public int Away { get; set; }
```

## Opdracht 2 QueryString - Gaan we Links of gaan we Rechts

Het idee is dat we een route gaan bijhouden die we hebben afgelegd. 
Iedere keer als we afslaan klikken we op de desbetreffende link.
We willen dus de gehele route bijhouden, dus meerdere waarden per query parameter.
De truc om meerdere waarden door te geven is als volgt: ``<a href='?a=x&a=y>...''``.

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

De truc is om het `Moodcounter` object plat te slaan als Json. Dit plat slaan en terughalen kan als volgt:
```c#

```

## Bruiden - Sessie en Cookies
Voor deze opdracht (bruidenopdracht) is het handig om een nieuwe Razor applicatie aan te maken *zonder authenticatie*.

Het idee is om een simpel login systeem te maken, voor bruiden (WeddingCouple) en hun gasten.
De bruiden moeten de locatie en trouwdatum kunnen opgeven en gasten moeten dit kunnen zien.
 
Het idee is dat we een Cookie aanmaken met de userid die een unieke  code bevat. 
Een unieke code kunnen we generen met `Guid.New()`.  
Het kan ook handig zijn om in een cookie bij te houden wat de rol is (gast, bruidspaar).

Maak de volgende webpagina's (Razor Pages):  
* Register.cshtml - Registratie pagina (username, password). 
Een bruidspaar (WeddingCouple) kunnen we opslaan in een database, echter dit hebben we nog niet behandeld.
Een truc is een `static variabele` te gebruiken, dan blijven de gegevens bestaan zolang de server draait.
Om dit te faciliteren is er de klasse `StaticWeddingRepository` gemaakt, die je kan gebruiken! 
Een bruidspaar toevoegen kan als volgt: `StaticWeddingRepository.AddUser(userid, user)`.

* Login.cshtml - Inlog Pagina.  
Als een paar een geldige password/username combinatie opgeeft dan wordt hij ingelogd.
Deze pagina zet de userid cookie dan, als je succesvol bent ingelogd word je geredirect naar de Overview.cshtml pagina.
Gasten kunnen de unieke code gebruiken om in te loggen en worden doorgestuurd naar de Overview.cshtml pagina.

* Logout.cshtml - Uitlog Pagina.  
 Deze verwijdert de cookie en laat zien dat het uitloggen succesvol is gelukt.

* Overview.cshtml - Overzichtspagina.  
    Als bruidpaar zie je hier je trouwdatum, locatie staan, echter als deze nog niet zijn ingevuld word je doorgestuurd (redirect) naar de pagina AddWeddingDate.cshtml.   
    Als gast zie je een pagina met de tekst 'Fijne bruiloft'  
Ophalen van de gebruikersgevens kan als volgt: ``StaticUserRepo.GetUser(userid)``. 
 
* AddWeddingDate.cshtml - Toevoegen van trouwdatum.   
Op deze pagina kan heet bruidspaar zijn trouwdatum opgeven. Dit wordt tijdelijke opgeslagen in een sessie variabele. Daarna wordt het bruidspaar doorgestuurd naar AddLocation.cshtml.

* AddLocation.cshtml - Toevoegen van trouwlocatie.   
Op deze pagina kan het bruidspaar zijn trouwlocatie opgeven. Dit wordt tijdelijke opgeslagen in een sessie variabele. Daarna wordt het bruidspaar doorgestuurd naar Confirm.cshtml.

* Confirm.cshtml - De locatie en datum worden weergegeven.
   Het bruidspaar kan de confirm knop drukken, dan worden de voorkeuren (locatie en datum) opgeslagen.
   Dit kan je doen door een gebruiker op te halen met ``GetUser()`` en vervolgens de properties aan te passen (Location, Date). 
   Het **opslaan** gaat hier automatisch, met een echte database zoals later zal blijken niet! 
   De sessie variabele(n) moet(en) verwijderd worden.  
   Indien het bruidspaar cancel knop indrukt, dan wordt het bruidspaar teruggestuurd naar pagina AddWeddingDate.cshtml.

De volgende pagina's:
Overview.cshtml, AddWeddingDate.cshtml, AddLocation.cshtml, Confirm.cshtml 
zijn alleen beschikbaar voor paren indien ze zijn ingelogd. 
Een niet inglogde gebruiker moet worden doorgestuurd naar Login.cshtml.


