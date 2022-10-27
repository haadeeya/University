function loadDropdown() {
    event.preventDefault();

    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", "/Home/GetSubjects", false);
    xmlHttp.send(null);
    var jsonobj = JSON.parse(xmlHttp.responseText);
    console.log(jsonobj);
    for (var i = 0; i < jsonobj.length; i++) {
        //debugger;
        var option = document.createElement("option");
        option.text = jsonobj[i].SubjectName;
        option.value = jsonobj[i].SubjectId;
        console.log(jsonobj[i]);
        var select = document.getElementById("subjectname");
        console.log(select);
        select.appendChild(option);

    }
}

function addSubject() {

}

