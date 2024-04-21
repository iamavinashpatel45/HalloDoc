const partialViewNames = ["", "_dayWiseScheduling", "_weekWiseScheduling", "_monthWiseScheduling"]
const weekDays = ["sun", "mon", "tue", "wed", "thu", "fir", "sat"]
var currentType = 1;
var dayWise, weekWise, monthWise;

async function chnagetab(type) {
    $(".tabButtons").removeClass("active");
    $("#displayWeek").text("");
    currentType = type;
    switch (type) {
        case 1: $("#dayButton").addClass("active");
                dayWise = new Date();
                await getData();
                setDate();
                break;
        case 2: $("#weekButton").addClass("active");
                weekWise = new Date();
                weekWise.setDate(weekWise.getDate() - weekWise.getDay())
                await getData();
                setWeek();
                break;
        case 3: $("#monthButton").addClass("active");
                monthWise = new Date();
                monthWise.setDate(1);
                await getData();
                setMonth();
                break;
    }
}

async function getData() {
    const formatter = new Intl.DateTimeFormat('en-US', {
        weekday: 'long',
        month: 'short',
        day: 'numeric',
        year: 'numeric'
    });
    var time;
    switch (currentType) {
        case 1: time = dayWise; break;
        case 2: time = weekWise; break;
        case 3: time = monthWise;
    }
    time = formatter.format(time);
    await $.ajax({
            url: "/Admin/ChangeTab",
            type: "Get",
            contentType: "application/json",
            async: true,
            data: {
                name: partialViewNames[currentType],
                regionId: $("#regionsList").val(),
                type: currentType,
                time: time,
            },
            success: function (response) {
                $(".tab").html(response);
            }
        })
}

$(document).on("change", "#regionsList", function () {
    getData();
})

$(document).ready(function () {
    dayWise = new Date;
    setDate(dayWise);
})

$(document).on("click", "#previous", async function () {
    switch (currentType) {
        case 1: dayWise.setDate(dayWise.getDate() - 1);
                await getData();
                setDate();
                break;
        case 2: weekWise.setDate(weekWise.getDate() - 7);
                await getData();
                setWeek();
                break;
        case 3: monthWise.setMonth(monthWise.getMonth() - 1);
                await getData();
                setMonth();
    }
})

$(document).on("click", "#next", async function () {
    switch (currentType) {
        case 1: dayWise.setDate(dayWise.getDate() + 1);
                await getData();
                setDate();
                break;
        case 2: weekWise.setDate(weekWise.getDate() + 7);
                await getData();
                setWeek();
                break;
        case 3: monthWise.setMonth(monthWise.getMonth() + 1);
                await getData();
                setMonth();
    }
})

function setDate() {        ///   display Date 
    const formatter = new Intl.DateTimeFormat('en-US', {
        weekday: 'long',
        month: 'short',
        day: 'numeric',
        year: 'numeric'
    });
    $("#display").text(formatter.format(dayWise));
}

function setWeek() {      ///   display Week duration
    var date = new Date(weekWise);
    var formatter = new Intl.DateTimeFormat('en-US', {
        month: 'short',
        day: 'numeric',
        year: 'numeric'
    });
    $("#display").text(formatter.format(date));
    $("#displayWeek").text(" - " + formatter.format(date.setDate((6 - date.getDay()) + date.getDate())));
    for (i = 6; i >= 0; i--) {
        $(`#${weekDays[i]}`).text(date.getDate());
        date.setDate(date.getDate() - 1);
    }
}

function setMonth() {       ///   display Month 
    var formatter = new Intl.DateTimeFormat('en-US', {
        month: 'short',
        year: 'numeric'
    });
    $("#display").text(formatter.format(monthWise));
}

async function viewMore(date) {   ///  on click of view more on monthwise Scheduling
    var tempDate = new Date(monthWise);
    dayWise = new Date(tempDate.setDate(date));
    currentType = 1;
    $(".tabButtons").removeClass("active");
    $("#displayWeek").text("");
    $("#dayButton").addClass("active");
    setDate();
    await getData();
}


/////   popup - create shift

$(document).on("change", "#isRepeat", function () {
    if ($(this).is(":checked")) {
        $("#repeatFields").prop("disabled", false);
    }
    else {
        $("#repeatEnd").val("");
        $(".weekDay").prop("checked",false);
        $("#repeatFields").prop("disabled", true);
        $("#repeatEndValidation").text("");
    }
});


$(document).on("change", "#regionCreateShift", function () {
    if ($(this).val().length != 0) {
        $.ajax({
            url: "/Admin/GetPhysicians",
            type: "Get",
            contentType: "application/json",
            data: {
                regionId: $(this).val(),
            },
            success: function (response) {
                $("#physician").html('<option disabled selected value="">Physicians</option>');
                $.each(response, function (index, item) {
                    var option = "<option value=" + index + ">" + item + "</option>";
                    $("#physician").append(option);
                });
            }
        });
    }
})


$(document).on("change", ".weekDay", function () {
    if (!$(this).is(":checked")) {
        $("#repeatValidation").text("Week Day is required");
    }
    else {
        $("#repeatValidation").text("");
    }
})

$(document).on("submit", "#createShiftForm", function (e)
{
    if ($("#regionCreateShift").val().length == 0 || $("#endTime").val().length == 0 || $("#startTime").val().length == 0 || $("#shiftDate").val().length == 0
        || $("#physician").val().length == 0 ) {
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




/////   popup - View(edit) shift

var date, startTime, endTime, Id;

$(document).on("click", ".editbutton", function () {
    editShift(true);
})

$(document).on("click", ".cancleEdit", function () {
    editShift(false);
})

function editShift(isEdit) {
    if (isEdit) {
        $(".editbutton").css("display", "none");
        $(".edit").css("display", "block");
        $("#editShiftDetils").prop("disabled", false);
    }
    else {
        $("#shiftDateViewShift").val(date);
        $("#startTimeViewShift").val(startTime);
        $("#endTimeViewShift").val(endTime);
        $(".edit").css("display", "none");
        $(".editbutton").css("display", "block");
        $("#editShiftDetils").prop("disabled", true);
    }
}

function openViewShiftModal(shiftDetailsId) {
    $.ajax({
        url: "/Admin/GetShiftDetails",
        type: "Get",
        contentType: "application/json",
        data: {
            shiftDetailsId,
        },
        success: function (response) {
            Id = shiftDetailsId
            $("#region").val(response.region);
            $("#Name").val(response.physicianName);
            date = response.shiftDate;
            startTime = response.startTime;
            endTime = response.endTime;
            $("#shiftDateViewShift").val(date);
            $("#startTimeViewShift").val(startTime);
            $("#endTimeViewShift").val(endTime);
        }
    });
}

$(document).on("click", ".conformButton", function () {    ////   for save changes from viewshift popup
    var data = JSON.stringify({
        ShiftDate: $("#shiftDateViewShift").val(),
        EndTime: $("#endTimeViewShift").val(),
        StartTime: $("#startTimeViewShift").val(),
        ShiftDetailsId: Id.toString(),
    });
    $.ajax({
        url: "/Admin/EditShiftDetails",
        type: "Get",
        contentType: "application/json",
        data: {
            data,
        },
        success: function (response) {
            window.location.href = response.redirect;
        }
    });
})

$(document).on("click", "#deletebutton", function () {     ///   for delete shift from viewshift opo up
    $.ajax({
        url: '/Admin/DeleteShiftDetails',
        type: 'GET',
        contentType: 'application/json',
        data: {
            data: JSON.stringify([Id.toString()]),
        },
        success: function (response) {
            window.location.href = response.redirect;
        }
    })
})


///  Provider on call

$(document).on("change", "#regionsListProviderOnCall", function () {
    $.ajax({
        url: "/Admin/GetProviderOnCall",
        type: "Get",
        contentType: "application/json",
        async: true,
        data: {
            regionId: $(this).val(),
        },
        success: function (response) {
            $(".providerList").html(response);
        }
    })
})