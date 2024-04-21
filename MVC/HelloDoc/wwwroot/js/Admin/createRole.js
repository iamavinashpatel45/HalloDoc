
$(document).on("change", "#type", function () {
    selectedMenus = [];
    if ($(this).val().length != 0) {
        $.ajax({
            url: "/Admin/ChangeMenusByRole",
            type: "Get",
            contentType: "application/json",
            data: {
                roleId: $(this).val(),
            },
            success: function (response) {
                $("#menuValidation").text("");
                $(".checkBoxs").html(response);
            }
        })
    }
})

var selectedMenus = [];

function changeCheckBox(doc) {
    if ($(doc).is(":checked")) {
        selectedMenus.push($(doc).attr("value"));
        $("#menuValidation").text("");
    }
    else {
        var index = selectedMenus.indexOf($(doc).attr("value"));
        if (index > -1) {
            selectedMenus.splice(index, 1);
        }
    } 
}

$(document).on("submit", "#createRoleForm", function (e) {
    if (selectedMenus.length == 0 && $("#type").val() != 1) {
        e.preventDefault();
        $("#menuValidation").text("Select Any One Filed");
    }
})