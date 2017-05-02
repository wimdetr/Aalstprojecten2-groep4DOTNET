$(document).ready(function () {
    $("#vulInKnop1")
        .click(function () {
            var knop = $(this);

            var uren = $("#uren1").val().trim();
            var bedrag = $("#brutoMaandloonFulltime1").val().trim();

            var bevatFout = false;
            if (uren === "" && bedrag === "") {
                bevatFout = true;
            }

            var re = new RegExp("[1-9][0-9]*([,][0-9]+)?");
            if (!re.test(bedrag)) {
                bevatFout = true;
            }

            var re2 = new RegExp("[1-9][0-9]*");
            if (!re2.test(uren)) {
                bevatFout = true;
            }

            if (bevatFout) {
                knop.parent().parent().parent().parent().parent().attr("action", "AnalyseBaat1Punt1");
                knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn").submit();
            } else {
                knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn").addClass("animated zoomOut");
                setTimeout(function () {
                    knop.parent().parent().parent().parent().parent().attr("action", "AnalyseBaat1Punt1");
                    knop.parent().parent().parent().parent().parent().submit();
                },
                    1000);
            }
        });


    $("#vulInKnop2")
        .click(function () {
            var knop = $(this);

            var uren = $("#uren2").val().trim();
            var bedrag = $("#brutoMaandloonFulltime2").val().trim();

            var bevatFout = false;
            if (uren === "" && bedrag === "") {
                bevatFout = true;
            }

            var re = new RegExp("[1-9][0-9]*([,][0-9]+)?");
            if (!re.test(bedrag)) {
                bevatFout = true;
            }

            var re2 = new RegExp("[1-9][0-9]*");
            if (!re2.test(uren)) {
                bevatFout = true;
            }

            if (bevatFout) {
                knop.parent().parent().parent().parent().parent().attr("action", "AnalyseBaat1Punt2");
                knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn").submit();
            } else {
                knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn").addClass("animated zoomOut");
                setTimeout(function () {
                    knop.parent().parent().parent().parent().parent().attr("action", "AnalyseBaat1Punt2");
                    knop.parent().parent().parent().parent().parent().submit();
                }, 1000);
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

            if ($(this).parent().parent().parent().parent().siblings(".invulgegevens").hasClass("verbergFormulier")) {
                $(this).parent().parent().parent().parent().siblings(".invulgegevens").removeClass("verbergFormulier animated zoomOut").addClass("toonFormulier animated zoomIn");
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
            var maandloon = waarden[3].substring(2, waarden[3].length);

            if (parseFloat(uren) === 0) {
                uren = "";
            }
            if (parseFloat(maandloon) === 0) {
                maandloon = "";
            }

            $("#volgendeLijn2").val(lijnId);
            $("#uren2").val(uren);
            $("#brutoMaandloonFulltime2").val(maandloon);

            if ($(this).parent().parent().parent().parent().siblings(".invulgegevens").hasClass("verbergFormulier")) {
                $(this).parent().parent().parent().parent().siblings(".invulgegevens").removeClass("verbergFormulier animated zoomOut").addClass("toonFormulier animated zoomIn");
            }
        });
});