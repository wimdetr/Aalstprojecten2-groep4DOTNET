$(document).ready(function () {
    $("#vulInKnop")
        .click(function () {
            var knop = $(this);

            var functie = $("#Functie").val().trim();
            var aantalUrenPerWeek = $("#UrenPerWeek").val().trim();
            var brutoMaandloonFulltime = $("#BrutoMaandloonFulltime").val().trim();
            var doelgroep = $("#dropDown1").val().trim();
            var vop = $("#dropDown2").val().trim();
            var aantalMaandenIBO = $("#aantalMaandenIBO").val().trim();
            var premieIBO = $("#productiviteitsPremie").val().trim();


            var bevatFout = false;
            if (functie === "" && aantalUrenPerWeek === "" && brutoMaandloonFulltime === "" && doelgroep === "Kies uw doelgroep" && vop === "Vlaamse ondersteuningspremie" && aantalMaandenIBO === "" && premieIBO === "") {
                bevatFout = true;
            }
            var re = new RegExp("[1-9][0-9]*([,][0-9]+)?");
            if (!re.test(brutoMaandloonFulltime)) {
                bevatFout = true;
            }
            if (!re.test(premieIBO)) {
                bevatFout = true;
            }
            var re2 = new RegExp("[1-9][0-9]*");
            if (!re2.test(aantalUrenPerWeek)) {
                bevatFout = true;
            }
            if (!re2.test(aantalMaandenIBO)) {
                bevatFout = true;
            }

            if (bevatFout) {
                knop.parent().removeClass("toonFormulier animated zoomIn").submit();
            } else {
                knop.parent().removeClass("toonFormulier animated zoomIn").addClass("animated zoomOut");
                setTimeout(function () {
                    knop.parent().submit();
                },
                    1000);
            }
        });
});