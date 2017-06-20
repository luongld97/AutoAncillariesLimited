// global variables
var billsDataTable;
$(document).ready(function () {
  billsDataTable = fillBillsTable();

});


var fillBillsTable = function () {
  return $("#table-bills").DataTable({
    ajax: {
      contentType: "application/json; charset=utf-8",
      url: "/Bill/Bills",
      dataSrc: ""
    },
    columns: [
      { data: "Id" },
      { data: "CreateDate" },
      { data: "EmployeeId" },
      { data: "Employee" }
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
        "targets": 1,
        "render": function (data) {
          var date = parseJsonDate(data);
          return date.getMonth() + 1 + "/" + date.getDate() + "/" + date.getFullYear();
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


function parseJsonDate(jsonDateString) {
  return new Date(parseInt(jsonDateString.replace("/Date(", "")));
}
