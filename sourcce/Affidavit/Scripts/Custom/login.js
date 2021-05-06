$(document).ready(function () {
    $('#loginFrm').submit(function () {
        if($('#loginFrm').valid())
            loadingDiv.show();
    });

    $('#loginFrmAdmin').submit(function () {
        if ($('#loginFrmAdmin').valid())
            loadingDiv.show();
    });
});