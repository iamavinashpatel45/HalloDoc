function openSlidebar() {
    document.getElementById("sidebar").style.display = "flex";
    document.getElementById("sidebar").style.flexDirection = "column";
    if (screen.availWidth <= 768) {
        document.getElementById("right-side").style.position = "absolute"
        document.getElementById("right-side").style.left = "0px"
        document.getElementById("right-side").style.top = "80px"
        document.getElementById("right-side").style.zIndex = "-1";
        document.getElementById("right-side").style.position = "absolute"
        document.getElementById("right-side").style.left = "0px"
        document.getElementById("right-side").style.top = "80px"
        document.getElementById("right-side").style.zIndex = "-1";
    }
}

function closeSlidebar() {
    document.getElementById("sidebar").style.display = "none";
}


function slidebar() {

    if (document.getElementById("sidebar").style.display === "none") {
        openSlidebar();
    } else {
        closeSlidebar();
    }

}

function firstTimeLoadPage() {
    if (screen.availWidth <= 768) {
        document.getElementById("sidebar").style.display = "none";
    }
}