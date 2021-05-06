var pageNumber;
var pageSize = 5;

//assign to multiple campaigns json object
var assignLanding = { landingFrom: '', landingTo: [] };

//filters
pFilter = {};

$(document).ready(function () {

    //Rich textbox initial configuration
    $('#Content').summernote({
        focus: true,
        height: 150,
        toolbar: [
        // [groupName, [list of button]]
        ['style', ['bold', 'italic', 'underline', 'clear']],
        ['font', ['strikethrough', 'superscript', 'subscript']],
        ['fontsize', ['fontsize']],
        ['color', ['color']],
        ['para', ['ul', 'ol', 'paragraph']],
        ['height', ['height']],
        ['picture', ['picture']]
        ],
        dialogsInBody: true
    });

    GridCallback();

    //Edit landing button click event
    $(document).on('click', '.edit-landing', function (ev) {
        //Modal Title
        $('#modalTitle').text($('#NS_EditLandingModalTitleAdmin').text());
        $('#Countries').val($(this).data('landingcountry'));

        $('#addModal #campaign').val($(this).data("landingcampaign"));
        $('#campaignid').text($(this).data("landingid"));
        $.getJSON(
            $('#NS_GridUrlActionGetLandingText').text() + '/?pLandingId=' + $(this).data("landingid")
            )
        .done(function (data) {
            $('#Content').summernote('code', data);
            $('#addModal').modal('show');
        })
        .fail(function (err) {
            console.log(err);
        });

    });

    //New landing button click event
    $(document).on('click', '.new-landing', function (ev) {
        //Modal Title
        $('#modalTitle').text($('#NS_NewLandingModalTitleAdmin').text());

        $('#addModal #campaign').val('');
        $('#Content').summernote('code', '');
        $('#addModal').modal('show');

    });


    //Save landing event
    $(document).on('click', '.saveLanding', function (ev) {
        
        if (!validateDataLanding()) {
            return;
        }
        //loadingDiv.show();
        var _campaignName = $('#addModal #campaign').val();
        loadingDiv.show();
        $.ajax({
            url: $('#NS_CheckCampaignNameAction').text(),
            data: { pCampaignName: _campaignName },
            type: 'GET',
            //contentType: 'application/json; charset=utf-8'
        })
         .done(function (data) {
             loadingDiv.hide();
             var _existCampaign = data.Exist;
             if (!_existCampaign) {
                 bootbox.dialog({
                     message: '<p class="text-center" style="font-size: 14px"> The campaign does not exist in CRM repository </p>',
                     closeButton: false,
                     size: "medium"
                 }).find('.modal-content').addClass("alert alert-danger");
                 setTimeout(function () { bootbox.hideAll(); }, 3000);
                 //loadingDiv.hide();
                 return false;
             } else {
                 //loadingDiv.show();
                 pModel = {};
                 pModel.LandingId = $('#campaignid').text();
                 pModel.LandingCampaign = $('#addModal #campaign').val();
                 pModel.LandingText = $('#Content').summernote('code');
                 pModel.CountryId = $('#Countries').val();

                 $.ajax({
                     url: $('#NS_GridUrlActionSaveLanding').text(),
                     type: 'POST',
                     data: JSON.stringify(pModel),
                     contentType: 'application/json; charset=utf-8'
                 })
                 .done(function (data) {
                     loadingDiv.hide();
                     
                     console.log(data.Message);
                     if (data.Success) {
                        $('#addModal').modal('hide');
                         bootbox.dialog({
                             message: '<p class="text-center" style="font-size: 14px">' + data.Message + '</p>',
                             closeButton: false,
                             size: "medium"
                         }).find('.modal-content').addClass("alert alert-success");
                         setTimeout(function () { bootbox.hideAll(); }, 3000);

                         GridCallback();
                     } else {
                         bootbox.dialog({
                             message: '<p class="text-center" style="font-size: 14px">' + data.Message + '</p>',
                             closeButton: false,
                             size: "medium"
                         }).find('.modal-content').addClass("alert alert-danger");
                         setTimeout(function () { bootbox.hideAll(); }, 3000);
                     }

                     //toastMessage.show(data, "alert-success");

                 });
                 //.fail(function (data) {
                 //    loadingDiv.hide();
                 //    console.log(err);
                 //    if (err.responseJSON.error) {
                 //        bootbox.dialog({
                 //            message: '<p class="text-center" style="font-size: 14px">' + err.responseJSON.errorMessage + '</p>',
                 //            closeButton: false,
                 //            size: "medium"
                 //        }).find('.modal-content').addClass("alert alert-danger");
                 //        setTimeout(function () { bootbox.hideAll(); }, 3000);
                 //        //toastMessage.show(err.responseJSON.errorMessage, "alert-danger");
                 //    }
                 //    //console.log(err);

                 //});
             }
             loadingDiv.hide();
         })
         .fail(function (err) {
             loadingDiv.hide();
             console.log(err);

         });

        
    });


    //Remove landing click button event
    $(document).on('click', '.remove-landing', function (ev) {
        var campaignName = $(this).data('landingcampaign');
        var campaignId =  $(this).data('landingid');
        bootbox.confirm($("#NS_DeleteRecordQuestion").text() + "<br /><br /> <b>" + $('#NS_CampaignAdmin').text() + ":</b> " + campaignName, function (result) {
            if (result) {
                loadingDiv.show();
                $.ajax({
                    url: $('#NS_GridUrlActionDeleteLanding').text() + "?pLandingId="+campaignId,
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8'
                })
               .done(function (data) {
                   loadingDiv.hide();
                   var _isExist = data.IsExist;
                   
                   if (_isExist) {
                       bootbox.dialog({
                           message: '<p class="text-center" style="font-size: 14px">' + data.Message + ', this letter is being used in an MDS</p>',
                           closeButton: false,
                           size: "medium"
                       }).find('.modal-content').addClass("alert alert-danger");
                       setTimeout(function () { bootbox.hideAll(); }, 4000);
                       //toastMessage.show(data, "alert-success");
                       
                   } else {
                       bootbox.dialog({
                           message: '<p class="text-center" style="font-size: 14px">' + data.Message + '</p>',
                           closeButton: false,
                           size: "medium"
                       }).find('.modal-content').addClass("alert alert-success");
                       setTimeout(function () { bootbox.hideAll(); }, 3000);
                   }
                   GridCallback();
               })
               .fail(function (err) {
                   loadingDiv.hide();
                   if (err.responseJSON.error) {
                       bootbox.dialog({
                           message: '<p class="text-center" style="font-size: 14px">' + err.responseJSON.errorMessage + '</p>',
                           closeButton: false,
                           size: "medium"
                       }).find('.modal-content').addClass("alert alert-danger");
                       setTimeout(function () { bootbox.hideAll(); }, 3000);
                       //toastMessage.show(err.responseJSON.errorMessage, "alert-danger");
                   }

               });
            }

        });
    });


    //Use a landing text as template for new landing
    $(document).on('click', '.use-landing', function () {
        //Modal Title
        $('#modalTitle').text($('#NS_UseLandingAsTemplateModalTitleAdmin').text());

        $('#addModal #campaign').val('');
        $('#campaignid').text('');

        $.getJSON(
            $('#NS_GridUrlActionGetLandingText').text() + '/?pLandingId=' + $(this).data("landingid")
            )
        .done(function (data) {
            $('#Content').summernote('code', data);
            $('#addModal').modal('show');
        })
        .fail(function (err) {
            console.log(err);
        });
    });

    //click on button to assign to multiple campaigns event
    $(document).on('click', '.use-landing-multiple', function () {
        assignLanding.landingFrom = $(this).data('landingid');
        $('#useMultipleModal').modal('show');
    });

    //Change the country list for list campaigns belong to this country
    $(document).on('change', '#CountriesMulti', function () {
        assignLanding.landingTo =[];
        var dat = $(this).val();
        if (dat !== "") {
            loadingDiv.show();
            $.ajax({
                url: $('#NS_GridUrlActionCampaignsToReplaceLanding').text() + "?pCountryId=" + dat + "&pLandingFrom=" + assignLanding.landingFrom,
                type: 'GET'
            })
                    .done(function (data) {
                        loadingDiv.hide();
                        $('#CampaignsToAsing').html(data);
                    })
                    .fail(function (err) {
                        loadingDiv.hide();
                        console.log(err);

                    });
        } else {
            $("#CampaignsToAsing").html("<div></div>")
        }

    });


    //Confirm the change for multiple landing
    $(document).on('click', '#changeMultipleLanding', function (e) {
        if (assignLanding.landingTo.length == 0) {

            toastMessage.show($('#NS_SelectAtleastOneCampaignToAssignMessage').text(), "alert-warning");
            return;
        }

        loadingDiv.show();
        $.ajax({
            url: $('#NS_GridUrlActionReplaceLandingToMultipleCampaing').text(),
            type: 'POST',
            data: JSON.stringify(assignLanding),
            contentType: 'application/json; charset=utf-8'
        })
                .done(function (data) {
                    $('#useMultipleModal').modal('hide');
                    loadingDiv.hide();
                    bootbox.dialog({
                        message: '<p class="text-center" style="font-size: 14px">' + $("#NS_AssignLandingToOthersSuccessfulMessage").text() + '</p>',
                        closeButton: false,
                        size: "medium"
                    }).find('.modal-content').addClass("alert alert-success");
                    setTimeout(function () { bootbox.hideAll(); }, 3000);

                    //toastMessage.show($("#NS_AssignLandingToOthersSuccessfulMessage").text(), "alert-success");
                    GridCallback();
                })
                .fail(function (err) {
                    loadingDiv.hide();
                    console.log(err);
                });
    });

    
    //Manage click of each campaing to assign other landing content
    $(document).on('click', '.campaign-replace-item', function () {
        if ($(this).find('i').hasClass('fa-square-o')) {
            assignLanding.landingTo.push($(this).data('campaingreplaceid'))
            $(this).find('i').removeClass('fa-square-o').addClass('fa-check-square-o');
        } else {
            var index = assignLanding.landingTo.indexOf($(this).data('campaingreplaceid'));

            if (index > -1) {
                assignLanding.landingTo.splice(index, 1);
            }
            $(this).find('i').removeClass('fa-check-square-o').addClass('fa-square-o');
        }
    });


    //Hide or close the assign modal event
    $('#useMultipleModal').on('hidden.bs.modal', function (ev) {
        $('#CountriesMulti').val('');
        $("#CampaignsToAsing").html("<div></div>")
        assignLanding = { landingFrom: '', landingTo: [] };
    });


    //Hide or close modal new o edit event
    $('#addModal').on('hidden.bs.modal', function (ev) {
        $('#campaignid').text('');
        $('#addModal #campaign').val('');
        $('#Countries').val('');
        $('#Content').summernote('code', '');
    });

    //Pagination
    $(document).on('click', '.NS-paginator li', function () {
        var clickedPage = $(this).data('page');
        if (clickedPage !== undefined) {
            pageNumber = clickedPage

            GridCallback();
        }
    });

    //Filter button event
    $(document).on('click', '.filter-table .btn-filter', function () {
        var value = $(this).closest('span').prev('.filter-text').val().trim();
        pFilter[$(this).data('filtername')] = value;
        pageNumber = 1;
        GridCallback();
    });

    //Filter enter event
    $(document).on('keypress', '.filter-text', function (e) {
        if (e.which == 13) {
            var filterColumn = $(this).next().find('button').data('filtername');
            pFilter[filterColumn] = $(this).val().trim();
            pageNumber = 1;

            GridCallback();
        }
    });

    //Clear Filters button
    $(document).on('click', '.btn-clear-filters', function () {
        pFilter = {};
        pageNumber = 1;

        GridCallback();
    });

});


//Perform the callback to reload all data in the datagrid
function GridCallback() {
    loadingDiv.show();
    $.ajax({
        url: $('#NS_GridUrlActionGridCallBack').text() + "?pPage=" + pageNumber + "&pPageSize=" + pageSize,
        //data: JSON.stringify(selectedValuesToFilter),
        type: 'POST',
        data: JSON.stringify(pFilter),
        contentType: 'application/json; charset=utf-8'
    })
            .done(function (data) {
                loadingDiv.hide();
                $('#grid-div').html(data);
            })
            .fail(function (err) {
                loadingDiv.hide();
                console.log(err);

            });
}

function validateDataLanding() {
    if ($('#addModal #campaign').val().trim() == "") {
        //alert($("#NS_CampaignFieldRequiredAdmin").text());
        bootbox.dialog({
            message: '<p class="text-center" style="font-size: 14px">' + $("#NS_CampaignFieldRequiredAdmin").text() + '</p>',
            closeButton: false,
            size: "medium",
        }).find('.modal-content').addClass("alert alert-danger");
        setTimeout(function () { bootbox.hideAll(); }, 3000);
        return false;
    }

    if ($('#Countries').val() == "") {
        bootbox.dialog({
            message: '<p class="text-center" style="font-size: 14px">' + $("#NS_CountryFieldRequiredAdmin").text() + '</p>',
            closeButton: false,
            size: "medium"
        }).find('.modal-content').addClass("alert alert-danger");
        setTimeout(function () { bootbox.hideAll(); }, 3000);
        //toastMessage.show($("#NS_CountryFieldRequiredAdmin").text(), "alert-danger");
        return false;
    }    

    return true;
}

