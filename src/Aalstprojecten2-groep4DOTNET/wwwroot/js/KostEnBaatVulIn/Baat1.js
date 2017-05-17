$(document).ready(function () {
    var tijd = 800;
    if ($("#animatiesAanUit").text().trim() === "Animaties aanzetten") {
        tijd = 0;
    }
    $("#vulInKnop1")
        .click(function () {
            localStorage.setItem("scrollPos", $(window).scrollTop());
            var knop = $(this);

            var uren = $("#uren1").val().trim();
            var bedrag = $("#brutoMaandloonFulltime1").val().trim();

            var bevatFout = false;
            if (uren === "" && bedrag === "") {
                bevatFout = true;
            }

            var re = new RegExp("[1-9][0-9]*([,][0-9]+)?");
            if (bedrag !== "" && !re.test(bedrag)) {
                bevatFout = true;
            }

            var re2 = new RegExp("[1-9][0-9]*");
            if (uren !== "" && !re2.test(uren)) {
                bevatFout = true;
            }

            if (bevatFout) {
                knop.closest("form").attr("action", "AnalyseBaat1Punt1");
                knop.closest("form").removeClass("toonFormulier animated zoomIn").submit();
            } else {
                knop.closest("form").removeClass("toonFormulier animated zoomIn");
                if (tijd !== 0) {
                    knop.closest("form").addClass("animated zoomOut");
                }
                setTimeout(function () {
                    knop.closest("form").attr("action", "AnalyseBaat1Punt1");
                    knop.closest("form").submit();
                },
                    tijd);
            }
        });


    $("#vulInKnop2")
        .click(function () {
            localStorage.setItem("scrollPos", $(window).scrollTop());
            var knop = $(this);

            var uren = $("#uren2").val().trim();
            var bedrag = $("#brutoMaandloonFulltime2").val().trim();

            var bevatFout = false;
            if (uren === "" && bedrag === "") {
                bevatFout = true;
            }

            var re = new RegExp("[1-9][0-9]*([,][0-9]+)?");
            if (bedrag !== "" && !re.test(bedrag)) {
                bevatFout = true;
            }

            var re2 = new RegExp("[1-9][0-9]*");
            if (uren !== "" && !re2.test(uren)) {
                bevatFout = true;
            }

            if (bevatFout) {
                knop.closest("form").attr("action", "AnalyseBaat1Punt2");
                knop.closest("form").removeClass("toonFormulier animated zoomIn").submit();
            } else {
                knop.closest("form").removeClass("toonFormulier animated zoomIn");
                knop.closest("form").addClass("animated zoomOut");
                setTimeout(function () {
                    knop.closest("form").attr("action", "AnalyseBaat1Punt2");
                    knop.closest("form").submit();
                }, tijd);
            }
        });

    $(".editKnop1")
        .click(function() {
            var knop = $(this);
            var waarden = knop.parent().siblings("td").map(function() {
                return $(this).text();
            });

            var lijnId = waarden[0];
            var uren = waarden[2];
            var maandloon = waarden[3].substring(2, waarden[3].length).replace(".", "");
            if (parseFloat(uren) === 0) {
                uren = "";
            }
            if (parseFloat(maandloon) === 0) {
                maandloon = "";
            }

            $("#volgendeLijn1").val(lijnId);
            $("#uren1").val(uren);
            $("#brutoMaandloonFulltime1").val(maandloon);

            if ($(this).parent().parent().parent().parent().parent().siblings(".invulgegevens").hasClass("verbergFormulier")) {
                $(this).parent().parent().parent().parent().parent().siblings(".invulgegevens").removeClass("verbergFormulier animated zoomOut").addClass("toonFormulier");
                if (tijd !== 0) {
                    $(this).parent().parent().parent().parent().parent().siblings(".invulgegevens").addClass("animated zoomIn");
                }
            }
        });

    $(".editKnop2")
        .click(function () {
            var knop = $(this);
            var waarden = knop.parent().siblings("td").map(function () {
                return $(this).text();
            });

            var lijnId = waarden[0];
            var uren = waarden[2];
            var maandloon = waarden[3].substring(2, waarden[3].length).replace(".", "");

            if (parseFloat(uren) === 0) {
                uren = "";
            }
            if (parseFloat(maandloon) === 0) {
                maandloon = "";
            }

            $("#volgendeLijn2").val(lijnId);
            $("#uren2").val(uren);
            $("#brutoMaandloonFulltime2").val(maandloon);

            if ($(this).parent().parent().parent().parent().parent().siblings(".invulgegevens").hasClass("verbergFormulier")) {
                $(this).parent().parent().parent().parent().parent().siblings(".invulgegevens").removeClass("verbergFormulier animated zoomOut").addClass("toonFormulier");
                if (tijd !== 0) {
                    $(this).parent().parent().parent().parent().parent().siblings(".invulgegevens").addClass("animated zoomIn");
                }
            }
        });
});