$(document).ready(function () {
    $("#vulInKnop1")
        .click(function () {
            var knop = $(this);

            var beschrijving = $("#type1").val().trim();
            var bedrag = $("#Bedrag1").val().trim();

            var bevatFout = false;
            if (beschrijving === "" && bedrag === "") {
                bevatFout = true;
            }
            var re = new RegExp("[1-9][0-9]*([,][0-9]+)?");
            if (!re.test(bedrag)) {
                bevatFout = true;
            }


            if (bevatFout) {
                knop.parent().parent().parent().parent().parent().attr("action", "AnalyseKost4Punt1");
                knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn").submit();
            } else {
                knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn").addClass("animated zoomOut");
                setTimeout(function () {
                    knop.parent().parent().parent().parent().parent().attr("action", "AnalyseKost4Punt1");
                    knop.parent().parent().parent().parent().parent().submit();
                },
                    1000);
            }
        });


    $("#vulInKnop2")
        .click(function () {
            var knop = $(this);

            var uren = $("#uren").val().trim();
            var bedrag = $("#brutoMaandloonBegeleider").val().trim();

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
                knop.parent().parent().parent().parent().parent().attr("action", "AnalyseKost4Punt2");
                knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn").submit();
            } else {
                knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn").addClass("animated zoomOut");
                setTimeout(function () {
                    knop.parent().parent().parent().parent().parent().attr("action", "AnalyseKost4Punt2");
                    knop.parent().parent().parent().parent().parent().submit();
                },
                    1000);
            }
        });


    $("#vulInKnop3")
        .click(function () {
            var knop = $(this);

            var beschrijving = $("#type3").val().trim();
            var bedrag = $("#Bedrag3").val().trim();

            var bevatFout = false;
            if (beschrijving === "" && bedrag === "") {
                bevatFout = true;
            }
            var re = new RegExp("[1-9][0-9]*([,][0-9]+)?");
            if (!re.test(bedrag)) {
                bevatFout = true;
            }


            if (bevatFout) {
                knop.parent().parent().parent().parent().parent().attr("action", "AnalyseKost4Punt3");
                knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn").submit();
            } else {
                knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn").addClass("animated zoomOut");
                setTimeout(function () {
                    knop.parent().parent().parent().parent().parent().attr("action", "AnalyseKost4Punt3");
                    knop.parent().parent().parent().parent().parent().submit();
                },
                    1000);
            }
        });


    $("#vulInKnop4")
        .click(function () {
            var knop = $(this);

            var beschrijving = $("#type4").val().trim();
            var bedrag = $("#Bedrag4").val().trim();

            var bevatFout = false;
            if (beschrijving === "" && bedrag === "") {
                bevatFout = true;
            }
            var re = new RegExp("[1-9][0-9]*([,][0-9]+)?");
            if (!re.test(bedrag)) {
                bevatFout = true;
            }


            if (bevatFout) {
                knop.parent().parent().parent().parent().parent().attr("action", "AnalyseKost4Punt4");
                knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn").submit();
            } else {
                knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn").addClass("animated zoomOut");
                setTimeout(function () {
                    knop.parent().parent().parent().parent().parent().attr("action", "AnalyseKost4Punt4");
                    knop.parent().parent().parent().parent().parent().submit();
                },
                    1000);
            }
        });
});