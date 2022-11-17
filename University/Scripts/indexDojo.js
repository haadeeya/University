require(["dojo/dom", "dojo/request", "dojo/domReady!"], function (dom, request) {
    request("/Student/GetStudent").then(
        function (response) {
            var jsonobj = JSON.parse(response);
            console.log(jsonobj);
            dom.byId('fname').textContent = jsonobj.Name;
            dom.byId('lname').textContent = jsonobj.Surname;
            dom.byId('nid').textContent = jsonobj.NID;
            var dateString = jsonobj.DateOfBirth.substr(6);
            var currentDate = new Date(parseInt(dateString));
            var month = currentDate.getMonth() + 1;
            var day = currentDate.getDate();
            var year = currentDate.getFullYear();
            var date = day + "/" + month + "/" + year;
            dom.byId("dob").textContent = date;
            dom.byId("emailaddress").textContent = jsonobj.EmailAddress;
            dom.byId("phonenum").textContent = jsonobj.PhoneNumber;
            dom.byId("guardianname").textContent = jsonobj.GuardianName;
            for (var i = 0; i < jsonobj.Subjects.length; i++) {
                let subject = `
            <div class="property-name">${jsonobj.Subjects[i].Subject.SubjectName}</div>
            <div class="property-value">${jsonobj.Subjects[i].Grade}</div>
`
                dom.byId("subjects").innerHTML += subject;
                dom.byId("guardianname").textContent
            }
        },
        function (error) {
            console.log(error);
            toastr.error("Someting went wrong");
        }
    );
});



