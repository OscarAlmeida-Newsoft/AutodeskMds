$(document).on('click', '#SendAssessmentDraft', function () {
    loadingDiv.show();
    $('#pQuestionAnswersModelIsFinal').val(false);
    gAnalytics.throwFormEvent('Click Save Draft', 'Assestments', $(this).data().eventLabel, $('#AssessmentQuestionsForm'));
    //$('#AssessmentQuestionsForm').submit();

    return false;

});

$(document).on('click', '#SendAssessmentFinal', function () {
    debugger;
    var auxThis = $(this);
    var answerQuestions = $('input[type=radio]:checked').length;

    if (answerQuestions != totalQuestions) {
        //$("#opacidad").show();
        //$("#modalNotCompleted").show();
        semaphore.showError(
            $("#TH_Assessments_Questions_Modal_Error_Title").text(),
            $("#TH_Assessments_Questions_Modal_Error_Body").text());
       

    } else {
        //$("#opacidad").show();
        //$("#modalConfirm").show();
        semaphore.showAtention(
            $("#TH_Assessments_Questions_Modal_Atention_Title").text(),
            $("#TH_Assessments_Questions_Modal_Atention_Body").text(),
            function () {
                loadingDiv.show();
                $('#pQuestionAnswersModelIsFinal').val(true);
                gAnalytics.throwFormEvent('Click End', 'Assestments', auxThis.data().eventLabel, $('#AssessmentQuestionsForm'));
            }
        );
    }
    
    
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