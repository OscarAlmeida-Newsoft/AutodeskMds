
$(document).ready(function () {
    GridCallback();
});


function GridCallback() {
    loadingDiv.show();

    $.ajax({
        url: $('#NS_GridUrlActionGridCallBack').text(),
        type: 'POST',
        contentType: 'application/json; charset=utf-8'
    })
    .done(function (data) {
        $('#grid-div').html(data);
    })
    .fail(function (jqXHR, exception) {
        toastMessage.show(exception, "alert-success");
    })
    .always(function () {
        loadingDiv.hide();
    });
}