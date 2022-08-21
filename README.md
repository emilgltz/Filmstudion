# Användning och start #

*Klona ner repot och se till att din dator är kompatibel med .net 6.0, starta projektet i Visual Studio för att köra igång programmet samt API:et. Navigera till http://localhost:46666/ för att komma till klientgränssnittet*

# Åtkomstpunkter till projektets API # 

*Nedanför listas samtliga åtkomstpunkter till API:et,för att använda API:ets samtliga åtkomstpunkter kan du använda `postman` alternativt `swagger`. När programmet är startat i Visual Studio kan du besöka`http://localhost:46666/swagger/index.html` där alla åtkomstpunkter kan användas på ett smidigt sätt, via `swagger` kan du dessutom se vilka parametrar/bodyinnehåll som servern behöver hos respektive åtkomstpunkt för ett lyckat anrop/svar*

**FilmCopies**
*Filmkopior*

*Hanterar och justerar filmupplagor i filmstudion*


*GET*
`/api/v2/FilmCopies/{movieId}`

*GET*
`/api/v2/FilmCopies`

*PUT*
`/api/v2/FilmCopies/{movieId}/rent`

*PUT*
`/api/v2/FilmCopies/{movieId}/return`


**Movies**
*Filmer*


*PUT*
`/api/v2/Movies/{movieId}`

*GET*
`/api/v2/Movies`

*POST*
`/api/v2/Movies`


**Open**
*Öppna/Publika Åtkomstpunker*


*GET*
`/api/v2/Open/filmstudios`

*GET*
`/api/v2/Open/filmstudios/{filmStudioId}`

*GET*
`/api/v2/Open/movies`

*GET*
`/api/v2/Open/movies/{movieId}`


**Users**
*Hanterar användare*


*POST*
`/Users/register/admin`

*POST*
`/Users/authenticate`

*POST*
`/Users/register/filmStudio`