
const topratedRestaurantCity = $("#topratedRestaurant").getAttribute("city")

    $.ajax({
    url: 'https://api.weatherstack.com/current',
    data: {
        access_key: 'd0fb22bfd7cc353225d7c6126a89631b',
        query: 'New York'
    },
    dataType: 'json',
    success: function (apiResponse) {
        console.log(`Current temperature in ${apiResponse.location.name} is ${apiResponse.current.temperature}℃`);
    }
});
