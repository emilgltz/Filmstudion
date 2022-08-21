"use strict";

const app = {
    content: document.getElementById('content'),
    loginMsg: document.getElementById('logMessage'),
    rentMsg: document.getElementById('rentMessage'),
    regButton: document.getElementById('reg'),
    loginButton: document.getElementById('login'),
    filmStudioButton: document.getElementById('studios'),
    filmButton: document.getElementById('movies'),
    globaltoken: "",
    id: 0

};



app.filmButton.addEventListener('click', function getMovies() {
    app.rentMsg.innerHTML = "";

    if (app.globaltoken === "") {
        fetch('/api/v2/open/movies/')
            .then(resp => resp.json())
            .then(movies => showMovies(movies))
    }
    else {
        fetch('/api/v2/movies', {
            method: 'GET',
            headers: {
                'Content-type': 'application/json; charset=UTF-8',
                'Authorization': `Bearer ${app.globaltoken}`
            }
        })
            .then(resp => resp.json())
            .then(movies => showMovies(movies))
    }
});



async function showRented(result) {
    console.log(result);
    app.rentMsg.innerHTML = `<h4 class="message">Ni har nu lånat en upplaga av filmen: ${result.movieName}</h4>`;
    getUpdatedMovies();
}

app.filmStudioButton.addEventListener('click', function () {
    fetch('/api/v2/open/filmstudios/')
        .then(resp => resp.json())
        .then(studios => showOpenFilmStudios(studios))
});

async function showOpenFilmStudios(filmStudios) {
    app.rentMsg.innerHTML = "";

    if (app.globaltoken === "") {
        app.loginMsg.innerHTML = "";
    }

    app.content.innerHTML = `<div id="filmStudiosDiv"></div>`;
    let filmStudiosDiv = document.getElementById('filmStudiosDiv');

    for (var i = 0; i < filmStudios.length; i++) {
        let studio = filmStudios[i];
        filmStudiosDiv.innerHTML += `<div id="studio"><h3>${studio.filmStudioName} i ${studio.city}</h3>
                <h4>Ordförande: ${studio.presidentName}</h4>
                <h4>Epost: ${studio.email}</h4></div>`
    }
}

app.regButton.addEventListener('click', function () {
    app.rentMsg.innerHTML = "";

    if (app.globaltoken !== "") {
        getUserStudio(app.id)
    }

    if (app.globaltoken === "") {
        app.loginMsg.innerHTML = "";

        app.content.innerHTML =
            `<h2>Registrera en ny filmstudio</h2>
            <form id="registerForm">
                <div class="form-group">
                    <label for="filmStudioName">Filmstudions namn</label><br />
                    <input type="text" class="form-control" id="filmStudioName">
                </div>
                <div class="form-group">
                    <label for="city">Stad</label><br />
                    <input type="text" class="form-control" id="city">
                </div>
                <div class="form-group">
                    <label for="presidentName">Ordförande</label><br />
                    <input type="text" class="form-control" id="president">
                </div>
                <div class="form-group">
                    <label for="email">E-post</label><br />
                    <input type="email" class="form-control" id="email">
                </div>
                <div class="form-group">
                    <label for="password">Lösenord</label><br />
                    <input type="password" class="form-control" id="password"><br />
                </div>
                <div>
                    <br /><button type="submit" class="btn btn-dark" id="submitRegister">Registrera</button>
                </div>
            </form>`

        register();
    }
});

async function register() {

    let registerForm = document.getElementById('registerForm');

    registerForm.addEventListener('submit', async (e) => {
        e.preventDefault();

        let filmStudioName = document.getElementById('filmStudioName').value;
        let city = document.getElementById('city').value;
        let president = document.getElementById('president').value;
        let email = document.getElementById('email').value;
        let password = document.getElementById('password').value;

        fetch('/users/register/filmStudio', {
            method: 'POST',
            body: JSON.stringify({
                FilmStudioName: filmStudioName,
                City: city,
                PresidentName: president,
                Email: email,
                Password: password
            }),
            headers: {
                'Content-type': 'application/json; charset=UTF-8',
            },
        })
        showLogin()
    });
}

app.loginButton.addEventListener('click', function showLogin() {
    app.rentMsg.innerHTML = "";

    if (app.globaltoken === "") {
        app.loginMsg.innerHTML = "";
    }

    app.content.innerHTML =
        `<h2>Logga in</h2>
            <form id="loginForm">
                <div class="form-group">
                    <label for="Email">E-post</label><br />
                    <input type="email" class="form-control" id="email">
                </div>
                <div class="form-group">
                    <label for="Password">Lösenord</label><br />
                    <input type="password" class="form-control" id="password"><br />
                </div>
                <div>
                    <br /><button type="submit" class="btn btn-dark" id="submitLogin">Logga in </button>
                </div>
            </form>`;
    loginUser();
});

async function showLogin() {
    app.rentMsg.innerHTML = "";

    if (app.globaltoken === "") {
        app.loginMsg.innerHTML = "";
    }

    app.content.innerHTML =
        `<h2>Logga in</h2>
            <form id="loginForm">
                <div class="form-group">
                    <label for="Email">E-post</label><br />
                    <input type="email" class="form-control" id="email">
                </div>
                <div class="form-group">
                    <label for="Password">Lösenord</label><br />
                    <input type="password" class="form-control" id="password"><br />
                </div>
                <div>
                    <br /><button type="submit" class="btn btn-dark" id="submitLogin">Logga in </button>
                </div>
            </form>`;
    loginUser();
}

async function loginUser() {
    let loginForm = document.getElementById("loginForm");

    loginForm.addEventListener('submit', async (e) => {
        e.preventDefault();

        let email = document.getElementById('email').value;
        let password = document.getElementById('password').value;

        let response = await fetch('/users/authenticate', {
            method: 'POST',
            body: JSON.stringify({
                Email: email,
                Password: password
            }),
            headers: {
                'Content-type': 'application/json; charset=UTF-8',
            },
        })

        let user = await response.json();
        app.globaltoken = user.token;
        app.id = user.id;

        if (app.globaltoken !== undefined) {
            app.regButton.innerHTML = 'Aktuella lån';
            app.loginButton.innerHTML = 'Logga ut';
            getUserStudio(app.id);
        }
        if (app.globaltoken === undefined) {
            app.content.innerHTML = '<h1>Tyvärr gick inte registreringen igenom.</h1>';
        }

        app.loginButton.addEventListener('click', function () {

            if (app.globaltoken !== "") {
                app.globaltoken = "";
                app.id = 0;
                app.content.innerHTML = "";
                app.loginMsg.innerHTML = `<h2>Du är nu utloggad</h2>`;

                app.regButton.innerHTML = 'Registrering';
                app.loginButton.innerHTML = 'Inloggning';
            }
        });
    });
}

function getUserStudio(id) {
    fetch('/api/v2/open/filmStudios/' + id)
        .then(resp => resp.json())
        .then(studio => showStudio(studio))
}

function showStudio(studio) {
    getRentedMovies();
    app.loginMsg.innerHTML = `<h2>Inloggad via filmstudion: <span id="spanStudio">` + studio.filmStudioName + `</span></h2>`;
    app.rentMsg.innerHTML = "";
}

function getRentedMovies() {

    fetch('/api/v2/FilmCopies', {
        method: 'GET',
        headers: {
            'Content-type': 'application/json; charset=UTF-8',
            'Authorization': `Bearer ${app.globaltoken}`
        }
    })
        .then(resp => resp.json())
        .then(rented => showRentedMovies(rented))
}

async function showRentedMovies(rented) {

    app.content.innerHTML = `<div id="moviesDiv"></div>`;
    let moviesDiv = document.getElementById('moviesDiv');

    if (rented.length === 0) {
        moviesDiv.innerHTML += `<div><h2>Filmstudion har för tillfället inga aktiva lån</h2></div>`;
    }

    else if (rented.length >= 1) {
        moviesDiv.innerHTML += `<div><h2>Aktiva lån:</h2></div>`;

        for (var i = 0; i < rented.length; i++) {
            let movie = rented[i];
            let id = movie.movieId;
            moviesDiv.innerHTML += `<div id='movie'><h3>${movie.movieName}</h3><p>Film Id: ` + id + `</p>
            
            </div>`
        }
        moviesDiv.innerHTML += `<div id='returnDiv'><h3>Returnera</h3>
            <form id="returnForm">
                <div class="form-group">
                    <label for="MovieId">Film Id</label><br />
                    <input type="text" class="form-control" id="movieId">
                </div>
                <div>
                    <br /><button type="submit" class="btn btn-dark" id="return">Återlämna</button>
                </div>
            </form></div>`;

        let returnForm = document.getElementById('returnForm');

        returnForm.addEventListener('submit', async (e) => {
            e.preventDefault();

            let id = document.getElementById('movieId').value;

            let response = await fetch('/api/v2/FilmCopies/' + id + '/return', {
                method: 'PUT',
                headers: {
                    'Content-type': 'application/json; charset=UTF-8',
                    'Authorization': `Bearer ${app.globaltoken}`
                }
            })

            let result = await response.json();
            showReturned(result);
        });
    }
}

async function showReturned(result) {
    app.rentMsg.innerHTML = `<h4 class="message">${result.movieName} är nu returnerad.</h4>`;

    getRentedMovies();
}



async function showMovies(movies) {
    if (app.globaltoken === "") {
        app.loginMsg.innerHTML = "";
    }

    app.content.innerHTML = `<div id="moviesDiv"></div>`;
    let moviesDiv = document.getElementById('moviesDiv');

    for (var i = 0; i < movies.length; i++) {

        let movie = movies[i];
        if (app.globaltoken !== "") {
            if (movie.availablefCopies === 0) {
                moviesDiv.innerHTML += `<div id='movie2'><h3>Tyvärr finns det inga tillgängliga upplagor</h3><h4>Film Id: ${movie.movieId}</h4><h4>${movie.title}</h4> 
                <p>Regissör:   ${movie.director}</p>
                <p>Årtal:     ${movie.releaseYear}</p>
                <p>Land:       ${movie.country}</p>
                </div>`
            }
            else {
                moviesDiv.innerHTML += `<div id='movie'><h3>Film Id: ${movie.movieId}</h3><h3>${movie.title}</h3> 
                <p>Regissör:   ${movie.director}</p>
                <p>Årtal:     ${movie.releaseYear}</p>
                <p>Land:       ${movie.country}</p>
                <p>Upplagor tillgängliga: ${movie.availablefCopies}</p></div>`
            }
        }
        else {
            moviesDiv.innerHTML += `<div id='movie'><h3>${movie.title}</h3> 
            <p>Regissör:   ${movie.director}</p>
            <p>Årtal:     ${movie.releaseYear}</p>
            <p>Land:       ${movie.country}</p></div>`
        }
    }

    if (app.globaltoken !== "") {
        moviesDiv.innerHTML += `<div id='movie'><h2>Låna</h2>
            <form id="rentForm">
                <div class="form-group">
                    <label for="MovieId">Film Id</label><br />
                    <input type="text" class="form-control" id="movieId">
                </div>
                <div>
                    <br /><button type="submit" class="btn btn-dark" id="rent">Låna</button>
                </div>
            </form></div>`;

        let rentForm = document.getElementById('rentForm');

        rentForm.addEventListener('submit', async (e) => {
            e.preventDefault();

            let id = document.getElementById('movieId').value;

            let response = await fetch('/api/v2/FilmCopies/' + id + '/rent', {
                method: 'PUT',
                headers: {
                    'Content-type': 'application/json; charset=UTF-8',
                    'Authorization': `Bearer ${app.globaltoken}`
                }
            })
            let result = await response.json();
            showRented(result);
        });
    }
}

async function getUpdatedMovies() {
    fetch('/api/v2/movies', {
        method: 'GET',
        headers: {
            'Content-type': 'application/json; charset=UTF-8',
            'Authorization': `Bearer ${app.globaltoken}`
        }
    })
        .then(resp => resp.json())
        .then(movies => showMovies(movies))
}














