
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

    //var tr = '<div id=row-id-' + rowId + '> class="flex-container" style="flex: 0 0 800px"';


    //tr += '<td style="padding-bottom: 10px"><textarea></textarea></td>';

    tr = '<div id="row-id-' + rowId + '" class="survey-question-container">';
    tr += '<div class="survey-question-box">';
    tr += '    <textarea id="EditedQuestions_' + rowId + '_Id" class="q-textarea form-control" name = "EditedQuestions[' + rowId + ']" required ></textarea>';
    tr += '</div>';

    //    '<td style="padding-left: 20px"><a href="#" data-row-id="' + rowId + '" onclick="deleteRow(event)">delete</a></td></div>';
    //     <div style="padding-left: 20px; flex-shrink: 0; flex-grow: 0;"><a href="#" data-row-id="@(rowId)" onclick="deleteRow(event)">delete</a></div >
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
        $(this).attr("id", "EditedQuestions_" + qid + "_Id");
        ++qid;
    });
}

