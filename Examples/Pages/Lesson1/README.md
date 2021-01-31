# Volgorde van voorbeelden met uitleg

## GetRequest.cshtml


## QueryStrings.cshtml

Een GET request kan op twee manieren worden aangroepen, namelijk:,
1. aanroepen m.b.v. link `(<a href=...)`
2. form met GET method 

Er is een derde manier, namelijk met een AJAX request met JavaScript of jQuery maar dit wordt later behandeld.

Het ophalen van een GET request waarde (QueryString), gebeurt met: `Request.Query["naam"]`. Waarbij `naam` de key van het het name attribute in de input is.
Dus in het GetRequest.cshtml bestand:
```razor
<form>
    <input name="wachtwoord" type="text">
    <button type="submit">Verzenden</button>
</form>
```
In *code-behinde* bestand (GetRequest.cshtml.cs):
```c#
    Request.Query["wachtwoord"]
```
Een request kan meerdere waardes bevatten (b.v. voor een checkbox), dus vaak is het volgende nodig om de eerste (en engiste) waarde te selecteren:
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
Publieke attributen (zoals ``Name``, ``public string Name { get; set; }``) kunnen gebruiken in de razor bestand (GetRequest.cshtml).
```razor
<h1>Hello @Model.Name</h1>
```
Dit moet altijd wel voorafgegaan worden door `@Model`, `@Model` wijst als het ware terug naar de cshtml.cs bestand.

[Uitleg van Mike](https://www.learnrazorpages.com/razor-pages/state-management#query-strings)

## GetIncrementCounter.cshtml
Een voorbeeld van bovenstaande technieken. 
**Opdrachten:**
1. probeer zelf een knop toe te voegen die ook de count kan verlagen
2. gebruikt nu geen knop maar een hyperlink (`<a href="">..<a>`)).

Het probleem is dat je niet weet of de counter waarde moet verhogen of verlagen.
Wat er vaak gebeurt is dat er in een formulier een hidden field wordt toegevoegd:
`<input type="hidden" name="action">`
en in een hyperlink een extra query parameter `<a href="test?action=increment&count=3`.


## PostRequest.cshtml
POST-request gebeurt pas al er eerst een pagina is opgehaald m.b.v. (GET) vanwaaruit de POST gedaan kan worden (In dit geval is het dezelfde pagina).
Post worden altijd gedaan m.b.v. formulieren (of JavaScript/jQuery). 
`<form method="POST">` als method attribute wordt weggelaten dan is het altijd een post request.
Dus `<form method="POST">..` is het zelfde als `<form>...`
Om bij de waarden te kunnen is het nodig `Request.Form["name"]` nodig. 
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

Je kan ook een post request naar een andere pagina doen, door het action attribute te gebruiken in een formulier.
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



## RouteParameters.cshtml


## Redirect.cshtml

## Cookies.cshtml
Cookies kunnen gebruikt worden voor het opslaan van tekst gegevens in de browser (client).
Als een cookie eenmaal gezet zijn dan worden deze altijd meegestuurd bij de volgende aanroepen (ook naar andere pagina's binnen hetzelfde domein).
Let op cookies zijn beperkter dan Sessies en worden vaak gebruikt om een uniek id te zetten zodat we weten wie je bent (userid, sessionid), en eventuele voorkeuren op te slaan van een gebruiker.  
Het voordeel van een cookie is dat ze oneindig (of een beperkte tijd) geldig blijven in tegenstelling tot een sessie.  

Opdrachten Cookies: 
1. Kijk maar eens welke cookies een webshop over jou bijhoud (waar jij spullen koopt). Dit kan met de Chrome Development tools.
2. Maak zelf een cookie aan met daarin een uniek code `Guid.NewGuid()`, gebruik deze unieke code om dit te koppelen aan een gebruiker.
Je kan een dictionary maken van `Dictionary<string, string>` waarbij het eerste generic type de GUID is en het tweede de naam.
Het probleem is dat de lijst na iedere request niet meer bestaat. Er is een work-around, maak de lijst static. Opmerking: doe dit nooit in een echte website maar sla het dan op in b.v. een database!
3. bestudeer het http protocol m.b.t. cookies en probeer dit zonder rechtstreeks te doen (dus zonder de bovengenoemde methoden).
Je kan als volgt met headers werken:
    ```c#
        HttpContext.Response.Headers.Add("Content-Type", "text/html");
        HttpContext.Request.Headers.ContainsKey("Some request header")
        HttpContext.Request.Headers["Some request header"]
    ```  
4. Tracking cookies zijn intressant en het is aan te raden om deze video even te kijken [How cookies can track you (Simply Explained)](https://www.youtube.com/watch?v=QWw7Wd2gUJk&ab_channel=SimplyExplained).


Uitleg toevoegen van set-cookies met headers

De volgende operaties kunnen worden gedaan met Cookies:
..* setCookie 

## Sessions.cshtml



 

