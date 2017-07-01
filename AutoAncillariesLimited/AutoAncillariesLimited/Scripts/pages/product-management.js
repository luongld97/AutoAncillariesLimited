var cmbWarehouses;
var cmbCategories;
var cmbSuppliers;
var areaProductForm;
var productsTable;
var detailRows = [];
$(document).ready(function () {
  initialize();
});

function initialize() {
  cmbWarehouses = $("#cmb-warehouse-id");
  cmbCategories = $("#cmb-category-id");
  cmbSuppliers = $("#cmb-supplier-id");
  areaProductForm = $("#area-product-form");
  areaProductForm.hide();
  cmbWarehouses.change(function () {
    cmbChangeEvent("/Warehouses/ProductsInWarehouse", $(this));
  });
  cmbCategories.change(function () {
    cmbChangeEvent("/Category/ProductsInCategory", $(this));
  });
  cmbSuppliers.change(function () {
    cmbChangeEvent("/Supplier/ProductsOfSupplier", $(this));
  });

  var config = productsTableConfig("/Product/Products");
  productsTable = $("#products-table").DataTable(config);
  $("#btn-product-add-form-open").click(btnProductAddFormOpenEvent);
  $("#products-table").on("click", ".btn-product-expand", btnProductRowExpandEvent);
  $("#products-table").on("click", ".btn-product-update", btnProductUpdateFormOpenEvent);
  $("#radio-product-active").change(function () {
    radioProductActiveEvent($(this).is(":checked"));
  });
}

function radioProductActiveEvent(status) {
  productsTable.destroy();
  var config = productsTableConfig("/Product/Products", { status: status });
  productsTable = $("#products-table").DataTable(config);
}
function cmbChangeEvent(url, obj) {
  productsTable.destroy();
  var config = productsTableConfig(url, { id: obj.val() });
  productsTable = $("#products-table").DataTable(config);
}

function btnProductAddFormOpenEvent() {
  $.ajax({
    url: "/Product/ProductInsertForm",
    contentType: "text/html",
    success: function (data) {
      areaProductForm.html(data);
    }
  }).done(function () {
    areaProductForm.slideDown();
  });
}
function fillProductsTableRowData(data) {
  var description = data.Description === null ? "No description" : data.Description;
  return '<label style="color:red;">Product category: ' +
    data.CategoryName +
    '</label><br/>' +
    '<label style="color: red">Description:</label><br/><span style="font-size: 12px; color: black;">' +
    description +
    '</span>';
}

function productsTableConfig(url, data) {
  var config = {};
  var ajax = { dataSrc: "" };
  config.dom = '<"toolbar">frtip';
  config.scrollX = false;
  config.columnDefs = [
    {
      "class": "text-center",
      "width": "10%",
      "sortable": false,
      "searchable": false,
      "targets": 4,
      "defaultContent": '<button class="btn btn-default btn-product-expand">' +
      '<span class="glyphicon glyphicon-menu-down"/></button> ' +
      '<button class="btn btn-default btn-product-update">' +
      '<span class="glyphicon glyphicon-pencil"/></button>'
    },
    {
      "width": "4%",
      "targets": 0,
      "class": "text-center"
    },
    {
      "targets": [2, 3],
      "width": "8%",
      "class": "text-center"
    }
  ];
  config.columns = [
    { data: "Id" },
    { data: "Name" },
    { data: "Inventory" },
    { data: "Price" }
  ];
  ajax.url = url;
  ajax.data = data;
  config.ajax = ajax;
  return config;
}
function btnProductFormCloseEvent() {
  $("#area-product-form").slideUp(500);
  var timeOut = setTimeout(function () {
    $("#area-product-form").html("");
  }, 500);
  clearTimeout(timeOut);
}

function btnProductRowExpandEvent() {
  // Lấy thẻ 'tr' bao button $(this)
  var tr = $(this).closest('tr');
  // lấy dòng trong table
  var row = productsTable.row(tr);
  var span = $(this).find('span');
  var idx = $.inArray(tr.attr("id"), detailRows);
  if (row.child.isShown()) {
    tr.removeClass("details");
    row.child.hide();
    span.addClass('glyphicon-menu-down');
    span.removeClass('glyphicon-menu-up');
    // Remove from the 'open' array
    detailRows.splice(idx, 1);
  } else {
    tr.addClass("details");
    row.child(fillProductsTableRowData(row.data())).show();
    span.addClass('glyphicon-menu-up');
    span.removeClass('glyphicon-menu-down');
    // Add to the 'open' array
    if (idx === -1) {
      detailRows.push(tr.attr("id"));
    }
  }
}
function productFormComplete() {
  this.reset();
  productsTable.destroy();
  var config = productsTableConfig("/Product/Products");
  productsTable = $("#products-table").DataTable(config);
}

function btnProductUpdateFormOpenEvent() {
  var tr = $(this).closest("tr");
  var productId = productsTable.row(tr).data().Id;
  $.ajax({
    url: "/Product/ProductUpdateForm",
    contentType: "text/html",
    data: { id: productId },
    success: function (data) {
      areaProductForm.html(data);
    }
  }).done(function () {
    areaProductForm.slideDown();
  });
}