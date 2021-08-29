__READY = function () {
    __AJAX_POST();
}


__AJAX_POST = function () {

    $.ajax({
        type: "post",
        dataType: "json",
        contentType: "application/json",
        url: '/Chart/Post_FB',
        //data: JSON.stringify(jsonObject),
        success: function (_response) {

            console.log(_response);
            let _canvas = document.getElementById('myChart');
            new Chart(_canvas, _response);

        }
    })

}

$(document).ready(function () {
    __READY();
})