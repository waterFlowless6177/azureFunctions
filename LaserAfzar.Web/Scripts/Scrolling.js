$(function () {

    //$(window).on("load resize", function () {
    //    $(".welcome-screen").css("height", window.innerHeight);
    //});

    //add bootstrap's scrollspy
    $('body').scrollspy({
        target: '.navbar',
        offset: 61
    });

    // smooth scrolling
    $('nav a, .welcome-screen-start a').bind('click', function () {
        $('html, body').stop().animate({
            scrollTop: ($($(this).attr('href')).offset().top - 59)
        }, 1500, 'easeInOutExpo');
        event.preventDefault();
    });

    //nanogallery
    //$(document).ready(function () {
    //    $(".nanoGallery").nanoGallery({
    //        //itemsBaseURL: 'http://brisbois.fr/nanogallery/demonstration/'
    //        thumbnailWidth: 'auto',
    //        thumbnailHeight: 300,

    //        colorScheme: 'none',
    //        thumbnailHoverEffect: [{ name: 'labelAppear75', duration: 300 }],
    //        thumbnailGutterWidth: 0,
    //        thumbnailGutterHeight: 0,
    //        i18n: { thumbnailImageDescription: 'نمایش تصویر' },
    //        thumbnailLabel: { display: true, position: 'overImageOnMiddle', align: 'center' }

    //    });
    //});
    
    // initialize WOW for element animation
    new WOW({ offset: 0 }).init()

    // parallax scrolling with stellar.js
    $(window).stellar();
});

var myNanoGalleryFunc = function () {
    //element.css('color', 'red');

    //selects all div element wid id starts with "nanoGallery"
    $("div[id^='nanoGallery']").nanoGallery({
        //itemsBaseURL: 'http://brisbois.fr/nanogallery/demonstration/'
        thumbnailWidth: 'auto',
        thumbnailHeight: 300,

        colorScheme: 'none',
        thumbnailHoverEffect: [{ name: 'labelAppear75', duration: 300 }],
        thumbnailGutterWidth: 0,
        thumbnailGutterHeight: 0,
        i18n: { thumbnailImageDescription: 'نمایش تصویر' },
        thumbnailLabel: { display: true, position: 'overImageOnMiddle', align: 'center' }

    });
};