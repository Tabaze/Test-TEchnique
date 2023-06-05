import { getAll, login } from "./service.js";

$(document).ready(function () {
    $('#formLogIn').on('submit', this, function () {
        let data = formToJson($(this))
        console.log(data)
        let dt = login(data)
        console.log(dt)
        if (dt) {
            
        }
        else {
            alertify.error("Not Found")
        }
    })
});