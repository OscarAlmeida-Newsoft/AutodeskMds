// JavaScript Document
$(document).ready(function () {
    $('#basic_date, .partnerDate').trigger("click");
    $(document).on('click', '#basic_date, .partnerDate', function () {
        $('#basic_date, .partnerDate').datetimepicker({
            yearOffset: 0,
            //lang: 'en',
            timepicker: false,
            format: 'Y/m/d',
            formatDate: 'Y/m/d',
            //minDate: '-1970/01/02', // yesterday is minimum date
            maxDate: '+2100/01/02' // and tommorow is maximum date calendar
        });
    });

    try{
        $.datetimepicker.setLocale(lenguaje);
    }
    catch(err)
    {
        $.datetimepicker.setLocale('en');
    }
    
});