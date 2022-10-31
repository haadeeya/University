﻿function login() {
    event.preventDefault();

    sendData();
}

function sendData() {
    const XHR = new XMLHttpRequest();

    XHR.addEventListener("load", (event) => {
        let data = JSON.parse(event.target.responseText);

        if (data.error) {
            toastr.error('Oops ' + data.error);
            return false;
        } else {
            window.location = data.url;
        }
    });

    XHR.addEventListener("error", (event) => {
        toastr.error('Something went wrong');
    });
    //debugger;
    XHR.open("POST", "/Account/Login");
    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;

    let data = new FormData();
    data.set('Username', username);
    data.set('Password', password);

    console.log(data);

    XHR.send(data);
}