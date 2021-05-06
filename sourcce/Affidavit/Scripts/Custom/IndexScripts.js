AnswerQuestionsWindows = [];
AnswerQuestionsSQL = [];

var windowsServerInformation = [];
var datosSQLServer = [];

var totalServer;

var _currentValue;

var activeProducts = [];
var activeSQLProducts = [];
var isOtherWS = false;
var isOtherSQL = false;

//Aqui se guarda el valor del campo antes de hacerle alguna modificación
$(document).on("focusin", ".inputChange", function (e) {
    _currentValue = parseInt($(this).val());
    //console.log(_currentValue);
});


//Funcion para suma los valores que se agregan al numero de licencias por producto
$(document).on("blur", '.inputChange', function (e) {
    var model = {};
    var changed = $(e.target);
    var isWServer = $(this).closest('.familias').data('iswserver');
    var isSQLServer = $(this).closest('.familias').data('issqlserver');
    var isCal = $(this).closest('tr').data('iscal');
    var productVersion = $(this).data('version');


    if (isWServer) {
        var productoBlur = {
            productName: $(this).data("productname") + " " + productVersion,
            productId: $(this).data("productid"),
            qty: parseInt($(this).val())
        };


        var existeProducto = false;
        var i = 0;
        var debeBorrar = false;
        var borrarId = 0;
        var productIdBorrado = 0;
        var inserto = false;

        if (activeProducts.length != 0) {
            activeProducts.forEach(function (product) {
                if (productoBlur.productId == product.productId) {
                    if (productoBlur.qty == 0) {
                        debeBorrar = true;
                        borrarId = i;
                    }
                    existeProducto = true;
                    return false;
                }
                i++;
            });
            if (debeBorrar) {
                productIdBorrado = activeProducts[borrarId].productId;
                activeProducts.splice(borrarId, 1);

            }
        } else {
            if (productoBlur.qty != 0) {
                activeProducts.splice(activeProducts.length - 1, 0, productoBlur);
                inserto = true;
            }
            existeProducto = true;
        }

        if (!existeProducto) {
            //activeProducts.push(productoBlur);
            activeProducts.splice(activeProducts.length - 1, 0, productoBlur);
            inserto = true;
        }

        if (inserto) {

            var list = document.getElementsByClassName("wsproduct");

            for (i = 0; i < list.length; i++) {
                var option = document.createElement("option");
                option.text = productoBlur.productName;
                option.value = productoBlur.productId;

                list[i].insertBefore(option, list[i].childNodes[list[i].length - 1]);
            }
        } else if (debeBorrar) {
            $(".wsproduct option[value=" + productIdBorrado + "]").remove();
        }
    }


    if (isSQLServer) {
        var productoBlur = {
            productName: $(this).data("productname") + " " + productVersion,
            productId: $(this).data("productid"),
            qty: parseInt($(this).val())
        };


        var existeProducto = false;
        var i = 0;
        var debeBorrar = false;
        var borrarId = 0;
        var productIdBorrado = 0;
        var inserto = false;

        if (activeSQLProducts.length != 0) {
            activeSQLProducts.forEach(function (product) {
                if (productoBlur.productId == product.productId) {
                    if (productoBlur.qty == 0) {
                        debeBorrar = true;
                        borrarId = i;
                    }
                    existeProducto = true;
                    return false;
                }
                i++;
            });
            if (debeBorrar) {
                productIdBorrado = activeSQLProducts[borrarId].productId;
                activeSQLProducts.splice(borrarId, 1);

            }
        } else {
            if (productoBlur.qty != 0) {
                activeSQLProducts.splice(activeSQLProducts.length - 1, 0, productoBlur);
                inserto = true;
            }
            existeProducto = true;
        }

        if (!existeProducto) {
            activeSQLProducts.splice(activeSQLProducts.length - 1, 0, productoBlur);
            inserto = true;
        }

        if (inserto) {
            //$(".sqlproduct").append('<option value="' + productoBlur.productId + '">' + productoBlur.productName + '</option>');
            //$.each(activeSQLProducts, function (i, product) {
            //    $(".wsproduct").append('<option value="' + product.productId + '">' + product.productName + '</option>');
            //    // here we are adding option for States
            //});

            var list = document.getElementsByClassName("sqlproduct");

            for (i = 0; i < list.length; i++) {
                var option = document.createElement("option");
                option.text = productoBlur.productName;
                option.value = productoBlur.productId;

                list[i].insertBefore(option, list[i].childNodes[list[i].length - 1]);
            }

        } else if (debeBorrar) {
            $(".sqlproduct option[value=" + productIdBorrado + "]").remove();
        }

    }

    //Se actualiza el total para cada version
    var inputs = $(this).closest('.version-content').find(':input').not('.TextHidden');
    var totalVersion = 0;
    var totalCal = 0;
    inputs.each(function (idx, element) {
        if ($(this).closest('tr').data('iscal') == "True") {

            totalCal += parseInt($(this).val());
        } else {
            totalVersion += parseInt($(this).val());
        }

    });

    //Se asigna el total para la versión del campo que se modifico
    $(this).closest('.version-content').children('.total').children('H5').text(totalVersion + totalCal);

    //Se asigna el total para la familia que se modifico
    var _licenseXFamily = $(this).closest('.family-tabcontent').find('.LicensesxFamily');
    var _licenseXFamilyCAL = $(this).closest('.family-tabcontent').find('.LicensesxFamilyCAL');

    if (isCal == "False") {
        var _total = parseInt(_licenseXFamily.text()) - _currentValue + parseInt($(this).val());
        _licenseXFamily.text(_total);
        totalServer = _total;
    } else {
        var _total = parseInt(_licenseXFamilyCAL.text()) - _currentValue + parseInt($(this).val());
        _licenseXFamilyCAL.text(_total);
        totalServer = _total;
    }
});

//función para validar ingreso de información del formulario affidavit
$(document).ready(function () {
    //tabNumber
    $("#tabs li:nth-child(" + tabNumber + ")").click()


    //Nuevo desarrollo (31.08.2017)
    $('.NS-VLS, .NS-OEM, .NS-FPP').focusin(function (e) {
        //Se obtiene el valor
        lastValue = parseInt($(this).val());
        if (isNaN(lastValue))
            lastValue = 0;
    });


    $('.NS-VLS, .NS-OEM, .NS-FPP').focusout(function (e) {
        //Se obtiene el valor nuevo ingresado
        var valueObject = $(this);
        var value = parseInt(valueObject.val());

        if (isNaN(value)) {
            value = 0;
        }

        //SE obtiene el valor gap para este
        var gapObject = $(this).parent().siblings().find(".NS-GAP");
        var gapValue = parseInt(gapObject.text());
        //################################################# NUEVO SIN LA VALIDACION DEL GAP  ########################################
        gapValue = gapValue + (lastValue - value);
        gapObject.text(gapValue);
        //################################################# NUEVO SIN LA VALIDACION DEL GAP  ########################################

        //################################################# VIEJO PARA LA VALIDACION DEL GAP  ########################################
        //if (value != null && value != undefined && !isNaN(value)) {
        //    if (value != lastValue) {
        //        if (value > lastValue) {
        //            var resta = gapValue - (value - lastValue);
        //            if (resta >= 0) {
        //                gapValue = resta;
        //                //Se cambia el valor gap
        //                gapObject.text(gapValue);
        //            } else {
        //                //Se alerta y se devuelve el campo de esa licencia al valor anterior
        //                bootbox.alert($("#NewWarningGAP").text());
        //                $('.bootbox-body').css("font-size", "14px");
        //                valueObject.val(lastValue);
        //            }
        //        }
        //        else {
        //            gapValue = gapValue + (lastValue - value);
        //            gapObject.text(gapValue);
        //            //Se cambia el valor gap
        //            //De lo contrario habria que ver si el al restar este valor el CAMPO GAP Es >=0, ya que si no no se podra realizar la resta y se debera reestablecer el campo

        //        }
        //    }
        //}
        //################################################# VIEJO PARA LA VALIDACION DEL GAP  ########################################
    });


    $('#confirmar').click(function () {
        //if (windowsServerInformation.length != AnswerQuestionsWindows.length) {
        //    console.log("ws tamaño: " + windowsServerInformation.length + " ----- aq tamaño: " + AnswerQuestionsWindows.length);
        //    toastMessage.show("no coinciden el numero de servidores con las respuestas a responder", "alert-danger");
        //    return;
        //}

        var bandera = true;
        var flag = true;

        var _vlsNotEmpty = document.getElementsByClassName("isNotEmptyVLS");
        var _contratosVolumen = document.getElementsByClassName("contratosVolumen");
        //var _vlsCount = 0;

        //for (var i = 0; i < _vlsNotEmpty.length; i++) {
        //    if (_vlsNotEmpty[i].value != 0) {

        //        _vlsCount++;
        //        for (var j = 0; j < _contratosVolumen.length; j++) {
        //            if (_contratosVolumen[j].value != "") {

        //                vlsFlag = true;
        //                break;
        //            } else {

        //                if (j >= _contratosVolumen.length-1) {
        //                    var _contratodVolumenLabel = $("#ContratodVolumenLabel").text();
        //                    toastMessage.show(_contratodVolumenLabel, "alert-danger");
        //                }
        //            }
        //        }
        //        break;
        //    }
        //}

        //if (_vlsCount == 0) {
        //    vlsFlag = true;
        //}

        var _conf = $("#ConfirmLabel").text();



        if ($("input[name=CompanyName]")[0].value == "" || $("input[name=CompanyName]")[0].value == null) {

            var _nameLabel = $("#CompanyNameLabel").text();
            //toastMessage.show(_nameLabel, "alert-danger");
            semaphore.showError("", _nameLabel)
            bandera = false;
            flag = false;

        } else if ($("input[name=NombreContacto]")[0].value == "" || $("input[name=NombreContacto]")[0].value == null) {

            var _contactNameLabel = $("#CompanyContactNameLabel").text();
            //toastMessage.show(_contactNameLabel, "alert-danger");
            semaphore.showError("", _contactNameLabel)
            bandera = false;
            flag = false;

        } else if ($("input[name=Email]")[0].value == "" || $("input[name=Email]")[0].value == null) {

            var _companyEmailLabel = $("#CompanyEmailLabel").text();
            //toastMessage.show(_companyEmailLabel, "alert-danger");
            semaphore.showError("", _companyEmailLabel)
            bandera = false;
            flag = false;

        } else if ($("input[name=Telefono]")[0].value == "" || $("input[name=Telefono]")[0].value == null) {

            var _companyPhoneLabel = $("#CompanyPhoneLabel").text();
            //toastMessage.show(_companyPhoneLabel, "alert-danger");
            semaphore.showError("", _companyPhoneLabel)

            bandera = false;

            //} else if($("input[name=Fecha]")[0].value == "" || $("input[name=Fecha]")[0].value == null){

            //    var _companyDateLabel = $("#CompanyDateLabel").text();
            //    toastMessage.show(_companyDateLabel, "alert-danger");

        } else if (!$("input[name=TipoCliente]")[0].checked && !$("input[name=TipoCliente]")[1].checked && !$("input[name=TipoCliente]")[2].checked &&
            !$("input[name=TipoCliente]")[3].checked && !$("input[name=TipoCliente]")[4].checked) {

            var _clientTypeMessage = $("#ClientTypeLabel").text();
            semaphore.showError("", _clientTypeMessage)
            //toastMessage.show(_clientTypeMessage, "alert-danger");
            bandera = false;

        } else if ($("input[name=NumeroTotalEmpleados]")[0].value == "" || $("input[name=NumeroPCsEscritorio]")[0].value == "") {

            var _numeroTotalEmpleadosPcsLabel = $("#NumeroTotalEmpleadosPcsLabel").text();
            //toastMessage.show(_numeroTotalEmpleadosPcsLabel, "alert-danger");
            semaphore.showError("", _numeroTotalEmpleadosPcsLabel)
            bandera = false;

        } else if (!$("input[name=ProcesoCompra]")[0].checked && !$("input[name=ProcesoCompra]")[1].checked) {

            var _procesoCompraLabel = $("#ProcesoCompraLabel").text();
            //toastMessage.show(_procesoCompraLabel, "alert-danger");
            semaphore.showError("", _procesoCompraLabel)
            bandera = false;

        } else if ($("input[name=ProcesoCompra]")[0].checked) {

            if (($("input[name=NombreExactoFacturacion]")[0].value == "")) {
                var _invoicingNameLabel = $("#InvoicingNameLabel").text();
                //toastMessage.show(_invoicingNameLabel, "alert-danger");
                semaphore.showError("", _invoicingNameLabel)
                bandera = false;
            }

            //} else if ($("input[name=NombreExactoFacturacion]")[0].value == "") {
            //    var _invoicingNameLabel = $("#InvoicingNameLabel").text();
            //    toastMessage.show(_invoicingNameLabel, "alert-danger");

        }

        if (bandera) {

            if ((!$("input[name=Assurance]")[0].checked && !$("input[name=Assurance]")[1].checked)) {

                var _radioLabel = $("#SoftwareAssuranceRadioButtonLabel").text();
                //toastMessage.show(_radioLabel, "alert-danger");
                semaphore.showError("", _radioLabel)

                bandera = false;

            } else if ((!$("input[name=NewLinceses]")[0].checked && !$("input[name=NewLinceses]")[1].checked)) {

                var _radioLabel = $("#NewLicencesRadioButtonLabel").text();
                //toastMessage.show(_radioLabel, "alert-danger");
                semaphore.showError("", _radioLabel)
                bandera = false;


            } else if ((!$("input[name=UpgrateLicenses]")[0].checked && !$("input[name=UpgrateLicenses]")[1].checked)) {

                var _radioLabel = $("#UpgradesRadioButtonLabel").text();
                //toastMessage.show(_radioLabel, "alert-danger");
                semaphore.showError("", _radioLabel)
                bandera = false;

            } else if ((!$("input[name=AutMicChannel]")[0].checked && !$("input[name=AutMicChannel]")[1].checked)) {

                var _radioLabel = $("#AuthorizedRadioButtonLabel").text();
                //toastMessage.show(_radioLabel, "alert-danger");
                semaphore.showError("", _radioLabel)
                bandera = false;

            } else if (!$("input[name=EstLicenciamiento]")[0].checked && !$("input[name=EstLicenciamiento]")[1].checked && !$("input[name=EstLicenciamiento]")[2].checked) {

                var _estLicenciamientoLabel = $("#EstLicenciamientoLabel").text();
                //toastMessage.show(_estLicenciamientoLabel, "alert-danger");
                semaphore.showError("", _estLicenciamientoLabel)
                bandera = false;

            } else if ($("input[name=AutMicChannel]")[0].checked) {
                if (($("input[name=AutChannel]")[0].value == "")) {

                    var _authorized = $("#AuthorizedLabel").text();
                    //toastMessage.show(_authorized, "alert-danger");
                    semaphore.showError("", _authorized);
                    bandera = false;

                } else if ($("input[name=ContactNameCanalAutorizado]")[0].value == "") {

                    var _channelContactNameLabel = $("#ChannelContactNameLabel").text();
                    //toastMessage.show(_channelContactNameLabel, "alert-danger");
                    semaphore.showError("", _channelContactNameLabel);
                    bandera = false;

                } else if ($("input[name=TelefonoCanalAutorizado]")[0].value == "") {

                    var _channelPhoneLabel = $("#ChannelPhoneLabel").text();
                    //toastMessage.show(_channelPhoneLabel, "alert-danger");
                    semaphore.showError("", _channelPhoneLabel);
                    bandera = false;

                } else if ($("input[name=EmailCanalAutorizado]")[0].value == "") {

                    var _channelEmailLabel = $("#ChannelEmailLabel").text();
                    //toastMessage.show(_channelEmailLabel, "alert-danger");
                    semaphore.showError("", _channelEmailLabel);
                    bandera = false;
                }
            }
        }

        if (bandera == true) {

            if ($("input[id=IDLetterInfoText1]:checked").length <= 0) {
                var _CheckBoxMessageLabel = $("#CheckBoxMessageLabel").text();
                //toastMessage.show(_CheckBoxMessageLabel, "alert-danger");
                semaphore.showError("", _CheckBoxMessageLabel);

            }
            else {
                semaphore.showAtention("",
                                        _conf,
                                        function () {
                                            loadingDiv.show();
                                            $('#IsFinal').val(true);
                                            gAnalytics.throwFormEvent('Click Submit', 'MDS Form', $(this).data().eventLabel, $('#formulario'))
                                        },
                                        function () {
                                        })
                //bootbox.confirm(_conf, function (r) {
                //    $('.bootbox-body').css("font-size", "14px");
                //    if (r == true) {
                //        loadingDiv.show();
                //        $('#IsFinal').val(true);
                //        $('#formulario').submit();
                //    }
                //});
                //$('.bootbox-body').css("font-size", "14px");
            }
            //bootbox.confirm(_conf, function (result) {

            //    console.log("Confirm result: " + result);
            //    if (result == true) {
            //        $('#IsFinal').val(true);
            //        $('#formulario').submit();
            //    }
            //});
        }
    });
});

$(document).on("change", "#autMicChannelSi", function () {

    if ($("input[name=AutMicChannel]")[0].checked) {

        $("#partnerInfo").show(1000);
    }
});

$(document).on("change", "#autMicChannelNo", function () {

    if ($("input[name=AutMicChannel]")[1].checked) {

        $("#partnerInfo").hide(1000);
    }
});

$(document).on("change", "#procesoCompraSi", function () {
    console.log($("input[name=ProcesoCompra]"))
    if ($("input[name=ProcesoCompra]")[0].checked) {

        $("#txtboxProcesoCompra").show(1000);
    }
});

$(document).on("change", "#procesoCompraNo", function () {

    if ($("input[name=ProcesoCompra]")[1].checked) {

        $("#txtboxProcesoCompra").hide(1000);
    }
});


//jquery para mostrar o esconder las tablas para info de partner
$(document).ready(function () {

    $('input[type=radio]').change(function () {
        loadingDiv.show();

        var valor = $("input[name=TipoCliente]:checked").prop("value");
        var _procesoCompra = $("input[name=ProcesoCompra]:checked").prop("value");

        if (valor == "Partner" || valor == "DeveloperPartner") {
            $('#PartnerDiv').show();
            loadingDiv.hide();
        } else {
            $('#PartnerDiv').hide();
            loadingDiv.hide();
        }

        if (valor == "DeveloperPartner") {
            $('#customOrBasicAppDiv').show();
            loadingDiv.hide();
        } else {
            $('#customOrBasicAppDiv').hide();
            loadingDiv.hide();
        }

        if (valor == "Academic") {
            $('#AcademicDiv').show();
            loadingDiv.hide();
        } else {
            $('#AcademicDiv').hide();
            loadingDiv.hide();
        }

        if (_procesoCompra == "true") {
            $('#txtboxProcesoCompra').show();
            loadingDiv.hide();
        } else {
            $('#txtboxProcesoCompra').hide();
            loadingDiv.hide();
        }
    });
});


//Realiza el submit del formulario
$(document).on("click", ".botonEnviar", function (e) {
    var boton = $(this);
    e.preventDefault();
    loadingDiv.show();
    $('#CurrentTab').val(boton.data('tabnumber'));
    $('#IsFinal').val(false);

    gAnalytics.throwFormEvent('Click Postpone Filling', 'MDS Form', $(this).data().eventLabel, $('#formulario'))
    //$('#formulario').submit();
});

//Función para aceptar solo números enteros positivos en un campo de texto
$(document).on("keydown", ".integerOnly", function (e) {

    if (!((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 96 && e.keyCode <= 105) ||
   (e.keyCode == 8 || e.keyCode == 46 || e.keyCode == 37 || e.keyCode == 9 ||
    e.keyCode == 39)))
        e.preventDefault();
});

//Valida si el Email ingresado es un Email válido
$(document).on("focusout", ".EmailValidation", function () {

    var _email = $('#Email').val();
    var _emailCanalAut = $('#EmailCanalAutorizado').val();
    var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
    if (!pattern.test(_email)) {
        $('#emailMessage').show();
        //var _authorized = $("#AuthorizedLabel").text();
        //bootbox.alert(_authorized);
    } else {
        $('#emailMessage').hide();
    }

    if (!pattern.test(_emailCanalAut)) {
        $('#emailChannelMessage').show();
        //var _authorized = $("#AuthorizedLabel").text();
        //bootbox.alert(_authorized);
    } else {
        $('#emailChannelMessage').hide();
    }
});

//para popup's 

$(document).on("mouseenter", "#myFunctionVLS", function (e) {
    var vls = document.getElementById('vls');
    vls.classList.toggle('show');
});

$(document).on("mouseenter", "#myFunctionOEM", function (e) {
    var oem = document.getElementById('oem');
    oem.classList.toggle('show');
});

$(document).on("mouseenter", "#myFunctionFPP", function (e) {
    var fpp = document.getElementById('fpp');
    fpp.classList.toggle('show');
});

$(document).on("mouseenter", "#softAssuranceHelp", function (e) {
    var assurance = document.getElementById('AssuranceHelp');
    assurance.classList.toggle('show');
});

$(document).on("mouseover", "#ServerCal", function (e) {
    //var assurance = $(this).closest('td').find('.popuptext');
    //console.log(assurance);
    var assurance = document.getElementById('ServerCalHelp');
    assurance.classList.toggle('show');
});

$(document).on("mouseover", "#DsktpCAL", function (e) {
    var assurance = document.getElementById('DsktpCALHelp');
    assurance.classList.toggle('show');
});

$(document).on("mouseout", "#DsktpCAL", function (e) {
    console.log("mouseout");
    var assurance = document.getElementById('DsktpCALHelp');
    assurance.classList.toggle('hide');
});












































//Funcion para suma los valores que se agregan al numero de licencias por producto
$(document).on("blur", '.serverChange', function (e) {
    debugger;
    var serverType = e.target.id;
    var nroCurrentServers = 0;

    //Se consulta cuantos servidores hay guardados en este momento
    loadingDiv.show();
    $.ajax({
        url: $('#NS_UrlActionGetNroServersbyCategory').text() + "?category=" + serverType + "&pCompanyID=" + $('#CompanyID').val() + "&pCampaignId=" + $('#CampaignID').val(),
        type: 'GET',
        async: false,
        success: function (data) {

            nroCurrentServers = data;
            loadingDiv.hide();
        },
        fail: function (err) {
            //$('#progressbargrid').addClass('hide');
            loadingDiv.hide();
            console.log(err);
        }
    });

    var quanty = parseInt($(this).val());

    if (quanty == undefined || isNaN(quanty)) {
        quanty = 0;
    }

    if (nroCurrentServers > quanty) {

        var theID = this.id;

        //Se lanza alerta advirtiendo al usuario q borrando servidores y que la informacion sera borrada
        semaphore.showAtention(
            "",
            $('#ModifiedNumberServers_Delete').text(),
            function () {
                GetAdditionalQuestions(serverType, quanty);
            },
            function () {
                $("#" + theID).val(nroCurrentServers);

            });
    } else {
        if (quanty > 500) {
            semaphore.showError("", $("#NewWarningMAXServers").text());
            $(this).val(nroCurrentServers)
        }
        else {
            GetAdditionalQuestions(serverType, quanty);
        }

        //Entra aqui si encuentra que agregara servidores
        //quanty es la nueva cantidad, nroCurrentServers es la cantidad de servidores actuales, es decir 


    }













    //if (isWServer) {
    //    var dato = $.grep(windowsServerInformation, function (element, index) {

    //        return element.productId == linea.productId && element.productIsVLS == linea.productIsVLS && element.productIsFPP == linea.productIsFPP
    //            && element.productIsOEM == linea.productIsOEM && element.productIsWeb == linea.productIsWeb
    //            && element.productInstalledLicenses == linea.productInstalledLicenses;

    //    });

    //    if (dato.length == 0) {
    //        if (isCal != "True") {

    //            windowsServerInformation.push(linea);
    //        } else {
    //            windowsServerInformation.push(lineaAlterna);
    //        }
    //    } else {
    //        dato[0].qty = linea.qty;
    //    }

    //    model.windowsServerInformation = windowsServerInformation;
    //    loadingDiv.show();

    //    $.ajax({
    //        url: $('#NS_UrlActionGetAdditionalQuestions').text() + "?pLeadID=" + $('#LeadID').text(),
    //        data: JSON.stringify(model),
    //        type: 'POST',
    //        contentType: 'application/json; charset=utf-8'
    //    })
    //        .done(function (data) {

    //            //$('#progressbargrid').addClass('hide');
    //            $('#dvDetailsResults').html(data);
    //            loadingDiv.hide();
    //            //console.log(data);
    //        })
    //        .fail(function (err) {
    //            //$('#progressbargrid').addClass('hide');
    //            loadingDiv.hide();
    //            console.log(err);

    //        });

    //}

    ////AQUI EMPIEZA PARA SQLSERVER
    //else if (isSQLServer) {
    //    var dato = $.grep(datosSQLServer, function (element, index) {

    //        return element.productId == linea.productId && element.productIsVLS == linea.productIsVLS && element.productIsFPP == linea.productIsFPP
    //            && element.productIsOEM == linea.productIsOEM && element.productIsWeb == linea.productIsWeb
    //            && element.productInstalledLicenses == linea.productInstalledLicenses;

    //    });

    //    if (dato.length == 0) {
    //        if (isCal != "True") {

    //            datosSQLServer.push(linea);
    //        } else {
    //            datosSQLServer.push(lineaAlterna);
    //        }
    //    } else {
    //        dato[0].qty = linea.qty;
    //    }

    //    model.sqlServerInformation = datosSQLServer

    //    loadingDiv.show();
    //    $.ajax({
    //        url: $('#NS_UrlActionGetAdditionalQuestionsSQL').text() + "?pLeadID=" + $('#LeadID').text(),
    //        data: JSON.stringify(model),
    //        type: 'POST',
    //        contentType: 'application/json; charset=utf-8'
    //    })
    //        .done(function (data) {
    //            //$('#progressbargrid').addClass('hide');
    //            $('#dvDetailsResultsSQL').html(data);
    //            loadingDiv.hide();
    //            //console.log(data);
    //        })
    //        .fail(function (err) {
    //            //$('#progressbargrid').addClass('hide');
    //            loadingDiv.hide();
    //            console.log(err);

    //        });
    //}




    ////Se actualizan las versiones
    //var inputs = $(this).closest('.version-content').find(':input').not('.TextHidden');
    //var totalVersion = 0;
    //var totalCal = 0;
    //inputs.each(function (idx, element) {


    //    if ($(this).closest('tr').data('iscal') == "True") {

    //        totalCal += parseInt($(this).val());
    //    } else {
    //        totalVersion += parseInt($(this).val());
    //    }

    //});
    ////Se asigna el total para la versión
    //$(this).closest('.version-content').children('.total').children('H5').text(totalVersion + totalCal);

    ////Se asigna el total para la familia
    //var _licenseXFamily = $(this).closest('.family-tabcontent').find('.LicensesxFamily');
    //var _licenseXFamilyCAL = $(this).closest('.family-tabcontent').find('.LicensesxFamilyCAL');

    //if (isCal == "False") {
    //    var _total = parseInt(_licenseXFamily.text()) - _currentValue + parseInt($(this).val());
    //    _licenseXFamily.text(_total);
    //    totalServer = _total;
    //} else {
    //    var _total = parseInt(_licenseXFamilyCAL.text()) - _currentValue + parseInt($(this).val());
    //    _licenseXFamilyCAL.text(_total);
    //    totalServer = _total;
    //}
});



function GetAdditionalQuestions(serverType, quanty) {
    var model = {};

    if (serverType === "physicServer") {

        var linea = {
            CompanyId: $('#CompanyID').val(),
            CampaignId: $('#CampaignID').val(),
            qty: quanty,
            leadID: $('#LeadID').text(),
            IsVirtual: false,
            IsNew: true,
            productId: 84,
            IsInside: null,
            productInstalledLicenses: true,
            //productName: "Physic Server",
            productFamilyComplete: "Windows Server"
        };

        var lineaAlterna = {
            CompanyId: $('#CompanyID').val(),
            CampaignId: $('#CampaignID').val()
        };



        windowsServerInformation = [];
        windowsServerInformation.push(linea);
        model.windowsServerInformation = windowsServerInformation;
        loadingDiv.show();

        $.ajax({
            url: $('#NS_UrlActionGetAdditionalQuestions').text() + "?pLeadID=" + $('#LeadID').text(),
            data: JSON.stringify(model),
            type: 'POST',
            contentType: 'application/json; charset=utf-8'
        })
            .done(function (data) {

                //$('#progressbargrid').addClass('hide');
                $('#dvDetailsResults').html(data);
                loadingDiv.hide();
                //console.log(data);
            })
            .fail(function (err) {
                //$('#progressbargrid').addClass('hide');
                loadingDiv.hide();
                console.log(err);

            });
    } else if (serverType === "virtualServer") {

        var linea = {
            CompanyId: $('#CompanyID').val(),
            CampaignId: $('#CampaignID').val(),
            qty: quanty,
            leadID: $('#LeadID').text(),
            IsVirtual: true,
            IsNew: true,
            productId: 84,
            IsInside: null,
            productInstalledLicenses: true,
            //productName: "Physic Server",
            productFamilyComplete: "Windows Server"
        };

        var lineaAlterna = {
            CompanyId: $('#CompanyID').val(),
            CampaignId: $('#CampaignID').val()
        };



        windowsServerInformation = [];
        windowsServerInformation.push(linea);
        model.windowsServerInformation = windowsServerInformation;
        loadingDiv.show();

        $.ajax({
            url: $('#NS_UrlActionGetAdditionalQuestions').text() + "?pLeadID=" + $('#LeadID').text(),
            data: JSON.stringify(model),
            type: 'POST',
            contentType: 'application/json; charset=utf-8'
        })
            .done(function (data) {

                //$('#progressbargrid').addClass('hide');
                $('#dvDetailsResults').html(data);
                loadingDiv.hide();
                //console.log(data);
            })
            .fail(function (err) {
                //$('#progressbargrid').addClass('hide');
                loadingDiv.hide();
                console.log(err);

            });
    } else if (serverType === "sqlServer") {

        var linea = {
            CompanyId: $('#CompanyID').val(),
            CampaignId: $('#CampaignID').val(),
            qty: quanty,
            leadID: $('#LeadID').text(),
            IsVirtual: false,
            IsNew: true,
            productId: 139,
            IsInside: null,
            productInstalledLicenses: true,
            //productName: "Physic Server",
            productFamilyComplete: "SQL Server"
        };

        var lineaAlterna = {
            CompanyId: $('#CompanyID').val(),
            CampaignId: $('#CampaignID').val()
        };



        sqlServerInformation = [];
        sqlServerInformation.push(linea);
        model.sqlServerInformation = sqlServerInformation;
        loadingDiv.show();

        $.ajax({
            url: $('#NS_UrlActionGetAdditionalQuestionsSQL').text() + "?pLeadID=" + $('#LeadID').text(),
            data: JSON.stringify(model),
            type: 'POST',
            contentType: 'application/json; charset=utf-8'
        })
            .done(function (data) {

                //$('#progressbargrid').addClass('hide');
                $('#dvDetailsResultsSQL').html(data);
                loadingDiv.hide();
                //console.log(data);
            })
            .fail(function (err) {
                //$('#progressbargrid').addClass('hide');
                loadingDiv.hide();
                console.log(err);

            });

    }

}

