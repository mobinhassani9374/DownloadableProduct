$(document).ready(function () {
  $('[data-toggle="tooltip"]').tooltip();

  $('.product__item__price > span').each(function (index, item) {
    $(item).html(numberWithCommas(Number($(item).html())))
  })

  $('.slick__item').slick({
    infinite: false,
    slidesToShow: 5,
    slidesToScroll: 1,
    arrows: false,
    responsive: [
      {
        breakpoint: 900,
        settings: {
          slidesToShow: 4
        }
      },
      {
        breakpoint: 768,
        settings: {
          slidesToShow: 3
        }
      },
      {
        breakpoint: 480,
        settings: {
          slidesToShow: 2
        }
      }
    ]
  });

  $('[data-role="slick-left"]').on('click', function () {
    $('.slick__item').slick('slickPrev');
  })
  $('[data-role="slick-right"]').on('click', function () {
    $('.slick__item').slick('slickNext');
  })
});

function numberWithCommas(x) {
  return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}