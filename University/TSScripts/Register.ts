import * as toastr from "toastr";
function register() {
    event.preventDefault();

    sendData();
}

function sendData() {
    const XHR = new XMLHttpRequest();

    XHR.addEventListener("load", (event) => {
        let data = JSON.parse(XHR.responseText);

        if (data.error) {
            toastr.error(data.error);
            return false;
        } else {
            window.location = data.url;
        }
    });

    XHR.addEventListener("error", (event) => {
        toastr.error('Something went wrong');
    });
    XHR.open("POST", "/Account/Register");
    var username = (<HTMLInputElement>document.getElementById("username")).value;
    var password = (<HTMLInputElement>document.getElementById("password")).value;
    var email = (<HTMLInputElement>document.getElementById("email")).value;

    let data = new FormData();
    data.set('Username', username);
    data.set('Password', password);
    data.set('Email', email);

    console.log(data);

    XHR.send(data);
}