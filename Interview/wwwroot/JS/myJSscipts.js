var popup = document.getElementById("popup");
var openPopup = document.getElementById("open-popup");
var closePopup = document.getElementsByClassName("close")[0];

openPopup.onclick = function () {
    popup.style.display = "block";
}

closePopup.onclick = function () {
    popup.style.display = "none";
}

window.onclick = function (event) {
    if (event.target == popup) {
        popup.style.display = "none";
    }
}