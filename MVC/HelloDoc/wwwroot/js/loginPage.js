var temp = true;
$(document).on("click", "#passwordIcon", function () {
    if (temp) {
        $(this).addClass("bi-eye-slash");
        $(this).removeClass("bi-eye");
        $("#password").prop("type", "text");
    }
    else {
        $(this).addClass("bi-eye");
        $(this).removeClass("bi-eye-slash");
        $("#password").prop("type", "password");
    }
    temp = !temp;
})

var temp2 = true;
$(document).on("click", "#passwordIcon2", function () {
    if (temp2) {
        $(this).addClass("bi-eye-slash");
        $(this).removeClass("bi-eye");
        $("#conformpassword").prop("type", "text");
    }
    else {
        $(this).addClass("bi-eye");
        $(this).removeClass("bi-eye-slash");
        $("#conformpassword").prop("type", "password");
    }
    temp2 = !temp2;
})