$(document)
    .ready(function () {
        $("#file-1")
            .change(function (event) {
                $(".logo").find("img").prop("src", URL.createObjectURL(event.target.files[0]));
            });
    });