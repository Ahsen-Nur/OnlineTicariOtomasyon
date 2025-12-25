$(document).ready(function () {

    if (localStorage.getItem("darkMode") === "true") {
        enableDarkMode();
    }

    $("#darkModeBtn").click(function () {
        if ($("body").hasClass("dark")) {
            disableDarkMode();
        } else {
            enableDarkMode();
        }
    });

});

function enableDarkMode() {
    $("body").addClass("dark bg-dark text-light");
    localStorage.setItem("darkMode", "true");
}

function disableDarkMode() {
    $("body").removeClass("dark bg-dark text-light");
    localStorage.setItem("darkMode", "false");
}
