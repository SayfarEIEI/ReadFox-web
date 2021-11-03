$(document).ready(function () {
    $('#autoWidth,#autoWidth2,#autoWidth3').lightSlider({
        autoWidth: true,
     
        onSliderLoad: function () {
            $('#autoWidth,#autoWidth2,#autoWidth3').removeClass('cS-hidden');
        }
    });
});