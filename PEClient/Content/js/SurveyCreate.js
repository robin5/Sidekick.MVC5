var rowId = -1;

function addBlankRow() {
    $('#survey-questions').append(NextBlankQuestion());
    enumerateQuestions();
}

function NextBlankQuestion() {

    if (rowId == -1) {
        rowId = $('.question-row').length - 1;
    }

    rowId++;
    var tr = '<div id="row-id-' + rowId + '" class="survey-question-container">';

    //tr += '<td><textarea class="q-textarea form-control" required></textarea></td>';
    tr += '<div class="survey-question-box">';
    tr += '    <textarea id="question-' + rowId + '" class="q-textarea form-control" name = "Questions[' + rowId + ']" required ></textarea>';
    tr += '</div>';

    //tr += '<td ><a href="#" data-row-id="' + rowId + '" onclick="deleteRow(event)">delete</a></td></tr>';
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
        $(this).attr("name", "Questions[" + qid + "]");
        $(this).attr("id", "question-" + qid);
        ++qid;
    });
}
