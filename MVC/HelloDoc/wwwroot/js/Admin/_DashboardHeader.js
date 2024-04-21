function sidebar() {  /// for mobile view silebar
    var temp = $("#side-bar").css("display");
    if (temp == "none") {
        $("#side-bar").css("display","block");
    } else {
        $("#side-bar").css("display", "none");
    }
}
