# Opdrachten Les 2 - Pagina's structuren, Model Binding & Validation. 

Er zijn verschillende soorten Razor Pages files die indien slim gebruikt worden, 
duplicatie van code voorkomen en de structuur geven aan de webpagina's.

Dit wordt hier uitgelegd:  
[Different types of Razor files](https://www.learnrazorpages.com/razor-pages/files/)

Het komt vaak voor dat we gegevens (Model) naar de server willen sturen en deze willen valideren op correcte input. 
Deze validatie moet *altijd* op de server worden uitgevoerd en kan optioneel uitgevoerd worden op de cliënt (browser).

Alle opdrachten voor deze week hebben betrekking op de eindopdracht (NHLStenden Café). 
Voor de meeste van deze technieken heeft het weinig zin om ze te oefenen in een te kleine context.

# Opdracht 1 - NHL Café paginastructuur maken. 

Je hebt (als het goed is) vorige week een inlog systeem gemaakt voor obers. Wellicht heb je nog geen Bootstrap toegevoegd. 
Het idee is om dit nu toe te voegen. Waarschijnlijk is dit al gedaan bij het aanmaken van de website. 
Gebruik _Layout.cshtml pagina om de structuur van je project op te zetten en content pagina's voor de individuele pagina's.

De _ViewStart.cshtml kan gebruik worden om altijd dezelfde code uit te voeren voor een pagina. 
Het controleren of er ingelogd is, is hier een goed voorbeeld van. 
Zet de pagina's waarop gecontroleerd moet worden of een ober is ingelogd in een aparte folder (b.v. Members).
Voeg vervolgens aan deze folder een  _ViewStart.cshtml toe waarop controles uitgevoerd worden of er wel of niet ingelogd is. 
Bij een niet ingelogde gebruiker kan je redirecten naar de login pagina.

*Opmerking: 
misschien is het handig tijdens het ontwikkelen om het login gedeelte te deactiveren zodat je niet altijd hoeft in te loggen tijdens het ontwikkelwerk (scheelt een hoop gedoe).*

Belangrijke aandachtpunten:  
- Zorg ervoor dat je de grid structuur van Bootstrap correct gebruikt, container > row > col. De container staat waarschijnlijk in de _Layout.cshtml pagina!  
- Gebruik de juiste bootstrap klassen voor o.a. je formulieren, etc.  
- Zorg voor een duidelijke navigatiestructuur in je pagina's zit (zie mock-ups), de logout button moet verschijnt in het navigatie menu (b.v. navbar) als er is ingelogd.  

Je hoeft niet per se Bootstrap te gebruiken, gebruik dan een ander layout framework naar keus (b.v. Material Design). 

# Opdracht 2 - Partial Pages

Wellicht kan je Partial Pages handig gebruiken. 
Tip: maak voor verschillende onderdelen van de webpagina's partials aan, zet daarin b.v. dummy gegevens.
Hopelijk wordt je code een stuk leesbaarder en misschien kan je code ook hergebruiken:
[Partial Pages](https://www.learnrazorpages.com/razor-pages/partial-pages).

# Opdracht 3 - View Components (optioneel - kan lastig zijn)

Maak een login [ViewComponent](https://www.learnrazorpages.com/razor-pages/view-components), zodat er een herbruikbaar component wordt gemaakt waarmee een login scherm kan worden weergegeven.
Naast de weergave heeft deze ViewComponent ook code om het inloggen te faciliteren.
 
Roep deze ViewComponent aan in je pagina (waarschijnlijk _Layout.cshtml).

# Opdracht 4 - Validation & Model Binding

Bij het registeren van ober willen we graag valideren of de input correct is.
Indien de input niet correct is willen we uiteraard  fatsoenlijke foutmeldingen zien, zodat de gebruiker weet hoe deze te corrigeren.
Voor het juist stijlen van foutmeldingen in Bootstrap i.c.m. validatie is een voorbeeld te zien in `Lesson2/Products/Create.cshtml`. 

Het volgende willen we controleren:  
- Een ober moet een geldig email adres opgeven.  
- Een ober moet twee keer het gewenste wachtwoord intypen, deze moet aan elkaar gelijk zijn.  
- Een wachtwoord moet minimaal 8 tekens bevatten.

* [Validation](https://www.learnrazorpages.com/razor-pages/validation) 
* [Model Binding](https://www.learnrazorpages.com/razor-pages/model-binding)
* [Model Validation Docs van Microsoft](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-3.1)
