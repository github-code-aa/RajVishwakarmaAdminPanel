/* Prototyping
/* ========================================================================== */
(function (window, ElementPrototype, ArrayPrototype, polyfill) {
	function NodeList() { [polyfill] }
	NodeList.prototype.length = ArrayPrototype.length;
	ElementPrototype.matchesSelector = ElementPrototype.matchesSelector ||
		ElementPrototype.mozMatchesSelector ||
		ElementPrototype.msMatchesSelector ||
		ElementPrototype.oMatchesSelector ||
		ElementPrototype.webkitMatchesSelector ||
		function matchesSelector(selector) {
			return ArrayPrototype.indexOf.call(this.parentNode.querySelectorAll(selector), this) > -1;
		};
	ElementPrototype.ancestorQuerySelectorAll = ElementPrototype.ancestorQuerySelectorAll ||
		ElementPrototype.mozAncestorQuerySelectorAll ||
		ElementPrototype.msAncestorQuerySelectorAll ||
		ElementPrototype.oAncestorQuerySelectorAll ||
		ElementPrototype.webkitAncestorQuerySelectorAll ||
		function ancestorQuerySelectorAll(selector) {
			for (var cite = this, newNodeList = new NodeList; cite = cite.parentElement;) {
				if (cite.matchesSelector(selector)) ArrayPrototype.push.call(newNodeList, cite);
			}
			return newNodeList;
		};
	ElementPrototype.ancestorQuerySelector = ElementPrototype.ancestorQuerySelector ||
		ElementPrototype.mozAncestorQuerySelector ||
		ElementPrototype.msAncestorQuerySelector ||
		ElementPrototype.oAncestorQuerySelector ||
		ElementPrototype.webkitAncestorQuerySelector ||
		function ancestorQuerySelector(selector) {
			return this.ancestorQuerySelectorAll(selector)[0] || null;
		};
})(this, Element.prototype, Array.prototype);
/* Helper Functions
/* ========================================================================== */
function generateTableRow() {
	var emptyColumn = document.createElement('tr');
	emptyColumn.setAttribute('id', '0');
	emptyColumn.innerHTML = '<td></td><td><a class="cut" style="color: white;">-</a><span contenteditable></span></td>' +
		'<td><span contenteditable></span></td>' +
		'<td><span contenteditable></span></td>' +
		'<td><span data-prefix>&#8377;</span><span contenteditable>0.00</span></td>' +
		'<td><span warranty-prefix contenteditable>0</span></td>' +
		'<td><span data-prefix>&#8377;</span><span>0.00</span></td>';
	return emptyColumn;
}
function parseFloatHTML(element) {
	return parseFloat(element.innerHTML.replace(/[^\d\.\-]+/g, '')) || 0;
}
function parsePrice(number) {
	return number.toFixed(2).replace(/(\d)(?=(\d\d\d)+([^\d]|$))/g, '$1,');
}
/* Update Number
/* ========================================================================== */
function updateNumber(e) {
	var
		activeElement = document.activeElement,
		value = parseFloat(activeElement.innerHTML),
		wasPrice = activeElement.innerHTML === parsePrice(parseFloatHTML(activeElement));
	if (!isNaN(value) && (e.keyCode === 38 || e.keyCode === 40 || e.wheelDeltaY)) {
		e.preventDefault();
		value += e.keyCode === 38 ? 1 : e.keyCode === 40 ? -1 : Math.round(e.wheelDelta * 0.025);
		value = Math.max(value, 0);
		activeElement.innerHTML = wasPrice ? parsePrice(value) : value;
	}
	updateInvoice();
}
/* Update Invoice
/* ========================================================================== */
function updateInvoice() {
	var total = 0;
	var cells, price, total, a, i;
	// update inventory cells
	// ======================
	for (var a = document.querySelectorAll('table.inventory tbody tr'), i = 0; a[i]; ++i) {
		// get inventory row cells
		cells = a[i].querySelectorAll('span:last-child');
		// set price as cell[2] * cell[3]
		price = parseFloatHTML(cells[3]) * parseFloatHTML(cells[4]);
		// add price to total
		total += price;
		// set row total
		cells[5].innerHTML = price;
	}

	// update balance cells
	// ====================
	// get balance cells
	cells = document.querySelectorAll('table.balance td:last-child span:last-child');
	// set total
	cells[0].innerHTML = total;
	cells[0].innerHTML.bold();	
	// set balance and meta balance
//	cells[2].innerHTML =  parsePrice(total - parseFloatHTML(cells[1]));
	// update prefix formatting
	// ========================
	//var prefix = document.querySelector('#prefix').innerHTML;
	//for (a = document.querySelectorAll('[data-prefix]'), i = 0; a[i]; ++i) a[i].innerHTML = prefix;
	//// update price formatting
	//// =======================
	for (a = document.querySelectorAll('span[data-prefix] + span'), i = 0; a[i]; ++i)
		if (document.activeElement !== a[i])
			a[i].innerHTML = parsePrice(parseFloatHTML(a[i]));

	for (a = document.querySelectorAll('span[warranty-prefix]'), i = 0; a[i]; ++i)
		if (document.activeElement !== a[i])
			a[i].innerHTML = parsePrice(parseFloatHTML(a[i]));
}
/* On Content Load
/* ========================================================================== */
function onContentLoad() {
	updateInvoice();

	var	input = document.querySelector('input'),
		image = document.querySelector('img');

	function onClick(e) {
		var element = e.target.querySelector('[contenteditable]'), row;

		element && e.target !== document.documentElement && e.target !== document.body && element.focus();

		if (e.target.matchesSelector('.add')) {
			var stop = $('table.inventory tbody');
			if (stop[0].rows.length === 15) {
				toastr.error('CANNOT ADD MORE THAN 15 ITEM');
			}
			else {
				document.querySelector('table.inventory tbody').appendChild(generateTableRow());
				//auto number added rows
				$('table.inventory tbody tr').each(function (idx) {
					$(this).children("td:eq(0)").html(idx + 1);
				});
			}
		}
		else if (e.target.className === 'cut') {
			row = e.target.ancestorQuerySelector('tr');

			row.parentNode.removeChild(row);
			//auto number added rows
			$('table.inventory tbody tr').each(function (idx) {
				$(this).children("td:eq(0)").html(idx + 1);
			});
		}

		updateInvoice();
	}

	function onEnterCancel(e) {
		e.preventDefault();

		image.classList.add('hover');
	}

	function onLeaveCancel(e) {
		e.preventDefault();

		image.classList.remove('hover');
	}

	//function onFileInput(e) {
	//	image.classList.remove('hover');

	//	var
	//		reader = new FileReader(),
	//		files = e.dataTransfer ? e.dataTransfer.files : e.target.files,
	//		i = 0;

	//	reader.onload = onFileLoad;

	//	while (files[i]) reader.readAsDataURL(files[i++]);
	//}

	//function onFileLoad(e) {
	//	var data = e.target.result;

	//	image.src = data;
	//}

	if (window.addEventListener) {
		document.addEventListener('click', onClick);

		document.addEventListener('mousewheel', updateNumber);
		document.addEventListener('keydown', updateNumber);

		document.addEventListener('keydown', updateInvoice);
		document.addEventListener('keyup', updateInvoice);

		input.addEventListener('focus', onEnterCancel);
		input.addEventListener('mouseover', onEnterCancel);
		input.addEventListener('dragover', onEnterCancel);
		input.addEventListener('dragenter', onEnterCancel);

		input.addEventListener('blur', onLeaveCancel);
		input.addEventListener('dragleave', onLeaveCancel);
		input.addEventListener('mouseout', onLeaveCancel);

		//input.addEventListener('drop', onFileInput);
		//input.addEventListener('change', onFileInput);
	}
}

window.addEventListener && document.addEventListener('DOMContentLoaded', onContentLoad);


//handle page load events
$(document).ready(function () {
	if ($('#Id').val() > 0) {
		var EditSalesSerializeValue = null;
		EditSalesSerializeValue = jQuery.parseJSON($('#SalesItemGridSerialize').val());
		if (EditSalesSerializeValue === "" || EditSalesSerializeValue === null) {
			console.log("No Value in AddressGridSerialize");
		}
		else {
			for (var j = 0; j < EditSalesSerializeValue.length; j++) {
				document.querySelector('table.inventory tbody').appendChild(generateTableRowEdited(EditSalesSerializeValue[j].id, EditSalesSerializeValue[j].ItemName, EditSalesSerializeValue[j].Warranty, EditSalesSerializeValue[j].HSN, EditSalesSerializeValue[j].Amount, EditSalesSerializeValue[j].Qunatity));
			}
			//auto number added rows
			$('table.inventory tbody tr').each(function (idx) {
				$(this).children("td:eq(0)").html(idx + 1);
			});
			$('table.inventory tbody').click();
		}
		DisableFormInput();
	}
	else {
		console.log("ADD FORM INITIALIZED");
	}

	var CustoemerList = $("#CustomerName").autocomplete({
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
			return false;
		},
		change: function (event, ui) {
			if (!ui.item) {
				var validator = $("#FormSales").validate();
				validator.showErrors({
					"CustomerName": "PLEASE SELECT CUSTOMER NAME FROM LIST !"
				});
				$("#CustomerId").val('');
			}
		},
		focus: function (event, ui) {
			return false;
		},
		minLength: 3
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
			var validator = $("#FormSales").validate();
			validator.showErrors({
				"CustomerName": "PLEASE SELECT CUSTOMER NAME FROM LIST !"
			});
			$("#CustomerId").val('');
		}
	});


	function generateTableRowEdited(salesid, item, warranty, hsn, rate, quantity) {
		var emptyColumn = document.createElement('tr');
		emptyColumn.setAttribute('id', salesid);
		emptyColumn.innerHTML = '<td></td><td><a class="cut" style="color: white;">-</a><span contenteditable>' + item + '</span></td>' +
			'<td><span contenteditable>' + warranty + '</span></td>' +
			'<td><span contenteditable>' + hsn + '</span></td>' +
			'<td><span data-prefix>&#8377;</span><span contenteditable>' + rate + '</span></td>' +
			'<td><span warranty-prefix contenteditable>' + quantity + '</span></td>' +
			'<td><span data-prefix>&#8377;</span><span></span></td>';
		return emptyColumn;
	}

	function DisableFormInput() {

		var $form = $("#FormSales");
		$form.find(':input[name]:not(:disabled)').filter(':not(:checkbox), :checked').map(function () {
			var input = $(this);
			input.prop('disabled', true);
			$('#btn_SaveSales').prop('disabled', true);
			$('.add').hide();
			$('.cut').hide();
		});
	}
});




$('#Discount').blur(function () {
	$(this).val($(this).val() || 0);
});

//handle clicks events
$(document).ready(function () {

	$.validator.addMethod("IsValidCustomer", function (value, element) {
		if (element.value === "")
			return false;

		var Hasclass = element.classList.contains('error') ? false : true;
		return Hasclass;
	}, "PLEASE SELECT CUSTOMER NAME FROM LIST !");

	$("#FormSales").validate({

		rules: {
			CustomerName: { IsValidCustomer: true },
			SalesDate: {
				required: true
			},
			TaxSlab: {
				required: true
			},
			Discount: {
				pattern: /^[0-9]*\.?[0-9]*$/
			}

		},
		// Specify validation error messages
		messages: {
			CustomerName: "PLEASE SELECT CUSTOMER NAME FROM LIST !",
			SalesDate: { required: "SELECT BILL DATE" },
			TaxSlab: { required: "SELECT TAX SLAB" },
			Discount: {pattern:"ONLY NUMERICS ARE ALLOWED"}
		},

		submitHandler: function (form) {
			form.submit();
		}
	});

	$('#btn_ClearSales').click(function (e) {
		var $form = $("#FormSales");
		$form.trigger("reset");
		$("#Mytable tr").remove(); 

	});

	$('#btn_SaveSales').click(function (e) {
		var tablelist = $('#Mytable');
		var tablegrid = $('#myTableheadList');
		var $form = $("#FormSales");
		var Serialize = null;
		$form.validate();
		if (!$form.valid()) {
			return false;
		}
		if (tablelist[0].rows.length === 0) {
			toastr.error('ENTER AT LEAST ONE SALES ITEM');
			return false;
		}
		for (var m = 0; m < $(tablegrid.find('tbody tr')).length; m++) {
			var tableRow = $(tablegrid.find('tbody tr')[m]);
			if ($.trim(tableRow.find('td').eq(1).text()) === "-" || tableRow.find('td').eq(6).text() === "₹0.00") {
				toastr.error('ITEM NAME OR PRICE CANNOT BE BLANK ON ITEM NO : '+ (m+1));
				return false;
			}
		}

		e.preventDefault();

		$('#SalesItemGridSerialize').val('');

		//if ($('#Id').val() > 0) {

		//	var tablegrids = $('#myTableheadList');
		//	Serialize = htmlTableToJsonEdit(tablegrids);
		//	IsHaveidfeault = validateAdressTableIsDefualt(Serialize);
		//	if (!IsHaveidfeault) {
		//		toastr.error('SELET AT LEAST ONE DEFUALT ADDRESS');
		//		return false;
		//	}
		//	$('#AddressGridSerialize').val(JSON.stringify(Serialize));
		//}
		//else {
			Serialize = htmlTableToJson(tablegrid, 0, 0);
		$('#SalesItemGridSerialize').val(JSON.stringify(Serialize));
		//}

		
		var formData = new FormData();
		var data = $form.find(':input[name]').filter(':not(:checkbox), :checked').map(function () {
			var input = $(this);
			
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
			url: '/Sales/CreateEditSales/',
			data: formData,
			type: "POST",
			contentType: false,
			processData: false,
			success: function (data, textStatus, xhr) {
				if (data.success === true) {
					$(".block-page-btn-example-3-unblock").click();
					$('#FormSales').trigger("reset");
					$("#Mytable tr").remove();
					if ($('#Id').val() > 0) {
						swal("EDITED!", "YOUR SALES HAS BEEN EDITED SUCCESFULLY.", "success");
						setTimeout(function () {
							var url = $("#RedirectToIndex").val();
							window.location.href = url;
						}, 2000);
					}
					else {
						swal("ADDED!", "YOUR SALES HAS BEEN ADDED SUCCESFULLY.", "success");
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

	function htmlTableToJson(table, edit = 0, del = 0) {
		// If exists the cols: "edit" and "del" to remove from JSON just pass values = 1 to edit and del params
		var minus = edit + del;
		var data = [];
		var colsLength = $(table.find('thead tr')[0]).find('th').length - minus-1;
		var rowsLength = $(table.find('tbody tr')).length;
		// first row needs to be headers
		//var headers = [];
		//for (var i = 0; i < colsLength; i++) {
		//	//headers[i] = $(table.find('thead tr')[0]).find('td').eq(i).text();
		//	headers[i] = $(table.find('thead tr')[0]).find('th').eq(i).attr('id');
		//}
		var headers = ['SALESITEMID', 'ITEM', 'WARRANTY', 'HSN', 'RATE', 'QUANTITY', 'PRICE'];
		// go through cells
		for (var k = 0; k < rowsLength; k++) {
			var tableRow = $(table.find('tbody tr')[k]);
			var rowData = {};
			if (tableRow.eq(0).attr('id') === "") {
				rowData[headers[0]] = '0';
			}
			else {
				rowData[headers[0]] = tableRow.eq(0).attr('id');
			}

			for (var j = 0; j < colsLength; j++) {

				if (j === 0) {
					rowData[headers[j+1]] = $.trim(tableRow.find('td').eq(j+1).text().substring(1, 50));
				}
				else {

					rowData[headers[j+1]] = $.trim(tableRow.find('td').eq(j+1).text().replace('₹', ''));
				}
			}
			data.push(rowData);
		}
		return data;
	}

	$('#btn_EditSales').click(function (e) {
		$(this).prop('disabled', true);
		var $form = $("#FormSales");
		$form.find(':input[name]').filter(':not(:checkbox), :checked').map(function () {
			var input = $(this);
			input.removeAttr('disabled');
			$('#btn_SaveSales').removeAttr('disabled');
		});
		$('#SalesDate').prop('disabled', true);
		$('#CustomerName').prop('disabled', true);
		$('.add').show();
		$('.cut').show();
	});

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

	// 3 Capitalize string every 1st chacter of word to uppercase
	$('#Remarks').keyup(function () {
		var str = $('#Remarks').val();
		var spart = str.split(" ");
		for (var i = 0; i < spart.length; i++) {
			var j = spart[i].charAt(0).toUpperCase();
			spart[i] = j + spart[i].substr(1);
		}
		$('#Remarks').val(spart.join(" "));

	});
});