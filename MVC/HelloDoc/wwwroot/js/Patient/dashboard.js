function changeTab(element1, element2) {
    //var temp;
    //temp = document.getElementsByClassName("tab-content");
    //for (i = 0; i < temp.length; i++) {
    //    temp[i].style.display = "none";
    //}
    // display.getElementsByClassName("element").style.fontweight="bold";
    //element1.style.display = "Block";
    if (element2) {
        document.getElementById("dashboard").style.backgroundColor = "#dcdbdb";
        document.getElementById("dashboard").style.color = "cadetblue";
        document.getElementById("profile").style.backgroundColor = "white";
        document.getElementById("profile").style.color = "black";
    } else {
        document.getElementById("profile").style.backgroundColor = "#dcdbdb";
        document.getElementById("profile").style.color = "cadetblue";
        document.getElementById("dashboard").style.backgroundColor = "white";
        document.getElementById("dashboard").style.color = "black";
    }
    /*//document.getElementById("side-bar").style.display = "none";*/
}

function sidebar() {  /// for mobile view silebar
    var temp = $("#header-buttom").css("display");
    if (temp == "none") {
        $("#header-buttom").css("display", "flex");
    } else {
        $("#header-buttom").css("display", "none");
    }
}

function ProfileEdit(temp) {
    if (temp) {
        $('#edit').css("display", "none");
        $('#submit').css("display", "flex");
        $('#submit').css("justifyContent", "end");
        document.getElementById("Fieldset").disabled = false;
    }
    else {
        document.getElementById("Fieldset").disabled = true;
        $('#edit').css("display", "flex");
        $('#edit').css("justifyContent", "end");
        $('#submit').css("display", "none");
    }
}

//function changeTab(element1) {
//    if (element1) {
//        $("#dashboardHr").css("display", "block");
//        $("#profileHr").css("display", "none");
//    }
//    else {
//        $("#profileHr").css("display", "block");
//        $("#dashboardHr").css("display", "none");
//    }
//}

function changeRadio(element) {
    if (element) {
        $('.forMe').css("background-color", "cadetblue");
        $('.forMe').css("color", "white");
        $('.forSomeOne').css("background-color", "white");
        $('.forSomeOne').css("color", "cadetblue");
        $("#forMe").css("display", "block");
        $("#forSomeOne").css("display", "none");
    }
    else {
        $('.forSomeOne').css("background-color", "cadetblue");
        $('.forSomeOne').css("color", "white");
        $('.forMe').css("background-color", "white");
        $('.forMe').css("color", "cadetblue");
        $("#forSomeOne").css("display", "block");
        $("#forMe").css("display", "none");
    }
}

function NavigatToViewDocument(id) {
    $.ajax({
        url: '/Patient/SetRequestId',
        type: 'GET',
        contentType: 'application/json',
        data: { requestId: id },
        success: function (response) {
            window.location.href = response.redirect;
        }
    })
}
