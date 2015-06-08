////Delete popups
//$('.deleteButton').click(function (e) {
//    e.preventDefault();
//    var id = $(this).data('id');
//    $('#notekiller').val(id);

//});

//$('#deleteNoteReally').click(function () {

//    var id = $('#notekiller').val();

//    $.post('@Url.Action("Delete", "Notes")', { id: id }).done(function () {
//        location.reload();
//    }).fail(function () {
//        alert('Notes could not be saved.');
//    });

//});