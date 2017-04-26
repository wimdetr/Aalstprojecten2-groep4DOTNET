$(document).ready(function () {
    $(".toverknop")
        .click(function () {
            if ($(this).parent().parent().parent().parent().siblings(".invulgegevens").hasClass("verbergFormulier")) {
                $(this).parent().parent().parent().parent().siblings(".invulgegevens").removeClass("verbergFormulier animated zoomOut").addClass("toonFormulier animated zoomIn");
            } else {
                $(this).parent().parent().parent().parent()
                    .siblings(".invulgegevens")
                    .removeClass("toonFormulier animated zoomIn")
                    .addClass("animated zoomOut");
                setTimeout(function () {
                    $(".toverknop").siblings(".invulgegevens").addClass("verbergFormulier");
                }, 1000);
            }
        });

    $(".annuleerKnop")
        .click(function () {
            var knop = $(this);
            knop.parent().removeClass("toonFormulier animated zoomIn").addClass("animated zoomOut");
            setTimeout(function () {
                knop.parent().addClass("verbergFormulier");
                var inputsToClean = knop.siblings(".invulveldjes1").children(".form-group").find("input");
                var length = inputsToClean.length;
                for (var i = 0; i < length; i++) {
                    inputsToClean[i].value = null;
                }
                $("span").text("");
            }, 1000);
        });
    $(".annuleerKnopKost1")
        .click(function () {
            var knop = $(this);
            $(this).parent().removeClass("toonFormulier animated zoomIn").addClass("animated zoomOut");
            setTimeout(function () {
                knop.parent().addClass("verbergFormulier");
                var volledigeInvulveldjes = knop.siblings(".volledigeInvulveldjes");
                var inputsToClean1 = volledigeInvulveldjes.children(".invulveldjes1").children(".form-group").find("input");
                var inputsToClean2 = volledigeInvulveldjes.children(".invulveldjes2").children(".form-group").find("input");
                for (var i = 0; i < inputsToClean1.length; i++) {
                    inputsToClean1[i].value = null;
                }
                for (var j = 0; j < inputsToClean2.length; j++) {
                    inputsToClean2[j].value = null;
                }
                $("#dropDown1").val("Kies uw doelgroep");
                $("#dropDown2").val("Vlaamse ondersteuningspremie");
                $("span").text("");
            }, 1000);
        });
});