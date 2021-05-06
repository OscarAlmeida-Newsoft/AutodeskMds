$(document).on("click", ".EditButton", function (e) {
    //debugger;
    var _currentIdQuestion = $(this).data("idquestion");
    var _productFamilyId = $(this).data("productfamilyid");
    //$('#IdQuestion').val(_currentIdQuestion);
    console.log(_currentIdQuestion);
    $.ajax({
        url: '@Url.Action("EditQuestion")' + '?pQuestionID=' + _currentIdQuestion + '&&pProductFamilyID=' + _productFamilyId,
        //data: JSON.stringify(AnswerQuestionsWindows),
        type: 'GET',
        contentType: 'application/json; charset=utf-8'
    })
        .done(function (data) {
            $('#progressbargrid').addClass('hide');
            $('#EditQuestionPartial').html(data);
            $('#WindowsModal').modal({ backdrop: "static" }, { keyboard: false });
        })
        .fail(function (err) {
            //$('#progressbargrid').addClass('hide');
            console.log(err);

        });

});

$(document).on("click", "#CreateButton", function (e) {
    //debugger;
    //var _currentIdQuestion = $(this).data("idquestion");
    //$('#IdQuestion').val(_currentIdQuestion);
    //console.log(_currentIdQuestion);

    $.ajax({
        url: '@Url.Action("CreateNewQuestion")',
        //data: JSON.stringify(AnswerQuestionsWindows),
        type: 'GET',
        contentType: 'application/json; charset=utf-8'
    })
        .done(function (data) {
            //$('#progressbargrid').addClass('hide');
            $('#CreateQuestionPartial').html(data);
            $('#exampleModal').modal({ backdrop: "static" }, { keyboard: false });

        })
        .fail(function (err) {
            //$('#progressbargrid').addClass('hide');
            console.log(err);

        });

});

$(document).on("click", ".DeleteButton", function (e) {
    var _idQuestion = $(this).data("idquestiondelete");
    console.log(_idQuestion);
    bootbox.confirm("Are you sure you want to delete this question?", function (result) {

        if (result == true) {

            $.ajax({
                url: '@Url.Action("DeleteQuestion")' + '?pQuestionID=' + _idQuestion,
                data: { pQuestioID: _idQuestion },
                type: 'GET'
                //contentType: 'application/json; charset=utf-8'
            })
            .done(function (data) {
                bootbox.alert("Question successfully deleted").hide(2000);
            })
            .fail(function (err) {
                //$('#progressbargrid').addClass('hide');
                console.log(err);

            });
        }
    });
});

$(document).on("click", ".AssiNewProduct", function (e) {
    var _idQuestion = $(this).data("idquestion");

    console.log(_currentIdQuestion);
    $.ajax({
        url: '@Url.Action("AssiNewProductToQuestion")' + '?pQuestionID=' + _idQuestion,
        //data: JSON.stringify(AnswerQuestionsWindows),
        type: 'GET',
        contentType: 'application/json; charset=utf-8'
    })
        .done(function (data) {
            $('#progressbargrid').addClass('hide');
            $('#AssignNewProductPartial').html(data);
            $('#ProductsModal').modal({ backdrop: "static" }, { keyboard: false });
        })
        .fail(function (err) {
            //$('#progressbargrid').addClass('hide');
            console.log(err);

        });
});

$('#WindowsModal').on('hidden.bs.modal', function (e) {
    $('#EditQuestionPartial').empty();
});

$('#exampleModal').on('hidden.bs.modal', function (e) {
    $('#CreateQuestionPartial').empty();
});