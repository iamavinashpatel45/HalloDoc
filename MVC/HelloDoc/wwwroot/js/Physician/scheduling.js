var monthWise;

$(document).ready(function () {
    monthWise = new Date();
    monthWise.setDate(1);
    setMonth();
    $("#physician").remove();
})

function setMonth() {       ///   display Month 
    var formatter = new Intl.DateTimeFormat('en-US', {
        month: 'short',
        year: 'numeric'
    });
    $("#display").text(formatter.format(monthWise));
}

async function getData() {
    const formatter = new Intl.DateTimeFormat('en-US', {
        weekday: 'long',
        month: 'short',
        day: 'numeric',
        year: 'numeric'
    });
    await $.ajax({
        url: "/Physician/GetMonthWiseData",
        type: "Get",
        contentType: "application/json",
        async: true,
        data: {
            time: formatter.format(monthWise),
        },
        success: function (response) {
            $(".tab").html(response);
        }
    })
}

$(document).on("click", "#previous", async function () {
    monthWise.setMonth(monthWise.getMonth() - 1);
    await getData();
    setMonth();
})

$(document).on("click", "#next", async function () {
    monthWise.setMonth(monthWise.getMonth() + 1);
    await getData();
    setMonth();
})

$(document).on("change", "#shiftDate", function () {
    const formatter = new Intl.DateTimeFormat('en-US', {
        weekday: 'long',
        month: 'short',
        day: 'numeric',
        year: 'numeric'
    });
    var currentDate = new Date();
    var selectedDate = new Date($(this).val());
    console.log(currentDate);
    console.log(selectedDate);
    if (formatter.format(selectedDate) == formatter.format(currentDate)) {
        var currentTime = currentDate.getHours() + ":" + currentDate.getMinutes();
        $("#startTime").attr('min', currentTime);
        $("#endTime").attr('min', currentTime);
    }
    else {
        $("#startTime").removeAttr('min');
        $("#endTime").removeAttr('min');
    }
})


/// create shift (popup)

$(document).on("change", "#isRepeat", function () {
    if ($(this).is(":checked")) {
        $("#repeatFields").prop("disabled", false);
    }
    else {
        $("#repeatEnd").val("");
        $(".weekDay").prop("checked", false);
        $("#repeatFields").prop("disabled", true);
        $("#repeatEndValidation").text("");
    }
});


$(document).on("change", ".weekDay", function () {
    if (!$(this).is(":checked")) {
        $("#repeatValidation").text("Week Day is required");
    }
    else {
        $("#repeatValidation").text("");
    }
})

$(document).on("submit", "#createShiftForm", function (e) {
    if ($("#regionCreateShift").val().length == 0 || $("#endTime").val().length == 0 || $("#startTime").val().length == 0 || $("#shiftDate").val().length == 0
        || $("#physician").val().length == 0) {
        e.preventDefault();
    }
    if ($("#isRepeat").is(":checked")) {
        if (!$(".weekDay").is(":checked") || $("#repeatEnd").val().length == 0) {
            e.preventDefault();
            if (!$(".weekDay").is(":checked")) {
                $("#repeatValidation").text("Week Day is required");
            }
            else {
                $("#repeatValidation").text("");
            }
        }
    }
})