$(document)
    .ready(function () {
        var huidige = 0;
        var volgordeWaarden = new Array();
        maakVolgordeArray();
        $("#activeerTooltipKnop")
            .click(function() {
                if (!$(this).hasClass("active")) {
                    huidige = 0;
                    $(".verbergPijl").removeClass("verbergPijl").addClass("toonPijl");
                    $(".tooltip:not(.geenTooltip)")
                        .each(function () {
                            if ($(this).data("volgorde") === volgordeWaarden[huidige]) {
                                $(this).find(".tooltiptext").removeClass("verbergTooltip tooltipFadeOut").addClass("toonTooltip tooltipFadeIn");
                            }
                        });
                }
            });
        $("#deactiveerTooltipKnop")
            .click(function() {
                if (!$(this).hasClass("active")) {
                    huidige = 0;
                    $(".toonPijl").removeClass("toonPijl").addClass("verbergPijl");
                    $(".tooltip:not(.geenTooltip)")
                        .each(function() {
                            $(this).find(".tooltiptext").removeClass("toonTooltip tooltipFadeIn").addClass("tooltipFadeOut");
                            var tip = $(this);
                            setTimeout(function() {
                                    tip.find(".tooltiptext").addClass("verbergTooltip");
                                },
                                1000);
                        });
                }
            });
        $("#linkerPijl")
            .click(function () {
                verbergTooltipsOpPositie();
                huidige = (huidige - 1 + volgordeWaarden.length) % volgordeWaarden.length;
                toonTooltipsOpPositie();
            });
        $("#rechterPijl")
            .click(function () {
                verbergTooltipsOpPositie();
                huidige = (huidige + 1) % volgordeWaarden.length;
                toonTooltipsOpPositie();
            });


        function maakVolgordeArray() {
            $(".tooltip:not(.geenTooltip)")
                .each(function() {
                    if (volgordeWaarden.indexOf($(this).data("volgorde")) === -1) {
                        volgordeWaarden.push($(this).data("volgorde"));
                    }
                });
            volgordeWaarden = volgordeWaarden.sort((a, b) => a - b);
        }

        function verbergTooltipsOpPositie() {
            $(".tooltip")
                .each(function() {
                    if ($(this).data("volgorde") === volgordeWaarden[huidige]) {
                        $(this).find(".tooltiptext").removeClass("toonTooltip tooltipFadeIn").addClass("tooltipFadeOut");
                        var tip = $(this);
                        setTimeout(function () {
                            tip.find(".tooltiptext").addClass("verbergTooltip");
                        },
                            1000);
                    }
                });
        }

        function toonTooltipsOpPositie() {
            $(".tooltip:not(.geenTooltip)").each(function () {
                    if ($(this).data("volgorde") === volgordeWaarden[huidige]) {
                        $(this).find(".tooltiptext").removeClass("verbergTooltip tooltipFadeOut").addClass("toonTooltip tooltipFadeIn");
                    }
                });
        }
    });