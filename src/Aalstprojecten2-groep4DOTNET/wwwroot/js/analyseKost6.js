$(document).ready(function () {
    $("#vraag1").click(function () {
        verbergAntwoorden($(this).prop("id"));
        if ($("#antwoord1A").hasClass("verbergAntwoord")) {
            $("#antwoord1A").removeClass("verbergAntwoord");
        }
        if ($("#antwoord1B").hasClass("verbergAntwoord")) {
            $("#antwoord1B").removeClass("verbergAntwoord");
        }

        if ($("#antwoord1A").data("geselecteerd") === true) {
            $("#linkerAntwoord").addClass("gekozen").removeClass("nietGekozen");
        } else {
            $("#linkerAntwoord").addClass("nietGekozen").removeClass("gekozen");
        }

        if ($("#antwoord1B").data("geselecteerd") === true) {
            $("#rechterAntwoord").addClass("gekozen").removeClass("nietGekozen");
        } else {
            $("#rechterAntwoord").addClass("nietGekozen").removeClass("gekozen");
        }

        $(this).removeClass("nietActief nietActief2").addClass("actief");

        $(this).find("button").removeClass("knopTitel2").addClass("knopTitel");

    });

    $("#vraag2").click(function () {
        verbergAntwoorden($(this).prop("id"));
        if ($("#antwoord2A").hasClass("verbergAntwoord")) {
            $("#antwoord2A").removeClass("verbergAntwoord");
        }
        if ($("#antwoord2B").hasClass("verbergAntwoord")) {
            $("#antwoord2B").removeClass("verbergAntwoord");
        }

        if ($("#antwoord2A").data("geselecteerd") === true) {
            $("#linkerAntwoord").addClass("gekozen").removeClass("nietGekozen");
        } else {
            $("#linkerAntwoord").addClass("nietGekozen").removeClass("gekozen");
        }

        if ($("#antwoord2B").data("geselecteerd") === true) {
            $("#rechterAntwoord").addClass("gekozen").removeClass("nietGekozen");
        } else {
            $("#rechterAntwoord").addClass("nietGekozen").removeClass("gekozen");
        }

        $(this).removeClass("nietActief nietActief2").addClass("actief");

        $(this).find("button").removeClass("knopTitel2").addClass("knopTitel");
    });

    $("#vraag3").click(function () {
        verbergAntwoorden($(this).prop("id"));
        if ($("#antwoord3A").hasClass("verbergAntwoord")) {
            $("#antwoord3A").removeClass("verbergAntwoord");
        }
        if ($("#antwoord3B").hasClass("verbergAntwoord")) {
            $("#antwoord3B").removeClass("verbergAntwoord");
        }

        if ($("#antwoord3A").data("geselecteerd") === true) {
            $("#linkerAntwoord").addClass("gekozen").removeClass("nietGekozen");
        } else {
            $("#linkerAntwoord").addClass("nietGekozen").removeClass("gekozen");
        }

        if ($("#antwoord3B").data("geselecteerd") === true) {
            $("#rechterAntwoord").addClass("gekozen").removeClass("nietGekozen");
        } else {
            $("#rechterAntwoord").addClass("nietGekozen").removeClass("gekozen");
        }

        $(this).removeClass("nietActief nietActief2").addClass("actief");

        $(this).find("button").removeClass("knopTitel2").addClass("knopTitel");
    });

    $("#vraag4").click(function () {
        verbergAntwoorden($(this).prop("id"));
        if ($("#antwoord4A").hasClass("verbergAntwoord")) {
            $("#antwoord4A").removeClass("verbergAntwoord");
        }
        if ($("#antwoord4B").hasClass("verbergAntwoord")) {
            $("#antwoord4B").removeClass("verbergAntwoord");
        }

        if ($("#antwoord4A").data("geselecteerd") === true) {
            $("#linkerAntwoord").addClass("gekozen").removeClass("nietGekozen");
        } else {
            $("#linkerAntwoord").addClass("nietGekozen").removeClass("gekozen");
        }

        if ($("#antwoord4B").data("geselecteerd") === true) {
            $("#rechterAntwoord").addClass("gekozen").removeClass("nietGekozen");
        } else {
            $("#rechterAntwoord").addClass("nietGekozen").removeClass("gekozen");
        }

        $(this).removeClass("nietActief nietActief2").addClass("actief");

        $(this).find("button").removeClass("knopTitel2").addClass("knopTitel");
    });

    $("#antwoord1A").click(function () {
        if ($("#antwoord1B").hasClass("gekozen")) {
            $("#antwoord1B").removeClass("gekozen").addClass("nietGekozen");
        }
        $(this).data("geselecteerd", true);
        $("#antwoord1B").data("geselecteerd", false);

        if (!$("#antwoord1A").hasClass("verbergAntwoord")) {
            $("#antwoord1A").addClass("verbergAntwoord");
        }

        if (!$("#antwoord1B").hasClass("verbergAntwoord")) {
            $("#antwoord1B").addClass("verbergAntwoord");
        }

        $("#antwoord2A").removeClass("verbergAntwoord");
        $("#antwoord2B").removeClass("verbergAntwoord");
        
        $("#vraag1").removeClass("actief").addClass("nietActief nietActief2");
        $("#vraag1").find("button").removeClass("knopTitel").addClass("knopTitel2");
        $("#vraag2").find("button").removeClass("knopTitel2").addClass("knopTitel");
        $("#vraag2").removeClass("nietActief nietActief2").addClass("actief");
    });

    $("#antwoord1B").click(function () {
        if ($("#antwoord1A").hasClass("gekozen")) {
            $("#antwoord1A").removeClass("gekozen").addClass("nietGekozen");
        }

        $(this).data("geselecteerd", true);
        $("#antwoord1A").data("geselecteerd", false);

        if (!$("#antwoord1A").hasClass("verbergAntwoord")) {
            $("#antwoord1A").addClass("verbergAntwoord");
        }

        if (!$("#antwoord1B").hasClass("verbergAntwoord")) {
            $("#antwoord1B").addClass("verbergAntwoord");
        }

        $("#antwoord2A").removeClass("verbergAntwoord");
        $("#antwoord2B").removeClass("verbergAntwoord");

        $("#vraag1").removeClass("actief").addClass("nietActief nietActief2");
        $("#vraag1").find("button").removeClass("knopTitel").addClass("knopTitel2");
        $("#vraag2").find("button").removeClass("knopTitel2").addClass("knopTitel");
        $("#vraag2").removeClass("nietActief nietActief2").addClass("actief");
    });

    $("#antwoord2A").click(function () {
        if ($("#antwoord2B").hasClass("gekozen")) {
            $("#antwoord2B").removeClass("gekozen").addClass("nietGekozen");
        }
        $(this).data("geselecteerd", true);
        $("#antwoord2B").data("geselecteerd", false);

        if (!$("#antwoord2B").hasClass("verbergAntwoord")) {
            $("#antwoord2B").addClass("verbergAntwoord");
        }

        if (!$("#antwoord2A").hasClass("verbergAntwoord")) {
            $("#antwoord2A").addClass("verbergAntwoord");
        }

        $("#antwoord3A").removeClass("verbergAntwoord");
        $("#antwoord3B").removeClass("verbergAntwoord");

        $("#vraag2").removeClass("actief").addClass("nietActief nietActief2");
        $("#vraag3").removeClass("nietActief nietActief2").addClass("actief");

        $("#vraag2").find("button").removeClass("knopTitel").addClass("knopTitel2");
        $("#vraag3").find("button").removeClass("knopTitel2").addClass("knopTitel");
    });

    $("#antwoord2B").click(function () {
        if ($("#antwoord2A").hasClass("gekozen")) {
            $("#antwoord2A").removeClass("gekozen").addClass("nietGekozen");
        }
        $(this).data("geselecteerd", true);
        $("#antwoord2A").data("geselecteerd", false);

        if (!$("#antwoord2A").hasClass("verbergAntwoord")) {
            $("#antwoord2A").addClass("verbergAntwoord");
        }

        if (!$("#antwoord2B").hasClass("verbergAntwoord")) {
            $("#antwoord2B").addClass("verbergAntwoord");
        }

        $("#antwoord3A").removeClass("verbergAntwoord");
        $("#antwoord3B").removeClass("verbergAntwoord");

        $("#vraag2").removeClass("actief").addClass("nietActief nietActief2");
        $("#vraag3").removeClass("nietActief nietActief2").addClass("actief");

        $("#vraag2").find("button").removeClass("knopTitel").addClass("knopTitel2");
        $("#vraag3").find("button").removeClass("knopTitel2").addClass("knopTitel");
    });

    $("#antwoord3A").click(function () {
        if ($("#antwoord3B").hasClass("gekozen")) {
            $("#antwoord3B").removeClass("gekozen").addClass("nietGekozen");
        }
        $(this).data("geselecteerd", true);
        $("#antwoord3B").data("geselecteerd", false);

        if (!$("#antwoord3B").hasClass("verbergAntwoord")) {
            $("#antwoord3B").addClass("verbergAntwoord");
        }

        if (!$("#antwoord3A").hasClass("verbergAntwoord")) {
            $("#antwoord3A").addClass("verbergAntwoord");
        }

        $("#antwoord4A").removeClass("verbergAntwoord");
        $("#antwoord4B").removeClass("verbergAntwoord");

        $("#vraag3").removeClass("actief").addClass("nietActief nietActief2");
        $("#vraag4").removeClass("nietActief nietActief2").addClass("actief");

        $("#vraag3").find("button").removeClass("knopTitel").addClass("knopTitel2");
        $("#vraag4").find("button").removeClass("knopTitel2").addClass("knopTitel");
    });

    $("#antwoord3B").click(function () {
        if ($("#antwoord3A").hasClass("gekozen")) {
            $("#antwoord3A").removeClass("gekozen").addClass("nietGekozen");
        }
        $(this).data("geselecteerd", true);
        $("#antwoord2A").data("geselecteerd", false);

        if (!$("#antwoord3A").hasClass("verbergAntwoord")) {
            $("#antwoord3A").addClass("verbergAntwoord");
        }

        if (!$("#antwoord3B").hasClass("verbergAntwoord")) {
            $("#antwoord3B").addClass("verbergAntwoord");
        }

        $("#antwoord4A").removeClass("verbergAntwoord");
        $("#antwoord4B").removeClass("verbergAntwoord");

        $("#vraag3").removeClass("actief").addClass("nietActief nietActief2");
        $("#vraag4").removeClass("nietActief nietActief2").addClass("actief");

        $("#vraag3").find("button").removeClass("knopTitel").addClass("knopTitel2");
        $("#vraag4").find("button").removeClass("knopTitel2").addClass("knopTitel");
    });

    $("#antwoord4A").click(function () {
        if ($("#antwoord4B").hasClass("gekozen")) {
            $("#antwoord4B").removeClass("gekozen").addClass("nietGekozen");
        }
        $("#linkerAntwoord").addClass("gekozen").removeClass("nietGekozen");
    });

    $("#antwoord4B").click(function () {
        if ($("#antwoord4A").hasClass("gekozen")) {
            $("#antwoord4A").removeClass("gekozen").addClass("nietGekozen");
        }
        $("#rechterAntwoord").addClass("gekozen").removeClass("nietGekozen");
    });

    function verbergAntwoorden(knopId) {
        if (!$("#antwoord1A").hasClass("verbergAntwoord") && knopId !== "vraag1") {
            $("#antwoord1A").addClass("verbergAntwoord");
        }

        if (!$("#antwoord1B").hasClass("verbergAntwoord") && knopId !== "vraag1") {
            $("#antwoord1B").addClass("verbergAntwoord");
        }

        if (!$("#antwoord2A").hasClass("verbergAntwoord") && knopId !== "vraag2") {
            $("#antwoord2A").addClass("verbergAntwoord");
        }

        if (!$("#antwoord2B").hasClass("verbergAntwoord") && knopId !== "vraag2") {
            $("#antwoord2B").addClass("verbergAntwoord");
        }

        if (!$("#antwoord3A").hasClass("verbergAntwoord") && knopId !== "vraag3") {
            $("#antwoord3A").addClass("verbergAntwoord");
        }

        if (!$("#antwoord3B").hasClass("verbergAntwoord") && knopId !== "vraag3") {
            $("#antwoord3B").addClass("verbergAntwoord");
        }

        if (!$("#antwoord4A").hasClass("verbergAntwoord") && knopId !== "vraag4") {
            $("#antwoord4A").addClass("verbergAntwoord");
        }

        if (!$("#antwoord4B").hasClass("verbergAntwoord") && knopId !== "vraag4") {
            $("#antwoord4B").addClass("verbergAntwoord");
        }

        if ($("#vraag1").hasClass("actief") && knopId !== "vraag1") {
            $("#vraag1").removeClass("actief").addClass("nietActief nietActief2");
            $("#vraag1").find("button").removeClass("knopTitel").addClass("knopTitel2");
        }

        if ($("#vraag2").hasClass("actief") && knopId !== "vraag2") {
            $("#vraag2").removeClass("actief").addClass("nietActief nietActief2");
            $("#vraag2").find("button").removeClass("knopTitel").addClass("knopTitel2");
        }

        if ($("#vraag3").hasClass("actief") && knopId !== "vraag3") {
            $("#vraag3").removeClass("actief").addClass("nietActief nietActief2");
            $("#vraag3").find("button").removeClass("knopTitel").addClass("knopTitel2");
        }

        if ($("#vraag4").hasClass("actief") && knopId !== "vraag4") {
            $("#vraag4").removeClass("actief").addClass("nietActief nietActief2");
            $("#vraag4").find("button").removeClass("knopTitel").addClass("knopTitel2");
        }
    }
});