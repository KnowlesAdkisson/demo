(function () {
    "use strict";

    //------
    //Delete
    //------

    var doDelete = function (event) {

        var empID = event.target.dataset.empid;
        //alert("You want to delete EmpID " + empID);

        var xhr = new XMLHttpRequest();

        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4) { //done
                if (xhr.status === 200) { //ok
                    alert(xhr.responseText);
                    doSearch();
                } else {
                    alert(xhr.responseText);
                }
            }
        };

        xhr.open('GET', 'https://localhost:5001/api/employees/delete?empid=' + empID);
        xhr.send(null);
    }

    //------------------
    //Search (ok button)
    //------------------

    var buttonOk = document.getElementById("buttonOk");

    var doSearch = function () {

        var xhr = new XMLHttpRequest();

        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4) { //done
                if (xhr.status === 200) { //ok
                    var employees = JSON.parse(xhr.responseText);

                    var table = '<table class="table table-sm table-bordered"><thead><tr><th scope="col">EmpID</th><th scope="col">EmpFirstName</th><th scope="col">EmpLastName</th><th scope="col">EmpSalary</th><th scope="col">DeptID</th><th></th></tr></thead><tbody>';

                    var i;
                    for (i = 0; i < employees.length; i++) {
                        table = table + '<tr><th scope="row">' + employees[i].empID + '</th><td>' + employees[i].empFirstName + '</td><td>' + employees[i].empLastName + '</td><td>' + employees[i].empSalary + '</td><td>' + employees[i].deptID + '</td><td><button data-empid="' + employees[i].empID + '" type="button" class="btn btn-outline-danger btn-jm-delete">Delete</button></td></tr>';
                    }

                    table = table + '</tbody></table>';

                    document.getElementById("response_01").innerHTML = table;

                    var buttons = document.getElementsByClassName("btn-jm-delete");

                    var j;
                    for (j = 0; j < buttons.length; j++) {
                        buttons[j].addEventListener("click", doDelete);
                    }

                } else {
                    console.log('Error: ' + xhr.status);
                }
            }
        };

        var department = document.getElementById("inputSearch");
        var id = department.value;

        xhr.open('GET', 'https://localhost:5001/api/employees/' + id);
        xhr.send(null);
    };

    buttonOk.addEventListener("click", doSearch);

    //---------------------
    //Insert (save button)'
    //---------------------

    var buttonSave = document.getElementById("buttonSave");

    var doInsert = function () {

        var xhr = new XMLHttpRequest();

        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4) { //done
                if (xhr.status === 200) { //ok
                    alert(xhr.responseText);
                } else {
                    alert(xhr.responseText);
                }
            }
        };

        var empID = parseInt(document.getElementById("empID").value, 10);
        var empFirstName = document.getElementById("empFirstName").value;
        var empLastName = document.getElementById("empLastName").value;
        var empSalary = parseInt(document.getElementById("empSalary").value, 10);
        var deptID = parseInt(document.getElementById("deptID").value, 10);

        var employee = { "EmpID": empID, "EmpFirstName": empFirstName, "EmpLastName": empLastName, "EmpSalary": empSalary, "DeptID": deptID };
        var employeeJSON = JSON.stringify(employee);

        //console.log(employeeJSON);
        xhr.open('POST', 'https://localhost:5001/api/employees/insert', true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.send(employeeJSON);
    };

    buttonSave.addEventListener("click", doInsert);

    // ---------
    // Nav links
    // ---------

    var nav02 = document.getElementById("nav_02");

    var handleNav02Click = function (e) {

        var page02 = document.getElementById("page_02");
        page02.classList.remove("my-hidden");

        e.preventDefault();
    };

    nav02.addEventListener("click", handleNav02Click);


})();