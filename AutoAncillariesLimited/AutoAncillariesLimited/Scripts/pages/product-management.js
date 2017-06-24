// 
var productsDataTable;
var detailRows;
$(document).ready(function () {
  detailRows = [];
  productsDataTable = fillProductsTable();
  console.log(productsDataTable);
  $("#product-form-area").hide();
  // Expand product row when button click
  // tại #table-products > tbody, khi click vào .btn-product-expand, thực thi hàm tableRowEvent
  $("#table-products tbody").on("click", ".btn-product-expand", tableRowEvent);
  $("#table-products tbody").on("click", ".btn-product-update", btnProductUpdateEvent);
  $("#table-products tbody").on("click", ".btn-product-delete", btnProductDeleteEvent);
  $("#btn-product-insert-form-open").click(btnProductInsertFormOpenEvent);
});

var fillProductsTable = function () {
  return $("#table-products").DataTable({
    ajax: {
      url: "/Product/Products/",
      dataSrc: ""
    },
    columns: [
      { data: "Id" },
      { data: "Name" },
      { data: "Inventory" },
      { data: "Price" },
      { data: "Description" }
    ],
    columnDefs: [
      {
        "class": "text-center",
        "width": "10%",
        "sortable": false,
        "searchable": false,
        "targets": 5,
        "data": null,
        "render": function () {
          return '<button class="btn btn-default btn-product-expand"><span class="glyphicon glyphicon-menu-down"/></button> ' +
            '<button class="btn btn-default btn-product-delete"><span class="glyphicon glyphicon-remove"/></button> ' +
            '<button class="btn btn-default btn-product-update"><span class="glyphicon glyphicon-pencil"/></button>';
        }
      },
      {
        "width": "4%",
        "targets": 0,
        "class": "text-center"
      },
      {
        "targets": 4,
        "sortable": false,
        "searchable": false
      },
      {
        "targets": 2,
        "width": "4%",
        "class": "text-center"
      },
      {
        "targets": 3,
        "width": "4%",
        "class": "text-center"
      },
      {
        "targets": 1,
        "width": "25%",
        "class": "text-center"
      }
    ],
    responsive: true
  });
}


var productDetail = function (product) {
  return '<span>' +
    product.Id +
    '</span>' +
    '<br/>' +
    '<span>' +
    product.Name +
    '</span>' +
    '<br/>' +
    '<span>' +
    product.Price +
    '</span>' +
    '<br/>' +
    '<span>' +
    product.PromotionPrice +
    '</span>' +
    '<img src=""/>' +
    '<br/>' +
    '<span>' +
    product.Description +
    '</span>';
}

var tableRowEvent = function () {
  // Lấy thẻ 'tr' bao button $(this)
  var tr = $(this).closest('tr');
  // lấy dòng trong table 
  var row = productsDataTable.row(tr);
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
    row.child(productDetail(row.data())).show();
    span.addClass('glyphicon-menu-up');
    span.removeClass('glyphicon-menu-down');
    // Add to the 'open' array
    if (idx === -1) {
      detailRows.push(tr.attr("id"));
    }
  }
}

var btnProductInsertFormOpenEvent = function () {
  $.ajax({
    url: "/Product/ProductInsertForm",
    method: "get",
    contentType: "text/html",
    success: function (data) {
      $("#product-form-area").html(data);
    }
  }).done(function () {
    $("#product-form-area").slideDown(500);
    $("#btn-product-insert-form-open").hide();
  });
}

var btnProductInsertFormCloseEvent = function () {
  $("#product-form-area").slideUp(500);
  $("#btn-product-insert-form-open").show();
  var timeout = setTimeout(function () { $("#product-form-area").html("") }, 1000);
  clearTimeout(timeout);
}

var btnProductUpdateEvent = function () {
  var tr = $(this).closest("tr");
  var row = productsDataTable.row(tr);
  var id = row.data().Id;
  $.ajax({
    url: "/Product/ProductUpdateForm",
    method: "post",
    data: {id: id},
    success: function (data) {
      console.log(data);
      $("#product-form-area").html(data);
    }
  }).done(function () {
    $("#product-form-area").slideDown(500);
    $("#btn-product-insert-form-open").hide();
  });
}

var btnProductDeleteEvent = function() {
  var tr = $(this).closest("tr");
  var row = productsDataTable.row(tr);
  var id = row.data().Id;
  $.ajax({
    url: "/Product/ProductDelete",
    method: "post",
    data: { id: id }
  });
}