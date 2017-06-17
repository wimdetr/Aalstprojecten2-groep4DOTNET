$(document)
    .ready(function () {
        var temp = window.location.href.split("/");
        var pagina = temp[temp.length - 1];
        var tijd = 1000;
        if ($("#animatiesAanUit").text().trim() === "Animaties aanzetten") {
            tijd = 0;
        }
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
                                if (pagina !== "Index") {
                                    if (!$(this).hasClass("hoofdSchermTooltip")) {
                                        $(this).find(".tooltiptext").removeClass("verbergTooltip tooltipFadeOut").addClass("toonTooltip");
                                        if (tijd !== 0) {
                                            $(this).find(".tooltiptext").addClass("tooltipFadeIn");
                                        }
                                    }
                                } else {
                                    $(this).find(".tooltiptext").removeClass("verbergTooltip tooltipFadeOut").addClass("toonTooltip");
                                    if (tijd !== 0) {
                                        $(this).find(".tooltiptext").addClass("tooltipFadeIn");
                                    }
                                }
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
                            $(this).find(".tooltiptext").removeClass("toonTooltip tooltipFadeIn");
                            if (tijd !== 0) {
                                $(this).find(".tooltiptext").addClass("tooltipFadeOut");
                            }
                            var tip = $(this);
                            setTimeout(function() {
                                    tip.find(".tooltiptext").addClass("verbergTooltip");
                                },
                                tijd);
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
                        if (pagina !== "Index") {
                            if (!$(this).hasClass("hoofdSchermTooltip")) {
                                volgordeWaarden.push($(this).data("volgorde"));
                            }
                        } else {
                            volgordeWaarden.push($(this).data("volgorde"));
                        }
                    }
                });
            volgordeWaarden = volgordeWaarden.sort((a, b) => a - b);
        }

        function verbergTooltipsOpPositie() {
            $(".tooltip")
                .each(function() {
                    if ($(this).data("volgorde") === volgordeWaarden[huidige]) {
                        $(this).find(".tooltiptext").removeClass("toonTooltip tooltipFadeIn");
                        if (tijd !== 0) {
                            $(this).find(".tooltiptext").addClass("tooltipFadeOut");
                        }
                        var tip = $(this);
                        setTimeout(function () {
                            tip.find(".tooltiptext").addClass("verbergTooltip");
                        },
                            tijd);
                    }
                });
        }

        function toonTooltipsOpPositie() {
            $(".tooltip:not(.geenTooltip)").each(function () {
                if ($(this).data("volgorde") === volgordeWaarden[huidige]) {
                    if (pagina !== "Index") {
                        if (!$(this).hasClass("hoofdSchermTooltip")) {
                            $(this).find(".tooltiptext").removeClass("verbergTooltip tooltipFadeOut").addClass("toonTooltip");
                            if (tijd !== 0) {
                                $(this).find(".tooltiptext").addClass("tooltipFadeIn");
                            }
                        }
                    } else {
                        $(this).find(".tooltiptext").removeClass("verbergTooltip tooltipFadeOut").addClass("toonTooltip");
                        if (tijd !== 0) {
                            $(this).find(".tooltiptext").addClass("tooltipFadeIn");
                        }
                    }
                }
            });
        }
    });