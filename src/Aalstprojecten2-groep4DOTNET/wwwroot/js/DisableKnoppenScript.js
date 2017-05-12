$(document)
    .ready(function() {
        $(".teDisablenKnop")
            .click(function() {
                $(this).prop("disabled", true);
                $(this).closest("form").submit();
            });
    });