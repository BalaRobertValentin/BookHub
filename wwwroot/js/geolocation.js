const API_KEY = "459b9a5fbb47463d9a52c84faff2c6a8";

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition, showError);
    } else {
        document.getElementById("location").innerHTML = "Geolocation is not supported by this browser.";
    }
}

function showPosition(position) {
    let lat = position.coords.latitude;
    let lng = position.coords.longitude;
    fetch(`https://api.opencagedata.com/geocode/v1/json?q=${lat}+${lng}&key=${API_KEY}`)
        .then(response => response.json())
        .then(data => {
            let city = data.results[0].components.city || data.results[0].components.town || data.results[0].components.village;
            let country = data.results[0].components.country;
            document.getElementById("location").innerHTML = `City: ${city}<br>Country: ${country}`;
        })
        .catch(error => {
            document.getElementById("location").innerHTML = "Unable to retrieve city and country.";
        });
}

function showError(error) {
    switch (error.code) {
        case error.PERMISSION_DENIED:
            document.getElementById("location").innerHTML = "User denied the request for Geolocation."
            break;
        case error.POSITION_UNAVAILABLE:
            document.getElementById("location").innerHTML = "Location information is unavailable."
            break;
        case error.TIMEOUT:
            document.getElementById("location").innerHTML = "The request to get user location timed out."
            break;
        case error.UNKNOWN_ERROR:
            document.getElementById("location").innerHTML = "An unknown error occurred."
            break;
    }
}