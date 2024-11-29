function init() {
    if ($('#locations').length) {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (e) {
                var data = sort(e.coords.latitude, e.coords.longitude);
                //$('#dvMap').html(data);
            }, function () {
                $.getJSON("http://ipinfo.io/json", function (e) {
                    var latLong = e.loc.split(",");
                    var data = sort(latLong[0], latLong[1]);
                    //$('#dvMap').html(data);
                });
            });
        }
    }
}

function distance(p1, p2) {
    return (google.maps.geometry.spherical.computeDistanceBetween(p1, p2) / 1000).toFixed(2);
}

function sort(lat, long) {
    var current = new google.maps.LatLng(lat, long);

    var map = document.getElementById('locations');
    var arrayMap = Array.prototype.slice.call(map.querySelectorAll('.list-see'), 0);

    arrayMap.sort(function (a, b) {
        var loc1 = a.getAttribute('data-latlon').split(',');
        var loc2 = b.getAttribute('data-latlon').split(',');

        dist1 = distance(current, new google.maps.LatLng(Number(loc1[0]), Number(loc1[1])));
        dist2 = distance(current, new google.maps.LatLng(Number(loc2[0]), Number(loc2[1])));

        return dist1 - dist2;
    });

    map.innerHTML = '';

    var data = '';
    arrayMap.forEach(function (el) {
        if (data == '')
            data = $('div.embedmap').eq($(el).data('id')).find('.data-map').html();

        var temp = el.getAttribute('data-latlon').split(',');
        var dist = distance(current, new google.maps.LatLng(Number(temp[0]), Number(temp[1])));

        el.innerHTML = el.innerHTML + '<div class="distance">Khoảng cách: ' + dist + ' km</div>';

        map.appendChild(el);
    });

    return data;
};



































