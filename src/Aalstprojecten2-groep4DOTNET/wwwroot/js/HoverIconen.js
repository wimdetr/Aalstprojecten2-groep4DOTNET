$(document).ready(function () {
    $(".homeIcoon").hover(function () {
        $(this).find(".homePrent").attr("src", "/images/iconen/home_hover.png");
    }, function () {
        $(this).find(".homePrent").attr("src", "/images/iconen/home.png");
    });

    $(".nieuwIcoon").hover(function () {
        $(this).find(".nieuwPrent").attr("src", "/images/iconen/nieuw_hover.png");
    }, function () {
        $(this).find(".nieuwPrent").attr("src", "/images/iconen/nieuw.png");
    });


    $(".archiefIcoon").hover(function () {
        $(this).find(".archiefPrent").attr("src", "/images/iconen/archief_hover.png");
    }, function () {
        $(this).find(".archiefPrent").attr("src", "/images/iconen/archief.png");
    });

    $(".verwijderIcoon").hover(function () {
        $(this).find(".verwijderPrent").attr("src", "/images/iconen/bin_hover.png");
    }, function () {
        $(this).find(".verwijderPrent").attr("src", "/images/iconen/bin.png");
    });

    $(".verstuurIcoon").hover(function () {
        $(this).find(".verstuurPrent").attr("src", "/images/iconen/send_hover.png");
    }, function () {
        $(this).find(".verstuurPrent").attr("src", "/images/iconen/send.png");
    });

    $(".downloadIcoon").hover(function () {
        $(this).find(".downloadPrent").attr("src", "/images/iconen/download_hover.png");
    }, function () {
        $(this).find(".downloadPrent").attr("src", "/images/iconen/download.png");
    });

    $(".archiveerIcoon").hover(function () {
        $(this).find(".archiveerPrent").attr("src", "/images/iconen/archive_hover.png");
    }, function () {
        $(this).find(".archiveerPrent").attr("src", "/images/iconen/archive.png");
    });

    $(".potloodIcoon").hover(function () {
        $(this).find(".potloodPrent").attr("src", "/images/iconen/edit_hover.png");
    }, function () {
        $(this).find(".potloodPrent").attr("src", "/images/iconen/edit.png");
    });







    $(".sendIcoon").hover(function () {
        $(this).find(".sendPrent").attr("src", "/images/iconen/send_hover.png");
    }, function () {
        $(this).find(".sendPrent").attr("src", "/images/iconen/send.png");
    });

    $(".binIcoon").hover(function () {
        $(this).find(".binPrent").attr("src", "/images/iconen/bin_hover.png");
    }, function () {
        $(this).find(".binPrent").attr("src", "/images/iconen/bin.png");
    });

    $(".downloadIcoon").hover(function () {
        $(this).find(".downloadPrent").attr("src", "/images/iconen/download_hover.png");
    }, function () {
        $(this).find(".downloadPrent").attr("src", "/images/iconen/download.png");
    });

    $(".deArchiveerIcoon").hover(function () {
        $(this).find(".deArchiveerPrent").attr("src", "/images/iconen/unarchive_hover.png");
    }, function () {
        $(this).find(".deArchiveerPrent").attr("src", "/images/iconen/unarchive.png");
    });
});