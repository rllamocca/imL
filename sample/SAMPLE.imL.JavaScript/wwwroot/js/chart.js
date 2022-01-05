$(document).ready(function () {
    __READY();
})
//################################################################
__READY = function () {
    __AJAX_POST_CHARTJS('myChart'); // id canvas
}
//################################################################

__AJAX_POST_CHARTJS = function (_idelement) {

    $.ajax({
        type: 'post',
        dataType: 'json',
        contentType: 'application/json',
        url: '/Chart/Post_FB',
        success: function (_response) {

            //console.log(_response);
            let _canvas = document.getElementById(_idelement);
            new Chart(_canvas, _response);

        }
    });

}