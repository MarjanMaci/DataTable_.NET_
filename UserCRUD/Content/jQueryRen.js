import $ from jQuery

jQuery.ajax({
    type: "GET",
    url: "/user",
    success: function (data) {
        console.log(data);
    }
});