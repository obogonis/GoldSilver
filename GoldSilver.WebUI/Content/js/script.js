
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

                if (result.data[x].type == 'image') {
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
                    // .append('<li><img src="' + result.data[x].images.low_resolution.url + '"></li>');
                    // result.data[x].images.thumbnail.url - URL картинки 150х150
                    // result.data[x].images.standard_resolution.url - URL картинки 612х612
                    // result.data[x].link - URL страницы данного поста в Инстаграм
                }

                if (result.data[x].type == 'video') {
                    $('#instagram-block').append('<div class="col-md-2 col-sm-4 col-xs-6 p5 hover-block">' +
                        '<div class="instra-image-block">' +
                        '<div class="image-wrap-instagram">' +
                        '<div class="instra-item-descr-video">' +
                        '<span class="glyphicon glyphicon-facetime-video"></span>' +
                        '</div>' +
                        '</div>' +
                        '<a href="' + result.data[x].link + '">' +
                        //'<img src="' + result.data[x].images.low_resolution.url + '" alt="" class="img-responsive">' +
                        '<video width="180" height="180" class="hover-play">' +
                        '<source src="' + result.data[x].videos.low_resolution.url + '"' +
                        'type="video/mp4"/>' +
                        '</video>' +

                        '</a>' +
                        '</div>' +
                        '</div>');

                }
            }


        },
        error: function (result) {
            console.log(result); // пишем в консоль об ошибках
        }
    });
});


$(document).ready(function () {

    $(".player").mb_YTPlayer();

    //$(".image-block").hover(function () {
    //    $(this).find(".image-wrap").animate({ width: "100%", opacity: 1 }, 500);
    //}, function () {
    //    $(this).find(".image-wrap").animate({ width: "0", opacity: 0 }, 500);
    //});

    //$(".hover-play").hover(function () {
    //    $(this).find(".instra-item-descr-video").animate({ opacity: 0 }, 500);
    //}, function () {
    //    $(this).find(".instra-item-descr-video").animate({ opacity: 0 }, 500);
    //});

    //$('.hover-play').mouseover(function () {
    //    $(this).find(".instra-item-descr-video").animate({ opacity: 0 }, 500);
    //});
});

$(document).ready(function () {

    $("#wrapper_mbYTP_bgndVideo").animate({ opacity: ".8" }, 4000);

});


var xSwitch = $('#container').XSwitch({

    // CSS selectors
    selectors: {
        sections: '.sections',
        section: '.section',
        page: '.pages',
        active: '.active'
    },

    // starting section
    index: 0,

    // jQuery easing function
    easing: 'ease',

    // animation speed
    duration: 1000,

    // infinite looping
    loop: false,

    // enable side navigation
    pagination: true,

    // enable keyboard navigation
    keyboard: true,

    // 'vertical' or 'horizontal'
    direction: 'vertical',

    // callback function
    callback: ''

});

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


/* play video when mouse hover on video elem. */
$('#instagram-block').on('mouseover', '.hover-play', hoverVideo);

$('#instagram-block').on('mouseout', '.hover-play', hideVideo);

function hoverVideo(e) {
    this.play();
}

function hideVideo(e) {
    this.pause();
}

function showGoogleMaps() {

    var latLng = new google.maps.LatLng(49.8034911, 23.9928643);
    var centerPossition = new google.maps.LatLng(49.8039911, 23.9928643);

    var mapOptions = {
        zoom: 17, // initialize zoom level - the max value is 21
        streetViewControl: false, // hide the yellow Street View pegman
        scaleControl: true, // allow users to zoom the Google Map
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        center: centerPossition
    };

    map = new google.maps.Map(document.getElementById('google-maps'),
        mapOptions);

    // Show the default red marker at the location
    marker = new google.maps.Marker({
        position: latLng,
        map: map,
        draggable: false,
        animation: google.maps.Animation.DROP
    });
}

