$(document)
    .ready(function () {
        $("#gelezenKnop").prop("disabled", true);
        $("#verwijderKnop").prop("disabled", true);
        $(".mailKaartje")
            .click(function () {
                var mailKaart = $(this);
                if ($("#selecteerKnop").data("geselecteerd") === true) {
                    if (mailKaart.hasClass("isGeselecteerd")) {
                        mailKaart.removeClass("isGeselecteerd");
                        mailKaart.children("input").prop("checked", false);
                        var heeftGeselecteerde = false;
                        var checkboxen = $(".geselecteerdeMailCheckBox");
                        for (var i = 0; i < checkboxen.length; i++) {
                            if (checkboxen[i].checked === true) {
                                heeftGeselecteerde = true;
                            }
                        }
                        if (heeftGeselecteerde === false) {
                            if ($("#gelezenKnop").prop("disabled") === false) {
                                $("#gelezenKnop").prop("disabled", true);
                            }
                            if ($("#verwijderKnop").prop("disabled") === false) {
                                $("#verwijderKnop").prop("disabled", true);
                            }
                        }
                        
                    } else {
                        mailKaart.addClass("isGeselecteerd");
                        mailKaart.children("input").prop("checked", true);
                        if ($("#gelezenKnop").prop("disabled") === true) {
                            $("#gelezenKnop").prop("disabled", false);
                        }
                        if ($("#verwijderKnop").prop("disabled") === true) {
                            $("#verwijderKnop").prop("disabled", false);
                        }
                    }
                } else {
                    if (mailKaart.find("#dataDiv").hasClass("nogNietGelezen")) {
                        mailKaart.find("a")[0].click();
                    } else {
                        if (mailKaart.hasClass("verbergMail")) {
                            mailKaart.removeClass("verbergMail").addClass("toonMail");
                        }
                        $("#geopendeMail").css("display", "block");
                        $("#afzenderNaam").text(mailKaart.find("#dataDiv").data("naam"));
                        $("#mailDatum").text(mailKaart.find("#dataDiv").data("datum"));
                        $("#afzenderMail").text(mailKaart.find("#dataDiv").data("email"));
                        $("#mailOnderwerp").text(mailKaart.find("#dataDiv").data("onderwerp"));
                        $("#mailInhoud").text(mailKaart.find("#dataDiv").data("inhoud"));
                    }
                }
            });
        $("#selecteerKnop")
            .click(function() {
                if ($(this).data("geselecteerd") === true) {
                    $(this).data("geselecteerd", false);

                    $("#gelezenKnop").prop("disabled", true);
                    $("#verwijderKnop").prop("disabled", true);

                    $(".mailKaartje").removeClass("isGeselecteerd");
                    var checkboxen = $(".geselecteerdeMailCheckBox");
                    for (var i = 0; i < checkboxen.length; i++) {
                        if (checkboxen[i].checked === true) {
                            checkboxen[i].checked = false;
                        }
                    }
                } else {
                    $(this).data("geselecteerd", true);
                }
            });
        $("#gelezenKnop")
            .click(function() {
                $(this).submit();
            });
        $("#verwijderKnop")
            .click(function() {
                $(this).submit();
            });
        $("#antwoordMailKnop")
            .click(function() {

            });
    });