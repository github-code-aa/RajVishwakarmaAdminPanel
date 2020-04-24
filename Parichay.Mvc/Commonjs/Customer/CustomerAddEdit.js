$(document).ready(function () {

    $('#GstNumber').keyup(function () {
        $(this).val($(this).val().toUpperCase());
    });

    // 2 Capitalize string first character to uppercase
    $('#FirstName').keyup(function () {
        var caps = $('#FirstName').val();
        caps = caps.charAt(0).toUpperCase() + caps.slice(1);
        $('#FirstName').val(caps);
    });

    $('#MiddleName').keyup(function () {
        var caps = $('#MiddleName').val();
        caps = caps.charAt(0).toUpperCase() + caps.slice(1);
        $('#MiddleName').val(caps);
    });

    $('#LastName').keyup(function () {
        var caps = $('#LastName').val();
        caps = caps.charAt(0).toUpperCase() + caps.slice(1);
        $('#LastName').val(caps);
    });

    $('#AddressName').keyup(function () {
        var caps = $('#AddressName').val();
        caps = caps.charAt(0).toUpperCase() + caps.slice(1);
        $('#AddressName').val(caps);
    });

    $('#AddressLineOne').keyup(function () {
        var caps = $('#AddressLineOne').val();
        caps = caps.charAt(0).toUpperCase() + caps.slice(1);
        $('#AddressLineOne').val(caps);
    });

    $('#AddressLineTwo').keyup(function () {
        var caps = $('#AddressLineTwo').val();
        caps = caps.charAt(0).toUpperCase() + caps.slice(1);
        $('#AddressLineTwo').val(caps);
    });

    $('#Country').keyup(function () {
        var caps = $('#Country').val();
        caps = caps.charAt(0).toUpperCase() + caps.slice(1);
        $('#Country').val(caps);
    });

    $('#City').keyup(function () {
        var caps = $('#City').val();
        caps = caps.charAt(0).toUpperCase() + caps.slice(1);
        $('#City').val(caps);
    });

    $('#State').keyup(function () {
        var caps = $('#State').val();
        caps = caps.charAt(0).toUpperCase() + caps.slice(1);
        $('#State').val(caps);
    });
    // 3 Capitalize string every 1st chacter of word to uppercase
    $('#Remark').keyup(function () {
        var str = $('#Remark').val();


        var spart = str.split(" ");
        for (var i = 0; i < spart.length; i++) {
            var j = spart[i].charAt(0).toUpperCase();
            spart[i] = j + spart[i].substr(1);
        }
        $('#Remark').val(spart.join(" "));

    });

     /////////////////////////////////BASIC FUNCTION///////////////////////////////////////////////////
    if ($('#Id').val() > 0) {
        
        DisableFormInput();
        var EditAddressListValue = null;
        EditAddressListValue = jQuery.parseJSON($('#AddressGridSerialize').val());
        if (EditAddressListValue === "" || EditAddressListValue === null) {
            console.log("No Value in AddressGridSerialize");
        }
        else {
            for (var j = 0; j < EditAddressListValue.length; j++) {
                $("#myTableAddList").append("<tr id=" + EditAddressListValue[j].id + "><td>" + EditAddressListValue[j].AddressName + "</td><td>" + EditAddressListValue[j].AddressLineOne + "</td><td>" + EditAddressListValue[j].AddressLineTwo + "</td><td>" + EditAddressListValue[j].Country + "</td><td>" + EditAddressListValue[j].State + "</td><td>" + EditAddressListValue[j].City + "</td><td>" + EditAddressListValue[j].pincode + "</td></tr>");
            }
            InsertRadioButton();
            CheckRadioButton(EditAddressListValue);
            var $rows = $('#myTableAddList tr');
            $rows.find('input[type=radio]').prop('disabled', true);
        }
    }
    else {
        console.log("ADD FORM INITIALIZED");
    }

    function DisableFormInput() {

        var $form = $("#FormCustomer");
        $form.find(':input[name]:not(:disabled)').filter(':not(:checkbox), :checked').map(function () {
            var input = $(this);


            input.prop('disabled', true);

            $('#btn_SaveCustomer').prop('disabled', true);
            $('.Button_AddlistAdd').prop('disabled', true);

            


        });
    }

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "3000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    function htmlTableToJson(table, edit = 0, del = 0) {
        // If exists the cols: "edit" and "del" to remove from JSON just pass values = 1 to edit and del params
        var minus = edit + del;
        var data = [];
        var colsLength = $(table.find('thead tr')[0]).find('th').length - minus;
        var rowsLength = $(table.find('tbody tr')).length;

        // first row needs to be headers
        var headers = [];
        for (var i = 0; i < colsLength; i++) {
            //headers[i] = $(table.find('thead tr')[0]).find('td').eq(i).text();
            headers[i] = $(table.find('thead tr')[0]).find('th').eq(i).attr('id');
        }

        // go through cells
        for (var k = 0; k < rowsLength; k++) {
            var tableRow = $(table.find('tbody tr')[k]);
            var rowData = {};
            for (var j = 0; j < colsLength; j++) {

                if (j === 7) {
                    var isChecked = tableRow[0].cells[j].childNodes[0].checked;
                    if (isChecked === true) {
                        rowData[headers[j]] = true;
                    }
                    else {
                        rowData[headers[j]] = false;
                    }

                }
                else {
                    rowData[headers[j]] = tableRow.find('td').eq(j).text();
                }
            }
            data.push(rowData);
        }
        return data;
    }

    function htmlTableToJsonEdit(table) {
        // If exists the cols: "edit" and "del" to remove from JSON just pass values = 1 to edit and del params
        
        var data = [];
        var colsLength = $(table.find('thead tr')[0]).find('th').length ;
        var rowsLength = $(table.find('tbody tr')).length;

        // first row needs to be headers
        var headers = ['_AddressID', '_AddressName', '_AddressLineOne', '_AddressLinetwo', '_Country', '_State', '_City', '_pincode','_isDefault'];
        //for (var i = 0; i < colsLength; i++) {
        //    //headers[i] = $(table.find('thead tr')[0]).find('td').eq(i).text();
        //    headers[i] = $(table.find('thead tr')[0]).find('th').eq(i).attr('id');
        //}

        // go through cells
        for (var i = 0; i < rowsLength; i++) {
            var tableRow = $(table.find('tbody tr')[i]);
            var rowData = {};
            if (tableRow.eq(0).attr('id') === "") { rowData[headers[0]] = '0';}
            else { rowData[headers[0]] = tableRow.eq(0).attr('id');}
            
            for (var j = 0; j < colsLength; j++) {
                
                if (j === 7) {
                    var isChecked = tableRow[0].cells[j].childNodes[0].checked;
                    if (isChecked === true) {
                        rowData[headers[j+1]] = true;
                    }
                    else {
                        rowData[headers[j+1]] = false;
                    }

                }
                else {
                    rowData[headers[j+1]] = tableRow.find('td').eq(j).text();
                }
            }

            data.push(rowData);
        }
        return data;
    }

    function validateAdressTableIsDefualt(Serialize) {
        var count = 0;

        for (var i = 0; i < Serialize.length; i++) {

            if (Serialize[i]._isDefault === true) {
                count++;
            }
            else {
                console.log(i);
            }
        }
        if (count > 0) {
            return true;
        }
        else {
            return false;
        }
    }

    function InsertRadioButton() {
        // foreach row in the table
        // we create a new checkbox
        // and wrap it with a td element
        // then put that td at the beginning of the row
        var $rows = $('#myTableAddList tr');
        for (var i = 0; i < $rows.length; i++) {

            if ($rows[i].cells.length === 8) {
                console.log("Skipped to Insert Radio on Row no" + $rows[i]);
            }
            else {
                var $checkbox = $('<input />', {
                    type: 'radio',
                    name: 'type_radio_',
                    id: 'Radioid' + i
                });
                $checkbox.wrap('<td></td>').parent().appendTo($rows[i]);
            }
        }
    }
    function CheckRadioButton(data) {
        // foreach row in the table
        // we create a new checkbox
        // and wrap it with a td element
        // then put that td at the beginning of the row
        var $rows = $('#myTableAddList tr');
        for (var i = 0; i < data.length; i++) {
            if (data[i].DefaultAddress === true) {
                //check check box
                //var id = data[i].id;
                var radioid = $rows[i].cells[7].firstElementChild.id;
                $('#' + radioid).prop('checked', true);

            }
            else {
                //ignore
            }
        }

    }
     ///////////////////////////////////FULL CUSTOMER FORM SUBMIT/////////////////////////////////////////////////////
    $("#FormCustomer").validate({
        
        rules: {
            FirstName: "required",
            LastName: "required",
            MobileNumberOne: { required: true, digits: true },
            MobileNumbertwo: { digits: true },
            EmailId: {
                email: true
            },
            Gender: "required",
            GstNumber: {
                pattern: /^([0][1-9]|[1-2][0-9]|[3][0-7])([a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9a-zA-Z]{1}[zZ]{1}[0-9a-zA-Z]{1})+$/
                
            }
 
        },
        // Specify validation error messages
        messages: {
            FirstName: "ENTER FIRST NAME",
            LastName: "ENTER LAST NAME",
            MobileNumberOne: { required: "ENTER MOBILE NUMBER", digits: "ENTER NUMBERS ONLY" },
            MobileNumbertwo: { digits: "ENTER NUMBERS ONLY" },
            GstNumber: {  pattern: "INVALID GST NUMBER" },
            EmailId: { email: "ENTER A VALID EMAIL." },
            Gender: "SELECT GENDER"
          
        },
        
        submitHandler: function (form) {
            form.submit();
        }
    });

    $('#btn_SaveCustomer').click(function (e) {
        var table = $('#myTableAddList');

        var $form = $("#FormCustomer");
        $form.validate();
        if (!$form.valid()) {
            return false;
        }
        if (table[0].rows.length === 0) {
            toastr.error('ENTER AT LEAST ONE ADDRESS');
            return false;
        }
        var Serialize = null;
        var IsHaveidfeault = null;
        $('#AddressGridSerialize').val('');

        if ($('#Id').val() > 0) {

            var tablegrids = $('#myTableheadList');
            Serialize = htmlTableToJsonEdit(tablegrids);
             IsHaveidfeault = validateAdressTableIsDefualt(Serialize);
            if (!IsHaveidfeault) {
                toastr.error('SELET AT LEAST ONE DEFUALT ADDRESS');
                return false;
            }
            $('#AddressGridSerialize').val(JSON.stringify(Serialize));
        }
        else {
            var tablegrid = $('#myTableheadList');
            Serialize = htmlTableToJson(tablegrid, 0, 0);

            
             IsHaveidfeault = validateAdressTableIsDefualt(Serialize);
            if (!IsHaveidfeault) {
                toastr.error('SELET AT LEAST ONE DEFUALT ADDRESS');
                return false;
            }
            $('#AddressGridSerialize').val(JSON.stringify(Serialize));
        }
        

        e.preventDefault();



        var formData = new FormData();
        var data = $form.find(':input[name]:not(:disabled)').filter(':not(:checkbox), :checked').map(function () {
            var input = $(this);
            //if (input.attr('type') === "file") {
            //    formData.append(input.attr('name'), input[0].files[0]);
            //    return {
            //        name: input.attr('name'),
            //        value: input[0].files[0]
            //    };
            //}
            if (input.attr('type') === "radio") {
                if (input.prop("checked")) {
                    formData.append(input.attr('name'), input.val());
                    return {
                        name: input.attr('name'),
                        value: $.trim(input.val())
                    };
                }
            }
            else {
                formData.append(input.attr('name'), input.val());
                return {
                    name: input.attr('name'),
                    value: $.trim(input.val())
                };
            }
        }).get();
        $(".block-page-btn-example-3").click();
        console.log(data);
        $.ajax({
            url: '/Customer/CreateEditCustomer/',
            data: formData,
            type: "POST",
            contentType: false,
            processData: false,
            success: function (data, textStatus, xhr) {
                if (data.success === true) {
                    $(".block-page-btn-example-3-unblock").click();
                    $('#FormCustomer').trigger("reset");
                    $('#FormAdress').trigger("reset");
                    $("#myTableAddList tr").remove(); 
                    if ($('#Id').val() > 0) {
                            swal("EDITED!", "YOUR CUSTOMER HAS BEEN EDITED SUCCESFULLY.", "success");
                        setTimeout(function () {
                            var url = $("#RedirectToIndex").val();
                            window.location.href = url;
                        }, 2000);
                      
                    }
                    else {
                        swal("ADDED!", "YOUR CUSTOMER HAS BEEN ADDED SUCCESFULLY.", "success");
                      

                    }
                }
                else {
                    $(".block-page-btn-example-3-unblock").click();
                    swal("ERROR!", "ERROR IN OPERATION CONTACT YOUR ADMINISTRATOR.", "error");
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                $(".block-page-btn-example-3-unblock").click();
                swal("ERROR!", "ERROR IN OPERATION CONTACT YOUR ADMINISTRATOR.", "error");
            }
        });        
    });

    $('#btn_EditCustomer').click(function (e) {
        $(this).prop('disabled', true);
        var $form = $("#FormCustomer");
        $form.find(':input[name]').filter(':not(:checkbox), :checked').map(function () {
            var input = $(this);
            input.removeAttr('disabled');
            $('#btn_SaveCustomer').removeAttr('disabled');
            $('.Button_AddlistAdd').removeAttr('disabled');
            $('#myTableAddList tr').find('input[type=radio]').prop('disabled', false);
        });
    });
    ///////////////////////////////////ADDRESS FROM SUBMIT AND VALIDATE/////////////////////////////////////////////////
    $("#FormAdress").validate({
        // Specify validation rules
        rules: {
            // The key name on the left side is the name attribute
            // of an input field. Validation rules are defined
            // on the right side
            AddressName: "required",
            AddressLineOne: "required",
            AddressLineTwo: "required",
            Country: "required",
            City: "required",
            State: "required",
            pincode: {
                required: true,
                digits: true
            }
           
            //password: {
            // required: true,
            //minlength: 5
            //}
        },
        // Specify validation error messages
        messages: {
            AddressName: "ENTER ADDRESS NAME",
            AddressLineOne: "ENTER ADRESS LINE 1",
            AddressLineTwo: "ENTER ADRESS LINE 2",
            Country: "ENTER COUNTY",
            City: "ENTER CITY",
            State: "ENTER STATE",
            pincode: { required: "ENTER PINCODE", digits: "ENTER NUMBERS ONLY" }
            // password: {
            // required: "Please provide a password",
            // minlength: "Your password must be at least 5 characters long"
            // },
            // email: "Please enter a valid email address"
        },
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        submitHandler: function (form) {
            form.submit();
        }
    });

    $(document).on("click", "#myTableAddList tr", function () {
        $(this).addClass("selected").siblings().removeClass("selected");
        $('#squarespaceModal').addClass("EditAddress").removeClass("AddAddress");
    });

    $(document).on("click", ".Button_AddlistAdd", function () {
        $('#squarespaceModal').addClass("AddAddress").removeClass("EditAddress");
        $('#exampleModalLongTitle').text('ADD ADDRESS');
        $('#FormAdress').trigger("reset");
        $('#btn_ModalAddressdelete').hide();
        
    });

    $(document).on("dblclick", "#myTableAddList tr", function (e) {

        $('#btn_ModalAddressdelete').show();
        e.preventDefault();
        var data = [];
        var result = $('tr.selected:first', '#myTableAddList');
        $('#AddressID').val('');
        $('#AddressID').val(result.eq(0).attr('id'));
        //Get Header
        var table = $('#myTableheadList');
        var colsLength = $(table.find('thead tr')[0]).find('th').length;
        var headers = [];
        for (var i = 0; i < colsLength; i++) {
            headers[i] = $(table.find('thead tr')[0]).find('th').eq(i).attr('id');
        }
        var rowData = {};
        if (result.length > 0) {
            for (var ei = 0; ei < result[0].cells.length; ei++) {
                rowData[headers[ei]] = result[0].cells[ei].innerText;
            }
            data.push(rowData);

            //console.log(data);
            $('#ModalOpen').click();
            $('#squarespaceModal').addClass("EditAddress").removeClass("AddAddress");
            $('#exampleModalLongTitle').text('EDIT ADDRESS');
            $('#btn_ModalAddressdelete').show();


            


            $('#AddressName').val(data[0]._AddressName);
            $('#AddressLineOne').val(data[0]._AddressLineOne);
            $('#AddressLineTwo').val(data[0]._AddressLinetwo);
            $('#Country').val(data[0]._Country);
            $('#City').val(data[0]._City);
            $('#State').val(data[0]._State);
            $('#pincode').val(data[0]._pincode);
        }
        else {
            swal('ERROR', "NO ROW SELECTED", 'error');
        }



    });

    $('#btn_ModalAddressSave').click(function (e) {

        

        var $form = $("#FormAdress");
        $form.validate();
        if (!$form.valid()) {
            return false;
        }
        e.preventDefault();
        var formData = new FormData();
        var data = $form.find(':input[name]:not(:disabled)').filter(':not(:checkbox), :checked').map(function () {
            var input = $(this);
            //if (input.attr('type') === "file") {
            //    formData.append(input.attr('name'), input[0].files[0]);
            //    return {
            //        name: input.attr('name'),
            //        value: input[0].files[0]
            //    };
            //}
            if (input.attr('type') === "radio") {
                if (input.prop("checked")) {
                    formData.append(input.attr('name'), input.val());
                    return {
                        name: input.attr('name'),
                        value: $.trim(input.val())
                    };
                }
            }
            else {
                formData.append(input.attr('name'), input.val());
                return {
                    name: input.attr('name'),
                    value: $.trim(input.val())
                };
            }
        }).get();
        var modalvar = $('#squarespaceModal');
        if (modalvar.hasClass('AddAddress')) {

            $("#myTableAddList").append("<tr id=\"\"><td>" + data[1].value + "</td><td>" + data[2].value + "</td><td>" + data[3].value + "</td><td>" + data[4].value + "</td><td>" + data[6].value + "</td><td>" + data[5].value + "</td><td>" + data[7].value + "</td></tr>");
            InsertRadioButton();
            $('#FormAdress').trigger("reset");
            $('#CloseModal').click();
            toastr.success('ADDRESS ADDED SUCCESFULLY');
        }
        else if (modalvar.hasClass('EditAddress')) {

            var results = null;
            var $rows = $('#myTableAddList tr');

            results = $('tr.selected:first', '#myTableAddList');

            if (results.length > 0) {
                var indexnNumbeer = results[0].rowIndex - 1;
                
                var newRow = $("<tr id=" + data[0].value+"><td>" + data[1].value + "</td><td>" + data[2].value + "</td><td>" + data[3].value + "</td><td>" + data[4].value + "</td><td>" + data[6].value + "</td><td>" + data[5].value + "</td><td>" + data[7].value + "</td></tr>");
                $rows.eq(indexnNumbeer).after(newRow);

                results.remove();
                InsertRadioButton();
                $('#FormAdress').trigger("reset");
                $('#CloseModal').click();
                toastr.success('ADDRESS EDITED SUCCESFULLY');
            }
            else {
                toastr.error('NO ROW SELECTED');
            }
        }
        else {
            toastr.error('CANNOT INSERT OR UPDATE DATA');
        }
    });

    $('#btn_ModalAddressdelete').click(function(e) {
        var result = $('tr.selected:first', '#myTableAddList');
        if (result.length > 0) {
            result.remove();
            $('#FormAdress').trigger("reset");
            $('#CloseModal').click();
            toastr.success('ADDRESS DELETED');
            
        }
        else {
            toastr.error('NO ROW SELECTED');
            
        }
    });
});