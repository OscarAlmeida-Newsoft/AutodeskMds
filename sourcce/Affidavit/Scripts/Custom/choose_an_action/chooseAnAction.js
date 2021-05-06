$(document).on('click', '#modalConfirm .cerrar', function () {
    $("#opacidad").hide();
    $("#modalConfirm").hide();
});


////Cancel Modal
//$(document).on('click', '#SendAssessmentFinalOk', function () {
//    var showModalNextTime = $('#showModalNextTime').prop('checked');
//    window.location.href = gGetUrlAction.MeasureMyPlataform + "?MyPlatformModal=" + showModalNextTime;
//});
function redirectToSAMLive() {
    var url = gGetUrlAction.MeasureMyPlataform;

    if (gGetUrlAction.IsSAM360On == "True") {
        gAnalytics.throwRedirectEvent('Click Continue', 'SAM360', url);
    }
    else
    {
        gAnalytics.throwRedirectEvent('Click Continue', 'SAM Live', url);
    }
    
    
}

$(document).on('click', '#SendAssessmentFinalOk', function () {
    debugger;
    var showModalNextTime = $('#showModalNextTime').prop('checked');
    var url = gGetUrlAction.MeasureMyPlataform + "?MyPlatformModal=" + showModalNextTime;

    if (gGetUrlAction.IsSAM360On == "True")
    {
        gAnalytics.throwRedirectEvent('Click Continue', 'SAM360', url);
    }
    else
    {
        gAnalytics.throwRedirectEvent('Click Continue', 'SAM Live', url);
    }
    
});


//$(document).on('click', '#SendAssessmentFinalOk', function () {
//    loadingDiv.show();
//    $('#pQuestionAnswersModelIsFinal').val(true);
//    gAnalytics.throwFormEvent('Click Continue', 'SAM Live', $('#AssessmentQuestionsForm'));
//    //$('#AssessmentQuestionsForm').submit();
//});

$(document).on('click', '#modalConfirm .cancel-button', function () {
    $("#opacidad").hide();
    $("#modalConfirm").hide();
}); 

$(document).on('click', '#measureMyPlatformAction', function () {
    $("#header").css('position', 'unset');
    $("#opacidadMeasure").show();
    $("#modalMeasure").show();
});

$(document).on('click',"#modalMeasure .cerrar",function () {
    $("#header").css('position', 'relative');
    $("#opacidadMeasure").hide();
    $("#modalMeasure").hide();
});




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