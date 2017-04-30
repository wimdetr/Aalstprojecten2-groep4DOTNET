$(document).ready(function () {
    $("#vulInKnop")
        .click(function () {
            var knop = $(this);

            var jaarbedrag = $("#jaarbedragOmzetverlies").val().trim();
            var percent = $("#besparing").val().trim();

            var bevatFout = false;
            if (percent === "" && jaarbedrag === "") {
                bevatFout = true;
            }
            var re = new RegExp("[1-9][0-9]*([,][0-9]+)?");
            if (!re.test(jaarbedrag)) {
                bevatFout = true;
            }
            var re2 = new RegExp("^[1-9][0-9]?$|^100$");
            if (re2.test(percent)) {
                bevatFout = true;
            }


            if (bevatFout) {
                knop.parent().parent().parent().parent().parent().attr("action", "AnalyseBaat3Punt1");
                knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn").submit();
            } else {
                knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn").addClass("animated zoomOut");
                setTimeout(function () {
                    knop.parent().parent().parent().parent().parent().attr("action", "AnalyseBaat3Punt1");
                    knop.parent().parent().parent().parent().parent().submit();
                },
                    1000);
            }
        });

});