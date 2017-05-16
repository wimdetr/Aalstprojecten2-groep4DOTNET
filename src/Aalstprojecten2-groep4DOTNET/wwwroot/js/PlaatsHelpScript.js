$(document)
    .ready(function () {
        $(window).resize(function() {
            hoogte = $(window).height();
            margin = hoogte - 430;
            if (margin > 0) {
                $("#helpKnop").css("margin-top", margin + "px");
            }
        });
    });