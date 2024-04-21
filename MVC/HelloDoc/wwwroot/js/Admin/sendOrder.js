function changeSelect() {
    $.ajax({
        url: "/Admin/GetBussinesses",
        type: "Get",
        contentType: "application/json",
        data: {
            professionId: $('#selectedProfession').val(),
        },
        success: function (response, xhr) {
            $("#selectedBusiness").children().remove();
            $("#selectedBusiness").append('<option disabled selected value="">Business</option>');
            $.each(response, function (index, item) {
                var option = "<option value=" + index + ">" + item + "</option>";
                $("#selectedBusiness").append(option);
            });
            $(".contact").val("").change();
            $(".email").val("").change();
            $(".faxNumber").val("").change();
        }
    });
}

function changeBussiness() {
    var venderId = $('#selectedBusiness').val();
    if (venderId != null) {
        $.ajax({
            url: "/Admin/GetBussinessData",
            type: 'Get',
            contentType: 'appliaction/json',
            data: {
                venderId: venderId,
            },
            success: function (response) {
                var valus = Object.values(response);
                $(".contact").val(valus[15]).change();
                $(".email").val(valus[14]).change();
                $(".faxNumber").val(valus[3]).change();
            }
        })
    }
    else {
        $(".contact").val("").change();
        $(".email").val("").change();
        $(".faxNumber").val("").change();
    }
}
//function orderDetailsValidation() {
//    if ($("#orderDetils").val().length == 0) {
//        $("#orderDetailsValidation").css("display", "block");
//    }
//    else {
//        $("#orderDetailsValidation").css("display", "none");
//    }
//}

//function bussinessValidation() {
//    if ($("#selectedBusiness").val() == null) {
//        $("#bussinessValidation").css("display", "block");
//    }
//    else {
//        $("#bussinessValidation").css("display", "none");
//    }
//}

//function professionValidation() {
//    if ($("#selectedProfession").val() == null) {
//        $("#professionValidation").css("display", "block");
//    }
//    else {
//        $("#professionValidation").css("display", "none");
//    }
//}

//$(document).on("change", "#selectedProfession", function () {
//    professionValidation();
//});

//$(document).on("change", "#selectedBusiness", function () {
//    bussinessValidation();
//});

//$(document).on("change", "#orderDetils", function () {
//    orderDetailsValidation();
//});

//$(document).on("submit", "#form", function (e) {
//    if ($("#selectedProfession").val() == null || $("#selectedBusiness").val() == null || $("#orderDetils").val().length == 0) {
//        e.preventDefault();
//        orderDetailsValidation();
//        professionValidation();
//        bussinessValidation();
//    }
//})
