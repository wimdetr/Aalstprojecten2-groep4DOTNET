$(document).ready(function() {
    $("#searchBarMetMeerShtijlDanDieVanNiels")
        .click(function() {
            $(this).addClass("fancySearch");
        });
    $("#searchBarMetMeerShtijlDanDieVanNiels")
        .on("input",
            function() {
                var organisatie = $("#searchBarMetMeerShtijlDanDieVanNiels").val();
                $(".cards")
                    .each(function () {
                        var zichtbaar = true;
                        var mijnOrganisatie = $(this).find("#naam").text();

                        if (mijnOrganisatie.toLowerCase().indexOf(organisatie.toLowerCase()) === -1) {
                            zichtbaar = false;
                        }
                        if (zichtbaar) {
                            $(this).removeClass("zetCardOnzichtbaar").addClass("zetCardZichtbaar");
                        } else {
                            $(this).removeClass("zetCardZichtbaar").addClass("zetCardOnzichtbaar");
                        }
                    });
                if ($(".zetCardZichtbaar").length === 0) {
                    $("#geenAnalysesDiv").removeClass("verbergPrent").addClass("toonPrent");
                } else {
                    $("#geenAnalysesDiv").removeClass("toonPrent").addClass("verbergPrent");
                }
            });
    $("#organisatieVeld")
        .on("input", function() {
            filterArchief();
        });
    $("#afdelingVeld")
        .on("input", function () {
            filterArchief();
        });
    $("#locatieVeld")
        .on("input", function () {
            filterArchief();
        });

});

function filterArchief() {
    var organisatie = $("#organisatieVeld").val();
    var afdeling = $("#afdelingVeld").val();
    var locatie = $("#locatieVeld").val();

    $(".cards")
        .each(function () {
            var zichtbaar = true;
            var mijnOrganisatie = $(this).find("#naam").text();
            var mijnAfdeling = $(this).find("#afdeling").text();
            var mijnLocatie = $(this).find("#locatie").text();

            if (mijnOrganisatie.toLowerCase().indexOf(organisatie.toLowerCase()) === -1) {
                zichtbaar = false;
            }
            if (mijnAfdeling.toLowerCase().indexOf(afdeling.toLowerCase()) === -1) {
                zichtbaar = false;
            }
            if (mijnLocatie.toLowerCase().indexOf(locatie.toLowerCase()) === -1) {
                zichtbaar = false;
            }
            if (zichtbaar) {
                $(this).removeClass("zetCardOnzichtbaar").addClass("zetCardZichtbaar");
            } else {
                $(this).removeClass("zetCardZichtbaar").addClass("zetCardOnzichtbaar");
            }
        });
    if ($(".zetCardZichtbaar").length === 0) {
        $("#geenAnalysesDiv").removeClass("verbergPrent").addClass("toonPrent");
    } else {
        $("#geenAnalysesDiv").removeClass("toonPrent").addClass("verbergPrent");
    }
}