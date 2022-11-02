function loadStudents() {
    event.preventDefault();

    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", "/Admin/GetStudents", false);
    xmlHttp.send(null);
    var jsonobj = JSON.parse(xmlHttp.responseText);
    console.log(jsonobj);

    const table = document.getElementById('students');
    for (var i = 0; i < jsonobj.length; i++) {
        var tr = document.createElement('tr');

        var td1 = document.createElement('td');
        var td2 = document.createElement('td');
        var td3 = document.createElement('td');
        var td4 = document.createElement('td');
        var td5 = document.createElement('td');
        var td6 = document.createElement('td');
        var td7 = document.createElement('td');
        var td8 = document.createElement('td');
        var td9 = document.createElement('td');
        var td10 = document.createElement('td');

        var text1 = document.createTextNode(jsonobj[i].Id);
        var text2 = document.createTextNode(jsonobj[i].Surname);
        var text3 = document.createTextNode(jsonobj[i].Name);
        var text4 = document.createTextNode(jsonobj[i].GuardianName);
        var text5 = document.createTextNode(jsonobj[i].PhoneNumber);
        var text6 = document.createTextNode(jsonobj[i].EmailAddress);

        var dateString = jsonobj[i].DateOfBirth.substr(6);
        var currentTime = new Date(parseInt(dateString));
        var month = currentTime.getMonth() + 1;
        var day = currentTime.getDate();
        var year = currentTime.getFullYear();
        var date = day + "/" + month + "/" + year;

        var text7 = document.createTextNode(date);
        var subjects = "";
        for (var j = 0; j < jsonobj[i].Subjects.length; j++) {
            if (j == jsonobj[i].Subjects.length - 1) {
                subjects += jsonobj[i].Subjects[j].Subject.SubjectName;
            }
            else {
                subjects += jsonobj[i].Subjects[j].Subject.SubjectName + "; ";
            }
            
        }
        var text8 = document.createTextNode(subjects);
        var text9 = document.createTextNode(jsonobj[i].Marks);
        var text10 = document.createTextNode(jsonobj[i].Status);

        td1.appendChild(text1);
        td2.appendChild(text2);
        td3.appendChild(text3);
        td4.appendChild(text4);
        td5.appendChild(text5);
        td6.appendChild(text6);
        td7.appendChild(text7);
        td8.appendChild(text8);
        td9.appendChild(text9);
        td10.appendChild(text10);
        tr.appendChild(td1);
        tr.appendChild(td2);
        tr.appendChild(td3);
        tr.appendChild(td4);
        tr.appendChild(td5);
        tr.appendChild(td6);
        tr.appendChild(td7);
        tr.appendChild(td8);
        tr.appendChild(td9);
        tr.appendChild(td10);

        table.appendChild(tr);
    }
}

function tableToCSV() {

    var csv_data = [];

    var rows = document.getElementsByTagName('tr');
    for (var i = 0; i < rows.length; i++) {

        var cols = rows[i].querySelectorAll('td,th');

        var csvrow = [];
        for (var j = 0; j < cols.length; j++) {

            csvrow.push(cols[j].innerHTML);
        }

        csv_data.push(csvrow.join(","));
    }

    csv_data = csv_data.join('\n');

    downloadCSVFile(csv_data);

}

function downloadCSVFile(csv_data) {

    CSVFile = new Blob([csv_data], {
        type: "text/csv"
    });

    var temp_link = document.createElement('a');

    temp_link.download = "students.csv";
    var url = window.URL.createObjectURL(CSVFile);
    temp_link.href = url;

    temp_link.style.display = "none";
    document.body.appendChild(temp_link);

    temp_link.click();
    document.body.removeChild(temp_link);
}