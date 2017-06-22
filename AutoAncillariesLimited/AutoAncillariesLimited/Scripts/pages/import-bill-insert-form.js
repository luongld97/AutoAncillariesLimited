// global variable
var selected = [];

$(document).ready(function () {
  initialize();
});
// initialize page when document ready
function initialize() {
  // Fill data to combobox 
  fillComboxbox("/Product/Products", "#cmb-products", " ---- Select product ---- ");
  fillComboxbox("/Warehouses/Warehouses", "#cmb-warehouses", " ---- Select warehouse ---- ");
  fillComboxbox("/Supplier/Suppliers", "#cmb-suppliers", " ---- Select supplier ---- ");
  // Set all from to default
  productFormReset();
  // Button event
  $("#add-table-product").submit(addTableProductSubmitEvent);
  $("#table-bill-products").on("click", ".btn-bill-product-remove", btnBillProductRemoveEvent);
  $("#cmb-products").change(cmbProductsEvent);
  $("#btn-import-bill-submit").click(btnImpportBillSubmitEvent);
  $("#btn-import-bill-insert-form-close").click(function () {
    $("#import-bill-insert-form").slideUp(500);
    $("#btn-import-bill-insert-form-open").show();
    var timeout = setTimeout(function () { $("#import-bill-insert-form").html("") }, 1000);
    clearTimeout(timeout);
  });
}
// reset form function
function productFormReset() {
  $("#input-quantity").prop("disabled", true);
  $("#input-price").prop("disabled", true);
  $("#input-note").prop("disabled", true);
  $("#input-quantity").val("");
  $("#input-price").val("");
  $("#input-note").val("");
}
// insert product into table function
function addTableProductSubmitEvent() {
  // get product combobox selected value
  var selectedValue = parseInt($("#cmb-products").val());
  // get textbox price value
  var price = parseFloat($("#input-price").val());
  // get textbox quantity value
  var quantity = parseInt($("#input-quantity").val());

  if (selectedValue !== -1) {
    // validate data in textbox price and quantity
    if (quantity <= 0 || isNaN(quantity)) {
      $("#input-quantity").closest(".form-group").addClass("has-error");
    } else if (price <= 0 || isNaN(price)) {
      $("#input-price").closest(".form-group").addClass("has-error");
      $("#input-quantity").closest(".form-group").removeClass("has-error");
    } else {
      // 
      var product = {};
      var obj = {};
      product.id = selectedValue;
      // get selected option tag
      var option = $("#cmb-products").find("option[value='" + selectedValue + "']");
      product.name = option.html();
      obj.product = product;
      obj.quantity = quantity;
      obj.price = price;
      obj.note = $("#input-note").val();
      var row = $("#table-bill-products tr:eq(1)").clone();
      tableAppendRow(row, obj);
      // remove option tag from combobox if selected
      option.remove();
      // support refresh combobox
      selected.push(selectedValue);
      // remove alert of validate data
      $("#input-price").closest(".form-group").removeClass("has-error");
      $("#input-quantity").closest(".form-group").removeClass("has-error");
      // reset from after add row
      productFormReset();
      // Đánh lại số thứ tự dòng
      countNo();
    }

  }

}
// remove product from table function
function btnBillProductRemoveEvent() {
  // get current row
  var row = $(this).closest("tr");
  // get value of column in row
  var id = $(".product", row).find("span:eq(0)").html();
  var name = $(".product", row).find("span:eq(1)").html();
  // insert option tag into combo box when unselect
  $("#cmb-products").append($("<option></option>").val(id).html(name));
  // remove row from table
  row.remove();
  // support refresh combobox
  var index = selected.indexOf(parseInt(id));
  selected.splice(index, 1);
  // Đánh lại số thứ tự dòng
  countNo();
}
// append row into table
function tableAppendRow(row, obj) {
  var table = $("#table-bill-products");
  // set columns data
  $(".product-quantity", row).find("span").html(obj.quantity);
  $(".product", row).find("span:eq(1)").html(obj.product.name);
  $(".product", row).find("span:eq(0)").html(obj.product.id);
  $(".product-price", row).find("span").html(obj.price);
  $(".bill-note", row).find("span").html(obj.note);
  // append row into table
  row.removeClass("hidden");
  table.append(row);
}
// No. set
function countNo() {
  var table = $("#table-bill-products");
  for (let i = 2; i < table.find("tr").length; i++) {
    var row = table.find("tr")[i];
    var span = $("td:eq(1)", row);
    span.text(i - 1);
  }
}
// fill data to comboboxs using ajax
function fillComboxbox(url, selector, text) {
  $.ajax({
    url: url,
    method: "get",
    contentType: "application/json",
    success: function (data) {
      $(selector).empty();
      if (text !== null)
        $(selector).append($("<option></option>").val(-1).html(text));
      $.each(data, function () {
        $(selector).append($("<option></option>").val(this.Id).html(this.Name));
      });
    }
  });
}
// products combobox onChange event
function cmbProductsEvent() {
  if (parseInt($(this).val()) !== -1) {
    $("#input-quantity").prop("disabled", false);
    $("#input-price").prop("disabled", false);
    $("#input-note").prop("disabled", false);
    $("#input-quantity").val(0);
    $("#input-price").val(0);
    $("#input-note").val("");
  } else {
    productFormReset();
  }
}
// submit event
function btnImpportBillSubmitEvent() {
  var details = [];
  // get all rows of product table
  var rows = $("#table-bill-products").find("tr");
  var rowLength = rows.length;
  // get data of warehouses, supplier combobox
  var supplierId = parseInt($("#cmb-suppliers").val());
  var warehouseId = parseInt($("#cmb-warehouses").val());
  // validate data and show alert
  if (rowLength <= 2) {
    $("#cmb-products").closest(".form-group").addClass("has-error");
  } else if (warehouseId < 0) {
    $("#cmb-products").closest(".form-group").removeClass("has-error");
    $("#cmb-warehouses").closest(".form-group").addClass("has-error");
  } else if (supplierId < 0) {
    $("#cmb-warehouses").closest(".form-group").removeClass("has-error");
    $("#cmb-suppliers").closest(".form-group").addClass("has-error");
  } else {
    $("#cmb-suppliers").closest(".form-group").removeClass("has-error");
    // create ImportBillDetail list
    for (let i = 2; i < rows.length; i++) {
      var detail = {};
      var productId = parseInt($(".product", rows[i]).find("span:eq(0)").html());
      var quantity = parseInt($(".product-quantity", rows[i]).find("span").html());
      var price = parseFloat($(".product-price", rows[i]).find("span").html());
      detail.ProductId = productId;
      detail.Quantity = quantity;
      detail.Price = price;
      details.push(detail);
    }
    // create  ImportBillViewModel object
    var bill = {};
    var importBill = {};
    importBill.SupplierId = supplierId;
    importBill.Status = true;
    bill.ImportBill = importBill;
    bill.Details = details;
    bill.WarehouseId = warehouseId;
    // convert object to json
    var json = { bill: JSON.stringify(bill) };
    // post data to server with ajax
    $.ajax({
      url: "/ImportBill/ImportBillInsert",
      method: "post",
      dataType: "json",
      data: json
    });

  }
}