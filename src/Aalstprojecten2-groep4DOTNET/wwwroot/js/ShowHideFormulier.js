$(document).ready(function () {
    var tijd = 800;
    if ($("#animatiesAanUit").text().trim() === "Animaties aanzetten") {
        tijd = 0;
    }
    var scrollPos = localStorage.getItem("scrollPos");
    localStorage.setItem("scrollPos", 0);
    $(window).scrollTop(scrollPos);
    $(".toverknop")
        .click(function () {
            if ($(this).closest(".Blok1").find("form").hasClass("verbergFormulier")) {
                $(this).closest(".Blok1").find("form").removeClass("verbergFormulier animated zoomOut").addClass("toonFormulier");
                if (tijd !== 0) {
                    $(this).closest(".Blok1").find("form").addClass("animated zoomIn");
                }
            } else {
                $(this).closest(".Blok1").find("form")
                    .removeClass("toonFormulier animated zoomIn");
                if (tijd !== 0) {
                    $(this).closest(".Blok1").find("form").addClass("animated zoomOut");
                }
                setTimeout(function () {
                    $(".toverknop").closest(".Blok1").find("form").addClass("verbergFormulier");
                }, tijd);
            }
        });

    $(".annuleerKnop")
        .click(function () {
            var knop = $(this);
            knop.closest("form").removeClass("toonFormulier animated zoomIn");
            if (tijd !== 0) {
                knop.closest("form").addClass("animated zoomOut");
            }
            setTimeout(function () {
                knop.closest("form").addClass("verbergFormulier");
                knop.closest("form").find("input").val("");
                knop.closest("form").find(".volgendeLijn").val("-1");
                $("span").text("");
            }, tijd);
        });
    $(".annuleerKnopKost1")
        .click(function () {
            var knop = $(this);
            $(this).parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn");
            if (tijd !== 0) {
                $(this).parent().parent().parent().parent().parent().addClass("animated zoomOut");
            }
            setTimeout(function () {
                knop.parent().parent().parent().parent().parent().addClass("verbergFormulier");
                knop.parent().parent().siblings("input").val("-1");
                knop.parent()
                    .parent()
                    .siblings(".vanKnopNaarInputVelden")
                    .children()
                    .children()
                    .find("input")
                    .val("");
                    
                $("#dropDown1").val("Kies uw doelgroep");
                $("#dropDown2").val("Vlaamse ondersteuningspremie");
                $("span").text("");
            }, tijd);
        });
    $(".verwijderRij")
        .click(function() {
            localStorage.setItem("scrollPos", $(window).scrollTop());
        });
});