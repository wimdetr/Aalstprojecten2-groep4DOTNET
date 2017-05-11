$(document).ready(function () {
    $(".toverknop")
        .click(function () {
            if ($(this).parent().parent().parent().siblings(".invulgegevens").hasClass("verbergFormulier")) {
                $(this).parent().parent().parent().siblings(".invulgegevens").removeClass("verbergFormulier animated zoomOut").addClass("toonFormulier animated zoomIn");
            } else {
                $(this).parent().parent().parent()
                    .siblings(".invulgegevens")
                    .removeClass("toonFormulier animated zoomIn")
                    .addClass("animated zoomOut");
                setTimeout(function () {
                    $(".toverknop").parent().parent().parent().siblings(".invulgegevens").addClass("verbergFormulier");
                }, 800);
            }
        });

    $(".annuleerKnop")
        .click(function () {
            var knop = $(this);
            knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn").addClass("animated zoomOut");
            setTimeout(function () {
                knop.parent().parent().parent().parent().parent().addClass("verbergFormulier");
                knop.parent().parent().siblings(".marginBenedenTekstvelden").find("input").val("-1");
                knop.parent().parent().siblings(".marginBenedenTekstvelden").children(".vanKnopNaarInputVelden").children().each(function () {
                    $(this).children().children("div").children(".form-group").find("input").val("");
                });
                $("span").text("");
            }, 800);
        });
    $(".annuleerKnopKost1")
        .click(function () {
            var knop = $(this);
            $(this).parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn").addClass("animated zoomOut");
            setTimeout(function () {
                knop.parent().parent().parent().parent().parent().addClass("verbergFormulier");
                knop.parent().parent().siblings("input").val("-1");
                knop.parent()
                    .parent()
                    .siblings(".vanKnopNaarInputVelden")
                    .children()
                    .children()
                    .each(function() {
                        $(this).children("div").children(".form-group").find("input").val("");
                    });
                $("#dropDown1").val("Kies uw doelgroep");
                $("#dropDown2").val("Vlaamse ondersteuningspremie");
                $("span").text("");
            }, 800);
        });
});