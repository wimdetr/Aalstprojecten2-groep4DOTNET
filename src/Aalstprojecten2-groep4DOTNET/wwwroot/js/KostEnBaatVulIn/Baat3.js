$(document).ready(function () {
    var tijd = 800;
    if ($("#animatiesAanUit").text().trim() === "Animaties aanzetten") {
        tijd = 0;
    }
    $("#vulInKnop")
        .click(function () {
            localStorage.setItem("scrollPos", $(window).scrollTop());
            var knop = $(this);

            var jaarbedrag = $("#jaarbedragOmzetverlies").val().trim();
            var percent = $("#besparing").val().trim();

            var bevatFout = false;
            if (percent === "" && jaarbedrag === "") {
                bevatFout = true;
            }
            var re = new RegExp("[1-9][0-9]*([,][0-9]+)?");
            if (jaarbedrag !== "" && !re.test(jaarbedrag)) {
                bevatFout = true;
            }
            var re2 = new RegExp("^[1-9][0-9]?$|^100$");
            if (percent !== "" && re2.test(percent)) {
                bevatFout = true;
            }


            if (bevatFout) {
                knop.parent().parent().parent().parent().parent().attr("action", "AnalyseBaat3Punt1");
                knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn").submit();
            } else {
                knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn");
                if (tijd !== 0) {
                    knop.parent().parent().parent().parent().parent().addClass("animated zoomOut");
                }
                setTimeout(function () {
                    knop.parent().parent().parent().parent().parent().attr("action", "AnalyseBaat3Punt1");
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
            var jaarbedrag = waarden[2].substring(2, waarden[2].length).replace(".", "");
            var besparing = waarden[3].substring(0, waarden[3].length-1);
            if (parseFloat(jaarbedrag) === 0) {
                jaarbedrag = "";
            }
            if (parseFloat(besparing) === 0) {
                besparing = "";
            }

            $("#volgendeLijn1").val(lijnId);
            $("#jaarbedragOmzetverlies").val(jaarbedrag);
            $("#besparing").val(besparing);

            if ($(this).parent().parent().parent().parent().parent().siblings(".invulgegevens").hasClass("verbergFormulier")) {
                $(this).parent().parent().parent().parent().parent().siblings(".invulgegevens").removeClass("verbergFormulier animated zoomOut").addClass("toonFormulier");
                if (tijd !== 0) {
                    $(this).parent().parent().parent().parent().parent().siblings(".invulgegevens").addClass("animated zoomIn");
                }
            }
        });
});