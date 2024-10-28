$(document).ready(function () {
    $.get(_baseUrl + "/Employee/GetEmployeeList").done((response) => {
        $(".table-container-employee").append(response);
    })
})