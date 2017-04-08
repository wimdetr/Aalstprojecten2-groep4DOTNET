$(document).ready(function () {
    $('.addBtn').on('click', function () {
        //var trID;
        //trID = $(this).closest('tr'); // table row ID 
        addTableRow();
    });
    $('.addBtnRemove').click(function () {
        $(this).closest('tr').remove();
    })
    var i = 1;
    function addTableRow() {
        var tempTr = $('<tr><td><input type="number" id="uren" class="form-control" placeholder="Uren" /></td><td><input type="number" id="brutoMaandloonFulltime" class="form-control" placeholder="Bruto maandloon fulltime" /></td><td><input type="number" id="totaleLoonkostPerJaar" class="form-control" placeholder="Loonkost Per Jaar" /></td></tr>').on('click', function () {
        });
        $("#tableAddRow").append(tempTr)
        i++;
    }
});

$(document).ready(function () {
    $('.addBtn2').on('click', function () {
        //var trID;
        //trID = $(this).closest('tr'); // table row ID 
        addTableRow();
    });
    $('.addBtnRemove').click(function () {
        $(this).closest('tr').remove();
    })
    var i = 1;
    function addTableRow() {
        var tempTr = $('<tr><td><input type="number" id="uren" class="form-control" placeholder="Uren" /></td><td><input type="number" id="brutoMaandloonFulltime" class="form-control" placeholder="Bruto maandloon fulltime" /></td><td><input type="number" id="totaleLoonkostPerJaar" class="form-control" placeholder="Loonkost Per Jaar" /></td></tr>').on('click', function () {
        });
        $("#tableAddRow2").append(tempTr)
        i++;
    }
});

$(document).ready(function () {
    $('.addBtn3').on('click', function () {
        //var trID;
        //trID = $(this).closest('tr'); // table row ID 
        addTableRow();
    });
    $('.addBtnRemove').click(function () {
        $(this).closest('tr').remove();
    })
    var i = 1;
    function addTableRow() {
        var tempTr = $('<tr><td><input type="text" id="beschrijving" class="form-control" placeholder="Beschrijving" /></td><td><input type="number" id="jaarbedrag" class="form-control" placeholder="Jaarbedrag" /></td></tr>').on('click', function () {
    });
    $("#tableAddRow3").append(tempTr)
    i++;
}
});


$(document).ready(function () {
    $('.addBtn4').on('click', function () {
        //var trID;
        //trID = $(this).closest('tr'); // table row ID 
        addTableRow();
    });
    $('.addBtnRemove').click(function () {
        $(this).closest('tr').remove();
    })
    var i = 1;
    function addTableRow() {
        var tempTr = $('<tr><td><input type="text" id="beschrijving" class="form-control" placeholder="Beschrijving" /></td><td><input type="number" id="jaarbedrag" class="form-control" placeholder="Jaarbedrag" /></td></tr>').on('click', function () {
        });
        $("#tableAddRow4").append(tempTr)
        i++;
    }
});

$(document).ready(function () {
    $('.addBtn5').on('click', function () {
        //var trID;
        //trID = $(this).closest('tr'); // table row ID 
        addTableRow();
    });
    $('.addBtnRemove').click(function () {
        $(this).closest('tr').remove();
    })
    var i = 1;
    function addTableRow() {
        var tempTr = $('<tr><td><input type="text" id="type" class="form-control" placeholder="Type Besparing" /></td><td><input type="number" id="jaarBedrag" class="form-control" placeholder="Jaarbedrag" /></td></tr>').on('click', function () {
        });
        $("#tableAddRow5").append(tempTr)
        i++;
    }
});