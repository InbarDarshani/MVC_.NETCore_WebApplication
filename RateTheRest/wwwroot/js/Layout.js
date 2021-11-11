
//Set RenderBody size accordint to navbar
document.addEventListener("DOMContentLoaded", function () {
    // add padding top to show content behind navbar
    navbar_height = document.querySelector(".navbar").offsetHeight;
    document.querySelector(".renderBody").style.paddingTop = navbar_height + 'px';
    // add canvas
    var c = document.getElementById("myCanvas");
    var ctx = c.getContext("2d");
    ctx.font = "16px Arial";
    ctx.fillText("Ⓒ Designed by Inbar, Heli, Linoy and Aviv", 10, 25);
});

