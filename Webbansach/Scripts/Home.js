function openNav() {
  document.getElementById("myNav").style.width = "250px";
};

function closeNav() {
  document.getElementById("myNav").style.width = "0%";
};
$(document).ready(function() {
  

  $(".owl-carousel").owlCarousel({
  
  items : 6,
  center: true,
  navigation : true,
  navigationText : ["<i class='fas fa-chevron-left'></i>","<i class='fas fa-chevron-right'></i>"],
  loop:true,
      responsive: {
      600: {
          items: 2
          },
      400: {
        items: 2
          },
      200: {
        items: 2
      },
      }

  });
  $(".owl-carousel-1").owlCarousel({
  
    items : 5,
    autoPlay: 2500,
    center: true,
    loop:true,
        responsive: {
          600: {
            items: 1
            },
        400: {
          items: 1
            },
        200: {
          items: 1
        },
        }
  
    });
});
