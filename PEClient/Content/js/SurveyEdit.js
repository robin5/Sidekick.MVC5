
var rowId = -1;

function addBlankRow() {
    $('#survey-questions').append(NextBlankQuestion());
    enumerateQuestions();
}

function NextBlankQuestion() {

    // Test for first time adding a row
    if (rowId == -1) {
        // Note: Each textarea question has been decorated with the q-textarea class
        // Calculate the next row ID from the number of q-textarea found
        rowId = $('.q-textarea').length - 1;
    }

    rowId++;

    tr = '<div id="row-id-' + rowId + '" class="survey-question-container">';
    tr += '<div class="survey-question-box">';
    tr += '    <textarea id="question-' + rowId + '_Id" class="q-textarea form-control" name = "EditedQuestions[' + rowId + ']" required ></textarea>';
    tr += '</div>';

    tr += '<div class="survey-delete-box">';
    tr += '    <a href="#" data-row-id="' + rowId + '" onclick="deleteRow(event)">delete</a>';
    tr += '</div></div>';
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
        $(this).attr("id", "question-" + qid);
        ++qid;
    });
}

