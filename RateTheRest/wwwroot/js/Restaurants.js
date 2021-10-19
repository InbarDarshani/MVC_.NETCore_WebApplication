
//Opening Hours
const days = document.querySelectorAll("#openingHours .openingHours-day");

for (let d of days) {
    const checkbox = d.querySelector("[type='checkbox']");
    const from = d.querySelector("[name='from']");
    const to = d.querySelector("[name='to']");
    const day = d.querySelector("[name='days']");

    //Initial states
    checkbox.checked = false;
    from.disabled = true;
    to.disabled = true;

    //Treat checkbox change
    checkbox.addEventListener('change', function () {
        from.disabled = to.disabled = !checkbox.checked;
        from.required = to.required = checkbox.checked;

        if (!checkbox.checked)
            d.querySelector(".openingHoursErrors").hidden = true;
    });

    from.addEventListener('blur', validateTimeRange);
    to.addEventListener('blur', validateTimeRange);
    function validateTimeRange() {
        if (checkbox.checked && (timeToValue(from.value) >= timeToValue(to.value) || from.value == "" || to.value == "")) {
            d.querySelector(".openingHoursErrors").innerHTML = "Please Enter a Valid Time Range for " + day.value;
            d.querySelector(".openingHoursErrors").hidden = false;
        }
        else {
            d.querySelector(".openingHoursErrors").hidden = true;
        }
    }
}
function timeToValue(str) { return parseInt(str.split(":")[0]) * 60 + parseInt(str.split(":")[1]) }

//Tags multiple selection style
$(function () {
    $('#MultipleCheckboxes').multiselect({
        enableClickableOptGroups: true,
        includeSelectAllOption: true,
        nonSelectedText: 'Select'
    });
});



