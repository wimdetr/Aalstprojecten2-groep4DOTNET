$(document).ready(function () {
    var tijd = 800;
    if ($("#animatiesAanUit").text().trim() === "Animaties aanzetten") {
        tijd = 0;
    }
    $("#vulInKnop1")
        .click(function () {
            localStorage.setItem("scrollPos", $(window).scrollTop());
            var knop = $(this);

            var beschrijving = $("#type1").val().trim();
            var jaarbedrag = $("#Bedrag1").val().trim();

            var bevatFout = false;
            if (beschrijving === "" && jaarbedrag === "") {
                bevatFout = true;
            }
            var re = new RegExp("[1-9][0-9]*([,][0-9]+)?");
            if (!re.test(jaarbedrag)) {
                bevatFout = true;
            }


            if (bevatFout) {
                knop.parent().parent().parent().parent().parent().attr("action", "AnalyseKost3Punt1");
                knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn").submit();
            } else {
                knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn");
                if (tijd !== 0) {
                    knop.parent().parent().parent().parent().parent().addClass("animated zoomOut");
                }
                setTimeout(function () {
                    knop.parent().parent().parent().parent().parent().attr("action", "AnalyseKost3Punt1");
                    knop.parent().parent().parent().parent().parent().submit();
                },
                    tijd);
            }
        });

    $("#vulInKnop2")
        .click(function () {
            localStorage.setItem("scrollPos", $(window).scrollTop());
            var knop = $(this);

            var beschrijving = $("#type2").val().trim();
            var jaarbedrag = $("#Bedrag2").val().trim();

            var bevatFout = false;
            if (beschrijving === "" && jaarbedrag === "") {
                bevatFout = true;
            }
            var re = new RegExp("[1-9][0-9]*([,][0-9]+)?");
            if (!re.test(jaarbedrag)) {
                bevatFout = true;
            }


            if (bevatFout) {
                knop.parent().parent().parent().parent().parent().attr("action", "AnalyseKost3Punt2");
                knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn").submit();
            } else {
                knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn");
                if (tijd !== 0) {
                    knop.parent().parent().parent().parent().parent().addClass("animated zoomOut");
                }
                setTimeout(function () {
                    knop.parent().parent().parent().parent().parent().attr("action", "AnalyseKost3Punt2");
                    knop.parent().parent().parent().parent().parent().submit();
                },
                    tijd);
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
                $(this).parent().parent().parent().parent().siblings(".invulgegevens").removeClass("verbergFormulier animated zoomOut").addClass("toonFormulier");
                if (tijd !== 0) {
                    $(this).parent().parent().parent().parent().siblings(".invulgegevens").addClass("animated zoomIn");
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
            var beschrijving = waarden[2];
            var bedrag = waarden[3].substring(2, waarden[3].length);


            if (parseFloat(bedrag) === 0) {
                bedrag = "";
            }

            $("#volgendeLijn2").val(lijnId);
            $("#type2").val(beschrijving);
            $("#Bedrag2").val(bedrag);

            if ($(this).parent().parent().parent().parent().siblings(".invulgegevens").hasClass("verbergFormulier")) {
                $(this).parent().parent().parent().parent().siblings(".invulgegevens").removeClass("verbergFormulier animated zoomOut").addClass("toonFormulier");
                if (tijd !== 0) {
                    $(this).parent().parent().parent().parent().siblings(".invulgegevens").addClass("animated zoomIn");
                }
            }
        });
});