function loadDropdown() {
    event.preventDefault();

    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", "/Student/GetSubjects", false);
    xmlHttp.send(null);
    var jsonobj = JSON.parse(xmlHttp.responseText);
    console.log(jsonobj);
    for (var i = 0; i < jsonobj.length; i++) {
        //debugger;
        var option = document.createElement("option");
        option.text = jsonobj[i].SubjectName;
        option.value = jsonobj[i].SubjectId;
        console.log(jsonobj[i]);
        var select = document.getElementById("subjectname0");
        console.log(select);
        select.appendChild(option);
    }
}

let count = 1;
function addSubject() {
    event.preventDefault();
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", "/Student/GetSubjects", false);
    xmlHttp.send(null);
    var jsonobj = JSON.parse(xmlHttp.responseText);
    console.log(count);
    var newselect = `<select id="subjectname${count}" name="user_subject">`
    for (var i = 0; i < jsonobj.length; i++) {
        //debugger;
        newselect += `<option value="${jsonobj[i].SubjectId}">${jsonobj[i].SubjectName}</option >`
    }
    newselect +=`</select>`
    let subjectresult = `<div class="dropdowncontainer" id="dropdown` + count +`">
                            <button onclick="removeSubject()">-</button>
                            <div class="item">
                                <label for="subject">Subject Name:</label>` + newselect + `
                            </div>

                            <div class="item">
                                <label for="grade">Grade:</label>
                                <select id="grade${count}" name="user_grade">
                                    <option value="A">A</option>
                                    <option value="B">B</option>
                                    <option value="C">C</option>
                                    <option value="D">D</option>
                                    <option value="E">E</option>
                                    <option value="F">F</option>
                                </select>
                           </div>
                       </div>`
    document.getElementById('dropdown').innerHTML += subjectresult;
    count++;
}

function removeSubject() {
    event.preventDefault();
    console.log("subjectname" + (count - 1));
    var dropdown = document.getElementById("dropdown" + (count - 1));
    dropdown.remove();
    count--;
}

function createProfile(event) {
    event.preventDefault();
    var subjects=[];
    var subject;
    var studentsubject;

    for (var i = 0; i < count; i++) {
        console.log(document.getElementById('subjectname' + i).value);
        subject = {
            'SubjectId': document.getElementById('subjectname' + i).value,
            'SubjectName': document.getElementById('subjectname' + i).options[document.getElementById('subjectname' + i).selectedIndex].text,
        };
        studentsubject = {
            'SubjectId': document.getElementById('subjectname' + i).value,
            'Subject': subject,
            'Grade': document.getElementById('grade' + i).value
        };
        subjects.push(studentsubject);
    }

    let student = {
        'Name': document.getElementById("fname").value,
        'Surname': document.getElementById("lname").value,
        'NID': document.getElementById("nid").value,
        'GuardianName': document.getElementById("gname").value, 
        'EmailAddress': document.getElementById("email").value,
        'DateOfBirth': document.getElementById("dateofbirth").value,
        'PhoneNumber': document.getElementById("phonenumber").value,
        'Subjects': subjects
    };

    var url = "/Student/CreateProfile";
    var request = new XMLHttpRequest();

    request.open("POST", url, false);
    request.setRequestHeader("Content-Type", "application/json");
    request.onreadystatechange = function () {
        if (request.readyState == XMLHttpRequest.DONE && request.responseText != "") {
            let response = JSON.parse(request.responseText);
            if (!request.error) {
                toastr.success("Registration successful. Redirecting User...");
                window.location = response.url;
            }
        }
        else {
            toastr.error('Unable to Register student');
            return false;
        }
    };
    request.send(JSON.stringify(student));
}




