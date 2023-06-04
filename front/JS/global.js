const Baseurl = 'http://localhost:5024/'
const formToJson = ($form) => {
    const data = {};
    $form.each(function () {
        $(this).find("input, select, textarea").each(function () {
            data[$(this).attr("name")] = $(this).val();
        });
    });
    return data
}
const ajaxJSON = (url, dt) => {
    let lt
    $.ajax({
        url: Baseurl + url,
        type: "POST",
        data: JSON.stringify(dt),
        contentType: "application/json",
        crossDomain: true,
        success: function (response) {
            console.log(response);
            lt = response
        },
        error: function (error) {
            console.log(error);
        }
    });
    return lt
}