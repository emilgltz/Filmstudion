## Reflektion ##

**REST**

*OPEN*
`I den öppna åtkomstpunkten har jag skapat samtliga åtkomstpunkter som inte kräver autentisering för att användas, alla dessa använder GET och returnerar anslutna filmstudios samt de filmer som finns tillgängliga för tillfället. Alla dessa är som sagt öppna och informationen kan hämtas av alla besökare. Data hämtas från OpenFilmStudioResource/OpenFilmStudioResource, i åtkomstpunkten open/movies kommer således ingen information om antal upplagor att visas utan endast information om filmerna  `

*MOVIES*
`Dessa tre åtkomstpunkter kräver autentisering, PUT /api/Movies/{movieId} samt POST api/Movies är endast tillgängliga med ett admin-konto. CreateUpdateMovieResource hanterar uppdatering av aktuella filmer samt lägger till nya filmer.`

*FILMCOPY*
`FilmCopyResource är resursen som hanterar och visar tillgängliga upplagor, Här krävs också autentisering och ett studiokonto kan låna, returnera samt se antalet tillgängliga kopior`

