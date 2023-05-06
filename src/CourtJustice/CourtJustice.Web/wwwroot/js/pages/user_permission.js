$(function () {

    var tree = $("#programTree");
    tree.jstree({
        'checkbox': {
            keep_selected_style: false,
            three_state: false,
            cascade: ''
        },
        plugins: ["checkbox"],
        core: {
            "themes": {
                "icons": false
            }
        }
    });
    tree.jstree(true).open_all();
    $('li[data-checkstate="checked"]').each(function () {
        tree.jstree('check_node', $(this));
    });
    $("li.jstree-node:not(.jstree-leaf) > a").addClass("no_checkbox");
});

$("#btnSave").on('click', function (e) {
    const url = $(this).attr("data-url");
    let userPermissions = [];
    const groupId = $('#GroupId').val();
    var selectedNodes = $('#programTree').jstree("get_selected", true);

    $.each(selectedNodes, function () {
        var userPermission = {};
        userPermission.GroupId = parseInt(groupId);
        userPermission.ProgramId = parseInt(this.id);
        userPermissions.push(userPermission);
    });
    $("#loaderbody").show();
    $.ajax({
        type: 'POST',
        url: url,
        data: JSON.stringify(userPermissions),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            if (res.isValid) {

                swal({
                    title: "Success",
                    text: "Permission is save successfully",
                    icon: "success"
                }).then(() => {
                    var url = $("#RedirectTo").val();
                    window.location.href = url;
                });
            } else {
                swal({
                    title: "Error",
                    text: res.message,
                    icon: "error"
                });
            }
            $("#loaderbody").hide();
        },
        error: function (err) {
            $("#loaderbody").hide();
            console.log(err);
        }
    });

});


