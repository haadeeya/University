﻿function loadDetails() {

    event.preventDefault();

    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", "/Home/GetStudent", false);
    xmlHttp.send(null);
    var jsonobj = JSON.parse(xmlHttp.responseText);
    console.log(jsonobj);
        //debugger;
    var studname = document.getElementById("names");
    studname.textContent = jsonobj.Name + " " + jsonobj.Surname;
    document.getElementById("fname").textContent = jsonobj.Name;
    document.getElementById("lname").textContent = jsonobj.Surname;
    document.getElementById("nid").textContent = jsonobj.NID;
    var dateString = jsonobj.DateOfBirth.substr(6);
    var currentTime = new Date(parseInt(dateString));
    var month = currentTime.getMonth() + 1;
    var day = currentTime.getDate();
    var year = currentTime.getFullYear();
    var date = day + "/" + month + "/" + year;
    document.getElementById("dob").textContent = date;
    document.getElementById("emailaddress").textContent = jsonobj.EmailAddress;
    document.getElementById("phonenum").textContent = jsonobj.PhoneNumber;
    document.getElementById("guardianname").textContent = jsonobj.GuardianName;

    for (var i = 0; i < jsonobj.Subjects.length; i++) {
        console.log(jsonobj.Subjects[i].Subject.SubjectName);
        console.log(jsonobj.Subjects[i].Grade);
        document.getElementsByClassName("subjects").textContent = jsonobj.Subjects[i].Subject.SubjectName;
    }

}