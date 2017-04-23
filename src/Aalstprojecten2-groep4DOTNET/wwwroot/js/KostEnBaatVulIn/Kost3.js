$(document).ready(function () {
    $("#vulInKnop1")
        .click(function () {
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
                knop.parent().attr("action", "AnalyseKost3Punt1");
                knop.parent().removeClass("toonFormulier animated zoomIn").submit();
            } else {
                knop.parent().removeClass("toonFormulier animated zoomIn").addClass("animated zoomOut");
                setTimeout(function () {
                    knop.parent().attr("action", "AnalyseKost3Punt1");
                    knop.parent().submit();
                },
                    1000);
            }
        });

    $("#vulInKnop2")
        .click(function () {
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
                knop.parent().attr("action", "AnalyseKost3Punt2");
                knop.parent().removeClass("toonFormulier animated zoomIn").submit();
            } else {
                knop.parent().removeClass("toonFormulier animated zoomIn").addClass("animated zoomOut");
                setTimeout(function () {
                    knop.parent().attr("action", "AnalyseKost3Punt2");
                    knop.parent().submit();
                },
                    1000);
            }
        });
});