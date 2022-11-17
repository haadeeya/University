"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var toastr = require("toastr");
function register() {
    event.preventDefault();
    sendData();
}
function sendData() {
    var XHR = new XMLHttpRequest();
    XHR.addEventListener("load", function (event) {
        var data = JSON.parse(XHR.responseText);
        if (data.error) {
            toastr.error(data.error);
            return false;
        }
        else {
            window.location = data.url;
        }
    });
    XHR.addEventListener("error", function (event) {
        toastr.error('Something went wrong');
    });
    XHR.open("POST", "/Account/Register");
    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;
    var email = document.getElementById("email").value;
    var data = new FormData();
    data.set('Username', username);
    data.set('Password', password);
    data.set('Email', email);
    console.log(data);
    XHR.send(data);
}
//# sourceMappingURL=Register.js.map