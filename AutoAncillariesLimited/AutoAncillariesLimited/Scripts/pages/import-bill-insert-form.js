var selected = [];

$(document).ready(function () {
  fillComboxbox("/Product/Products", "#cmb-products", " ---- Select product ---- ");
  fillComboxbox("/Warehouses/Warehouses", "#cmb-warehouses", " ---- Select warehouse ---- ");
  fillComboxbox("/Supplier/Suppliers", "#cmb-suppliers", " ---- Select supplier ---- ");
  productFormReset();
  $("#add-table-product").submit(addTableProductSubmitEvent);
  $("#table-bill-products").on("click", ".btn-bill-product-remove", btnBillProductRemoveEvent);
  $("#cmb-products").change(cmbProductsEvent);
  $("#btn-import-bill-submit").click(btnImpportBillSubmitEvent);
});

function productFormReset() {
  $("#input-quantity").prop("disabled", true);
  $("#input-price").prop("disabled", true);
  $("#input-note").prop("disabled", true);
  $("#input-quantity").val("");
  $("#input-price").val("");
  $("#input-note").val("");
}
function addTableProductSubmitEvent() {

  var selectedValue = parseInt($("#cmb-products").val());
  var price = parseFloat($("#input-price").val());
  var quantity = parseInt($("#input-quantity").val());
  if (selectedValue !== -1) {
    if (quantity <= 0 || isNaN(quantity)) {
      $("#input-quantity").closest(".form-group").addClass("has-error");
    } else if (price <= 0 || isNaN(price)) {
      $("#input-price").closest(".form-group").addClass("has-error");
      $("#input-quantity").closest(".form-group").removeClass("has-error");
    } else {
      var product = {};
      var obj = {};
      product.id = selectedValue;
      var option = $("#cmb-products").find("option[value='" + selectedValue + "']");
      product.name = option.html();
      obj.product = product;
      obj.quantity = quantity;
      obj.price = price;
      obj.note = $("#input-note").val();
      var row = $("#table-bill-products tr:eq(1)").clone();
      tableAppendRow(row, obj);
      option.remove();
      selected.push(selectedValue);
      $("#input-price").closest(".form-group").removeClass("has-error");
      productFormReset();
      // Đánh lại số thứ tự dòng
      countNo();
    }

  }

}

function btnBillProductRemoveEvent() {
  var row = $(this).closest("tr");
  var id = $(".product", row).find("span:eq(0)").html();
  var name = $(".product", row).find("span:eq(1)").html();
  $("#cmb-products").append($("<option></option>").val(id).html(name));
  row.remove();
  var index = selected.indexOf(parseInt(id));
  selected.splice(index, 1);
  // Đánh lại số thứ tự dòng
  countNo();
}

function tableAppendRow(row, obj) {
  var table = $("#table-bill-products");

  $(".product-quantity", row).find("span").html(obj.quantity);
  $(".product", row).find("span:eq(1)").html(obj.product.name);
  $(".product", row).find("span:eq(0)").html(obj.product.id);
  $(".product-price", row).find("span").html(obj.price);
  $(".bill-note", row).find("span").html(obj.note);

  row.removeClass("hidden");
  table.append(row);
}

function countNo() {
  var table = $("#table-bill-products");
  for (let i = 2; i < table.find("tr").length; i++) {
    var row = table.find("tr")[i];
    var span = $("td:eq(1)", row);
    span.text(i - 1);
  }
}

function fillProductsCombobox() {
  $.ajax({
    url: "/Product/Products",
    method: "get",
    contentType: "application/json",
    success: function (data) {
      $("#cmb-products").empty();
      $("#cmb-products").append($("<option></option>").val(-1).html(" ----Select product---- "));
      $.each(data, function () {
        $("#cmb-products").append($("<option></option>").val(this.Id).html(this.Name));
      });
    }
  });
}

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

function btnImpportBillSubmitEvent() {
  var details = [];
  var rows = $("#table-bill-products").find("tr");
  var rowLength = rows.length;
  var supplierId = parseInt($("#cmb-suppliers").val());
  var warehouseId = parseInt($("#cmb-warehouses").val());
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
    var bill = {};
    var importBill = {};
    importBill.EmployeeId = 1;
    importBill.SupplierId = supplierId;
    importBill.Status = true;
    bill.ImportBill = importBill;
    bill.Details = details;
    bill.WarehouseId = warehouseId;
    var json = { bill: JSON.stringify(bill) };
    console.log(json);
    $.ajax({
      url: "/ImportBill/ImportBillInsert",
      method: "post",
      dataType: "json",
      data: json
    });

  }
}