
    $(document).ready(function () {
        BindControls();

        $.validator.addMethod("IsValidCustomer", function (value, element) {
            if (element.value === "")
        return false;

    var Hasclass = element.classList.contains('error') ? false : true;
    return Hasclass;
}, "PLEASE SELECT CUSTOMER NAME FROM LIST !");

          $.validator.addMethod("IsValidSalesId", function (value, element) {
            if (element.value === "")
        return false;

    var Hasclass = element.classList.contains('error') ? false : true;
    return Hasclass;
  }, "PLEASE SELECT SALESID NAME FROM LIST !");

        $.validator.addMethod("ISpendinAmountGreater", function (value, element) {

            var PendingAmountdiv = parseFloat($('#pending').text().replace('₹',''));
    var txt_box = parseFloat(element.value);
    if (isNaN(PendingAmountdiv))
         return false;
    if (txt_box > PendingAmountdiv)
        return false;
    else
        return true;
}, "INVALID PENDING AMOUNT");


        $("#FormCusPayment").validate({

        rules: {
        CustomerName: {IsValidCustomer: true },
                SalesId: {IsValidSalesId: true },
                PaymentAmount: {
        ISpendinAmountGreater: true,
    required: true,
    pattern: /^[0-9]*\.?[0-9]*$/
},
                Remarks: {
        required: true
}

},
// Specify validation error messages
            messages: {
        CustomerName: "PLEASE SELECT CUSTOMER NAME FROM LIST !",
    SalesId: "PLEASE SELECT SALES ID FROM LIST !",
                PaymentAmount: {required: "PLEASE ENTER PAYMENT AMOUNT", pattern:"ALPHABETS NOT ALLOWED",ISpendinAmountGreater:"INVALID PENDING AMOUNT"},
    Remarks: "PLEASE ENTER SOMETHING IN REMARKS"
  
},

            submitHandler: function (form) {
        form.submit();
}
});


        $('#btn_CustomerPayment').click(function (e) {
            var $form = $("#FormCusPayment");
    $form.validate();
            if (!$form.valid()) {
                return false;
}
e.preventDefault();
var formData = new FormData();
            var data = $form.find(':input[name]').filter(':not(:checkbox), :checked').map(function () {
                var input = $(this);
    formData.append(input.attr('name'), input.val());
                return {
        name: input.attr('name'),
    value: $.trim(input.val())
};
}).get();
$(".block-page-btn-example-3").click();

console.log(data);
            $.ajax({
        url: '/CustomerPayment/Create/',
    data: formData,
    type: "POST",
    contentType: false,
    processData: false,
                success: function (data, textStatus, xhr) {
                    if (data.success === true) {
        $(".block-page-btn-example-3-unblock").click();
    $('#FormCusPayment').trigger("reset");
    $('#paymentlogs,#paymentsummary').addClass("d-xl-none");
    swal("ADDED!", "YOUR CUSTOMERPAYMENT HAS BEEN ADDED SUCCESFULLY.", "success");
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

});

    function BindControls() {

        $('#CustomerName').autocomplete({
            source: function (request, response) {
                var term = { Prefix: request.term };
                $.ajax({
                    url: '/Masters/Customer/AutofillCustomer',
                    data: term,
                    type: 'GET',
                    dataType: 'json',
                    contentType: "application/json",
                    success: function (data) {
                        response($.map(data, function (item) {

                            return { label: item.FullName, value: item.Id };

                        }));
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            select: function (e, i) {

                $("#CustomerName").val(i.item.label);
                $("#CustomerId").val(i.item.value);
                $("#CustomerName").removeClass('error').next('label.error').remove();
                $("#SalesId").val('');
                clearSummary();
                Getsalesid(i.item.value);
                return false;
            },
            change: function (event, ui) {
                if (!ui.item) {
                    var validator = $("#FormCusPayment").validate();
                    validator.showErrors({
                        "CustomerName": "PLEASE SELECT CUSTOMER NAME FROM LIST !"
                    });
                    $("#CustomerId").val('');
                    $("#SalesId").autocomplete({ source: [] });
                    $("#SalesId").val('');
                    clearSummary();
                }
            },
            //open: function () {
            //    $("ul.ui-menu").width($(this).innerWidth());

            //},
            minLength: 0

        }).focus(function (event, ui) {

            $(this).autocomplete("search", "");
            return false;
        }).on("blur", function (event) {
            var autocomplete = $(this).data("uiAutocomplete");
            var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex($(this).val()) + "$", "i");
            autocomplete.widget().children(".ui-menu-item").each(function () {
                //Check if each autocomplete item is a case-insensitive match on the input
                var item = $(this).data("uiAutocompleteItem");
                if (matcher.test(item.label || item.value || item)) {
                    //There was a match, lets stop checking
                    autocomplete.selectedItem = item;
                    $("#CustomerName").removeClass('error').next('label.error').remove();
                    return;
                }
            });
            //if there was a match trigger the select event on that match
            if (autocomplete.selectedItem) {
                autocomplete._trigger("select", event, {
                    item: autocomplete.selectedItem
                });
                //there was no match, clear the input
            } else {
                var validator = $("#FormCusPayment").validate();
                validator.showErrors({
                    "CustomerName": "PLEASE SELECT CUSTOMER NAME FROM LIST !"
                });
                $("#CustomerId").val('');
                $("#SalesId").val('');
            }
        });
}


    function Getsalesid(Customerid) {
        var CustId = new Object();
    CustId.customerId = Customerid;

   
        $('#SalesId').autocomplete({
        source: function (request, response) {
                var term = {Prefix: request.term };
                $.ajax({
        url: '/Sales/GetSalesIdByCustomerId',
    data: CustId,
    type: 'GET',
    dataType: 'json',
    contentType: "application/json",
                    success: function (data) {
        response($.map(data, function (item) {
            return { label: item.SalesId, value: item.SalesId };
        }));
},
                    error: function (response) {
        alert(response.responseText);
},
                    failure: function (response) {
        alert(response.responseText);
}
});
},
            select: function (e, i) {
        BindPaymentSummary(i.item.label)
                $("#SalesId").val(i.item.label);
    $("#SalesId").removeClass('error').next('label.error').remove();
    $('#paymentlogs,#paymentsummary').removeClass("d-xl-none");
    return false;
},
            change: function (event, ui) {
                if (!ui.item) {
        clearSummary();
    var validator = $("#FormCusPayment").validate();
                    validator.showErrors({
        "SalesId": "PLEASE SELECT INVOICE FROM LIST !"
});
 clearSummary();

}
},
minLength: 0

        }).focus(function (event, ui) {

        $(this).autocomplete("search", "");
    return false;
        }).on("blur", function (event) {
            var autocomplete = $(this).data("uiAutocomplete");
    var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex($(this).val()) + "$", "i");
            autocomplete.widget().children(".ui-menu-item").each(function () {
                //Check if each autocomplete item is a case-insensitive match on the input
                var item = $(this).data("uiAutocompleteItem");
                if (matcher.test(item.label || item.value || item)) {
        //There was a match, lets stop checking
        autocomplete.selectedItem = item;
    $("#SalesId").removeClass('error').next('label.error').remove();
    return;
}
});
//if there was a match trigger the select event on that match
            if (autocomplete.selectedItem) {
        autocomplete._trigger("select", event, {
            item: autocomplete.selectedItem
        });
   
    //there was no match, clear the input
            } else {
                var validator = $("#FormCusPayment").validate();
                validator.showErrors({
        "SalesId": "PLEASE SELECT INVOICE FROM LIST !"
});
clearSummary();
}
});
}

    function fullpaid() {
        $('#PaymentAmount').prop('disabled', true);
    $('#Remarks').prop('disabled', true);
    $('#btn_CustomerPayment').prop('disabled', true);
}

    function Resetfullpaid() {
        $('#PaymentAmount').removeAttr('disabled');
    $('#Remarks').removeAttr('disabled');
    $('#btn_CustomerPayment').removeAttr('disabled');
}

    function clearSummary() {
        $('#totalamount').text('');
    $('#paid').text('');
    $('#pending').text('');
    $('#PaymentAmount').val('');
    $('[id=summaryListItem]').remove();
    $('#paymentlogs,#paymentsummary').addClass("d-xl-none");
    Resetfullpaid();
}

    function BindPaymentSummary(salesId) {
        var form= new Object();
    form.SalesId = salesId;

        $.ajax({
        url: '/CustomerPayment/PaymentSummary/',
    data: form,
    type: 'GET',
    dataType: 'json',
    contentType: "application/json",
            success: function (data, textStatus, xhr) {
        $('#totalamount').text('₹ ' + data.TOTALAMOUNT);
    $('#paid').text('₹ '+ data.PAIDAMOUNT);
  $('#pending').text('₹ ' + data.PENDINGAMOUNT);
    $('#PaymentAmount').val(data.PENDINGAMOUNT);
    $('[id=summaryListItem]').remove()

                if (parseFloat(data.PENDINGAMOUNT) === 0) {
        fullpaid();
}

                for (var kk = 0; kk < data.PaymentSummary.length; kk++) {
                    var list = `<div class="vertical-timeline vertical-timeline--animate vertical-timeline--one-column" id="summaryListItem">
        <div class="vertical-timeline-item vertical-timeline-element">
            <div>
                <span class="vertical-timeline-element-icon bounce-in">
                    <i class="badge badge-dot badge-dot-xl badge-success"> </i>
                </span>
                <div class="vertical-timeline-element-content bounce-in">
                    <h4 class="timeline-title">PAID &#8377; ___paid___</h4>
                    <p>Paid at <a href="javascript:void(0);">___datetime____</a></p><span class="vertical-timeline-element-date">      </span>
                </div>
            </div>
        </div>
    </div>`;
list = list.replace('___paid___', data.PaymentSummary[kk].PaymentAmount);
list = list.replace('___datetime____', data.PaymentSummary[kk].CreatedDate);
$('#SummaryList').append(list);
}

                if (data.PaymentSummary.length === 0) {
                        var list = `<div class="vertical-timeline vertical-timeline--animate vertical-timeline--one-column" id="summaryListItem">
        <div class="vertical-timeline-item vertical-timeline-element">
            <div>
                <span class="vertical-timeline-element-icon bounce-in">
                    <i class="badge badge-dot badge-dot-xl badge-success"> </i>
                </span>
                <div class="vertical-timeline-element-content bounce-in">
                    <h4 class="timeline-title">NO DATA FOUND</h4>

                </div>
            </div>
        </div>
    </div>`;
 $('#SummaryList').append(list);
}




},
            error: function (xhr, textStatus, errorThrown) {
        console.log('Error');
}
});
}
