var firstName, LastName, email, phone, regions, add1, add2, city, state, zip, mobile;
var defaultRadioList = [];
function enableEditForAdministrator(temp) {
    if (temp) {
        phone = $("#phone1").val();
        fname = $("#firstName").val();
        lname = $("#lastName").val();
        email = $("#email").val();
        $("#editbutton1").css("display", "none");
        $("#edit1").css("display", "block");
        $("#fieldset1").prop("disabled", false);
        var radio = $(".form-check-input");
        for (i = 0; i < radio.length; i++) {
            if ($(radio[i]).is(":checked")) {
                radioList.push($(radio[i]).attr("id"));
                defaultRadioList.push($(radio[i]).attr("id"));
            }
        }
    }
    else {
        $(".form-check-input").prop("checked", false);
        for (i = 0; i < defaultRadioList.length; i++)
        {
            $("#" + defaultRadioList[i]).prop("checked", true);
        }
        defaultRadioList = [];
        $("#phone1").val(phone);
        $("#firstName").val(fname);
        $("#lastName").val(lname);
        $("#email").val(email);
        $("#confirmEmail").val(email);
        $(".administratorValidation").text("");
        $("#editbutton1").css("display", "block");
        $("#edit1").css("display", "none");
        $("#fieldset1").prop("disabled", true);
    }
}

var radioList = [];
function radioClick(doc) {
    if ($(doc).is(":checked")) {
        radioList.push($(doc).attr("id"));
    } else {
        var index = radioList.indexOf($(doc).attr("id"));
        if (index > -1) {
            radioList.splice(index , 1);
        }
    }
    validateRadioButton();
}

function enableEditForMailingAndBilling(temp) {
    if (temp) {
        add1 = $("#address1").val();
        add2 = $("#address2").val();
        city = $("#city").val();
        state = $("#state").val();
        zip = $("#zip").val();
        mobile = $("#phone2").val();
        $("#editbutton2").css("display", "none");
        $("#edit2").css("display", "block");
        $("#fieldset2").prop("disabled", false);
    }
    else {
        $("#address1").val(add1);
        $("#address2").val(add2);
        $("#city").val(city);
        $("#state").val(state);
        $("#zip").val(zip);
        $("#phone2").val(mobile);
        $(".Validation").text("");
        $("#editbutton2").css("display", "block");
        $("#edit2").css("display", "none");
        $("#fieldset2").prop("disabled", true);
    }
}


////  reset password

function validatePassword() {
    if ($("#password").val().length == 0) {
        $("#passwordValidation").text("Password is required");
    }
    else {
        $("#passwordValidation").text("");
    }
}

$(document).on("change", "#password", function () {
    validatePassword();
})

$(document).on("click", "#resetpassword", function () {
    var password = $("#password").val();
    if (password.length != 0) {
        $.ajax({
            url: "/Admin/ViewProfileEditPassword",
            type: "Get",
            async: false,
            contentType: "application/json",
            data: {
                newPassword: password,
            },
            success: function (response) {
                window.location.href = response.redirect;
            }
        })
    }
})

////  Administrator Informaction

function validateRadioButton() {
    if (radioList.length == 0) {
        $("#radioButtonValidation").text("Region is required");
    }
    else {
        $("#radioButtonValidation").text("");
    }
}

$(document).on("change", "#firstName", function () {
    if ($(this).val().length == 0) {
        $("#firstNameValidation").text("First Name is required");
    }
    else {
        $("#firstNameValidation").text("");
    }
})

$(document).on("change", "#lastName", function () {
    if ($(this).val().length == 0) {
        $("#lastNameValidation").text("Last Name is required");
    }
    else {
        $("#lastNameValidation").text("");
    }
})

$(document).on("change", "#lastName", function () {
    if ($(this).val().length == 0) {
        $("#lastNameValidation").text("Last Name is required");
    }
    else {
        $("#lastNameValidation").text("");
    }
})

$(document).on("change", "#email", function () {
    if ($(this).val().length == 0) {
        $("#emailValidation").text("Email is required");
    }
    else {
        $("#emailValidation").text("");
    }
})

$(document).on("change", "#confirmEmail", function () {
    if ($(this).val().length == 0) {
        $("#confirmEmailValidation").text("Confirm Email is required");
    }
    else {
        $("#confirmEmailValidation").text("");
    }
})

$(document).on("change", "#phone1", function () {
    if ($(this).val().length == 0) {
        $("#phone1Validation").text("Mobile is required");
    }
    else {
        $("#phone1Validation").text("");
    }
})

var validRegex = /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/;
$(document).on("click", "#administratorForm", function () {
    if ($("#lastName").val().length != 0 && $("#phone1").val().length != 0 && $("#confirmEmail").val().length != 0 && $("#email").val().length != 0
        && $("#firstName").val().length != 0 && $("#confirmEmail").val() == $("#email").val() && $("#email").val().match(validRegex) && radioList.length != 0) {
        var data1 = JSON.stringify({
            FirstName: $("#firstName").val(),
            LastName: $("#lastName").val(),
            Email: $("#email").val(),
            Mobile: $("#phone1").val(),
            SelectedRegions: radioList,
        });
        $.ajax({
            url: "/Admin/EditAdministratorInformation",
            type: "Get",
            async: false,
            contentType: "application/json",
            data: {
                data1: data1,
            },
            success: function (response) {
                window.location.href = response.redirect;
            }
        })
    }
    if ($("#confirmEmail").val() != $("#email").val()) {
        $("#confirmEmailValidation").text("Confirm Email and Email is not Same");
    }
    if (!$("#email").val().match(validRegex)) {
        $("#emailValidation").text("Emai is Not Valid");
    }
    if (radioList.length == 0) {
        validateRadioButton();
    }
})

////  Mailing & Billing Information

$(document).on("change", "#address1", function () {
    if ($(this).val().length == 0) {
        $("#address1Validation").text("Address1 is required");
    }
    else {
        $("#address1Validation").text("");
    }
})

$(document).on("change", "#address2", function () {
    if ($(this).val().length == 0) {
        $("#address2Validation").text("Address2 is required");
    }
    else {
        $("#address2Validation").text("");
    }
})

$(document).on("change", "#city", function () {
    if ($(this).val().length == 0) {
        $("#cityValidation").text("City is required");
    }
    else {
        $("#cityValidation").text("");
    }
})

$(document).on("change", "#zip", function () {
    if ($(this).val().length == 0) {
        $("#zipValidation").text("Zip is required");
    }
    else {
        $("#zipValidation").text("");
    }
})

$(document).on("change", "#phone2", function () {
    if ($(this).val().length == 0) {
        $("#phone2Validation").text("Addtional-Mobile is required");
    }
    else {
        $("#phone2Validation").text("");
    }
})

$(document).on("click", "#mailingAndBillingForm", function () {
    if ($("#address1").val().length != 0 && $("#phone2").val().length != 0 && $("#zip").val().length != 0 ||
        $("#city").val().length != 0 && $("#address2").val().length != 0) {
        var data = JSON.stringify({
            Address1: $("#address1").val(),
            Address2: $("#address2").val(),
            City: $("#city").val(),
            ZipCode: $("#zip").val(),
            Phone: $("#phone2").val(),
            SelectedRegion: $("#state").val(),
        })
        $.ajax({
            url: "/Admin/EditMailingAndBillingInformation",
            type: "Get",
            async: false,
            contentType: "application/json",
            data: {
                data: data,
            },
            success: function (response) {
                window.location.href = response.redirect;
            }
        })
    }
})
