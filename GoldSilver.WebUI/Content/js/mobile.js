
//Instagram widget
jQuery(function ($) {

    var tok = '3043155184.2f7669c.6813de387fa448bdb738d5d371ffa44c';
    userid = '3043155184';
    cnt = 18;

    $.ajax({
        url: 'https://api.instagram.com/v1/users/3043155184/media/recent',
        dataType: 'jsonp',
        type: 'GET',
        data: { access_token: tok, count: cnt },
        success: function (result) {
            for (x in result.data) {

                $('#instagram-block').append('<div class="col-md-2 col-sm-4 col-xs-6 p5">' +
                    '<div class="instra-image-block">' +
                    '<div class="image-wrap-instagram">' +
                    '<div class="instra-item-descr-img">' +
                    '<span class="glyphicon glyphicon-camera"></span>' +
                    '</div>' +
                    '</div>' +
                    '<a href="' + result.data[x].link + '">' +
                    '<img src="' + result.data[x].images.low_resolution.url + '" alt="" class="img-responsive" width="180" height="180" >' +
                    '</a>' +
                    '</div>' +
                    '</div>');
            }


        },
        error: function (result) {
            console.log(result);
        }
    });
});

// Load map
function initMap() {
    var location = { lat: 49.803213, lng: 23.992841 };
    var map = new google.maps.Map(document.getElementById('mapcanvas'), {
        zoom: 17,
        center: location
    });
    var marker = new google.maps.Marker({
        position: location,
        map: map
    });
}

$('#myModal').on('shown.bs.modal', function () {
    $('#myInput').focus();
    initMap();
});

