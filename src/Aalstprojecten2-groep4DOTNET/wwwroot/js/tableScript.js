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
        var tempTr = $('<tr><td><input type="text" id="Functie" class="form-control" placeholder="Functie" /></td><td><input type="number" id="UrenPerWeek" class="form-control" placeholder="Uren per week" /></td><td><input type="number" id="BrutoMaandloonFulltime" class="form-control" placeholder="Bruto maandloon fulltime" /></td><td class="selectpicker"><select class="btn btn-default selectpicker"><option selected>Maak uw Keuze</option><option>Werknemers < 25 jaar laag geschoold</option><option>Werknemers < 25 jaar midden geschoold</option><option>Werknemers > 59 jaar</option><option>Ander</option></select></td><td class="selectpicker"><select class="btn btn-default selectpicker" value="Maak uw keuze"><option selected>Maak uw Keuze</option><option>40%</option><option>30%</option><option>20%</option><option>0%</option></select></td><td><input type="number" id="aantalMaandenIBO" class="form-control" placeholder="Aantal maanden" /></td><td><input type="number" id="productiviteitsPremie" class="form-control" placeholder="Totale Premie" /></td></tr>').on('click', function () {
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
        var tempTr = $('<tr><td><input type="text" id="beschrijving" class="form-control" placeholder="Beschrijving" /></td><td><input type="number" id="jaarbedrag" class="form-control" placeholder="Jaarbedrag" /></td></tr>').on('click', function () {
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
        var tempTr = $('<tr><td><input type="text" id="type" class="form-control" placeholder="Type" /></td><td><input type="number" id="Bedrag" class="form-control" placeholder="Bedrag" /></td></tr>').on('click', function () {
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
        var tempTr = $('<tr><td><input type="text" id="type" class="form-control" placeholder="Type" /></td><td><input type="number" id="Bedrag" class="form-control" placeholder="Bedrag" /></td></tr>').on('click', function () {
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
        var tempTr = $('<tr><td><input type="text" id="type" class="form-control" placeholder="Type" /></td><td><input type="number" id="Bedrag" class="form-control" placeholder="Bedrag" /></td></tr>').on('click', function () {
        });
        $("#tableAddRow5").append(tempTr)
        i++;
    }
});

$(document).ready(function () {
    $('.addBtn6').on('click', function () {
        //var trID;
        //trID = $(this).closest('tr'); // table row ID 
        addTableRow();
    });
    $('.addBtnRemove').click(function () {
        $(this).closest('tr').remove();
    })
    var i = 1;
    function addTableRow() {
        var tempTr = $('<tr><td><input type="text" id="type" class="form-control" placeholder="Type" /></td><td><input type="number" id="Bedrag" class="form-control" placeholder="Bedrag" /></td></tr>').on('click', function () {
        });
        $("#tableAddRow6").append(tempTr)
        i++;
    }
});

$(document).ready(function () {
    $('.addBtn7').on('click', function () {
        //var trID;
        //trID = $(this).closest('tr'); // table row ID 
        addTableRow();
    });
    $('.addBtnRemove').click(function () {
        $(this).closest('tr').remove();
    })
    var i = 1;
    function addTableRow() {
        var tempTr = $('<tr><td><input type="number" id="uren" class="form-control" placeholder="Uren" /></td><td><input type="number" id="brutoMaandloonBegeleider" class="form-control" placeholder="Bruto Maandloon" /></td><td><input type="number" id="jaarbedrag" class="form-control" placeholder="Jaarbedrag" /></td></tr>').on('click', function () {
        });
        $("#tableAddRow7").append(tempTr)
        i++;
    }
});