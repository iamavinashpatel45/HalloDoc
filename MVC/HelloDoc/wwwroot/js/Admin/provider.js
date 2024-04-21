function onnotificationClick(doc) {
    $.ajax({
        url: "/Admin/EditProviderNotification",
        type: "Get",
        contentType: "application/json",
        data: {
            physicanId : $(doc).attr("id"),
            isNotification : $(doc).is(":checked"),
        },
    })
}

$(document).on("change", "#regionFilter", function () {
    $.ajax({
        url: "/Admin/RegionFilter",
        type: "Get",
        contentType: "application/json",
        data: {
            regionId: $(this).val(),
        },
        success: function (response) {
            $(".tableData").html(response);
        }
    })
})


// provider update page

$(document).on("click", "#editbutton1", function () {
    $(this).css("display", "none");
    $("#edit1").css("display", "block");
    $("#fieldset1").prop("disabled", false);
})

$(document).on("click", "#cancel1", function () {
    $("#editbutton1").css("display", "block");
    $("#edit1").css("display", "none");
    $("#fieldset1").prop("disabled", true);
})

$(document).on("click", "#editbutton2", function () {
    $(this).css("display", "none");
    $("#edit2").css("display", "block");
    $("#fieldset2").prop("disabled", false);
})

$(document).on("click", "#cancel2", function () {
    $("#editbutton2").css("display", "block");
    $("#edit2").css("display", "none");
    $("#fieldset2").prop("disabled", true);
})

$(document).on("click", "#editbutton3", function () {
    $(this).css("display", "none");
    $("#edit3").css("display", "block");
    $("#fieldset3").prop("disabled", false);
})

$(document).on("click", "#cancel3", function () {
    $("#editbutton3").css("display", "block");
    $("#edit3").css("display", "none");
    $("#fieldset3").prop("disabled", true);
})

$(document).on("click", "#editbutton4", function () {
    $(this).css("display", "none");
    $("#edit4").css("display", "block");
    $("#fieldset4").prop("disabled", false);
})

$(document).on("click", "#cancel4", function () {
    $("#editbutton4").css("display", "block");
    $("#edit4").css("display", "none");
    $("#fieldset4").prop("disabled", true);
})

$(document).on("click", "#editbutton5", function () {
    $(this).css("display", "none");
    $("#edit5").css("display", "block");
    $("#fieldset5").prop("disabled", false);
})

$(document).on("click", "#cancel5", function () {
    $("#editbutton5").css("display", "block");
    $("#edit5").css("display", "none");
    $("#fieldset5").prop("disabled", true);
})

$(document).on("click", ".createButton", function () {
    $("#signaturePad").css("display", "block");
})

$(document).on("click", "#signatureClear", function () {
    signaturePad.clear();
})

$(document).on("click", "#signatureSave", function () {
    $.ajax({
        url: "/Admin/SaveSignature",
        type: "Post",
        data: {
            file: signaturePad.toDataURL(),
        },
        success: function (response) {
            window.location.href = response.redirect;
        }
    })
})


//$(document).on("click", "#form1", function () {
//    $('#form2').validate();
//    $("#form2").validate();
//    console.log("asdjfnajskfbsajfb");
//    console.log($("#form2").valid());
//})


var signaturePad;

$(document).ready(function () {
    if ($("#signature-pad").length) {
        canvas = document.getElementById('signature-pad');
        signaturePad = new SignaturePad(canvas);
    }   
})
