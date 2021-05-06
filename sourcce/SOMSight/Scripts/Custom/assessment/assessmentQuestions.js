$(document).on('click', '#SendAssessmentDraft', function () {
    loadingDiv.show();
    $('#pQuestionAnswersModelIsFinal').val(false);
    $('#AssessmentQuestionsForm').submit();

    return false;

});

$(document).on('click', '#SendAssessmentFinal', function () {
    debugger;
    var answerQuestions = $('input[type=radio]:checked').length;

    if (answerQuestions != totalQuestions) {
        $("#opacidad").show();
        $("#modalNotCompleted").show();


    } else {
        $("#opacidad").show();
        $("#modalConfirm").show();
    }
});


$(document).on('click', '#SendAssessmentFinalOk', function () {
    loadingDiv.show();
    $('#pQuestionAnswersModelIsFinal').val(true);
    $('#AssessmentQuestionsForm').submit();
});

$(document).on('click', '#modalConfirm .cerrar', function () {
    $("#opacidad").hide();
    $("#modalConfirm").hide();
});


//Cancel Modal
$(document).on('click', '#modalConfirm .cancel-button', function () {
    $("#opacidad").hide();
    $("#modalConfirm").hide();
});

$(document).on('click', '#modalNotCompleted', function () {
    $("#opacidad").hide();
    $("#modalNotCompleted").hide();
});

$(document).on('change', 'input[type=radio]', function () {
    answeredQuestions = $('input[type=radio]:checked').length
    $('#answeredQuestions').text(answeredQuestions);
});