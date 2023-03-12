function initializeGoogleMaps() {
    var latlng = new google.maps.LatLng(51.58436, 4.79765);
    var options = {
        zoom: 16,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map(document.getElementById("map"), options);

    // Create a marker and set its position
    new google.maps.Marker({
        position: latlng,
        map: map,
        title: "FilmFlow"
    });
}
