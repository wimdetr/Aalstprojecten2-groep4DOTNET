$(document).ready(function () {
    var tijd = 800;
    if ($("#animatiesAanUit").text().trim() === "Animaties aanzetten") {
        tijd = 0;
    }
    $("#vulInKnop")
        .click(function () {
            localStorage.setItem("scrollPos", $(window).scrollTop());
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
                knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn").submit();
            } else {
                knop.parent().parent().parent().parent().parent().removeClass("toonFormulier animated zoomIn");
                if (tijd !== 0) {
                    knop.parent().parent().parent().parent().parent().addClass("animated zoomOut");
                }
                setTimeout(function () {
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
            var functie = waarden[2];
            var uren = waarden[3];
            var maandloon = waarden[4].substring(2, waarden[4].length).replace(".", "");
            var doelgroep = waarden[5];
            var ondersteurningsPremie = waarden[6];
            var maanden = waarden[8];
            var premie = waarden[9].substring(2, waarden[4].length).replace(".", "");
            if (parseFloat(uren) === 0) {
                uren = "";
            }
            if (parseFloat(maandloon) === 0) {
                maandloon = "";
            }
            if (parseFloat(maanden) === 0) {
                maanden = "";
            }
            if (parseFloat(premie) === 0) {
                premie = "";
            }

            $("#volgendeLijn1").val(lijnId);
            $("#Functie").val(functie);
            $("#UrenPerWeek").val(uren);
            $("#BrutoMaandloonFulltime").val(maandloon);
            $("#dropDown1").val(doelgroep);
            $("#aantalMaandenIBO").val(maanden);
            $("#productiviteitsPremie").val(premie);
            $("#dropDown2").val(ondersteurningsPremie);

            if ($(this).parent().parent().parent().parent().siblings(".invulgegevens").hasClass("verbergFormulier")) {
                $(this).parent().parent().parent().parent().siblings(".invulgegevens").removeClass("verbergFormulier animated zoomOut").addClass("toonFormulier");
                if (tijd !== 0) {
                    $(this).parent().parent().parent().parent().siblings(".invulgegevens").addClass("animated zoomIn");
                }
            }
        });
});