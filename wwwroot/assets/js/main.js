$(function() {
    
    "use strict";
    
    //===== Prealoder
    
    $(window).on('load', function(event) {
        $('#preloader').delay(500).fadeOut(500);
    });
    
    
    //===== Sticky
    
    $(window).on('scroll', function(event) {    
        var scroll = $(window).scrollTop();
        if (scroll < 110) {
            $(".header_navbar").removeClass("sticky");
            $(".navbar_transparent img").attr("src", "assets/images/logo-white.png");
        } else{
            $(".header_navbar").addClass("sticky");
            $(".navbar_transparent img").attr("src", "assets/images/logo.png");
        }
    });

    
    
    //===== Mobile Menu
    
    $(".navbar-toggler").on('click', function() {
        $(this).toggleClass("active");
    });
    
    var subMenu = $('.sub-menu-bar .navbar-nav .sub-menu');
    
    if(subMenu.length) {
        subMenu.parent('li').children('a').append(function () {
            return '<button class="sub-nav-toggler"> <span></span> </button>';
        });
        
        var subMenuToggler = $('.sub-menu-bar .navbar-nav .sub-nav-toggler');
        
        subMenuToggler.on('click', function() {
            $(this).parent().parent().children('.sub-menu').slideToggle();
            return false
        });
        
    }
    
    
    //===== Slick Slider

    function mainSlider() {
        var BasicSlider = $('.slider-active');
        var BasicSlider2 = $('.slider-active_3');
        
        BasicSlider.on('init', function (e, slick) {
            var $firstAnimatingElements = $('.single_slider:first-child').find('[data-animation]');
            doAnimations($firstAnimatingElements);
        });
        BasicSlider2.on('init', function (e, slick) {
            var $firstAnimatingElements = $('.single_slider_3:first-child').find('[data-animation]');
            doAnimations($firstAnimatingElements);
        });
        
        BasicSlider.on('beforeChange', function (e, slick, currentSlide, nextSlide) {
            var $animatingElements = $('.single_slider[data-slick-index="' + nextSlide + '"]').find('[data-animation]');
            doAnimations($animatingElements);
        });
        BasicSlider2.on('beforeChange', function (e, slick, currentSlide, nextSlide) {
            var $animatingElements = $('.single_slider_3[data-slick-index="' + nextSlide + '"]').find('[data-animation]');
            doAnimations($animatingElements);
        });
        
        BasicSlider.slick({
            autoplay: true,
            autoplaySpeed: 13000,
            dots: true,
            fade: true,
            arrows: true,
            pauseOnHover: false,
            prevArrow: '<span class="prev"><i class="fa fa-angle-left"></i></span>',
            nextArrow: '<span class="next"><i class="fa fa-angle-right"></i></span>',
            responsive: [
                {
                    breakpoint: 767,
                    settings: {
                        arrows: false
                    }
                }
            ]
        });
        BasicSlider2.slick({
            autoplay: true,
            autoplaySpeed: 8000,
            dots: false,
            fade: true,
            arrows: true,
            pauseOnHover: false,
            prevArrow: '<span class="prev"><i class="fa fa-angle-left"></i></span>',
            nextArrow: '<span class="next"><i class="fa fa-angle-right"></i></span>',
            responsive: [
                {
                    breakpoint: 767,
                    settings: {
                        arrows: false
                    }
                }
            ]
        });
        

        function doAnimations(elements) {
            var animationEndEvents = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';
            elements.each(function () {
                var $this = $(this);
                var $animationDelay = $this.data('delay');
                var $animationType = 'animated ' + $this.data('animation');
                $this.css({
                    'animation-delay': $animationDelay,
                    '-webkit-animation-delay': $animationDelay
                });
                $this.addClass($animationType).one(animationEndEvents, function () {
                    $this.removeClass($animationType);
                });
            });
        }
    }
    mainSlider();
    
    
    //====== Magnific Popup
    
    $('.video-popup').magnificPopup({
        type: 'iframe'
        // other options
    });
    
    
    //===== Magnific Popup
    
    $('.image-popup').magnificPopup({
      type: 'image',
      gallery:{
        enabled:true
      }
    });
    
    
    //===== Counter
    
    $('.counter').counterUp({
        delay: 10,
        time: 2000,
    });
    
    
    ///===== slick testimonial
    
    $('.testimonial_content_wrapper').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: false,
        dots: true,
        fade: true,
        speed: 800,
        asNavFor: '.testimonial_image_wrapper',
    });

    $('.testimonial_image_wrapper').slick({
        slidesToShow: 3,
        slidesToScroll: 1,
        asNavFor: '.testimonial_content_wrapper',
        arrows: false,
        dots: false,
        speed: 800,
        focusOnSelect: true,
        responsive: [
        {
          breakpoint: 1200,
          settings: {
            slidesToShow: 3,
          }
        },
        {
          breakpoint: 992,
          settings: {
            slidesToShow: 1,
          }
        },
        {
          breakpoint: 768,
          settings: {
            slidesToShow: 1,
          }
        },
        {
          breakpoint: 576,
          settings: {
            slidesToShow: 1,
          }
        }
      ]
    });
    
    
    ///===== slick testimonial

    $('.causes_active').slick({
        slidesToShow: 3,
        slidesToScroll: 2,
        arrows: false,
        dots: true,
        speed: 800,
        focusOnSelect: true,
        responsive: [
        {
          breakpoint: 1200,
          settings: {
            slidesToShow: 3,
          }
        },
        {
          breakpoint: 992,
          settings: {
            slidesToShow: 2,
          }
        },
        {
          breakpoint: 768,
          settings: {
            slidesToShow: 1,
          }
        },
        {
          breakpoint: 576,
          settings: {
            slidesToShow: 1,
          }
        }
      ]
    });
    
    
    ///===== slick testimonial

    $('.volunteer_slider').slick({
        slidesToShow: 2,
        slidesToScroll: 1,
        arrows: false,
        dots: true,
        speed: 800,
        focusOnSelect: true,
        responsive: [
        {
          breakpoint: 1200,
          settings: {
            slidesToShow: 2,
          }
        },
        {
          breakpoint: 992,
          settings: {
            slidesToShow: 2,
          }
        },
        {
          breakpoint: 768,
          settings: {
            slidesToShow: 2,
          }
        },
        {
          breakpoint: 576,
          settings: {
            slidesToShow: 1,
          }
        }
      ]
    });
    
    
    ///===== slick testimonial 2
    
    $('.testimonial_content_wrapper_2').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: false,
        dots: true,
        fade: true,
        speed: 800,
        asNavFor: '.testimonial_image_wrapper_2',
    });

    $('.testimonial_image_wrapper_2').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        asNavFor: '.testimonial_content_wrapper_2',
        arrows: false,
        dots: false,
        speed: 800,
        focusOnSelect: true,
    });
    
    
    ///===== slick testimonial 3
    
    $('.testimonial_area_active_3').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: false,
        dots: true,
        speed: 800,
    });
    
    
    ///===== slick Event
    
    $('.event_active').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: false,
        dots: true,
        speed: 800,
    });
    
    
    ///===== Progress Bar

    if ($('.progress_line').length) {
        $('.progress_line').appear(function () {
            var el = $(this);
            var percent = el.data('width');
            $(el).css('width', percent + '%');
        }, {
            accY: 0
        });
    }
    
    
    // Go to Top
    
    // Scroll Event
    $(window).on('scroll', function () {
        var scrolled = $(window).scrollTop();
        if (scrolled > 300) $('.go-top').addClass('active');
        if (scrolled < 300) $('.go-top').removeClass('active');
    });

    // Click Event
    $('.go-top').on('click', function () {
        $("html, body").animate({
            scrollTop: "0"
        }, 1200);
    });
      // Click Event
    $('.register').on('click', function () {
        Swal.fire({
            title: "<strong>Coming Soon!</strong>",
            icon: "info",
            html: `
    PABS Conference registration link will be <b>opened soon!</b>,

  `,
            showCloseButton: false,
            showCancelButton: false,
            focusConfirm: false,
            confirmButtonText: `
    <i class="fa fa-thumbs-up"></i> Great!
  `,
            confirmButtonAriaLabel: "Thumbs up, great!", confirmButtonColor: "#ff2744",
           
        });
    });
    
    
    //=====  WOW active
    
    var wow = new WOW({
        boxClass: 'wow', //
        mobile: false, // 
    })
    wow.init();
    
    
    //===== 
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
});

var countDownDate = new Date("Oct 31, 2025 8:00:00").getTime();

// Update the count down every 1 second
var x = setInterval(function () {

    // Get today's date and time
    var now = new Date().getTime();

    // Find the distance between now and the count down date
    var distance = countDownDate - now;

    // Time calculations for days, hours, minutes and seconds
    var days = Math.floor(distance / (1000 * 60 * 60 * 24));
    var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
    var seconds = Math.floor((distance % (1000 * 60)) / 1000);

    // Output the result in an element with id="demo"
    document.getElementById("demo").innerHTML = days + "d " + hours + "h "
        + minutes + "m " + seconds + "s ";

    // If the count down is over, write some text 
    if (distance < 0) {
        clearInterval(x);
        document.getElementById("demo").innerHTML = "EXPIRED";
    }
}, 1000);



document.getElementById('register_members').addEventListener('submit', function (e) {
    e.preventDefault(); // Prevent the default form submission

    // Show loading spinner
    document.getElementById('loading').style.display = 'block';

    // Create a FormData object from the form
    const formData = new FormData(this);
    const url =
        // Submit form data via fetch
        //fetch("https://localhost:44349/PesaPal/Upload", { // Replace with your controller name
        fetch("https://www.panafricanburns.com/PesaPal/Upload", { // Replace with your controller name
            method: 'POST',
            body: formData
        })
            .then(response => response.json())
            .then(data => {
                // Hide loading spinner
                document.getElementById('loading').style.display = 'none';

                if (data.success) {
                    //alert(data.message); // Show success message
                    document.getElementById('register_members').reset(); // Reset the form
                    document.getElementById("paymentIframe").src = data.data;
                    $("#PayModal").modal("show");

                } else {
                    alert(data.message); // Show error message
                    if (data.errors) {
                        console.log(data.errors); // Log validation errors
                    }
                }
            })
            .catch(error => {
                // Hide loading spinner
                document.getElementById('loading').style.display = 'none';
                alert("An error occurred while submitting the form.");
            });
});