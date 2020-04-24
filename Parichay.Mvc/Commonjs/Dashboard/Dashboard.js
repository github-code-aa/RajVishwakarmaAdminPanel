GetSessiontime();
function GetSessiontime() {
    $.getJSON('/Login/GetSession', function (data, textStatus, jqXHR) {
        $("#SessionTimeout").val(data);

        localStorage["Counter"] = parseInt($("#SessionTimeout").val());
        localStorage["CounterToUpdateSession"] = parseInt($("#SessionTimeout").val());
    });
}
var Path = location.host;
var VirtualDirectory;
        if (Path.indexOf("localhost") >= 0 && Path.indexOf(":") >= 0) {
        VirtualDirectory = "";
}
        else {
            var pathname = window.location.pathname;
    var VirtualDir = pathname.split('/');
    VirtualDirectory = VirtualDir[1];
    VirtualDirectory = '/' + VirtualDirectory;
}
$(document).ready(function () {

    $('#btn_logout').click(function (e) {
        console.log("LOGGING OUT USER");

        $.ajax({
            url: '/Login/EndSession/',
            data: "",
            type: "POST",
            contentType: false,
            processData: false,
            success: function (data, textStatus, xhr) {
                if (data.success === true) {
                    console.log("DELETING SESSION COMPLTED");
                    var LocationReload = '/LogIn/Index';
                    location.replace(LocationReload);
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                console.log("ERROR WHILE DELETING SESSION - EXCEPTION");
            }
        });
    });


    var myInterval = setInterval(function () {

        localStorage["Counter"] = localStorage["Counter"] - 1
        localStorage["CounterToUpdateSession"] = localStorage["CounterToUpdateSession"] - 1


        if (localStorage["Counter"] <= 0) {

            localStorage["Counter"] = 0;
            localStorage["CounterToUpdateSession"] = 0;
            clearInterval(myInterval);

            var myTimeout = setTimeout(function () {
                swal({
                    title: "SESSION!",
                    text: "EXPIRED!",
                    type: "error"
                }, function () {

                    $.ajax({
                        url: '/Login/EndSession/',
                        data: "",
                        type: "POST",
                        contentType: false,
                        processData: false,
                        success: function (data, textStatus, xhr) {
                            if (data.success === true) {
                                console.log("DELETING SESSION COMPLTED");
                                var LocationReload = '/LogIn/Index';
                                location.replace(LocationReload);
                            }

                        },
                        error: function (xhr, textStatus, errorThrown) {
                            console.log("ERROR WHILE DELETING SESSION - EXCEPTION");
                        }
                    });



                });
            }, 1000);
        }
    }, 1000);

    $(document).ajaxStart(function () {
        localStorage["Counter"] = parseInt($("#SessionTimeout").val());
    });

    $("input,select,div").click(function () {
        if (localStorage["CounterToUpdateSession"] <= 100) {
            $.ajax({
                url: '/Login/ExtendSession/',
                data: "",
                type: "POST",
                contentType: false,
                processData: false,
                success: function (data, textStatus, xhr) {
                    if (data.success === true) {
                        console.log("EXTENDING SESSION COMPLETED");

                        GetSessiontime();
                        //localStorage["CounterToUpdateSession"] = parseInt($("#SessionTimeout").val());
                    }
                    else if (data.success === false) {
                        console.log("ERROR WHILE EXTENDING SESSION - FALSE");
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.log("ERROR WHILE EXTENDING SESSION - EXCEPTION");
                }
            });
        }
        localStorage["Counter"] = parseInt($("#SessionTimeout").val());
    });


});

