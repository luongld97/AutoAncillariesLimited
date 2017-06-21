$(document).ready(function () {
  fillDataToComboBox();
  productFormReset();
  $("#add-table-product").submit(addTableProductSubmitEvent);
  $("#table-bill-products").on("click", ".btn-bill-product-remove", btnBillProductRemoveEvent);
  $("#cmb-products").change(cmbProductsEvent);
});

var productFormReset = function () {
  $("#input-quantity").prop("disabled", true);
  $("#input-price").prop("disabled", true);
  $("#input-note").prop("disabled", true);
  $("#input-quantity").val("");
  $("#input-price").val("");
  $("#input-note").val("");
}
var addTableProductSubmitEvent = function () {

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
      $("#input-price").closest(".form-group").removeClass("has-error");
      productFormReset();
      // Đánh lại số thứ tự dòng
      countNo();
    }

  }

}

var btnBillProductRemoveEvent = function () {
  var table = $("#table-bill-products");
  var row = $(this).closest("tr");
  var id = $(".product", row).find("span:eq(0)").html();
  var name = $(".product", row).find("span:eq(1)").html();
  $("#cmb-products").append($("<option></option>").val(id).html(name));

  if (table.find("tr").length > 2) {
    row.remove();
  }
  // Đánh lại số thứ tự dòng
  countNo();
}

var tableAppendRow = function (row, obj) {
  var table = $("#table-bill-products");

  $(".product-quantity", row).find("span").html(obj.quantity);
  $(".product", row).find("span:eq(1)").html(obj.product.name);
  $(".product", row).find("span:eq(0)").html(obj.product.id);
  $(".product-price", row).find("span").html(obj.price);
  $(".bill-note", row).find("span").html(obj.note);

  row.removeClass("hidden");
  table.append(row);
}

var countNo = function () {
  var table = $("#table-bill-products");
  for (let i = 2; i < table.find("tr").length; i++) {
    var row = table.find("tr")[i];
    var span = $("td:eq(1)", row);
    span.text(i - 1);
  }
}

var fillDataToComboBox = function () {
  $.ajax({
    url: "/Product/Products",
    success: function (data) {
      $("#cmb-products").empty();
      $("#cmb-products").append($("<option></option>").val(-1).html(" ----Select product---- "));
      $.each(data, function () {
        $("#cmb-products").append($("<option></option>").val(this.Id).html(this.Name));
      });
    }
  });
}
var cmbProductsEvent = function () {
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