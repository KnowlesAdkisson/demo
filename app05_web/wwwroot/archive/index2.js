(function () {
    "use strict";

    //your code goes here

    var button01 = document.getElementById("button_01");
    var button02 = document.getElementById("button_02");

    var handleButton01Click = function () {
        button01.classList.add("my-hidden");
        button02.classList.remove("my-hidden");
    }

    var handleButton02Click = function () {
        button01.classList.remove("my-hidden");
        button02.classList.add("my-hidden");
    }

    button01.addEventListener("mouseenter", handleButton01Click);
    button02.addEventListener("mouseenter", handleButton02Click);

})();