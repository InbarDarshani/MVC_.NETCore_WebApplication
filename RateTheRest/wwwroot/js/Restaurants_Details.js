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

//Weather
const city = $(".weather")[0].getAttribute("city")
$.ajax({
    url: 'http://api.weatherstack.com/current',
    data: {
        access_key: 'd0fb22bfd7cc353225d7c6126a89631b',
        query: city
    },
    dataType: 'json',
    success: function (apiResponse) {
        $(".weather").html(`Current temperature in ${apiResponse.location.name} is ${apiResponse.current.temperature}℃`);
    }
});
