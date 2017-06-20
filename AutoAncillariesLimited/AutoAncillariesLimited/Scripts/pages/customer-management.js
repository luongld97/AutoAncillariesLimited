// global variables
var customersDataTable;
$(document).ready(function () {
  customersDataTable = fillCustomersTable();

});


var fillCustomersTable = function () {
  return $("#table-customers").DataTable({
    ajax: {
      contentType: "application/json; charset=utf-8",
      url: "/Customer/Customers",
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