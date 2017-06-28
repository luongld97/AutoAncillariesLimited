var importBillTable;
var detailRows = [];
$(document).ready(function () {
  initialize();
});

function initialize() {
  importBillTable = $("#table-import-bills").DataTable({
    columnDefs: [
      {
        "width": "4%",
        "targets": 0,
        "class": "text-center"
      },
      {
        "targets": 1,
        "width": "10%",
        "class": "text-center"
      }
    ]
  });
//  $("#table-import-bills tbody").on("click", "tr", tableRowEvent);
}

function tableRowEvent() {

  // Lấy thẻ 'tr' bao button $(this)
  var tr = $(this).closest('tr');
  // lấy dòng trong table 
  var row = importBillTable.row(tr);
  var idx = $.inArray(tr.attr("id"), detailRows);
  if (row.child.isShown()) {
    tr.removeClass("details");
    row.child.hide();
    // Remove from the 'open' array
    detailRows.splice(idx, 1);
  } else {
    tr.addClass("details");
    $.ajax({
      url: "/ImportBill/Details",
      data: { id: parseInt(row.data()[0]) },
      success: function (data) {
        row.child(fillExpandRow(data)).show();
      }
    });

    // Add to the 'open' array
    if (idx === -1) {
      detailRows.push(tr.attr("id"));
    }
  }
}


function fillExpandRow(data) {

  var html = "";

  $.each(data,
    function () {
      html += '<tr>' +
        '<td>' +
        this.Product.Name +
        '</td>' +
        '<td>' +
        this.Price +
        '</td>' +
        '<td>' +
        this.Quantity +
        '</td>' +
        '</tr>';
    });

  return '<table>' +
    '<tr>' +
    '<th>Product name</th>' +
    '<th>Price</th>' +
    '<th>Quantity</th>' +
    '</tr>' +
    html +
    '</table>';
}