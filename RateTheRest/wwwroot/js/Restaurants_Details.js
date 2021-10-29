
//Reviews
$(document).ready(function () {
    //Create a review button
    $("#createReview").click(function () {
        $.ajax({
            url: "/Reviews/CreatePartial",
            method: "GET",
            contentType: "application/json; charset=utf-8",

            success: function (result) {
                $("#createReviewPartial").html(result);
                $("#reviewRestaurantId")[0].value = $("#restaurantIdValue")[0].value;
            },

            error: function (err) {
                alert('Failed to get review creation' + err);
            }
        })
    });
})