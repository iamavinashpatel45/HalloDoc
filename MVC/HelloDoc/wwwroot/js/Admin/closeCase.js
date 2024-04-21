var firstName, lastName, mobile, email, birthDate;

function enableEdit(temp) {
    if (temp) {
        firstName = $("#firstName").val();
        lastName = $("#lastName").val();
        mobile = $("#phone").val();
        email = $("#email").val();
        birthDate = $("#birthDate").val();
        $("#editbutton").css("display", "none");
        $(".edit").css("display", "block");
        $("#fieldset").prop("disabled", false);
    }
    else {
        $("#firstName").val(firstName);
        $("#lastName").val(lastName);
        $("#phone").val(mobile);
        $("#email").val(email);
        $("#birthDate").val(birthDate);
        $("#editbutton").css("display", "block");
        $(".validation").css("display", "none");
        $(".edit").css("display", "none");
        $("#fieldset").prop("disabled", true);
    }
}

$(function () {
    $('[type="date"]').prop('max', function () {
        return new Date().toJSON().split('T')[0];
    });
});

//$(document).on("change", "#phone", function () {
//    if ($(this).val().length == 0) {
//        $("#phoneValidation").css("display", "block");
//    }
//    else {
//        $("#phoneValidation").css("display", "none");
//    }
//})

//$(document).on("change", "#birthDate", function () {
//    if ($(this).val().length == 0) {
//        $("#birthDateValidation").css("display", "block");
//    }
//    else {
//        $("#birthDateValidation").css("display", "none");
//    }
//})

//$(document).on("change", "#lastName", function () {
//    if ($(this).val().length == 0) {
//        $("#lastNameValidation").css("display", "block");
//    }
//    else {
//        $("#lastNameValidation").css("display", "none");
//    }
//})

//$(document).on("change", "#firstName", function () {
//    if ($(this).val().length == 0) {
//        $("#firstNameValidation").css("display", "block");
//    }
//    else {
//        $("#firstNameValidation").css("display", "none");
//    }
//})

//$(document).on("submit", "#form", function (e) {
//    if ($("#phone").val().length == 0 || $("#firstName").val().length == 0 || $("#lastName").val().length == 0 || $("#birthDate").val().length == 0) {
//        e.preventDefault();
//    }
//})

var isArrowUp = true;
function shortTable() {
    if (isArrowUp) {
        isArrowUp = false;
        $(".bi-arrow-up").css("display", "none");
        $(".bi-arrow-down").css("display", "inline-block");
    }
    else {
        isArrowUp = true;
        $(".bi-arrow-up").css("display", "inline-block");
        $(".bi-arrow-down").css("display", "none");
    }
    $('tbody').each(function () {
        var list = $(this).children('tr');
        $(this).html(list.get().reverse());
    });
}