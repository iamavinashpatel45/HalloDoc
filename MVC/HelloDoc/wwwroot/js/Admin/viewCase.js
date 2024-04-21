function ProfileEdit(temp) {
    if (temp) {
        $('#edit').css("display", "none");
        $('#submit').css("display", "flex");
        $('#Fieldset').prop("disabled", false);
    }
    else {
        $(".text-danger").text("");
        $('#edit').css("display", "block");
        $('#submit').css("display", "none");
        $('#Fieldset').prop("disabled", true);
    }
}

function checkLocation() {
    var link = "http://maps.google.com/?q=" + $("#street").val() + "+" + $("#city").val() + "+" + $("#state").val() + "+" + $("#zip-code").val();
    link = link.replace(/\s+/g, "");
    window.open(link, "_blank");
}

function cancelPopUp(id) {
    $("#patientRequestId").val(id);
    $(".patientName").text($('#first-name').val() + ' ' + $('#last-name').val());
}

