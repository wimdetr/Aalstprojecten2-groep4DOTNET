$(document).ready(function () {
    $("#vulInKnop1")
        .click(function () {
            var knop = $(this);

            var beschrijving = $("#beschrijving1").val().trim();
            var jaarbedrag = $("#jaarbedrag1").val().trim();

            var bevatFout = false;
            if (beschrijving === "" && jaarbedrag === "") {
                bevatFout = true;
            }
            var re = new RegExp("[1-9][0-9]*([,][0-9]+)?");
            if (!re.test(jaarbedrag)) {
                bevatFout = true;
            }


            if (bevatFout) {
                knop.parent().attr("action", "AnalyseBaat2Punt1");
                knop.parent().removeClass("toonFormulier animated zoomIn").submit();
            } else {
                knop.parent().removeClass("toonFormulier animated zoomIn").addClass("animated zoomOut");
                setTimeout(function () {
                    knop.parent().attr("action", "AnalyseBaat2Punt1");
                    knop.parent().submit();
                },
                    1000);
            }
        });

    $("#vulInKnop2")
        .click(function () {
            var knop = $(this);

            var beschrijving = $("#beschrijving2").val().trim();
            var jaarbedrag = $("#jaarbedrag2").val().trim();

            var bevatFout = false;
            if (beschrijving === "" && jaarbedrag === "") {
                bevatFout = true;
            }
            var re = new RegExp("[1-9][0-9]*([,][0-9]+)?");
            if (!re.test(jaarbedrag)) {
                bevatFout = true;
            }


            if (bevatFout) {
                knop.parent().attr("action", "AnalyseBaat2Punt2");
                knop.parent().removeClass("toonFormulier animated zoomIn").submit();
            } else {
                knop.parent().removeClass("toonFormulier animated zoomIn").addClass("animated zoomOut");
                setTimeout(function () {
                    knop.parent().attr("action", "AnalyseBaat2Punt2");
                    knop.parent().submit();
                },
                    1000);
            }
        });
});