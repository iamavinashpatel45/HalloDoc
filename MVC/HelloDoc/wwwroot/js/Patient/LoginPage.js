var temp = true;
var temp2 = true;
function changeIcon() {
    if (temp) {
        document.getElementById("show-password").style.display = "none";
        document.getElementById("hide-password").style.display = "inline";
        document.getElementById("password").type = "text";
        temp = false;
    } else {
        document.getElementById("hide-password").style.display = "none";
        document.getElementById("show-password").style.display = "inline";
        document.getElementById("password").type = "password";
        temp = true;
    }
}

function changeIcon2() {
    if (temp2) {
        document.getElementById("show-conformpassword").style.display = "none";
        document.getElementById("hide-conformpassword").style.display = "inline";
        document.getElementById("conformpassword").type = "text";
        temp2 = false;
    } else {
        document.getElementById("hide-conformpassword").style.display = "none";
        document.getElementById("show-conformpassword").style.display = "inline";
        document.getElementById("conformpassword").type = "password";
        temp2 = true;
    }
}


// function darkmoad(){
//   // data-bs-theme="dark"
//   var html=document.querySelector("html");
//   console.log(html);
//   if(html.getAttribute("data-bs-theme")!="dark"){
//     html.setAttribute("data-bs-theme");
//   }
//   else
//   {
//     html.setAttribute("data-bs-theme")="auto";
//   }
// }
