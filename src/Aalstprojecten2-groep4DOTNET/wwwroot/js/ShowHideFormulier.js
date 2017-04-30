$(document).ready(function () {
    $(".toverknop")
        .click(function () {
            if ($(this).parent().parent().parent().parent().parent().siblings(".invulgegevens").hasClass("verbergFormulier")) {
                $(this).parent().parent().parent().parent().parent().siblings(".invulgegevens").removeClass("verbergFormulier animated zoomOut").addClass("toonFormulier animated zoomIn");
            } else {
                $(this).parent().parent().parent().parent().parent()
                    .siblings(".invulgegevens")
                    .removeClass("toonFormulier animated zoomIn")
                    .addClass("animated zoomOut");
                setTimeout(function () {
                    $(".toverknop").parent().parent().parent().parent().parent().siblings(".invulgegevens").addClass("verbergFormulier");
                }, 1000);
            }
        });

    $(".annuleerKnop")
        .click(function () {
            var knop = $(this);
            knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn").addClass("animated zoomOut");
            setTimeout(function () {
                knop.parent().parent().parent().parent().parent().addClass("verbergFormulier");
                knop.parent().parent().siblings(".marginBenedenTekstvelden").children(".vanKnopNaarInputVelden").children().each(function () {
                    $(this).children().children("div").children(".form-group").find("input").val("");
                });
                $("span").text("");
            }, 1000);
        });
    $(".annuleerKnopKost1")
        .click(function () {
            var knop = $(this);
            $(this).parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn").addClass("animated zoomOut");
            setTimeout(function () {
                knop.parent().parent().parent().parent().parent().addClass("verbergFormulier");
                //var volledigeInvulveldjes = knop.siblings(".volledigeInvulveldjes");
                //var inputsToClean1 = volledigeInvulveldjes.children(".invulveldjes1").children(".form-group").find("input");
                //var inputsToClean2 = volledigeInvulveldjes.children(".invulveldjes2").children(".form-group").find("input");
                //for (var i = 0; i < inputsToClean1.length; i++) {
                //    inputsToClean1[i].value = null;
                //}
                //for (var j = 0; j < inputsToClean2.length; j++) {
                //    inputsToClean2[j].value = null;
                //}
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
            }, 1000);
        });
});