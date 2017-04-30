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
});