$(document).ready(function () {
    $("#toonTooltip").click(function () {
        $(this).removeClass("toonMail").addClass("verbergMail");
        $("#verbergTooltip").removeClass("verbergMail").addClass("toonMail");
    });

    $("#verbergTooltip").click(function () {
        $(this).removeClass("toonMail").addClass("verbergMail");
        $("#toonTooltip").removeClass("verbergMail").addClass("toonMail");
    });

});