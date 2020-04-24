
var userDetails = null;

$(document).on('keydown', function (e) {
	if (e.keyCode === 27) { // ESC
		$('#popover594729').removeClass("show");
		$('#popover594729').hide();
		
		
	}
});

$(document).on("click", "#EditRow", function () {
	$('#popover594729').removeClass("show");
	var rowResult = $('tr.selected:first', '#Mytable ');
	var id = rowResult.attr("id");
	var url = $("#RedirectTo").val();
	window.location.href = url.replace('__id__', id);

});

$(document).on("click", "#DeleteRow", function () {
	$('#popover594729').removeClass("show");
	var rowResult = $('tr.selected:first', '#Mytable ');
	var id = rowResult.attr("id");

	swal({
		title: "PLEASE SELECT ACTION !!!",
		type: "info",
		//imageUrl: 'thumbs-up.jpg',
		showCancelButton: true,
		confirmButtonClass: "btn-danger",
		confirmButtonText: "DELETE",
		cancelButtonText: "CANCEL",
		closeOnConfirm: false,
		closeOnCancel: false
	},
		function (isConfirm) {
			if (isConfirm) {
				//Code to delete
				swal({
					title: "INPUT KEY",
					imageUrl: 'https://img.icons8.com/bubbles/2x/password.png',
					type: "input",
					inputPlaceholder: "ENTER YOUR SECRET KEY",
					showCancelButton: true,
					closeOnConfirm: false,
					showLoaderOnConfirm: true
				}, function (inputValue) {
					if (inputValue === false) return false;
					if (inputValue === "") {
						swal.showInputError("You need to write something!");
						return false;
						}
						var DataDelete = new Object();
						DataDelete.id = id;
						DataDelete.purpose = "DeleteCustomer";
						DataDelete.passkey = inputValue;

						setTimeout(function () {

						//ajax call to delete a Customer
							$.ajax({
								url: '/Customer/Delete',
								type: 'POST',
								dataType: 'json',
								data: DataDelete,
								success: function (data, textStatus, xhr) {
									if (data.success === true) {
										swal("DELETED!", "YOUR CUSTOMER HAS BEEN DELETED.", "success");
										$("#ClearGrid").click();
										UseAjaxQueryForFillGlobalArray();
										$("#ReloadGrid").click();
									}
									else {
										swal("ERROR!", "ERROR IN OPERATION OR WRONG PASSKEY.", "error");
									}
								},
								error: function (xhr, textStatus, errorThrown) {
									swal("ERROR!", "ERROR IN OPERATION CONTACT YOUR ADMINISTRATOR.", "error");
								}
							});
						}, 500);

				});

			} else {
				//code to edit
				swal.close();

			}
		});




});



$(document).on("dblclick", "#Mytable tr", function (e) {

	var id = $(this).attr('id');

	e.preventDefault();
	$('#popover594729').addClass("show");
	$('#popover594729').show();
	$('#questionMarkId').css('position', 'absolute');
	$('#questionMarkId').css('top', e.pageY - 150);
	$('#questionMarkId').css('left', e.pageX);
	$('#questionMarkId').show();

	var data = [];
	var result = $('tr.selected:first', '#Mytable');
	//Get Header
	var table = $('#example');
	var colsLength = $(table.find('thead tr')[0]).find('th').length;
	var headers = [];
	for (var i = 0; i < colsLength; i++) {
		headers[i] = $(table.find('thead tr')[0]).find('th').eq(i).attr('id');
	}
	var rowData = {};
	if (result.length > 0) {
		for (var ei = 0; ei < result[0].cells.length; ei++) {

			rowData[headers[ei]] = result[0].cells[ei].innerText;
			//var Add1 = result[0].cells[ei].innerText;
		}
		data.push(rowData);

		$('#lbl_fullname').text('');
		$('#lbl_phoneNumber').text('');
		$('#lbl_fullname').text(data[0].NAME);
		$('#lbl_phoneNumber').text(data[0].MOBILENUMBER);
		
	}
	else {
		swal('ERROR', "NO ROW SELECTED",'error');
	}

	

});

$(document).on("click", "#Mytable tr", function () {

	$(this).addClass("selected").siblings().removeClass("selected");

});


UseAjaxQueryForFillGlobalArray();

function UseAjaxQueryForFillGlobalArray() {


	$.getJSON('/Customer/PoopulateCustomer', function (data, textStatus, jqXHR) {
		
		(function removeNull(o) {
			for (var key in o) {
				if (null === o[key]) o[key] = '';
				if (typeof o[key] === 'object') removeNull(o[key]);
			}
		})(data);
		userDetails = data;
		$(document).ready(function () {

			

			AddDataToTable();

			function AddDataToTable() {
				for (var i=0; i < userDetails.length; i++) {


					var tab = '<tr id="' + userDetails[i].Id + '"><td style="display:none">' + userDetails[i].Id + "\n" + '</td><td>' + userDetails[i].FullName + "\n" + '</td><td>' + userDetails[i].Gender + "\n" + '</td><td>' + userDetails[i].MobileNumberOne + "\n" + '</td><td>'

						+ userDetails[i].EmailId + "\n" + '</td><td>' + userDetails[i].GstNumber + "\n" + '</td></tr>';

					$('#Mytable').append(tab);
				}

			}

			
		});

	});

}// invoke the function





