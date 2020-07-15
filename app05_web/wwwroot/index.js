(function () {
    "use strict";

    var buttonOk = document.getElementById("buttonOk");

    var showMessage = function () {

        var xhr = new XMLHttpRequest();

        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4) { //done
                if (xhr.status === 200) { //ok
                    var employees = JSON.parse(xhr.responseText);

                    var table = '<table class="table table-bordered"><thead><tr><th scope="col">EmpID</th><th scope="col">EmpFirstName</th><th scope="col">EmpLastName</th><th scope="col">EmpSalary</th><th scope="col">DeptID</th></tr></thead><tbody>';

                    var i;
                    for (i = 0; i < employees.length; i++) {
                        table = table + '<tr><th scope="row">' + employees[i].empID + '</th><td>' + employees[i].empFirstName + '</td><td>' + employees[i].empLastName + '</td><td>' + employees[i].empSalary + '</td><td>' + employees[i].deptID + '</td></tr>';
                    }

                    table = table + '</tbody></table>';

                    document.getElementById("response_01").innerHTML = table;
                } else {
                    console.log('Error: ' + xhr.status);
                }
            }
        };

        var email = document.getElementById("exampleInputEmail1");
        var id = email.value;

        xhr.open('GET', 'https://localhost:5001/weatherforecast/' + id);
        xhr.send(null);
    };

    buttonOk.addEventListener("click", showMessage);

})();