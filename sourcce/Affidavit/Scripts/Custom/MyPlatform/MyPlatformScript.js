var iframeLoaded = false;
$(document).ready(function () {
    debugger;
    if (IsSAM360On == "True" || IsSAM360On == true) {
        $(".embed_iframe iframe").load(
        function () {
            if (!iframeLoaded) {
                $(".embed_iframe iframe").attr("src", "https://app.sam360.com/")
                iframeLoaded = true;
            }

        });
    }

});
