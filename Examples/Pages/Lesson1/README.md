# Volgorde van voorbeelden met uitleg

Hieronder volgt een uitleg van de diverse voorbeelden en een handige volgorde om deze voorbeelden te bekijken.
Het is aan te raden om breakpoints te zetten en door de code te lopen, zodat je kan zien wat er gebeurt. Deze voorbeelden kunnen je wellicht helpen bij de opdrachten.

## LesDemo

Hierin komen een deel van de verschillende technieken/concepten gecombineerd terug die in de andere code voorbeelden behandeld zijn.

## RazorSyntax.cshtml & RazorSyntaxHttpRequest.cshtml

De Razor Syntax (notatie van C# in cshtml bestanden). Gebruik een `@` om naar C# modes te gaan. Meetal gaat het dan goed!
Mocht je alle details willen weten: [Razor syntax reference for ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-6.0)

## GetRequest.cshtml & PostRequest.cshtml.

Een webpagina kan op twee manier worden aangeroepen, namelijk:  
1. GET Request (als je de webpagina intypt in je adresbalk van je Browser, of een formulier met method="GET").
   In dit (GetRequest.cshtml) voorbeeld wordt gebruik gemaakt van een GET Request, deze roept de methode `OnGet(...)` aan in de Page Model (GetRequest.cs.cshtml) file.  
2. POST Request, dit wordt gedaan als je een formulier (```<form method="POST""```) verzendt.
   In dit (PostRequest.cshtml) voorbeeld wordt gebruik gemaakt van een POST Request, deze roept de methode `OnPost(...)` aan in de Page Model (GetPostRequest.cs.cshtml) file.

Publieke attributen (zoals ``Name``, ``public string Name { get; set; }``) kunnen gebruiken in het Razor (Content Page) bestand (GetRequest.cshtml & PostRequest.cshtml).
```razor
<h1>Hello @Model.Name</h1>
```
Dit moet altijd wel voorafgegaan worden door `@Model`, `@Model` wijst als het ware terug naar de Page Model (cs.cshtml) bestand.

## QueryStringMethodParameter & QueryStringMethodBindProperty & QueryStringMethodRequest

[Uitleg van Mike](https://www.learnrazorpages.com/razor-pages/state-management#query-strings)

Een GET request kan op twee manieren worden aangeroepen, namelijk:  
1. Aanroepen m.b.v. link `(<a href=...)`.  
2. Form met een GET method `<form method="GET">...</form>`. Let op: de `GET` method mag je weglaten omdat de `GET` de default waarde is!.  

Er is een derde manier, namelijk met een AJAX request met JavaScript of jQuery maar dit valt buiten de context van deze cursus. Voor AJAX-voorbeelden zie `Examples\Lesson4`. Een AJAX request kan zowel een GET als POST request zijn.

Er zijn in Razor Pages (Page Model) drie manier (methoden) om de QueryString parameters (meestal met GET Request) te gebruiken:

> Model Binding in Razor Pages is the process that takes values from HTTP requests and maps them to handler method parameters or PageModel properties. Model binding reduces the need for the developer to manually extract values from the request and then assign them, one by one, to variables or properties for later processing. This work is repetitive, tedious and error prone, mainly because request values are usually only exposed via string-based indexes.

Model Binding is methode 1 & 2, voor uitleg zie: [Model Binding](https://www.learnrazorpages.com/razor-pages/model-binding).

### 1. Method Query paramaters (QueryStringsMethodParameter.cshtml)
Je kan in de `OnGet(...)` methode gebruik maken van QueryParameters als volgt: `OnGet([FromQuery] int age)`.
Het attribute `[FromQuery]` mag je weglaten, maar maakt je intenstie wel duidelijker.
```C#
   //QueryStringMethodParameter.cshtml.cs        
   public void OnGet([FromQuery]string firstname, [FromQuery]IEnumerable<string> middleNames, [FromQuery]string lastname) 
   { 
    ... 
   }
```
### 2. BindProperty (QueryStringsBindProperty.cshtml)
   Gebruik van `[BindProperty]`, **let op:** dat voor een GET request is het volgende nodig: `[BindProperty(SupportsGet = true)]`!
```C#
   //QueryStringBindProperty.cshtml.cs
   [BindProperty(SupportsGet = true)] public string Firstname { get; set; }

   [BindProperty(SupportsGet = true)] public IEnumerable<string> MiddleNames { get; set; } = new List<string>();

   [BindProperty(SupportsGet = true)] public string Lastname { get; set; }

   public void OnGet()
   {

   }
```
### 3. Request.Query["key"]  (QueryStringsRequest.cshtml)
Deze methode valt niet aan te raden (gebruiker liever methode 1 & 2 indien mogelijk). 
De waardes moeten "handmatig" uit de request gehaald worden, `Request.Query["naam"]`. 
Hierbij is het belangrijk te realiseren dat een waarde mogelijk niet in de dictionary zit (check dit met `Request.Query.ContainsKey("firstname")` en de waarde kan eventueel leeg zijn en soms null!).
```C#
   //QueryStringRequest.cshtml.cs
   public string Firstname { get; set; }

   public IEnumerable<string> MiddleNames { get; set; } = new List<string>();

   public string Lastname { get; set; }

   public void OnGet()
   {
      Firstname = Request.Query["firstname"];
      MiddleNames = Request.Query["middleNames"].Where(x => !string.IsNullOrWhiteSpace(x));
      Lastname = Request.Query["lastname"];
   }
   ```
   Het ophalen van een GET request parameters (QueryString), dit gebeurt met: `Request.Query["naam"]`. Waarbij `naam` de key van het het name attribute uit de input is (`<input type="text" name="naam">`).
   Het toepassen van de `Request.Query["naam]` techniek valt niet aan te raden. Beter is het om gebruik te maken de technieken in: `QueryStringsMethodParameter.cshtml` & `QueryStringsBindProperty.cshtml`!
   Dus in het GetRequest.cshtml bestand:
```razor
   <form>
       <input name="naam" type="text">
       <button type="submit">Verzenden</button>
   </form>
```
   In de Page Model (GetRequest.cshtml.cs) staat het volgende:
```c#
    Request.Query["naam"]
```
Een request kan meerdere waardes bevatten (b.v. voor een checkbox), dus soms is het volgende nodig als je de eerste waarde wilt selecteren:
```c#
    Request.Query["naam"].First()
```

Nogmaal: vergeet ook niet dat een waarde niet aanwezig hoeft te zijn (null) en ook leeg kan (empty string) zijn!
```c#
    if (Request.Query.ContainsKey("name") && !string.IsNullOrWhiteSpace(Request.Query["name"]))
    {
        Name = Request.Query["name"].First().ToUpper();
    }    
```

### Wanneer gebruik ik welke methode? 

Het kan lastig zijn om te ontdekken wanneer je welke methode kan gebruikt. 
Probeer methode 1 & 2 en kijk wat je handig vindt. Persoonlijk vind ik het wel duidelijk om methode 1 te gebruiken, maar dan moet je toch weer vaak waarden naar een public property kopiëren als je deze op de Content Page wilt weergeven. 
Iedere methode heeft voor- en nadelen. 

## GetIncrementCounter.cshtml

Een teller die verhoogt kan worden m.b.v. GET Request i.c.m. met QueryParameters. Voor uitleg zie voorgaande voorbeelden.

**Opdrachten:**  
1. Probeer zelf een knop toe te voegen die ook de count kan verlagen.  
2. Gebruikt nu geen knop maar een hyperlink (`<a href="">..<a>`)).  

Het probleem is dat je niet weet of de counter waarde moet verhogen of verlagen.
Wat er vaak gebeurt is dat er in een form een hidden field wordt toegevoegd:
`<input type="hidden" name="action">`
of in een hyperlink een extra query parameter `<a href="test?action=increment&count=3`.

## PostRequest.cshtml - Post Request
Een POST-request kan pas gebeuren als er eerst een pagina is opgehaald m.b.v. (GET) van waaruit de POST gedaan kan worden (In dit geval is het dezelfde pagina).
POST worden altijd gedaan m.b.v. formulieren (of AJAX) met een POST method. 
`<form method="POST">` als method attribute wordt weggelaten dan is het altijd een GET request.
Dus `<form method="POST">..` is *niet* het zelfde als `<form>...`
Er is dus een formulier nodig:
````razor
   <form method="post">
    @* het name attribute is belangrijk, dit wordt gebruikt om bij de waarde te kunnen, b.v. Request.Form["naam"] of OnPost([FromForm]string naam) *@
      Naam: <input type="text" name="naam"> <br>
       <button type="submit">Verzenden</button>
   </form>
````

Er zijn drie manier om data uit POST request te halen (vergelijkbaar met de GET Request):
Model Binding is methode 1 & 2, voor uitleg zie: [Model Binding](https://www.learnrazorpages.com/razor-pages/model-binding).

1. In de parameterlijst van de mehtode `public void OnPost([FromForm]string surname)`. Het `[FromForm]` attribute mag je weglaten maar geeft we duidelijk de intentie weer.  
2. Een proprety annoteren met `[BindProperty]`, **let op** dat voor een GET request net anders, namelijk: `[BindProperty(SupportsGet = true)]`.
3. Wederom deze methode valt niet aan te raden. De waardes handmatig uit de request halen, `Request.Form["someKey"]` voor POST methode en `Request.Query["someKey"]` voor de Get Methode. Hierbij is het belangrijk te realiseren dat een waarde niet in de dictionary zit (check dit met `Request.Form.ContainsKey("firstname")` en de waarde kan eventueel leeg zijn (soms null?)).
     
**Let op: de details van een ophalen van data in een POST request en GET request zijn subtiel anders!!!**

[Uitleg van Mike](https://www.learnrazorpages.com/razor-pages/model-binding])

## PageHandler.cshtml - POST methode aanroepen

Een Page handler maakt het mogelijk om bij een *POST* request een methode aan te roepen (anders dan OnPost).

De methode naam moet altijd beginnen met `OnPost...` gevolgt door een naam, b.v. `OnPostIncrement`. 
Deze `public void OnPostIncrement(...)` methode kunnen we dan aanroepen door gebruik te maken van `asp-page-handler="Increment"`

```razor
<form method="post" asp-page-handler="Increment">
    <input type="hidden" name="count" value="@Model.Counter" >
    <button type="submit">+</button>
</form>
```

Het is ook mogelijk om een page handler te gebruiken op een knop i.p.v. het formulier (zeer handig), dus meerdere methoden:
```razor
<form method="post" >
    <input type="hidden" name="count" value="@Model.Counter" >
    <button type="submit" asp-page-handler="Increment">+</button>
    <button type="submit" asp-page-handler="Decrement">-</button>
</form>
```

## RouteParameters.cshtml

Routing is het systeem dat een url (adres wat je invoert in de webbrowser of een link (`<a href="product/detail/3">Productg Detail</a>`) matched met een Razor Pages.
Met andere woorden welke Razor page wordt aangeroepen, zie [Razor Pages Routing](https://www.learnrazorpages.com/razor-pages/routing).

Ook is het mogelijk om parameters (dit wordt Route Data genoemd) mee te geven als onderdeel van een route. 
B.v.:    
- Details/3   (3 is de route parameter value)
- Proucts/Tv/Sony (Tv en Sony zijn dan route parameters)

Boven aan de Razor Page kan je het volgende noteren (dit wordt de Route Template genoemd):
- `@page {productId}`
- `@page {category}\{brand}`

Het doorgeven van route values gebeurt als volgt met de `asp-route-nameOfVar` taghelper.
```razor
<form asp-route-action="increment" asp-route-count="@Model.Count" method="get">
    <button type="submit">+</button>
</form>
<hr>
<a asp-route-action="decrement" asp-route-count="@Model.Count">decrement</a>
```
Je kan dan de route als als volgt gebruiken (vergelijkbaar met POST en GET methoden):
* Als method parameters (deze manier heeft mijn voorkeur): 
```razor
   public void OnGet([FromRoute]int count, [FromRoute]string action)
```
* [BindProperty] werkt ook met Route Values.
* Handmatig, zonder Model Binding (niet aan te raden) `Request.RouteValues["count"]`.

## Redirect.cshtml

Het redirecten stelt je in staat om de gebruiker naar een andere pagina (of dezelfde) te sturen.

```C#
public IActionResult OnGet(int? countParameter)
{
   if (countParameter != null)
   {
       Count = countParameter.Value;
   }

   Count++;

   if (Count < 10)
   {
       return RedirectToPage("Redirect", new
       {
           countParameter = Count
       });
   }
   else
   {
       return Page();
   }
}
```
Let op het return type `IActionResult`. Er zijn veschillende action results.  
- ```return Page();``` roept de "eigen" Razor Page aan.   
- Roep een andere page aan: 
```C#
   return RedirectToPage("Redirect", new {
      countParameter = Count
   });
```
Het twee (optionele argument) kan je gebruiken om parameters mee te sturen. 
Deze worden dan beschikbaar als QueryParameters.     
* Voor andere returns types, check: [Uitleg over Action Results in Razor Pages](https://www.learnrazorpages.com/razor-pages/action-results).  

## Cookies.cshtml

Uitleg over cookies:  
- [How cookies can track you (Simply Explained)](https://www.youtube.com/watch?v=QWw7Wd2gUJk)  
- [Using Cookies in Razor Pages](https://www.learnrazorpages.com/razor-pages/cookies)  

Cookies kunnen gebruikt worden voor het opslaan van tekst (string) gegevens in de browser (client).
Als een cookie eenmaal gezet is, dan worden deze altijd meegestuurd bij de volgende aanroepen (ook naar andere pagina's binnen hetzelfde domein).
Let op cookies zijn beperkter dan Sessies en worden vaak gebruikt om een uniek id te zetten zodat we weten wie je bent (userid, sessionid), en eventuele voorkeuren op te slaan van een gebruiker.  
Het voordeel van een cookie is dat ze oneindig (of een beperkte tijd) geldig blijven in tegenstelling tot een sessie.  

## Sessions.cshtml & SessionsStoreObject.cshtml

Wat sessies zijn wordt uitgelegd in [Life Cycle Of A Session (HTTP Session)](https://www.youtube.com/watch?v=mzEwSlKMxzw).
Het gebruik van Sessie wordt uitgelegd in [Session State in Razor Pages](https://www.learnrazorpages.com/razor-pages/session-state).

In Sessions.cshtml is een simpel voorbeeld. Soms wil je echter objecten opslaan in een sessie. Hiervoor hebben we een extenstie methoden nodig.
Die het object platslaat (serialize) naar text en terughaalt van text (deserialize).
```C#
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            string json = JsonSerializer.Serialize(value);
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
```

Dit kan je als volgt gebruiken:
```C#
   
   ShoppingCart shoppingCart = new ShoppingCart();
   ...
   //serialize shoppingCart & store in Session with key shoppingcart
   HttpContext.Session.Set<ActionCounter>("shoppingCart", shoppingCart);
   
   
   //deserialize shoppingCart from session with key shoppingcart
   ShoppingCart shoppingCart = HttpContext.Session.Get<ShoppingCart>("actionCounter");
```

## SetTempData & ShowTempData

TempData is een techniek die het eenmalig gebruik van waardes toestaat. Dit maakt onderwater gebruik van Cookies. 
Dit is bruikbaar om een b.v. berichtje bij een redirect van de ene pagina naar de andere door te geven zonder dat het in de url zichtbaar wordt.
TempData is kort beschikbaar (alleen in de Response), daarna is gaan de TempData verloren. 

Voor uitleg zie: [Tempdata](https://www.learnrazorpages.com/razor-pages/tempdata)
