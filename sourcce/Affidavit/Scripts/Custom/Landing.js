$(document).ready(function () {
    var dat;
    /*Cada que se cargue el modal se limpia el valor del textarea con los motivos*/
    $('#disagreeReasonModal').on("show.bs.modal", function () {
        removeErrorClasses();
        $('#disagreeReasonText').val('');
    });


    $('#disagreeReasonText').blur(function () {
        validateInput();
    });


    /*Envío del formulario*/
    $('#sendDesagreeReason').click(function () {
        var data = {
            DisagreeReason: dat,
            LeadId: $('#LeadId').val()
        };

        if (validateInput()) {
            loadingDiv.show();
            $('#sendDesagreeReason').attr('disabled', 'disabled');
            $.ajax({
                url: 'DisagreeReason',
                data: data,
                type: 'POST'
            })
            .done(function (message) {
                loadingDiv.hide();
                    toastMessage.show(message, "alert-success");
                    setTimeout(function () {
                        /*Se redirecciona a la página principal*/
                        window.location = '/';
                    }, 3000);
                   

                //$('#disagreeReasonErrorServer').text(message.errorMessage);
                //$('#disagreeReasonErrorServer').removeClass('hide');

                $('#sendDesagreeReason').removeAttr('disabled', 'disabled');

            })
            .fail(function (message) {
                loadingDiv.hide();
                console.log(message);
                $('#sendDesagreeReason').removeAttr('disabled', 'disabled');
            });
        }
    });

    function removeErrorClasses() {
        $('#disagreeReasonErrorServer').addClass('hide');
        $('#disagreeReasonText').parent().removeClass('has-error');
        $('#disagreeReasonErrorLabel').addClass('hide');
    }

    function validateInput() {
        dat = $('#disagreeReasonText').val().trim();
        if (dat.length == 0) {
            $('#disagreeReasonText').parent().addClass('has-error');
            $('#disagreeReasonErrorLabel').removeClass('hide');
            return false;
        } else {
            removeErrorClasses();
        }
        return true;
    }
});
