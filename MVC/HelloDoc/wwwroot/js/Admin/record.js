
///  For Record Page

$(document).on("click", ".searchButton", function () {
    getRecordData();
})

$(document).on("click", ".resetRecords", function () {
    $("#filterForm").trigger("reset");
    getRecordData();
})

function getRecordData() {
    $.ajax({
        url: "/Admin/GetRecordsTableDate",
        type: "Post",
        data: $("#filterForm").serializeArray(),
        success: function (response) {
            $(".tableData").html(response);
        }
    })
}


// for Email log Page

$(document).on("click", ".searchButtonEmailLogs", function () {
    getEmailLogs();
})

$(document).on("click", ".resetEmaillog", function () {
    $("#filterFormEmailLogs").trigger("reset");
    getEmailLogs();
})

function getEmailLogs() {
    $.ajax({
        url: "/Admin/GetEmailLogsTableDate",
        type: "Post",
        data: $("#filterFormEmailLogs").serializeArray(),
        success: function (response) {
            $(".tableData").html(response);
        }
    })
}


/// for Sms log Page

$(document).on("click", ".searchButtonSMSLogs", function () {
    getSmslLogs();
})

$(document).on("click", ".resetSMSLogs", function () {
    $("#filterFormSMSLogs").trigger("reset");
    getSmslLogs();
})

function getSmslLogs() {
    $.ajax({
        url: "/Admin/GetSMSLogsTableDate",
        type: "Post",
        data: $("#filterFormSMSLogs").serializeArray(),
        success: function (response) {
            $(".tableData").html(response);
        }
    })
}

/// for Patient History page

$(document).on("click", ".searchButtonPatientHistory", function () {
    getPatientHistory(1);
})

$(document).on("click", ".resetPatientHistory", function () {
    $("#patientHistoryForm").trigger("reset");
    getPatientHistory(1);
})

function getPatientHistory(pageNo) {
    var data = JSON.stringify({
        FirstName: $("#firstname").val(),
        LastName: $("#lastName").val(),
        Email: $("#email").val(),
        Phone: $("#phone").val(),
    })
    $.ajax({
        url: "/Admin/GetPatinetHistoryTableDate",
        type: "Post",
        data: {
            model: data,
            pageNo: pageNo,
        },
        success: function (response) {
            $(".tableData").html(response);
        }
    })
}

function navigateToLastPage(totalRequestCount) {       /// for last page in pagination
    var lastPageNo = getotalRequestCount % 10 != 0 ? parseInt(totalRequestCount / 10, 10) + 1 : parseInt(totalRequestCount / 10, 10);
    getPatientHistory(lastPageNo);
}

function openRecord(userId){
    $.ajax({
        url: "/Admin/GetRecords",
        type: "Get",
        contentType: "application/json",
        data: {
            userId: userId,
        },
        success: function (response) {
            window.location.href = response.redirect;
        }
    })
}


////  Patient Record

function getPatientRecord(pageNo) {
    $.ajax({
        url: "/Admin/GetPatientRecord",
        type: "Get",
        contentType: "application/json",
        data: {
            pageNo: pageNo,
        },
        success: function (response) {
            $(".recordList").html(response);
        }
    })
}

function navigation(requestId, actionName) {
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

function navigatToViewCase(requestId) {
    navigation(requestId, "ViewCase");
}

function navigatToViewDocuments(requestId) {
    navigation(requestId, "ViewDocument");
}

function navigateToLastPagePatinetRecord(totalRequestCount) {       /// for last page in pagination
    var lastPageNo = getotalRequestCount % 10 != 0 ? parseInt(totalRequestCount / 10, 10) + 1 : parseInt(totalRequestCount / 10, 10);
    getPatientRecord(lastPageNo);
}


///   for Block History


$(document).on("click", ".searchButtonBlockHistory", function () {
    getBlckHistoryData(1);
})

$(document).on("click", ".resetBlockHistory", function () {
    $("#blockHistoryForm").trigger("reset");
    getBlckHistoryData(1);
})

function getBlckHistoryData(pageNo) {
    var data = JSON.stringify({
        Name: $("#firstname").val(),
        Email: $("#email").val(),
        Phone: $("#phone").val(),
    });
    $.ajax({
        url: "/Admin/GetBlockHistoryTableDate",
        type: "Post",
        data: {
            model: data,
            pageNo: pageNo,
            date: $("#date").val(),
        },
        success: function (response) {
            $(".tableData").html(response);
        }
    })
}

function navigateToLastPageBlockHistory(totalRequestCount) {       /// for last page in pagination
    var lastPageNo = getotalRequestCount % 10 != 0 ? parseInt(totalRequestCount / 10, 10) + 1 : parseInt(totalRequestCount / 10, 10);
    getPatientRecord(lastPageNo);
}

function unblockRequest(requestId) {
    $.ajax({
        url: "/Admin/UnblockRequest",
        type: "Post",
        data: {
            requestId: requestId,
        },
        success: function (response) {
            window.location.href = response.redirect;
        }
    })
}