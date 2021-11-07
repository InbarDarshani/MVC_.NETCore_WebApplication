
//Set RenderBody size accordint to navbar
document.addEventListener("DOMContentLoaded", function () {
    // add padding top to show content behind navbar
    navbar_height = document.querySelector(".navbar").offsetHeight;
    document.querySelector(".renderBody").style.paddingTop = navbar_height + 'px';
});