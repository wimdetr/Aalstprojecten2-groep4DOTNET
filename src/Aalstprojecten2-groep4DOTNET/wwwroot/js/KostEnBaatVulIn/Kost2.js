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
                knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn").submit();
            } else {
                knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn").addClass("animated zoomOut");
                setTimeout(function() {
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
            $("#beschrijving").val(beschrijving);
            $("#jaarbedrag").val(bedrag);

            if ($(this).parent().parent().parent().parent().siblings(".invulgegevens").hasClass("verbergFormulier")) {
                $(this).parent().parent().parent().parent().siblings(".invulgegevens").removeClass("verbergFormulier animated zoomOut").addClass("toonFormulier animated zoomIn");
            }
        });
});