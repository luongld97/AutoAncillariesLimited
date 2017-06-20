// global variables
var employeesDataTable;
$(document).ready(function () {
  employeesDataTable = fillEmployeesTable();

});


var fillEmployeesTable = function () {
  return $("#table-employees").DataTable({
    ajax: {
      contentType: "application/json; charset=utf-8",
      url: "/Employee/Employees",
      dataSrc: ""
    },
    columns: [
      { data: "Username" },
      { data: "Name" },
      { data: "Email" },
      { data: "Phone" },
      { data: "Address" },
      { data: "Options" }
    ],
    columnDefs: [
      {
        "class": "text-center",
        "width": "10%",
        "sortable": false,
        "searchable": false,
        "targets": 5,
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