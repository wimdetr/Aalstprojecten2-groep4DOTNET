$(document)
    .ready(function() {
        $("#zoekInput")
            .click(function() {
                $(this).addClass("fancySearch");
            });
        $("#zoekInput")
            .on("input",
                function() {
                    var werkgever = $("#zoekInput").val();
                    $(".filteredRow")
                        .each(function() {
                            var mijnWerkgever = $(this).children().text();
                            if (mijnWerkgever.toLowerCase().indexOf(werkgever.toLowerCase()) === -1) {
                                $(this).removeClass("toonRij").addClass("verbergRij");
                            } else {
                                $(this).removeClass("verbergRij").addClass("toonRij");
                            }
                        });
                    $("table").removeClass("table-striped");

                    // now add stripes to alternating rows
                    $(".toonRij").each(function (index) {
                        // but first remove class that may have been added by previous changes
                        $(this).removeClass("stripe");
                        if (index % 2 === 0) {
                            $(this).addClass("stripe");
                        }
                    });
                });
    });