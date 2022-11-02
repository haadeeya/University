function register() {
    event.preventDefault();

    sendData();
}

function sendData() {
    const XHR = new XMLHttpRequest();

    // Define what happens on successful data submission
    XHR.addEventListener("load", (event) => {
        let data = JSON.parse(event.target.responseText);

        if (data.error) {
            toastr.error('Oops ' + data.error);
            return false;
        } else {
            window.location = data.url;
        }
    });

    // Define what happens in case of error
    XHR.addEventListener("error", (event) => {
        toastr.error('Something went wrong');
    });
    //debugger;
    // Set up our request
    XHR.open("POST", "/Account/Register");
    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;
    var email = document.getElementById("email").value;

    let data = new FormData();
    data.set('Username', username);
    data.set('Password', password);
    data.set('Email', email);

    console.log(data);

    // The data sent is what the user provided in the form
    XHR.send(data);
}