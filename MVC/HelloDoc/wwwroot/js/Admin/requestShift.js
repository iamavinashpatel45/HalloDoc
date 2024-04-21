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

function getTableData(pageNo) {     ///get table data 
    $.ajax({
        url: '/Admin/GetRequestShifTableData',
        type: 'GET',
        contentType: 'application/json',
        data: {
            pageNo: pageNo,
            regionId: $("#regionFilter").val(),
            isMonth: isMonth,
        },
        success: function (response) {
            $("#tableData").html(response);
        }
    })
}

$(document).on("change", "#regionFilter", function () {    /// region filter
    getTableData(1);
})

var isMonth = false;
$(document).on("click", "#isMonthButton", function () {     ///  for month button
    isMonth = !isMonth;
    getTableData(1);
    if (isMonth) {
        $(this).text("View All Shifts");
    }
    else {
        $(this).text("View Current Month Shifts");
    }
})


///  Approve and Delete Shift 

var IdList = [];

$(document).on("click","#mainCheckBox",function(){
    if ($(this).is(':checked')) {
        $('.checkBox').prop('checked', true);
        var temp = $('.checkBox');
        IdList = [];
        for (let i = 0; i < temp.length; i++) {
            IdList.push($(temp[i]).attr("id"));
        }
    }
    else {
        $('.checkBox').prop('checked', false);
        IdList = [];
    }
});

function onCheckboxChnage(fileId, totalcount) {
    var id = $(fileId).attr("id");
    if ($(fileId).is(":checked")) {
        IdList.push(id);
    }
    else {
        var index = IdList.indexOf(id);
        IdList.splice(index, 1);
    }
    if (totalcount == IdList.length) {
        $('#mainCheckBox').prop('checked', true);
        $('.checkBox').prop('checked', true);
        var temp = $('.checkBox');
        IdList = [];
        for (let i = 0; i < temp.length; i++) {
            IdList.push($(temp[i]).attr("id"));
        }
    }
    else {
        $('#mainCheckBox').prop('checked', false);
    }
}

$(document).on("click", "#delete", function () {     ///  for slected shift delete
    chnageShiftDetails(false);
});

$(document).on("click", "#approved", function () {     ///  for slected shift approve
    chnageShiftDetails(true);
});

function chnageShiftDetails(isApprove) {
    if (IdList.length > 0) {
        $.ajax({
            url: '/Admin/UpdateShiftDetails',
            type: 'GET',
            contentType: 'application/json',
            data: {
                data: JSON.stringify(IdList),
                isApprove: isApprove,
            },
            success: function (response) {
                window.location.href = response.redirect;
            }
        })
    }
}
