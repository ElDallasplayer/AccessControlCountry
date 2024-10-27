$(document).ready(function () {
    $('form').on('submit', function (event) {
        event.preventDefault();

        var user = $('input[name="_user"]').val();
        var password = $('input[name="_password"]').val();

        if (user === "" || password === "") {
            alert("User and Password are required.");
            return;
        }

        $.ajax({
            type: 'POST',
            url: _baseUrl + '/User/LoginUser',
            data: {
                _user: user,
                _password: password
            },
            success: function (response) {

                console.log(response);

                if (response.response == "OK") {
                    window.location.href = response.redirectUrl;
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert("An error occurred while processing your request. Please, contact the administrator from web site.");
            }
        });
    });
});