$(document).on('keydown', function (e) {
    if (e.keyCode === 27) { // ESC
        $.ajax({
            url: '/Login/EndSession/',
            data: "",
            type: "POST",
            contentType: false,
            processData: false,
            success: function (data, textStatus, xhr) {
                if (data.success === true) {
                    console.log("DELETING SESSION COMPLTED");
                   
                }

            },
            error: function (xhr, textStatus, errorThrown) {
                console.log("ERROR WHILE DELETING SESSION - EXCEPTION");
            }
        });


    }
});


$(document).ready(function () {

   
        $(function () {
            $("#btnLogin").on("click", function (e) {
                var $form = $("#frmlogin");
                $form.validate();
                if (!$form.valid()) {
                    return false;
                }
                e.preventDefault();
                var formData = new FormData();
                var data = $form.find(':input[name]:not(:disabled)').filter(':not(:checkbox), :checked').map(function () {
                    var input = $(this);
                    if (input.attr('type') === "file") {
                        formData.append(input.attr('name'), input[0].files[0]); 
                        return {
                            name: input.attr('name'),
                            value: input[0].files[0]
                        };
                    }
                    else {
                        formData.append(input.attr('name'), input.val());
                        return {
                            name: input.attr('name'),
                            value:$.trim(input.val())
                        };
                    }
                }).get();
                $(".block-page-btn-example-3").click();
                $.ajax({
                    url: '/Login/Login',
                    data: formData,
                    type: "POST",
                    contentType: false,
                    processData: false,
                    success: function (data, textStatus, jqXHR) {
                        if (data.success === true) {
                            $(".block-page-btn-example-3-unblock").click();
                            swal({
                                title: data.Caption,
                                text: "Please Logout after Use!",
                                type: "success",
                                showCancelButton: false,
                                showDuration: true,
                                confirmButtonClass: "btn-success",
                                confirmButtonText: "Go to Dashboard!",
                                closeOnConfirm: true
                            },
                                function () {
                                    var url = $("#RedirectTo").val();
                                    location.href = url;
                                    return true;
                                });
                        }
                        if (data.ExceptionMessage) {
                            $(".block-page-btn-example-3-unblock").click();
                            swal("Snap !!", data.ExceptionMessage, "error");
                            return false;
                        }
                        if (data.success === false) {
                            $(".block-page-btn-example-3-unblock").click();
                            swal("Oops !!", data.Caption, "error");
                            return false;
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        return false;
                    },
                    complete: function (jqXHR, textStatus) {
                    }
                });
            });
        });
        toastr.options = {
            'closeButton': true,
            'debug': false,
            'newestOnTop': false,
            'progressBar': true,
            'positionClass': 'toast-top-right',
            'preventDuplicates': false,
            'showDuration': '1000',
            'hideDuration': '1000',
            'timeOut': '5000',
            'extendedTimeOut': '1000',
            'showEasing': 'swing',
            'hideEasing': 'linear',
            'showMethod': 'fadeIn',
            'hideMethod': 'fadeOut'
        }
});
