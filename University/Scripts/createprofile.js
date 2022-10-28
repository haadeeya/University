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
        var select = document.getElementById("subjectname");
        console.log(select);
        select.appendChild(option);

    }
}

function addSubject() {
    event.preventDefault();
    let subjectresult = `<div id="dropdown" onclick='loadDropdown()'>
                        <div class="text-container">
                            <label for="subject">Subject Name:</label>
                            <select id="subjectname" name="user_subject">
                            </select>
                        </div>

                        <div class="text-container">
                            <label for="grade">Grade:</label>
                            <select id="grade" name="user_grade">
                                <option value="A">A</option>
                                <option value="B">B</option>
                                <option value="C">C</option>
                                <option value="D">D</option>
                                <option value="E">E</option>
                                <option value="F">F</option>
                            </select>
                        </div>
                    </div>`
    document.getElementById("dropdown").innerHTML += subjectresult;
}

