var statusStrings = ["", "new", "pending", "active", "conclude", "close", "unpaid"];
var statusTableStrings = ["", "_NewTable", "_PendingTable", "_ActiveTable", "_ConcludeTable", "_CloseTable", "_UnpaidTable"];
var currentStatus = 1;         /// for which state is current
var url = "/Admin/GetTablesData"

function changeTable(temp) {      /// change view according to status
    $(".tables").css("display", "none");
    $(".optionButton").css('box-shadow', 'none');
    currentStatus = temp;
    $(`#${statusStrings[currentStatus]}`).css("display", "block");
    $(`#${statusStrings[currentStatus]}Option`).css('box-shadow', '10px 10px 5px #AAA');
    $("#statusText").text(`(${statusStrings[currentStatus]})`);
    getTableData(1);
}

function getTableData(pageNo) { ///get table data 
    $.ajax({
        url: url,
        type: 'GET',
        contentType: 'application/json',
        data: {
            pageNo: pageNo,
            status: statusStrings[currentStatus],
            partialViewName: statusTableStrings[currentStatus],
            patinetName: $(".searchPatient").val().trim().toLowerCase(),
            regionId: $(".searchRegion").val(),
            requesterTypeId: requesterType,
        },
        success: function (response) {
            setTableData(response);
        }
    })
}

function setTableData(response) {       /// set table date in tableview
    $(`#${statusStrings[currentStatus]}`).html(response);
}

requesterType = 0;
function requesterTypeSearch(_requesterType, pageNo) {     /// search based on requester type
    requesterType = _requesterType;
    $('.buttonHr').css("display", "none");
    $(`#hr-${_requesterType}`).css("display", "block");
    getTableData(pageNo);
}


//////

function getLastPageNo(totalRequestCount) {      /// get last page no
    return totalRequestCount % 10 != 0 ? parseInt(totalRequestCount / 10, 10) + 1 : parseInt(totalRequestCount / 10, 10);
}

function getNextPageData(currentPageNo, totalRequestCount) {   /// for next page in pagination
    var lastPageNo = getLastPageNo(totalRequestCount);
    if (currentPageNo < lastPageNo) {
        getTableData(currentPageNo + 1);
    }
}

function getPreviousPageData(currentPageNo) {   /// for previous page in pagination
    if (currentPageNo > 1) {
        getTableData(currentPageNo - 1);
    }
}

function navigateToFirstPage() {    /// for First page in pagination
    getTableData(1);
}

function navigateToLastPage(totalRequestCount) {       /// for last page in pagination
    var lastPageNo = getLastPageNo(totalRequestCount);
    getTableData(lastPageNo);
}

function patientSearch(pageNo) {    
    getTableData(pageNo);
}

function regionSearch(pageNo) {
    getTableData(pageNo);
}


///


$(document).on("click", "#exportData", function () {
    var temp = $(`#${statusStrings[currentStatus]}`).children()[2];
    var pageNo = $(temp).find(".currentPage").text();
    $.ajax({
        url: '/Admin/ExportData',
        type: 'GET',
        xhrFields: {
            responseType: 'arraybuffer'
        },
        data: {
            pageNo: pageNo,
            status: statusStrings[currentStatus],
            partialViewName: statusTableStrings[currentStatus],
            patinetName: $(".searchPatient").val(),
            regionId: $(".searchRegion").val(),
            requesterTypeId: requesterType,
        },
        success: function (response) {
            const a = document.createElement('a');
            var unit8array = new Uint8Array(response);
            a.href = window.URL.createObjectURL(new Blob([unit8array], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'}));
            a.download = 'Data.xlsx';
            a.click();
        }
    })
})


$(document).ready(function () {
    if ($("#physician").length > 0) {
        statusTableStrings = ["", "_NewTablePhysician", "_PendingTablePhysician", "_ActiveTablePhysician", "_ConcludeTablePhysician"];
        url = "/Physician/GetTablesData";
    }
})
