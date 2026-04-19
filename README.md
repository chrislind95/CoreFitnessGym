Beskrivning:

Detta är en ASP.NEt Core MVC-applikation för ett gym-system. Systemet använder ASP.NET Identity för autentisering och gör det möjligt för användare att skapa konto, logga in och logga ut. 
När användaren är inloggad kan den hantera sin profil, uppdatera personuppgifter och ladda upp en profilbild, Användaren ska kunna välja ett medlemskap, ändra det och boka pass, men dit har jag inte hunnit.
Anävndaren har även möjlighet att ta bort sitt konto och all information om sig själv. 

Applikationen innehåller också ett kontaktformulär där även användare som inte är inloggade kan skicka meddelanden som sparas i databasen.

Systemet är uppbyggt med en tydluig struktur affärslogik, datalager och presentation är spearerade efter Clean Architecture. 


Starta projektet lokalt:

1. Klona repository
git clone https://github.com/chrislind95/aspnet-christian-lindgren.git

2. Konfigurera databas
Kontrollera att connection string i appsettings.json är korrekt:
  "ConnectionStrings": {
    "SqlConnection": "......"
  }

3. Uppdatera databas

4. Starta applikationen

Applikationen startar då lokalt i din webbläsare.

## Författare
Christian Lindgren
