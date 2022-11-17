"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var toastr = require("toastr");
function login() {
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
    //debugger;
    XHR.open("POST", "/Account/Login");
    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;
    var data = new FormData();
    data.set('Username', username);
    data.set('Password', password);
    console.log(data);
    XHR.send(data);
}
//# sourceMappingURL=Login.js.map