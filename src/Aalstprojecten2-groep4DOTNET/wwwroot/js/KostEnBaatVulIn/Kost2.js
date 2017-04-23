$(document).ready(function () {
    $("#vulInKnop")
        .click(function () {
            var knop = $(this);

            var beschrijving = $("#beschrijving").val().trim();
            var jaarbedrag = $("#jaarbedrag").val().trim();

            var bevatFout = false;
            if (beschrijving === "" && jaarbedrag === "") {
                bevatFout = true;
            }
            var re = new RegExp("[1-9][0-9]*([,][0-9]+)?");
            if (!re.test(jaarbedrag)) {
                bevatFout = true;
            }


            if (bevatFout) {
                knop.parent().removeClass("toonFormulier animated zoomIn").submit();
            } else {
                knop.parent().removeClass("toonFormulier animated zoomIn").addClass("animated zoomOut");
                setTimeout(function() {
                        knop.parent().submit();
                    },
                    1000);
            }
        });
});