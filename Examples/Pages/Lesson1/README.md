# Volgorde van voorbeelden met uitleg

Hieronder volgt een uitleg van de diverse voorbeelden en een handige volgorde om deze voorbeelden te bekijken.
Het is aan te raden om breakpoints te zetten en door de code te lopen, zodat je kan zien wat er gebeurd. 

## LesDemo

Hierin komen een deel van de verschillende technieken/concepten gecombineerd terug die in de andere code voorbeelden behandeld zijn.

## GetRequest.cshtml

Een webpagina kan op twee manier worden met:
1. GET Request (als je de webpainga intype in je adresbalk van je Browser, of een formulier met method="GET")
2. POST Request, dit wordt gedaan als je een formulier (```<form method="POST""```) verzend

In dit voorbeeld wordt gebruik gemaakt van een GET Request, deze roept de methode OnGet(...) aan in de Page Model (GetRequest.cshtml.cs) file.

## QueryStrings.cshtml

Een GET request kan op twee manieren worden aangroepen, namelijk:,
1. aanroepen m.b.v. link `(<a href=...)`
2. form met een GET method `<form method="GET">...</form>`. Let op: de GET method mag je weglaten omdat dit de GET de default waarde is! 

Er is een derde manier, namelijk met een AJAX request met JavaScript of jQuery maar dit valt buiten de context van deze cursus.

Het ophalen van een GET request waarde (QueryString), gebeurt met: `Request.Query["naam"]`. Waarbij `naam` de key van het het name attribute in de input is.
Dus in het GetRequest.cshtml bestand:
```razor
<form>
    <input name="wachtwoord" type="text">
    <button type="submit">Verzenden</button>
</form>
```
In de page model (GetRequest.cshtml.cs) staat het volgende:
```c#
    Request.Query["wachtwoord"]
```
Een request kan meerdere waardes bevatten (b.v. voor een checkbox), dus soms is het volgende nodig als je de eerste waarde wilt selecteren:
```c#
    Request.Query["wachtwoord"].First()
```

Vergeet ook niet dat een waarde niet aanwezig hoeft te zijn (null) en ook leeg kan (empty string) zijn!
```c#
    if (Request.Query.ContainsKey("name") && !string.IsNullOrWhiteSpace(Request.Query["name"]))
    {
        Name = Request.Query["name"].First().ToUpper();
    }    
```

Met de regel ``Name = Request.Query["name"].First().ToUpper();`` wordt het Attribute Name gevuld. 
Publieke attributen (zoals ``Name``, ``public string Name { get; set; }``) kunnen gebruiken in het razor bestand (GetRequest.cshtml).
```razor
<h1>Hello @Model.Name</h1>
```
Dit moet altijd wel voorafgegaan worden door `@Model`, `@Model` wijst als het ware terug naar de cshtml.cs bestand, dat de page model wordt genoemd.

[Uitleg van Mike](https://www.learnrazorpages.com/razor-pages/state-management#query-strings)

## QueryStringMethodParameter & QueryStringMethodBindProperty & QueryStringMethodRequest

Er zijn in Razor Pages (page model) drie manier (methoden) om de QueryString parameters waardes te gebruiken:
Om data te gebruiken zijn er drie manieren in Razor Pages, namelijk:

1. In de parameterlijst van de mehtode `public void OnGet(string surname) { ... }`.
   ```C#
   //QueryStringMethodParameter.cshtml.cs        
   public void OnGet(  [FromQuery]string firstname,
   [FromQuery]IEnumerable<string> middleNames,
   [FromQuery]string lastname) { ... }
   ```
2. Een proprety annoteren met `[BindProperty]`, **let op** dat voor een GET request is het volgende nodig: `[BindProperty(SupportsGet = true)]`!
   ```C#
   //QueryStringBindProperty.cshtml.cs
   [BindProperty(SupportsGet = true)] public string Firstname { get; set; }

   [BindProperty(SupportsGet = true)] public IEnumerable<string> MiddleNames { get; set; } = new List<string>();

   [BindProperty(SupportsGet = true)] public string Lastname { get; set; }

   public void OnGet()
   {

   }
   ```
4. De waardes handmatig uit de request halen, `Request.Query["naam"]` voor GET methode en `Request.Query["someKey"]` voor de Get Methode. Hierbij is het belangrijk te realiseren dat een waarde niet in de dictionary zit (check dit met `Request.Query.ContainsKey("firstname")` en de waarde kan eventueel leeg zijn (soms null?)).
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
    
Het kan lastig zijn om te ontdekken wanneer je welke methode gebruikt. Mijn tips voor gebruik:
* Gebruik de querystring method aanpak als je relatief simpele waarde(s) naar de pagina wilt sturen (b.v. het Id van aan te passen object, searchstring, etc) 
* Gebruik de BindProperty methode als er meerdere waardes  (b.v. een formulier met meerdere inputs) opgestuurd moeten worden en gevoelige informatie (password).
* Liever niet de Request methode gebruiken, dit kan wel handig zijn als je deze waardes rechtstreeks wilt gebruiken in je Razor Page (cshtml).

Todo: add object binding example!

## GetIncrementCounter.cshtml

Een teller die verhoogt kan worden m.b.v. GET Request i.c.m. met QueryParameters. Voor uitleg zie voorgaande twee voorbeelden.

**Opdrachten:**
1. Probeer zelf een knop toe te voegen die ook de count kan verlagen.
2. Gebruikt nu geen knop maar een hyperlink (`<a href="">..<a>`)).

Het probleem is dat je niet weet of de counter waarde moet verhogen of verlagen.
Wat er vaak gebeurt is dat er in een form een hidden field wordt toegevoegd:
`<input type="hidden" name="action">`
of in een hyperlink een extra query parameter `<a href="test?action=increment&count=3`.

## PostRequest.cshtml
POST-request gebeurt pas als er eerst een pagina is opgehaald m.b.v. (GET) vanwaaruit de POST gedaan kan worden (In dit geval is het dezelfde pagina).
POST worden altijd gedaan m.b.v. formulieren (of JavaScript/jQuery) met een POST method. 
`<form method="POST">` als method attribute wordt weggelaten dan is het altijd een GET request.
Dus `<form method="POST">..` is *niet* het zelfde als `<form>...`
Om bij de waarden te kunnen is het nodig `Request.Form["name"]` in de page model. 
Ook hier geldt dat je moet controleren of een waarde aanwezig is en niet leeg.
````razor
    <form method="post">
        <input name="naam">
        <button type="submit">Verzenden</button>
    </form>
````

```c#
    if (Request.Form.ContainsKey("naam") && !string.IsNullOrWhiteSpace(Request.Form["naam"]))
    {
        Name = Request.Query["naam"].First().ToUpper();
    }    
````

Je kan ook een POST request naar een andere pagina sturen, door het action attribute te gebruiken in een formulier.
```razor
    <form method="post" action="displayNaam.cshtml">
        <input name="naam">
        <button type="submit">Verzenden</button>
    </form>
```

## SendDataWithRequest.cshtml
Om data te gebruiken zijn er drie manieren in Razor Pages, namelijk:
1. De waardes handmatig uit de request halen, `Request.Form["someKey"]` voor POST methode en `Request.Query["someKey"]` voor de Get Methode. Hierbij is het belangrijk te realiseren dat een waarde niet in de dictionary zit (check dit met `Request.Query.ContainsKey("firstname")` en de waarde kan eventueel leeg zijn (soms null?)). 
2. In de parameterlijst van de mehtode `public void OnPost(string surname)`.
3. Een proprety annoteren met `[BindProperty]`, **let op** dat voor een GET request net anders is `[BindProperty(SupportsGet = true)]`.
     
**Let op: de details van een ophalen van data in een POST request en GET request zijn subtiel anders!!!**

[Uitleg van Mike](https://www.learnrazorpages.com/razor-pages/model-binding])

## PageHandler.cshtml

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

Routing is het systeem dat een url (adres wat je invoert in de webbrowser) matched met een Razor Pages.
Met andere woorden welke Razor page wordt aangeroepen.

[https://www.learnrazorpages.com/razor-pages/routing](Razor Pages Routing)

Ook is het mogelijk om parameters (dit wordt Route Data genoemd) mee te geven als onderdeel van een route. 
B.v. 
* Details/3  (is de route paramter value)
* Proucts/Tv/Sony (Tv en Sony zijn dan route parameters)

Boven aan de Razor Page kan je het volgende noteren (Route Template):
* `@page {productId}`
* `@page {category}\{brand}`

Het doorgeven van route values gebeurt als volgt met de `asp-route-nameOfVar` taghandler.
```razor
<form asp-route-action="increment" asp-route-count="@Model.Count" method="get">
    <button type="submit">+</button>
</form>
<hr>
<a asp-route-action="decrement" asp-route-count="@Model.Count">decrement</a>
```
Je kan dan de route als met de volgende manier ophalen:
* Als method parameters (deze manier heeft de voorkeur): 
```razor
   public void OnGet([FromRoute]int count, [FromRoute]string action)
```
* Met `Request.RouteValues["count"]`. 

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
* ```return Page();``` roept de "eigen" Razor Page aan. 
* Roep een andere page aan: 
```C#
   return RedirectToPage("Redirect", new {
      countParameter = Count
   });
```
Het twee (optionele argument) kan je gebruiken om parameters mee te sturen. 
Deze worden dan beschikbaar als QueryParameters.   
* Voor andere returns, check: 
[Uitleg over Action Results in Razor Pages](https://www.learnrazorpages.com/razor-pages/action-results)

## Cookies.cshtml
Cookies kunnen gebruikt worden voor het opslaan van tekst gegevens in de browser (client).
Als een cookie eenmaal gezet is, dan worden deze altijd meegestuurd bij de volgende aanroepen (ook naar andere pagina's binnen hetzelfde domein).
Let op cookies zijn beperkter dan Sessies en worden vaak gebruikt om een uniek id te zetten zodat we weten wie je bent (userid, sessionid), en eventuele voorkeuren op te slaan van een gebruiker.  
Het voordeel van een cookie is dat ze oneindig (of een beperkte tijd) geldig blijven in tegenstelling tot een sessie.  

Opdrachten Cookies: 
1. Kijk maar eens welke cookies een webshop over jou bijhoud (waar jij spullen koopt). Dit kan met de Chrome Development tools.
2. Maak zelf een cookie aan met daarin een uniek code `Guid.NewGuid()`, gebruik deze unieke code om dit te koppelen aan een gebruiker.
Je kan een dictionary maken van `Dictionary<Guid, string>` waarbij het eerste generic type de GUID is en het tweede de naam van de gebruiker.
Het probleem is dat de lijst na iedere request niet meer bestaat. Er is een work-around, maak de lijst static. Opmerking: doe dit nooit in een echte website maar sla het dan op in b.v. een database!
3. bestudeer het HTTP protocol werkt m.b.t. cookies en probeer dit zonder rechtstreeks te doen (dus zonder de bovengenoemde methoden).
Je kan als volgt met headers werken:
    ```c#
        HttpContext.Response.Headers.Add("Content-Type", "text/html");
        HttpContext.Request.Headers.ContainsKey("Some request header")
        HttpContext.Request.Headers["Some request header"]
    ```  
4. Tracking cookies zijn intressant (zo weten reclame makers waarin in geïntresseerd bent) en het is aan te raden om deze video even te kijken [How cookies can track you (Simply Explained)](https://www.youtube.com/watch?v=QWw7Wd2gUJk&ab_channel=SimplyExplained).


Uitleg toevoegen van set-cookies met headers

De volgende operaties kunnen worden gedaan met Cookies:
..* setCookie 

## Sessions.cshtml



 

