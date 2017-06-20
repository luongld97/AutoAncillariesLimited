// global variables
var suppliersDataTable;
$(document).ready(function () {
  suppliersDataTable = fillSuppliersTable();

});


var fillSuppliersTable = function () {
  return $("#table-suppliers").DataTable({
    ajax: {
      contentType: "application/json; charset=utf-8",
      url: "/Supplier/Suppliers",
      dataSrc: ""
    },
    columns: [
      { data: "Name" },
      { data: "Phone" },
      { data: "Email" },
      { data: "Address" }
    ],
    columnDefs: [
      {
        "class": "text-center",
        "width": "10%",
        "sortable": false,
        "searchable": false,
        "targets": 4,
        "render": function () {
          return '<button class="btn btn-default btn-supplier-delete"><span class="glyphicon glyphicon-remove"/></button> ' +
            '<button class="btn btn-default btn-supplier-update"><span class="glyphicon glyphicon-pencil"/></button>';
        }
      },
      {
        "targets": 3,
        "sortable": false
      }
    ],
    responsive: true
  });
}