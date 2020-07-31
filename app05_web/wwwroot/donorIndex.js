(function () {
    "use strict";
    
    //-----------------
    //Get elements
    //-----------------
    var dropdownItems = document.getElementsByClassName("dropdown-item");

    var form1 = document.getElementById("form1"); 
    var donorName = document.getElementById("donorName");
    var donorButton = document.getElementById("donorButton");

    var form2 = document.getElementById("form2");
    var recipientName = document.getElementById("recipientName");
    var recipientButton = document.getElementById("recipientButton");

    var form3 = document.getElementById("form3");
    var ZIPName = document.getElementById("ZIPName");
    var ZIPButton = document.getElementById("ZIPButton");

    var form4 = document.getElementById("form4");
    var occupationName = document.getElementById("occupationName");
    var occupationButton = document.getElementById("occupationButton");

    //-----------------
    //handle drop down changed
    //-----------------
    var dropdown1Changed = function (event) {
        var chosenForm = event.target.dataset.form;
        //alert("You clicked the dropdown item for " + form + "!");

        form1.classList.add("my-hidden"); // add form 3 and 4 after these two
        form2.classList.add("my-hidden");
        form3.classList.add("my-hidden");
        form4.classList.add("my-hidden");

        var formToShow = document.getElementById(chosenForm);
        formToShow.classList.remove("my-hidden");
    };
    //-----------------
    //handle donor button click
    //-----------------
    //replicate for other searches and add form 3 and form 4 for HTML markup
    var donorButtonClicked = function (event) {
        // alert("You want to search for donor name " + donorName.value);

        var xhr = new XMLHttpRequest();

        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4) { //done
                if (xhr.status === 200) { //ok
                    var donors = JSON.parse(xhr.responseText);

                    var table = '<table class="table table-sm table-bordered"><thead><tr><th scope="col">Election Year</th><th scope="col">Donor</th><th scope="col">Address</th><th scope="col">City</th><th scope="col">Donor ZIP</th><th scope="col">Donor Employer</th><th scope="col">Donor Occupation</th><th scope="col">Donation Amount</th><th scope="col">Recipient</th><th scope="col">Donation Date</th></tr></thead><tbody>';

                    var i;
                    for (i = 0; i < donors.length; i++) {
                        table = table + '<tr><th scope="row">' + donors[i].feC_Election_Year + '</th><td>' + donors[i].contributor_Name + '</td><td>' + donors[i].contributor_Street_1 + '</td><td>' + donors[i].contributor_City + '</td><td>' + donors[i].contributor_ZIP + '</td><td>' + donors[i].contributor_Employer + '</td><td>' + donors[i].contributor_Occupation + '</td><td>' + donors[i].contribution_Receipt_Amount + '</td><td>' + donors[i].committee_name + '</td><td>' + donors[i].contribution_Receipt_Date + '</td></tr>';
                    }

                    table = table + '</tbody></table>';

                    document.getElementById("response_01").innerHTML = table;

                } else {
                    console.log('Error: ' + xhr.status);
                }
            }
        };

        var URL = 'https://localhost:5001/donors/' + donorName.value;
        xhr.open('GET', URL);
        xhr.send(null);
    };

    //-----------------
    //handle recipient button click
    //-----------------
    
    var recipientButtonClicked = function (event) {
    // {
    //     alert("You want to search for recipient name " + recipientName.value);
    // };
        var xhr = new XMLHttpRequest();

        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4) { //done
                if (xhr.status === 200) { //ok
                    var donors = JSON.parse(xhr.responseText);

                    var table = '<table class="table table-sm table-bordered"><thead><tr><th scope="col">Election Year</th><th scope="col">Donor</th><th scope="col">Address</th><th scope="col">City</th><th scope="col">Donor ZIP</th><th scope="col">Donor Employer</th><th scope="col">Donor Occupation</th><th scope="col">Donation Amount</th><th scope="col">Recipient</th><th scope="col">Donation Date</th></tr></thead><tbody>';

                    var i;
                    for (i = 0; i < donors.length; i++) {
                        table = table + '<tr><th scope="row">' + donors[i].feC_Election_Year + '</th><td>' + donors[i].contributor_Name + '</td><td>' + donors[i].contributor_Street_1 + '</td><td>' + donors[i].contributor_City + '</td><td>' + donors[i].contributor_ZIP + '</td><td>' + donors[i].contributor_Employer + '</td><td>' + donors[i].contributor_Occupation + '</td><td>' + donors[i].contribution_Receipt_Amount + '</td><td>' + donors[i].committee_name + '</td><td>' + donors[i].contribution_Receipt_Date + '</td></tr>';
                    }

                    table = table + '</tbody></table>';

                    document.getElementById("response_01").innerHTML = table;

                } else {
                    console.log('Error: ' + xhr.status);
                }
            }
        };
        var URL = 'https://localhost:5001/recipients/' + recipientName.value;
        xhr.open('GET', URL);
        xhr.send(null);
        
    };
    //-----------------
    //handle form3 button click
    
    // var ZIPName = document.getElementById("ZIP");
    // var ZIPButton = document.getElementById("ZIPButton");
    //-----------------

    var ZIPButtonClicked = function (event) {

        var xhr = new XMLHttpRequest();

        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4) { //done
                if (xhr.status === 200) { //ok
                    var donors = JSON.parse(xhr.responseText);

                    var table = '<table class="table table-sm table-bordered"><thead><tr><th scope="col">Election Year</th><th scope="col">Donor</th><th scope="col">Address</th><th scope="col">City</th><th scope="col">Donor ZIP</th><th scope="col">Donor Employer</th><th scope="col">Donor Occupation</th><th scope="col">Donation Amount</th><th scope="col">Recipient</th><th scope="col">Donation Date</th></tr></thead><tbody>';

                    var i;
                    for (i = 0; i < donors.length; i++) {
                        table = table + '<tr><th scope="row">' + donors[i].feC_Election_Year + '</th><td>' + donors[i].contributor_Name + '</td><td>' + donors[i].contributor_Street_1 + '</td><td>' + donors[i].contributor_City + '</td><td>' + donors[i].contributor_ZIP + '</td><td>' + donors[i].contributor_Employer + '</td><td>' + donors[i].contributor_Occupation + '</td><td>' + donors[i].contribution_Receipt_Amount + '</td><td>' + donors[i].committee_name + '</td><td>' + donors[i].contribution_Receipt_Date + '</td></tr>';
                    }

                    table = table + '</tbody></table>';

                    document.getElementById("response_01").innerHTML = table;

                } else {
                    console.log('Error: ' + xhr.status);
                }
            }
        };

        var URL = 'https://localhost:5001/donorzipcode/' + ZIPName.value;
        xhr.open('GET', URL);
        xhr.send(null);
    };
    //-----------------
    //handle form4 button click
    //-----------------

    var occupationButtonClicked = function (event) {

            var xhr = new XMLHttpRequest();
    
            xhr.onreadystatechange = function () {
                if (xhr.readyState === 4) { //done
                    if (xhr.status === 200) { //ok
                        var donors = JSON.parse(xhr.responseText);
    
                        var table = '<table class="table table-sm table-bordered"><thead><tr><th scope="col">Election Year</th><th scope="col">Donor</th><th scope="col">Address</th><th scope="col">City</th><th scope="col">Donor ZIP</th><th scope="col">Donor Employer</th><th scope="col">Donor Occupation</th><th scope="col">Donation Amount</th><th scope="col">Recipient</th><th scope="col">Donation Date</th></tr></thead><tbody>';
    
                        var i;
                        for (i = 0; i < donors.length; i++) {
                            table = table + '<tr><th scope="row">' + donors[i].feC_Election_Year + '</th><td>' + donors[i].contributor_Name + '</td><td>' + donors[i].contributor_Street_1 + '</td><td>' + donors[i].contributor_City + '</td><td>' + donors[i].contributor_ZIP + '</td><td>' + donors[i].contributor_Employer + '</td><td>' + donors[i].contributor_Occupation + '</td><td>' + donors[i].contribution_Receipt_Amount + '</td><td>' + donors[i].committee_name + '</td><td>' + donors[i].contribution_Receipt_Date + '</td></tr>';
                        }
    
                        table = table + '</tbody></table>';
    
                        document.getElementById("response_01").innerHTML = table;
    
                    } else {
                        console.log('Error: ' + xhr.status);
                    }
                }
            };
            var URL = 'https://localhost:5001/donoroccupation/' + occupationName.value;
            xhr.open('GET', URL);
            xhr.send(null);
            
        };

    //-----------------
    //attach event handlers
    //-----------------
    var i;
    for (i = 0; i < dropdownItems.length; i++) {
        dropdownItems[i].addEventListener("click", dropdown1Changed);
    }
    donorButton.addEventListener("click", donorButtonClicked);
    recipientButton.addEventListener("click", recipientButtonClicked);
    ZIPButton.addEventListener("click", ZIPButtonClicked);
    occupationButton.addEventListener("click", occupationButtonClicked);

    //add buttons for other two forms to add the event listener

})();