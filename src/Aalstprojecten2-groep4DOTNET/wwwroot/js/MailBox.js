$(document)
    .ready(function () {
        var mailKaart;
        $("#gelezenKnop").prop("disabled", true);
        $("#verwijderKnop").prop("disabled", true);
        $(".mailKaartje")
            .click(function () {
                mailKaart = $(this);
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
                        if ($("#geopendeMail").hasClass("toonMail")) {
                            $("#geopendeMail").removeClass("toonMail animated fadeIn").addClass("animated fadeOut");
                            setTimeout(function() {
                                    $("#geopendeMail").addClass("verbergMail");
                                    mailKaart.find("a")[0].click();
                                },
                                800);
                        } else {
                            mailKaart.find("a")[0].click();
                        }
                    } else {
                        if ($("#geopendeMail").hasClass("verbergMail")) {
                            $("#afzenderNaam").text(mailKaart.find("#dataDiv").data("naam"));
                            $("#mailDatum").text(mailKaart.find("#dataDiv").data("datum"));
                            $("#afzenderMail").text(mailKaart.find("#dataDiv").data("email"));
                            $("#mailOnderwerp").text(mailKaart.find("#dataDiv").data("onderwerp"));
                            $("#mailInhoud").html(mailKaart.find("#dataDiv").data("inhoud").split("\n").join("<br>"));
                            $("#antwoordMailKnop").removeClass("verbergMail").addClass("toonMail");
                            $("#verwijderGeselecteerdeMailKnop").removeClass("verbergMail").addClass("toonMail");
                            $("#geopendeMail").data("mailid", mailKaart.find("#dataDiv").data("id"));

                            if ($("#antwoordOpMailDiv").hasClass("toonMail")) {
                                $("#antwoordOpMailDiv")
                                    .removeClass("toonMail animated fadeIn")
                                    .addClass("animated fadeOut");
                                setTimeout(function () {
                                    $("#antwoordOpMailDiv").addClass("verbergMail");
                                    $("#geopendeMail")
                                        .removeClass("verbergMail animated fadeOut")
                                        .addClass("toonMail animated fadeIn");
                                },
                                    800);
                            } else {
                                $("#geopendeMail")
                                    .removeClass("verbergMail animated fadeOut")
                                    .addClass("toonMail animated fadeIn");
                            }
                        } else {
                            $("#geopendeMail").removeClass("toonMail animated fadeIn").addClass("animated fadeOut");
                            setTimeout(function () {
                                $("#geopendeMail").addClass("verbergMail");

                                $("#afzenderNaam").text(mailKaart.find("#dataDiv").data("naam"));
                                $("#mailDatum").text(mailKaart.find("#dataDiv").data("datum"));
                                $("#afzenderMail").text(mailKaart.find("#dataDiv").data("email"));
                                $("#mailOnderwerp").text(mailKaart.find("#dataDiv").data("onderwerp"));
                                $("#mailInhoud").html(mailKaart.find("#dataDiv").data("inhoud").split("\n").join("<br>"));
                                $("#antwoordMailKnop").removeClass("verbergMail").addClass("toonMail");
                                $("#verwijderGeselecteerdeMailKnop").removeClass("verbergMail").addClass("toonMail");
                                $("#geopendeMail").data("mailid", mailKaart.find("#dataDiv").data("id"));

                                if ($("#antwoordOpMailDiv").hasClass("toonMail")) {
                                    $("#antwoordOpMailDiv")
                                        .removeClass("toonMail animated fadeIn")
                                        .addClass("animated fadeOut");
                                    setTimeout(function () {
                                        $("#antwoordOpMailDiv").addClass("verbergMail");
                                        $("#geopendeMail")
                                            .removeClass("verbergMail animated fadeOut")
                                            .addClass("toonMail animated fadeIn");
                                    },
                                        800);
                                } else {
                                    $("#geopendeMail")
                                        .removeClass("verbergMail animated fadeOut")
                                        .addClass("toonMail animated fadeIn");
                                }
                            },
                                800);
                        }

                    }
                }
            });
        $("#selecteerKnop")
            .click(function () {
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
            .click(function () {
                $(this).submit();
            });
        $("#verwijderKnop")
            .click(function () {
                $(this).submit();
            });
        $("#antwoordMailKnop")
            .click(function () {
                $("#geopendeMail").removeClass("toonMail animated fadeIn").addClass("animated fadeOut");
                $(".antwoordVeld").val("");
                setTimeout(function () {
                    $("#geopendeMail").addClass("verbergMail");
                    var naam = $("#geopendeMail").find("#afzenderNaam").text();
                    var ontvanger = naam.substring(4, naam.length) + " <";
                    var email = $("#geopendeMail").find("#afzenderMail").text();
                    ontvanger += email + ">";
                    $("#ontvagerAntwoord").text("Aan: " + ontvanger);
                    $("#onderwerpAntwoord").text("Onderwerp: Re: " + $("#mailOnderwerp").text());
                    $("#inhoudAntwoord").text("");
                    $("#inhoudVorigeMailAntwoord").text("");
                    var naam2 = $("#geopendeMail").find("#afzenderNaam").text();
                    var ontvanger2 = naam2.substring(4, naam.length) + " " + "&#60;";
                    var email2 = $("#geopendeMail").find("#afzenderMail").text();
                    ontvanger2 += email2 + "&#62;";
                    var tekst = $("#mailInhoud").html();

                    $("#inhoudVorigeMailAntwoord").html("Van: " + ontvanger2 + "<br />" + "Verstuurd: " + $("#mailDatum").text() + "<br />" + "Onderwerp: " + $("#mailOnderwerp").text() + "<br /><br /><br />" + tekst);
                    $("#antwoordOpMailDiv").removeClass("verbergMail animated fadeOut").addClass("toonMail animated fadeIn");
                }, 800);

            });
        $("#verwijderGeselecteerdeMailKnop")
            .click(function () {
                var anchor = $(this);
                var href = anchor.attr('href').split('/');
                href[3] = $("#geopendeMail").data("mailid");
                anchor.attr('href', href.join('/'));
                anchor[0].click();
            });
        $("#annuleerKnop")
            .click(function () {
                $("#antwoordOpMailDiv").removeClass("toonMail animated fadeIn").addClass("animated fadeOut");
                setTimeout(function () {
                    $("#antwoordOpMailDiv").addClass("verbergMail");
                    $("#geopendeMail").removeClass("verbergMail animated fadeOut").addClass("toonMail animated fadeIn");
                }, 800);
            });
        $("#verstuurMailKnop")
            .click(function () {
                $(this).prop("disabled", true);
                $("#antwoordOntvanger").val($("#geopendeMail").find("#afzenderMail").text());
                $("#antwoordOnderwerp").val($("#onderwerpAntwoord").text().substring(11, $("#onderwerpAntwoord").text().length));
                var inhoud = $("#inhoudAntwoord").html().split("<div>").join("\n").split("</div>").join("").split("<br>").join("").split("\r").join("") + "\n\n\n\n\n" + $("#inhoudVorigeMailAntwoord").html().replace("&lt;", "<").replace("&gt;", ">").split("<br>").join("\n").split("\r").join("");
                $("#antwoordInhoud").val(inhoud);

                $("#antwoordOpMailDiv").removeClass("toonMail animated fadeIn").addClass("animated fadeOut");
                setTimeout(function () {
                    $("#antwoordOpMailDiv").addClass("verbergMail");
                    $("#formulier").attr("action", "BeantwoordMail");
                    $("#formulier").submit();
                }, 800);
            });
    });