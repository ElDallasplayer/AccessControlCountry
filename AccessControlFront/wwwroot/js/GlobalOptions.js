let _globalOptions = JSON.parse("[]");

$.ajax({
    url: _baseUrl + "/GlobalOptions/GetGlobalOptions",
    type: "get",
    success: function (response) {
        _globalOptions = JSON.parse(response);
    }
})