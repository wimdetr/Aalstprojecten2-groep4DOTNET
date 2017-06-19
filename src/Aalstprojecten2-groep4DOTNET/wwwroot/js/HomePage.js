$(document)
    .ready(function() {
        var aantalPerPagina = 10;
        var huidigePagina = 1;
        var aantalPaginas = Math.floor($(".analyseKaarten").length / aantalPerPagina);
        if ($(".analyseKaarten").length % aantalPerPagina > 0) {
            aantalPaginas++;
        }
        $("#paginaLabel").text(huidigePagina + "/" + aantalPaginas);
        if ($(".analyseKaarten").length <= aantalPerPagina) {
            $(".eerstePagina").prop("disabled", true);
            $(".vorigePagina").prop("disabled", true);
            $(".volgendePagina").prop("disabled", true);
            $(".laatstePagina").prop("disabled", true);
            $("#knoppenBlok").removeClass("toonKnoppenBlok").addClass("verbergKnoppenBlok");
            toonAnalyses(1, $(".analyseKaarten").length);
        } else {
            $(".eerstePagina").prop("disabled", true);
            $(".vorigePagina").prop("disabled", true);
            $(".volgendePagina").prop("disabled", false);
            $(".laatstePagina").prop("disabled", false);
            $("#knoppenBlok").removeClass("verbergKnoppenBlok").addClass("toonKnoppenBlok");
            toonAnalyses(1, aantalPerPagina);
        }
        $("#choiceBoxAantal")
            .change(function() {
                if ($(this).val() === "Alle") {
                    aantalPerPagina = $(".analyseKaarten").length;
                } else {
                    aantalPerPagina = $(this).val();
                }
                aantalPaginas = Math.floor($(".analyseKaarten").length / aantalPerPagina);
                if ($(".analyseKaarten").length % aantalPerPagina > 0) {
                    aantalPaginas++;
                }
                if ($(".analyseKaarten").length <= aantalPerPagina) {
                    $(".eerstePagina").prop("disabled", true);
                    $(".vorigePagina").prop("disabled", true);
                    $(".volgendePagina").prop("disabled", true);
                    $(".laatstePagina").prop("disabled", true);
                    $("#knoppenBlok").removeClass("toonKnoppenBlok").addClass("verbergKnoppenBlok");
                    toonAnalyses(1, $(".analyseKaarten").length);
                } else {
                    $(".eerstePagina").prop("disabled", true);
                    $(".vorigePagina").prop("disabled", true);
                    $(".volgendePagina").prop("disabled", false);
                    $(".laatstePagina").prop("disabled", false);
                    $("#knoppenBlok").removeClass("verbergKnoppenBlok").addClass("toonKnoppenBlok");
                    toonAnalyses(1, aantalPerPagina);
                }
                huidigePagina = 1;
                $("#paginaLabel").text(huidigePagina + "/" + aantalPaginas);
            });
        $(".eerstePagina")
            .click(function () {
                huidigePagina = 1;
                $(".eerstePagina").prop("disabled", true);
                $(".vorigePagina").prop("disabled", true);
                $(".volgendePagina").prop("disabled", false);
                $(".laatstePagina").prop("disabled", false);
                toonAnalyses((huidigePagina - 1) * aantalPerPagina + 1, (huidigePagina) * aantalPerPagina);
                $("#paginaLabel").text(huidigePagina + "/" + aantalPaginas);
            });
        $(".vorigePagina")
            .click(function () {
                huidigePagina -= 1;
                if (huidigePagina === 1) {
                    $(".eerstePagina").prop("disabled", true);
                    $(".vorigePagina").prop("disabled", true);
                }
                $(".volgendePagina").prop("disabled", false);
                $(".laatstePagina").prop("disabled", false);
                toonAnalyses((huidigePagina - 1) * aantalPerPagina + 1, (huidigePagina) * aantalPerPagina);
                $("#paginaLabel").text(huidigePagina + "/" + aantalPaginas);
            });
        $(".volgendePagina")
            .click(function() {
                huidigePagina += 1;
                if (huidigePagina === aantalPaginas) {
                    $(".volgendePagina").prop("disabled", true);
                    $(".laatstePagina").prop("disabled", true);
                }
                $(".eerstePagina").prop("disabled", false);
                $(".vorigePagina").prop("disabled", false);
                toonAnalyses((huidigePagina - 1) * aantalPerPagina + 1, (huidigePagina) * aantalPerPagina);
                $("#paginaLabel").text(huidigePagina + "/" + aantalPaginas);
            });
        $(".laatstePagina")
            .click(function() {
                huidigePagina = aantalPaginas;
                $(".volgendePagina").prop("disabled", true);
                $(".laatstePagina").prop("disabled", true);
                $(".eerstePagina").prop("disabled", false);
                $(".vorigePagina").prop("disabled", false);
                toonAnalyses((huidigePagina - 1) * aantalPerPagina + 1, (huidigePagina) * aantalPerPagina);
                $("#paginaLabel").text(huidigePagina + "/" + aantalPaginas);
            });

        //in mensentaal beginnen bij kaart 1 en tot en met de hoeveelste we willen laten zien.
        function toonAnalyses(eersteKaart, laatsteKaart) {
            var kaarten = $(".analyseKaarten");
            var eerste = eersteKaart - 1;
            var laatste = laatsteKaart - 1;

            for (var i = 0; i < kaarten.length; i++) {
                if (i >= eerste && i <= laatste) {
                    kaarten[i].classList.remove("verbergAnalyseKaart");
                    kaarten[i].classList.add("toonAnalyseKaart");
                } else {
                    kaarten[i].classList.remove("toonAnalyseKaart");
                    kaarten[i].classList.add("verbergAnalyseKaart");
                }
            }
        }
    });


