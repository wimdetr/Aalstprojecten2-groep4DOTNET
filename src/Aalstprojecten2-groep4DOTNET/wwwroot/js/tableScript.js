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
        var tempTr = $('<tr><td><input type="number" id="Uren" class="form-control" placeholder="€" /></td><td><input type="number" id="BrutoLoon" class="form-control" placeholder="€"/></td><td><input type="number" id="TotaleLoonKost" class="form-control" placeholder="€"/></td></tr>').on('click', function () {
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
        var tempTr = $('<tr><td><input type="number" id="Uren" class="form-control" placeholder="€" /></td><td><input type="number" id="BrutoLoon" class="form-control" placeholder="€"/></td><td><input type="number" id="TotaleLoonKost" class="form-control" placeholder="€"/></td></tr>').on('click', function () {
        });
        $("#tableAddRow2").append(tempTr)
        i++;
    }
});