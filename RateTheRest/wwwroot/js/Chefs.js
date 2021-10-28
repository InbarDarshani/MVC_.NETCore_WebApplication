
//Restaurants multiple selection
$(function () {
    $(".MultipleCheckboxes[name='restaurants']").multiselect({
        enableClickableOptGroups: true,
        includeSelectAllOption: false,
        allSelectedText: null,
        nonSelectedText: 'Select',
        enableFiltering: true,
        filterBehavior: 'text',
        filterPlaceholder: 'Search',
        includeFilterClearBtn: false,
        buttonWidth: 'auto'
    });
});



