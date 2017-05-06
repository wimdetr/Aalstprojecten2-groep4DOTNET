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
                    800);
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
                    800);
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
                    800);
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
                    800);
            }
        });

    $(".editKnop1")
        .click(function () {
            var knop = $(this);
            var waarden = knop.parent().siblings("td").map(function () {
                return $(this).text();
            });

            var lijnId = waarden[0];
            var beschrijving = waarden[2];
            var bedrag = waarden[3].substring(2, waarden[3].length).replace(".", "");
            if (parseFloat(bedrag) === 0) {
                bedrag = "";
            }

            $("#volgendeLijn1").val(lijnId);
            $("#type1").val(beschrijving);
            $("#Bedrag1").val(bedrag);

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
            var bedrag = waarden[3].substring(2, waarden[3].length);
            if (parseFloat(uren) === 0) {
                uren = "";
            }

            if (parseFloat(bedrag) === 0) {
                bedrag = "";
            }

            $("#volgendeLijn2").val(lijnId);
            $("#uren").val(uren);
            $("#brutoMaandloonBegeleider").val(bedrag);

            if ($(this).parent().parent().parent().parent().siblings(".invulgegevens").hasClass("verbergFormulier")) {
                $(this).parent().parent().parent().parent().siblings(".invulgegevens").removeClass("verbergFormulier animated zoomOut").addClass("toonFormulier animated zoomIn");
            }
        });
    $(".editKnop3")
        .click(function () {
            var knop = $(this);
            var waarden = knop.parent().siblings("td").map(function () {
                return $(this).text();
            });

            var lijnId = waarden[0];
            var beschrijving = waarden[2];
            var bedrag = waarden[3].substring(2, waarden[3].length).replace(".", "");
            if (parseFloat(bedrag) === 0) {
                bedrag = "";
            }

            $("#volgendeLijn3").val(lijnId);
            $("#type3").val(beschrijving);
            $("#Bedrag3").val(bedrag);

            if ($(this).parent().parent().parent().parent().siblings(".invulgegevens").hasClass("verbergFormulier")) {
                $(this).parent().parent().parent().parent().siblings(".invulgegevens").removeClass("verbergFormulier animated zoomOut").addClass("toonFormulier animated zoomIn");
            }
        });

    $(".editKnop4")
        .click(function () {
            var knop = $(this);
            var waarden = knop.parent().siblings("td").map(function () {
                return $(this).text();
            });

            var lijnId = waarden[0];
            var beschrijving = waarden[2];
            var bedrag = waarden[3].substring(2, waarden[3].length);


            if (parseFloat(bedrag) === 0) {
                bedrag = "";
            }

            $("#volgendeLijn4").val(lijnId);
            $("#type4").val(beschrijving);
            $("#Bedrag4").val(bedrag);

            if ($(this).parent().parent().parent().parent().siblings(".invulgegevens").hasClass("verbergFormulier")) {
                $(this).parent().parent().parent().parent().siblings(".invulgegevens").removeClass("verbergFormulier animated zoomOut").addClass("toonFormulier animated zoomIn");
            }
        });
});