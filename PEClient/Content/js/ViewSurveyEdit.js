
var rowId = -1;

function addBlankRow() {
    $('table').append(getNextRowId());
    enumerateQuestions();
}

function getNextRowId() {

    if (rowId == -1) {
        rowId = $('.question-row').length - 1;
    }

    rowId++;
    var tr = '<tr id=row-id-' + rowId + '>';
    tr += '<td style="padding-bottom: 10px"><textarea class="q-textarea form-control" cols="50" style="max-width: 100%;" required></textarea></td>';
    tr += '<td style="padding-left: 20px"><a href="#" data-row-id="' + rowId + '" onclick="deleteRow(event)">delete</a></td></tr>';
    return tr;
}

// Deletes a row from the table and prevents the anchor from firing.
function deleteRow(event) {
    var rowToDelete = "#row-id-" + event.target.getAttribute('data-row-id');
    $(rowToDelete).remove();
    event.preventDefault();
    enumerateQuestions();
}

// Enumerates the question input elements so they will be transmitted
// as an array that is zero indexed and not sparse
function enumerateQuestions() {

    var qid = 0;

    $('.q-textarea').each(function () {
        $(this).attr("name", "EditedQuestions[" + qid + "]");
        $(this).attr("id", "EditedQuestions_" + qid + "_Id");
        ++qid;
    });
}

