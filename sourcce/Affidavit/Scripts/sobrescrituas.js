var lastValue;

$(document).ready(function () {
    /*---Cebra en tablas---*/
    $("table tr:even").addClass("gris_claro");
    /*---Tabs pasos formulario---*/
    $(".pagina_sencilla").hide();
    $(".pagina_sencilla:first").show();
    $("#tabs li:first").addClass("activo");
    $("#tabs li").on('click', function() {
        var linkIndex = $("#tabs li").index(this);
        $("#tabs li").removeClass("activo");
        $(".pagina_sencilla:visible").hide();
        $(".pagina_sencilla:eq("+linkIndex+")").show();
        $(this).addClass("activo");
        return false;
    });
    /*---Tabs productos MS---*/
    $(".pagina_derecha_sencilla").hide();
    $(".pagina_derecha_sencilla:first").show();
    $("#tabs_vertical li:first").addClass("activo");
    $("#tabs_vertical li").bind('click', function() {
        var linkIndex = $("#tabs_vertical li").index(this);
        $("#tabs_vertical li").removeClass("activo");
        $(".pagina_derecha_sencilla:visible").hide();
        $(".pagina_derecha_sencilla:eq("+linkIndex+")").show();
        $(this).addClass("activo");
        return false;
    });
    /*---Botones Previo y Siguiente Formulario---*/
    function reset_tab(){
        $(".pagina_sencilla").hide();
        $("#tabs li").removeClass("activo");
    };
    //$(".next_paso_1").click(function(){
    //	reset_tab();
    //	$(".pagina_sencilla:nth-child(2)").show();
    //	$("#tabs li:nth-child(2)").addClass("activo");
    //});
    $(document).on("click", ".next_paso_1", function () {
        reset_tab();
        $(".pagina_sencilla:eq(1)").show();
        $("#tabs li:eq(1)").addClass("activo");
    });
    //$(".next_paso_2").click(function(){
    //	reset_tab();
    //	$(".pagina_sencilla:nth-child(3)").show();
    //	$("#tabs li:nth-child(3)").addClass("activo");
    //});
    $(document).on("click", ".next_paso_2", function () {
        reset_tab();
        $(".pagina_sencilla:eq(2)").show();
        $("#tabs li:eq(2)").addClass("activo");
    });
    //$(".prev_paso_2").click(function(){
    //	reset_tab();
    //	$(".pagina_sencilla:nth-child(1)").show();
    //	$("#tabs li:nth-child(1)").addClass("activo");
    //});
    $(document).on("click", ".prev_paso_2", function () {
        reset_tab();
        $(".pagina_sencilla:eq(0)").show();
        $("#tabs li:eq(0)").addClass("activo");
    });
    //$(".prev_paso_3").click(function(){
    //	reset_tab();
    //	$(".pagina_sencilla:nth-child(2)").show();
    //	$("#tabs li:nth-child(2)").addClass("activo");
    //});
    $(document).on("click", ".prev_paso_3", function () {
        reset_tab();
        $(".pagina_sencilla:eq(1)").show();
        $("#tabs li:eq(1)").addClass("activo");
    });
    /*---Selector idiomas---*/
    $("#idiomas p").click(function(){
        $(this).parent().find("ul").toggle("fast");
        $("#idiomas .flecha").toggleClass("activo");
    });
    /*---Acordeones versiones de productos---*/
    $(".acordeon .contenido").hide();
    $(".acordeon .titulo").click(function(){
        $(this).next(".contenido").slideToggle("slow")
		.siblings(".contenido:visible").slideUp("slow");
        $(this).toggleClass("active");
        $(this).siblings(".titulo").removeClass("active");
    });

	loadingDiv.hide();


	$(document).on('click', '.desplegable p', function () {
	    if (!$(this).parent().hasClass("activo")) {
	        $(".desplegable").removeClass("activo");
	        $(".desplegable ul").not(this).hide("fast");
	        $(this).parent().find("ul").toggle("fast");
	        $(".flecha").removeClass("activo");
	        $(this).parent().find(".flecha").addClass("activo");
	        $(this).parent().addClass("activo");
	    }
	    else {
	        $(".desplegable").removeClass("activo");
	        $(this).parent().find("ul").hide("fast");
	        $(".flecha").removeClass("activo");
	    };
	});
	//<!--===============================-->
	$("#mostrar p.hide").hide();
	$("#tabs_vertical li:gt(4)").addClass("oculto");
	$("#mostrar").click(function(){
	    $("#mostrar p.hide").toggle();
	    $("#mostrar p.show").toggle();
	    $("#tabs_vertical li:gt(4)").toggleClass("oculto");
	});

	//$(".doubts a.platform").click(function () {
	//    $("#header").css('position', 'unset');
	//    $("#opacidadMeasure").show();
	//    $("#modalMeasure").show();
	//});



});

//Nuevo desarrollo (03.08.2017)
//$('.NS-VLS, .NS-OEM, .NS-FPP, .NS-WEB').focusin(function (e) {
$('.NS-VLS, .NS-OEM, .NS-FPP').focusin(function (e) {
    //Se obtiene el valor
    lastValue = parseInt($(this).val());
});

//$('.NS-VLS, .NS-OEM, .NS-FPP, .NS-WEB').focusout(function (e) {
$('.NS-VLS, .NS-OEM, .NS-FPP').focusout(function (e) {
    //Se obtiene el valor nuevo ingresado
    var valueObject = $(this);
    var value = parseInt(valueObject.val());
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
    //                bootbox.alert("The GAP amount can not be less than 0!");
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
    //} else {
    //    //Si se deja el campo vacio se le pone el ultimo valor valido
    //    valueObject.val(lastValue);
    //}
    //################################################# VIEJO PARA LA VALIDACION DEL GAP  ########################################
});