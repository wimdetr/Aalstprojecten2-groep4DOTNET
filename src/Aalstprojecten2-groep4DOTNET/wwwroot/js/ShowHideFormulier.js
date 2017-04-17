$(document).ready(function () {
    $(".toverknop")
        .click(function () {
            if ($(this).siblings(".invulgegevens").hasClass("verbergFormulier")) {
                $(this).siblings(".invulgegevens").removeClass("verbergFormulier").addClass("toonFormulier");
            } else {
                $(this).siblings(".invulgegevens").removeClass("toonFormulier").addClass("verbergFormulier");
            }
        });
    $(".annuleerKnop")
        .click(function () {
            $(this).parent().removeClass("toonFormulier").addClass("verbergFormulier");
            var inputsToClean = $(this).siblings(".invulveldjes1").children(".form-group").find("input");
            var length = inputsToClean.length;
            for (var i = 0; i < length; i++) {
                inputsToClean[i].value = null;
            }
        });
    $(".annuleerKnopKost1")
        .click(function() {
            $(this).parent().removeClass("toonFormulier").addClass("verbergFormulier");
            var volledigeInvulveldjes = $(this).siblings(".volledigeInvulveldjes");
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
        });
});