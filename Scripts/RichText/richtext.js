
$(document).ready(function () {
    $('#content-editor').freshereditor({ toolbar_selector: "#toolbar", excludes: ['insertheading4'] });
    //'removeFormat',
    $("#content-editor").freshereditor("edit", true);
    $("#content-editor").on('change', function () {
        console.log("content changed");
    });
});

