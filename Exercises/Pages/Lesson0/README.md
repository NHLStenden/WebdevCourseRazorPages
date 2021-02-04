# Uitleg 

Als je het project (Exercises) runt (Play knop in Visual Studio en Rider, zorg ervoor dat je het juiste project runt) kun je de browser openen (als dat niet automatisch gaat) 
en de output bekijken per opdracht.
Dus b.v. `https://localhost:5001/Lesson0/assignment0`.

Wil je nu testen of je een opdracht goed hebt gemaakt. 
Dan kan je `Exercises.Tests/Lesson0.cs`. Als je rechts klikt in de methode die je wilt testen, als het goed is moet je nu `run unit test` aanklikken. 
Als het goed is runt je test en krijg je te zien of het goed (groen) of fout (rood) is gegaan. 

# Opdrachten Les 0 - HTTP Request & Response

## Opdracht 0 - Request & Response

Als we een HTTP `request` maken, dan krijgen we een HTTP `response` terug. 
We kunnen de `request` en `response` rechtstreeks gebruiken in C#. 
In de `response` staat een `StatusCode`.
Deze status codes staan in `HttpStatusCode`.  
Dit kan als volgt gebruikt worden `response.StatusCode = (int) HttpStatusCode.OK``;

Ook moet de `ContentType` worden gezet op de `response`, voor deze opdracht text/plain (*meestal text/html*).

We kunnen de message body als volgt aanpassen: 
`response.WriteAsync("Some Text")`.

De methoden retourneerd een `Task`. 
Het makkelijkste is om gebruikte te maken van `return Task.CompletedTask;`. Dit geeft aan de taak (request) afgerond is.  

Zorg ervoor dat de methode de string "Hello World" teruggeeft als plain text (text/plain). 

Het is **erg** belangrijk voor een webontwikkelaar om het HTTP protocol te begrijpen!
Het HTTP Protocol wordt gebruikt om resources op te vragen (html, css, js, plaatjes, etc) van een server.
Uitleg over het HTTP Protocol kan je hier vinden: 
* [LearnCode.academy](https://www.youtube.com/watch?v=e4S8zfLdLgQ&ab_channel=LearnCode.academy)
* [HTTP Message](https://en.wikipedia.org/wiki/HTTP_message_body)
* [HTTP Overview](https://developer.mozilla.org/en-US/docs/Web/HTTP/Overview)
* [HTTP Status Codes](https://developer.mozilla.org/nl/docs/Web/HTTP/Status)
* [HTTP Content Type](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Type)

## Opdracht 1 - Request & Response.

Bijna hetzelfde als de vorige opdracht. Echter zorg er nu voor dat je html terug geeft i.p.v. tekst. 
Zet **Hello World** in een H1 tag.

## Opdracht 2 - QueryString.

Met een QueryString (alles achter het vraagteken) is het mogelijk om data mee te geven in een request.
b.v. `http://localhost:5000/assignment2?name=test`  
De QueryString is in dit geval `?name=test`. Het vraagteken markeert het begin van de QueryString.
Vervolgens komt er een key value pair, gescheiden door een `=` teken `key=value`.
De key is in dit geval `name` en de value is `test`.

De opdracht is om een request te verwerken en de QueryString te gebruiken om de juiste (HTTP) response te retourneren.
Bekijk ook de bijbehorende test voor het verwachte resultaat. Zorg ook voor het juiste ContentType, namelijk text/html!

Code Tips:
```c#
    request.QueryString;  //alles achter het vraagteken
    request.QueryString.HasValue;
    request.QueryString.Value;
    
    //handige string methoden:
    someStringVar.Split("=")
    string.IsNullOrWhiteSpace(someStringVar)
```
De input (url) en verwachte output staan hieronder beschreven:
* http://localhost:5001/Lesson0/assignment2?name=Joris  =>   `<h1>Hello Joris</h1>   HttpStatusCode.OK`
* http://localhost:5000/Lesson0/assignment2             =>   `<h1>Bad Request</h1>   HttpStatusCode.BadRequest`
* http://localhost:5000/Lesson0/assignment2?name=       =>   `h1>Bad Request</h1>   HttpStatusCode.BadRequest`

**Opmerking: gebruik in deze opdracht `QueryString`, het idee is om de `QueryString` zelf uitelkaar te halen.** 

## Opdracht 3 - QueryString meerdere waarden.

Een querystring kan meerdere waardes (values) hebben voor dezelfde key (parameter), ook al is het meestal gebruikelijk maar 1 waarde per key (parameter) op te sturen.

Het is vaak best lastig om zelf de query string te verwerken met `QueryString`. We kunnen de query beter verwerken met: `Query["someKey"]`.
`Query` is een dictionary, daaruit kunnen we values opvragen, dit doen we als volgt: ``Query["someKey]"``. Het resultaat is een collectie met mogelijk meerdere waardes!!
Het is niet altijd duidelijk of een key een waarde heeft! Dus dit moet je **altijd** controleren met iets dergelijks: 
`if(Query.ContainsKey("someKey")) { ... }`

Maak een methode die de volgende input en output verwerkt:  
* http://localhost:5000/assignment3?name=Joris&name=Lops&age=32 => `<ul>Leeftijd: 32 van<li>Joris</li><li>Lops</li></ul>`  

## Opdracht 4 Beschermen tegen Cross-Site Scripting (XSS) aanvallen.

Als je de volgende URL aanroept dan wordt er JavaScripts uitgevoerd.
http://localhost:5001/Lesson0/assignment4?somevariable=%3Cscript%3Ealert(%27script%20from%20url%27);%3C/script%3E

Dit is zeer gevaarlijk, zie [xss attacks](https://owasp.org/www-community/attacks/xss/).

Dit kan voorkomen worden door de methodes van `WebUtility` klasse te gebruiken, zoals:
* `WebUtility.HtmlEncode(...)`     
* `WebUtility.HtmlDecode(...)`  
* `WebUtility.UrlDecode(...)`  
* `WebUtility.UrlEncode(...)`  
   
Zorg ervoor dat er geen script niet meer uitgevoet kan  worden! Door de juiste methodes
van WebUtility class te gebruiken!

* [cross site scripting voorbeeld](https://www.youtube.com/watch?v=9qfBJIoIUgs&ab_channel=MindertAardema)

## Opdracht 5 Formulier afhandeling en validatie. 

De GET request geeft een formulier weer op het scherm.
         
Dit formulier heeft drie textboxen, 1 voor voornaam, 1 voor achternaam, 1 voor leeftijd.
En natuurlijk een verzend (submit) knop met als tekst *Verzenden*.
De textboxen hebben respectievelijk  het name-attribute firstname, lastname, age
Dus b.v. `<input name="firstname" type="text" />`

Het formulier moet gebruik maken van de POST method, deze roept de Post Handler `Assignment5Post` aan. 
Deze moet  de input (3 textboxen) van het formulier verwerken.
Je kan bij de verzonden data van een "POST-formulier" met `request.Form` property, b.v. `request.Form["firstname"].First()`.
**Opmerking:** vergeet niet te controleren of een waard de wel aanwezig is: `request.Form.ContainsKey("firstname")`. En dat deze gevuld is:
`string.IsNullOrWhiteSpace(request.Form["firstname"])`!  
De First() methode haalt de eerste waarde op, het is mogelijk om meerdere waarden te versturen (b.v. checkbox).
In het geval van 1 waarde, is het aanroepen van `First()`  handig.

De input moet gecontroleerd worden op correctheid en er mag geen javascript worden uitgevoerd!
Zie voor uitleg de vorige opdracht.

Age moet daarnaast worden gecontroleerd of dat het een geheel getal is.
Voornaam en Achternaam moeten minimaal 2 karakters  zijn (check ook of de input leeg is).
Als er een fout in de pagina zit moet het formulier opnieuw weergegeven worden met foutmelding(en) "Ongeldige input" achter de desbetreffende input.

De ingevoerde waardes moeten blijven staan in het formulier.
Je moet dan opnieuw het formulier retourneren, tip maak een functie voor het weergeven van het formulier.
 
Als er geen fouten weergegeven worden dan moet de output als volgt zijn:
`$"<h1>{lastName}, {firstName} is {age} jaren oud</h1>"`

Kijk ook goed naar de testcases om de methode te maken volgens de specificatie (tests).



### Todo: Mogelijk extra opdrachten toevoegen: 
* Opdracht 2 uitbreiden met meerde query string parameters, voornaam en achteraam
* Url segementen (kan ook in volgende les)
* Opdracht met meerdere buttons toevoegen (voetbal score bord)
  * verschillende methoden 

* Optioneel: rekenmachine met meerdere knoppen

* Redirect van input

* 21 kaartspel
