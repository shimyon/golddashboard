function getGoogleAccessToken() {
    debugger
    if (location.hash) {
        if (location.hash.split('access_token=')) {
            var accessToken = location.hash.split('access_token=')[1].split('&')[0];
            if (accessToken) {
                isUserRegistered(accessToken);
            }
        }
    }

}

function isUserRegistered() {
    $.ajax({
        url: "/Dashboard/Index",
        method: 'Get',
        headers: {
            'content-type': 'application/JSON',
            'Authorization':'Bearer'+accessToken
        },
        success: function (responce) {
            if (responce.HasRegistered) {
                localStorage.setItem('accessToken', accessToken);
                localStorage.setItem('userName', responce.Email);
                window.location.href = "https://localhost:44310/Dashboard/Index/"
            } else {
                signupExternalUser(accessToken);
            }
        }
    });
}

function signupExternalUser(accessToken) {
    $.ajax({
        url: "/Dashboard/Index",
        method: 'Get',
        headers: {
            'content-type': 'application/JSON',
            'Authorization': 'Bearer' + accessToken
        },
        success: function (responce) {
            if (responce.HasRegistered) {
                localStorage.setItem('accessToken', accessToken);
                localStorage.setItem('userName', responce.Email);
                window.location.href = "data.html"
            } else {
                signupExternalUser(accessToken);
            }
        }
    });
}