
//Restaurants multiple selection
$(function () {
    $(".MultipleCheckboxes[name='restaurants']").multiselect({
        enableClickableOptGroups: true,
        includeSelectAllOption: false,
        allSelectedText: null,
        nonSelectedText: 'Select',
        enableFiltering: true,
        filterBehavior: 'text',
        includeFilterClearBtn: false,
        buttonWidth: 'auto'
    });
});

//Image file extention validation
const filesInputs = document.querySelectorAll("input[type=file]");
for (let fileInput of filesInputs) {
    fileInput.addEventListener("input", validateFileObjects);
    function validateFileObjects() {
        var allowedExtensions = /\.(jpeg|jpg|png|bmp|gif|jfif)$/i;
        var fileObjects = [...filesInputs[0].files];
        var fileName = fileObjects[0].name;
            if (!allowedExtensions.exec(fileName)) {
                document.querySelector(".imageFileErrors").innerHTML = "Please Enter a valid image file for " + fileInput.name;
                document.querySelector(".imageFileErrors").hidden = false;
                fileInput.value = "";
            }

    }
}

