$(document).on("change", "#profession", function () {
    getTableData();
});

$(document).on("input", "#searchPartners", function () {
    getTableData();
});

function getTableData() {
    $.ajax({
        url: "/Admin/GetPatnersData",
        type: "Get",
        contentType: "application/json",
        data: {
            regionId: $("#profession").val(),
            searchElement: $("#searchPartners").val(),
        },
        success: function (response) {
            $(".tableData").html(response);
        }
    })
}