# Het up to date houden van het project

Het is belangrijk om het project up to date te houden. 
Er zullen ongetwijfeld bugs aanwezig zijn en hopelijk worden deze gemeld om vervolgens opgelost te worden.
Jullie zijn de eerste groep studenten die hiermee bezig gaan dus dit is onvermijdelijk.  
Om het project up to date te houden kan je git gebruiken. Het is natuurlijk ook mogelijk om af en toe een nieuwe versie te downloaden of te klonen als je problemen met git ervaart.

Met git zijn er aantal mogelijkheden voor het updaten van je project met de "centrale" repository:
* Normale workflow: 
```bash 
git add <file-1> <file-2> <file-3>
git commit -m "mijn eigen werk opslaan in de local repository"
git pull 
```
* Gebruik van de stash:
```bash 
git stash
git pull
git stash pop
```
* Een alternatief is dat je gewoon opnieuw het repostipry kloont en verder werkt in deze directory. 
```bash 
git clone https://github.com/jorislops/WebdevCourseRazorPages.git <newDirectory>
```

Indien er merge conflicten zijn wordt dit aangegeven door je IDE (ontwikkelomgeving) en moet je dit oplossen!
Hopelijk komt dit niet al te vaak voor. Om dit zo veel mogelijk te voorkomen adviseer ik om de tests en voorbeelden niet aan te passen. 
Indien je dit wel doet is het handig om dit ongedaan te maken voordat je een git pull doet, anders heb je een grotere kans op merge conflicten en/of niet werkende voorbeelden/tests.   

# Uitleg van de solutionstructuur

Waar kan je wat vinden: 
* In *Examples* staan voorbeelden. Als je dit project start kan je de voorbeelden in de webbrowser bekijken. Deze voorbeelden bevatten de concepten die in de lessen worden gebruikt en worden toegepast in de opdrachten.
* In *Exercises* staan de opdrachten, deze kun je testen. 
Ook staat er vaak al een opzet waarin je de opdrachten moet maken om deze te kunnen testen. 
* In *Exercises.Tests* staan de testen voor de opdrachten.

## Voorbeelden

De voorbeelden in `Exercises` zijn bedoeld om de concepten & technieken uit te leggen, te begrijpen en in isolatie te zien.
Naar mate de lessen volgen worden er steeds meer concepten/technieken gebruikt. 
Het bekijken en **begrijpen** van de voorbeelden moet hopelijk helpen om de opdrachten te maken. 
Daarnaast staan er in de opdrachten vaak relevante bronnen die betrekking hebben op de concepten en technieken.

De voorbeelden kan je ook live proberen: start hiervoor het project (play knop). 
Zorg er dan wel voor dat het *Examples* project start en niet per ongelijk het Exercises project. 
Als je de webbrowser opent en navigeer naar de [index](https://localhost:5001/) pagina en klik op een voorbeeld, dan zie je het voorbeeld in actie en de code is ook zichtbaar. 

*Opmerking:* we beginnen met week 0 :-).

Voor de voorbeelden die gebruik maken van een database (MySQL)

## Opdrachten

De opdrachten zijn bedoeld om je de concepten en technieken bij te brengen. Ook zal er worden gewerkt in de opdrachten aan de bruidensite (eindopdracht).
Je kunt dan per week (met uitzondering van week 0) stappen zetten om je bruidenwebsite te maken. Deze moet eind week 5 al ingeleverd worden, 
het is dus belangrijk om proberen bij te blijven. In week 6 is er een kort assessment. 
Het gaat er hierbij om dat je snapt en kan uitleggen hoe de concepten en technieken werken! De ervaring leert dat het raadzaam is om de opdrachten zelf te maken omdat je dan beter begrijpt wat er gebeurt en dit dus kunt uitleggeen. 
Kom je er zelf niet uit, vraag een docenten (atelier uren) of medestudenten om hulp. 

Of je de opdrachten goed hebt gedaan valt te controleren met de test in *Exercises.Test*. Per opdracht zijn er 1 of meerdere tests. 
Niet alle opdrachten hebben tests. Mocht je het idee hebben dat je opdracht toch goed hebt gedaan maar de test is het daar niet mee eens.
Dan zijn dit een aantal bruikbare tips:
1. Kijk goed naar de foutmelding, dit zou een indicatie moeten geven wat er mogelijk misgaat.
2. Het kan ook raadzaam zijn om de testcode te bekijken om een gevoel te krijgen van wat er verwacht wordt. Komt dit overeen met wat jouw code oplevert of de interpretatie van de opdracht?
3. Het is niet ondenkbaar dat een test ook fouten kan bevatten! Kom je een fout tegen, zou je dit mij s.v.p. willen laten weten dan kan ik het proberen te fixen.

## Hot Code Reload

**!!Misschien is het beter onderstaande technieken niet te gebruiken, op eigen risico!!**

Je wilt eigenlijk een aanpassing doen aan je code en meteen het resultaat zien de browser! Helaas duurt het vaak nogal lang. De oplossing is hot reload software.
Om live te kunnen herladen (hot reload) na het aanpassen van de code zijn er een aantal oplossingen:
*Geen een oplossing is perfect!* 
Misschien is iedere keer de webapplicatie opnieuw starten (na updaten van code) de beste oplossing.

* dotnet watch (geen debugger :-()
* Razor file compilation in ASP.NET Core
* LiveReload (maakt error weergave in browser stuk)
* **LiveSharp (mijn keus, kost alleen $, werkt ook niet in alle omstandigheden goed)**
* Wachten tot ASP.NET Core versie 6, er wordt aan gewerkt. Dus we moeten nog even geduld hebben. Hopelijk is dit de perfecte oplossing. Fingers crossed.
Het staat in de [ASP.NET Core 6 Roadmap](https://github.com/dotnet/aspnetcore/issues/27883) en er zijn ook al previews van beschikbaar (of het werkt weet ik niet). 

###dotnet watch  
Dit is nogal traag (dus en het is nog steeds vaak relatief lang wachten) en het is lastig om er mee te debuggen (tenminste in Rider, misschien heb je in Visual Studio meer geluk).
* https://dotnetcoretutorials.com/2020/01/01/live-coding-net-core-using-dotnet-watch/
* https://docs.microsoft.com/en-us/aspnet/core/tutorials/dotnet-watch?view=aspnetcore-5.0

##Razor file compilation in ASP.NET Core
Zorgt ervoor dat je cshtml bestanden na een aanpassing opnieuw gecompiled worden, je zult zelf moeten refreshen in de browser.
https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-compilation?view=aspnetcore-5.0&tabs=visual-studio

###LiveReload
LiveReload herlaat de browser na het aanpassen van een cshtml bestand.
Het aangepast cshtml bestand zal gecompileerd worden door Razor File compilation (zie hierboven).
De browser update dan automatisch.  
Let op:
Let op als je dit aanzet. Dan doet de `app.UseDeveloperExceptionPage();` het niet meer.
Dit betekent dat je geen foutmeldingen te zien krijgt in de browser in geval van een Exception (fout in je code).
[LiveReload](https://github.com/RickStrahl/Westwind.AspnetCore.LiveReload)

###LiveSharp
De beste oplossing is die ik tot nu tot heb gevonden is [LiveSharp](https://www.livesharp.net/).
Is helaas maar 14 dagen gratis. Zeker de moeite waard om eens te proberen.
Wellicht handig om eens te proberen, misschien helpt het je het wachten te verkorten en kan je meer proberen/ontwikkelen in minder tijd!
Let op sommige dingen werken wel en andere niet (als je b.v. een nieuwe methode toevoegt en deze wilt aanroepen m.b.v. een handler method vanuit een cshmtl bestand dan werkt het niet, waarschijnlijk omdat dit ergens in ASP.NET geregeld wordt).

## Relevante bronnen

ASP.NET Core Razor Pages: 
* [LearnRazorPages](https://www.learnrazorpages.com/)
  * Per onderwerp zie: [Table of Content van learnrazorpages](https://www.learnrazorpages.com/table-of-contents)
* [Kudvenkat Razor Pages Video Tutorials](https://www.youtube.com/watch?v=3F9SpUYTB6Y&list=PL6n9fhu94yhX6J31qad0wSO1N_rgGbOPV&ab_channel=kudvenkat) - veel video’s over Razor Pages.
* [Pluralsight](https://www.pluralsight.com). Is niet gratis. Heeft een aantal kwalitatief goede curssen.
~~* [Master ASP.NET Core 3.1 Razor Pages](https://learning.oreilly.com/videos/master-asp-net-core/9781800568068/) - ik heb mijn twijfels bij de kwaliteit van deze video’s, hoog truckjes gehalte!~~

Voor Dapper:
* [Dapper Getting Started](https://dapper-tutorial.net/dapper)
* [Learn Dapper](https://www.learndapper.com/)
* [dapper github](https://github.com/StackExchange/Dapper) De officiele documentatie is misschien wat ingewikkeld.
* Ik ben nog opzoek naar een goede dapper video tutorial (suggesties zijn welkom).

## Software

Hieronder staat de benodigde software beschreven die noodzakelijk is om ASP.NET Razor Pages te kunnen ontwikkelen. 
Deze software moet je downloaden en installeren. 

### .NET Core

* [.NET ~~Core~~](https://dotnet.microsoft.com/download) (download & installeer minimaal versie 5.x).
Vanaf versie vijf wordt niet meer de naam ".NET Core"  gebruikt, maar heet het .NET. Versie 4 is overgeslagen.   
Je kunt controleren of je de juiste versie hebt (grote kans dat het al geïnstalleerd is) in je console/terminal, 
met `dotnet --version`.

### Database MySQL

MySQL is een Relationele Database Management Systeem (DBMS) dat we gebruiken in deze cursus.
Download en installeer de [MySQL Community Edition](https://dev.mysql.com/downloads/).
De scripts om een databases aan te maken en te vullen (tabellen en dummy data) kan je vinden in `Examples/Pages/Lesson3/SQL/MySQL_CreateAndFillTable.sql`, `Exercises/Tests/MysqlCafe.SQL` (Let op: maak zelf de database aan Exercises en Tests) en `DbUtils`.
Als username gebruik ik `root` en als wachtwoord `Test@1234!`. Gebruik jij iets anders pas dit dan aan in de config bestanden en DBUtils:
`Examples/appsettings.Development`, `Exercises.Tests`.
Een connectionstring ziet er vaak zo uit en moet worden aangepast aan jou eigen situatie:
`WebdevCourseRazorPages.Exercises.MySQL": "Server=127.0.0.1;Port=3306;Database=Exercises;Uid=root;Pwd=Test@1234!;`.

Voor het testen moet je zelf een database (schema) aanmaken met de naam `Tests`.

Een connectionstring kan je testen op de pagina: `http://localhost:5000/Utils/TestConnection`.
Wil je weten hoe je een connectiestring moet opbouwen: [Connectionstrings - mysql-connector-net-mysqlconnection](https://www.connectionstrings.com/mysql-connector-net-mysqlconnection/).

### IDE's
Er zijn veel IDE's (ontwikkelomgeving) beschikbaar om ASP.NET Core Razor Pages mee te ontwikkelen, zoals:
* [Rider](https://www.jetbrains.com/rider/) (mijn persoonlijke voorkeur, omdat ik een Mac gebruik en gewone Visual Studio daarop niet werkt en Visual Studio for Mac niet echt een fantastisch product is).
Voor de producten van JetBrains kan je een grais [studenten licentie aanvragen](https://www.jetbrains.com/community/education/#students).
* [Visual Studio Family](https://visualstudio.microsoft.com/):  
  * **Visual Studio**: een prima IDE om Razor Pages mee te ontwikkelen. De community versie is gratis. 
  * Visual Studio Code: hiermee kan je Razor Pages ontwikkelen echter zijn er wel een aantal uitbreidingen (extension) hiervoor nodig.
  * Visual Studio for Mac: hiermee kan je Razor Pages ontwikkelen, zelf ben ik niet onder de indruk van deze IDE (mening van mij).    
We kunnen je helpen met problemen met Rider en Visual Studio omdat wij daarmee voldoende ervaring hebben. 
Gebruik je een andere IDE dan ben je wellicht meer op jezelf aangewezen!

# Heel veel plezier en happy coding! 
