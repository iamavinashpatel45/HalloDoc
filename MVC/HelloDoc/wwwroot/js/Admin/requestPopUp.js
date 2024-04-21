var requesterTextColor = [ "", "text-danger", "text-success", "text-warning", "text-primary" ];
var requester = ["", "Business", "Patient", "Family", "Concierge"];

function changeSelect(document) {
    $.ajax({
        url: "/Admin/GetPhysicians",
        type: "Get",
        contentType: "application/json",
        data: {
            regionId: $(document).val(),
        },
        success: function (response) {
            $(".physician").html('<option disabled selected value="">Physicians</option>');
            $.each(response, function (index, item) {
                var option = "<option value=" + index + ">"+item+"</option>";
                $(".physician").append(option);
            });
        }
    });
}

function displayPopUp(id) {
    var idName = "#name-" + id;
    $(".patientRequestId").val(id);
    $(".patientName").text($(idName).text());
    $('.validation').css("display", "none");
}

function displaySendAgreementPopUp(id) {
    $(".patientRequestId").val(id);
    $.ajax({
        url: "/Admin/GetEmailAndMobileNumber",
        type: "Get",
        async: false,
        contentType: "application/json",
        data: {
            requestId: id,
        },
        success: function (response) {
            $("#patientEmail").val(response.item1);
            $("#patientNumber").val(response.item2);
            $("#requesterIcon").addClass(requesterTextColor[response.item3]);
            $("#requesterName").html(requester[response.item3]);
        }
    })
}

function clearCase() {
    $.ajax({
        url: "/Admin/ClearPopUp",
        type: "Get",
        async: false,
        contentType: "application/json",
        data: {
            requestId: $(".patientRequestId").val(),
        },
        success: function (response) {
            window.location.href = response.redirect;
        }
    })
}

//$("#assignForm").submit(function (e) {
//    if ($("#assignRequestRegion").val() == null) {
//        e.preventDefault();
//        $("#assignValidationforRegions").css("display", "block");
//    }
//    else {
//        $("#assignValidationforRegions").css("display", "none");
//    }
//    if ($("#assignRequestPhysician").val() == null) {
//        e.preventDefault();
//        $("#assignValidationPhysician").css("display", "block");
//    }
//    else {
//        $("#assignValidationPhysician").css("display", "none");
//    }
//})

$("#transferForm").submit(function (e) {
    if ($("#transferRequestRegion").val() == null) {
        e.preventDefault();
        $("#transferValidationforRegions").css("display", "block");
    }
    else {
        $("#transferValidationforRegions").css("display", "none");
    }
    if ($("#transferRequestPhysician").val() == null) {
        e.preventDefault();
        $("#transferValidationPhysician").css("display", "block");
    }
    else {
        $("#transferValidationPhysician").css("display", "none");
    }
})

//$("#cacelForm").submit(function (e) {
//    if ($("#cancelRequestRegions").val() == null) {
//        e.preventDefault();
//        $("#cancelValidation").css("display", "block");
//    }
//    else {
//        $("#cancelValidation").css("display", "none");
//    }
//})

$("#sendAgreementForm").submit(function (e) {
    if ($("#patientNumber").val().length == 0) {
        e.preventDefault();
        $("#patientNumberValidation").css("display", "block");
    }
    else {
        $("#patientNumberValidation").css("display", "none");
    }
    if ($("#patientEmail").val().length == 0) {
        e.preventDefault();
        $("#patientEmailValidation1").css("display", "block");
        $("#patientEmailValidation2").css("display", "none");
    }
    else {
        $("#patientEmailValidation1").css("display", "none");
        var validRegex = /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/;
        if (!$("#patientEmail").val().match(validRegex)) {
            e.preventDefault();
            $("#patientEmailValidation2").css("display", "block");
        }
        else {
            $("#patientEmailValidation2").css("display", "none");
        }
    }
})


////

function navigatToViewCase(requestId) {
    navigation("ViewCase", requestId);
}

function navigatToViewNotes(requestId) {
    navigation("ViewNotes", requestId);
}

function navigatToViewDocuments(requestId) {
    navigation("ViewDocument", requestId);
}

function navigatToConcludeCare(requestId) {
    navigation("ConcludeCare", requestId);
}

function navigatToSendOrder(requestId) {
    navigation("SendOrder", requestId);
}

function navigatToEncounterForm(requestId) {
    navigation("EncounterForm", requestId);
}

function navigatToCloseCase(requestId) {
    navigation("CloseCase", requestId);
}


function navigation(actionName, requestId) {
    $.ajax({
        url: "/Admin/SetRequestId",
        type: "Get",
        contentType: "application/json",
        data: {
            requestId: requestId,
            actionName: actionName,
        },
        success: function (response) {
            window.location.href = response.redirect;
        }
    })
}


//// provider popup - contect provider

var email = false, sms = false;

$(document).on("change", "#email", function () {
    if ($(this).is(":checked")) {
        email = true;
    }
    else {
        email = false;
    }
    isBothRadionbuttonslected();
})

$(document).on("change", "#sms", function () {
    if ($(this).is(":checked")) {
        sms = true;
    }
    else {
        sms = false;
    }
    isBothRadionbuttonslected();
})

function isBothRadionbuttonslected() {
    if (email && sms) {
        $("#both").prop("checked", true);
    }
    else {
        $("#both").prop("checked", false);
    }
}

$(document).on("change", "#both", function () {
    if ($(this).is(":checked")) {
        sms = true;
        email = true;
        $("#email").prop("checked", true);
        $("#sms").prop("checked", true);
    }
    else {
        sms = false;
        email = false;
        $("#email").prop("checked", false);
        $("#sms").prop("checked", false);
    }
})

$(document).on("submit", "#contactForm", function (e) {
    if (!email && !sms) {
        e.preventDefault();
        $("#medium").css("display", "block");
    }
    else {
        $("#medium").css("display", "none");
    }
    if ($("#message").val().length == 0) {
        e.preventDefault();
        $("#messageValidation").css("display", "block");
    }
    else {
        $("#messageValidation").css("display", "none");
    }
})


////   Request Support

function messageValidation() {
    if ($("#message").val().length == 0) {
        $("#messageValidation").text("Message is required");
    }
    else {
        $("#messageValidation").text("");
    }
}

$(document).on("change", "#message", function () {
    messageValidation();
})

$("#requestSupportForm").submit(function (e) {
    if ($("#message").val().length == 0) {
        e.preventDefault();
        messageValidation();
    }
})




///  physician Encounter

function checkEncounter(isEncounter, requestId, isFinaliz) {
    if (isEncounter == 1 && !isFinaliz) {
        navigatToEncounterForm(requestId);
    }
    else if (isEncounter == 1 && isFinaliz) {
        $(".patientRequestId").val(requestId);
        $("#downloadReport").modal('show');
    }
    else {
        $(".patientRequestId").val(requestId);
        $("#encounterPopUp").modal('show');
    }
}

var isvideoCall = true;
function careButtonChnage(temp) {
    if (temp) {
        $("#videoCall").addClass("activeButton");
        $("#houseVisit").removeClass("activeButton");
        isvideoCall = true;
    }
    else {
        isvideoCall = false;
        $("#houseVisit").addClass("activeButton");
        $("#videoCall").removeClass("activeButton");
    }
}

function encounter() {
    $.ajax({
        url: "/Physician/SetEncounter",
        type: "Get",
        contentType: "application/json",
        data: {
            isVideoCall: isvideoCall,
            requestId: $("#requestId").val(),
        },
        success: function (response) {
            window.location.href = response.redirect;
        }
    })
}

//// download pdf popup

$(document).on("click", ".downloadPdf", function () {
    $("#downloadReport").modal('hide');
})

