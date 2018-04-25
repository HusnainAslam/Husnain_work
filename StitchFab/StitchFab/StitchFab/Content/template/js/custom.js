
/*=============================================================
    Authour URI: www.binarytheme.com
    License: Commons Attribution 3.0

    http://creativecommons.org/licenses/by/3.0/

    100% Free To use For Personal And Commercial Use.
    IN EXCHANGE JUST GIVE US CREDITS AND TELL YOUR FRIENDS ABOUT US
   
    ========================================================  */

(function ($) {
    "use strict";
    var mainApp = {
        slide_fun: function () {
            $('#carousel-div').carousel({
                interval: 4000 //TIME IN MILLI SECONDS
            });

        },
        wow_fun: function () {

            new WOW().init();

        },
        gallery_fun: function () {
            /*====================================
    FOR IMAGE/GALLERY POPUP
    ======================================*/
            $("a.preview").prettyPhoto({
                social_tools: false
            });
            /*====================================
          FOR IMAGE/GALLERY FILTER
          ======================================*/

            // MixItUp plugin
            // http://mixitup.io

            $('#port-folio').mixitup({
                targetSelector: '.portfolio-item',
                filterSelector: '.filter',
                effects: ['fade'],
                easing: 'snap',


            });
        },
       
        custom_fun:function()
        {
            
            /*====================================
             WRITE YOUR   SCRIPTS  BELOW
            ======================================*/




        },
       

    }
   
   
    $(document).ready(function () {
        mainApp.slide_fun();
        mainApp.wow_fun();
        mainApp.gallery_fun();
        mainApp.custom_fun();
       
    });
}(jQuery));

//CLIENTS SECTION SCRIPTS
$(window).load(function () {
$('.flexslider').flexslider({
    animation: "slide",
    animationLoop: false,
    itemWidth: 200,
    itemMargin: 15,
    pausePlay: false,
    start: function (slider) {
        $('body').removeClass('loading');
    }
});
});

$('#cnic').keydown(function(){

    //allow  backspace, tab, ctrl+A, escape, carriage return
    if (event.keyCode == 8 || event.keyCode == 9 
                      || event.keyCode == 27 || event.keyCode == 13 
                      || (event.keyCode == 65 && event.ctrlKey === true) )
        return;
    if((event.keyCode < 48 || event.keyCode > 57))
        event.preventDefault();

    var length = $(this).val().length; 
              
    if(length == 5 || length == 13)
        $(this).val($(this).val()+'-');

});

