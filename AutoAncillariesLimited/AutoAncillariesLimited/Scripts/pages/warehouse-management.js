// Global variable
var warehousesDataTable;
var detailRows = [];
$(document).ready(function() {
  warehousesDataTable = fillWarehouseDataTable();

});


function fillWarehouseDataTable() {
  $("#table-warehouses").DataTable({
    ajax: {
      url: "/Warehouses/Warehouses/",
      dataSrc: ""
    },
    columns: [
      { data: "Id" },
      { data: "Name" },
      { data: "Address" }
    ],
    columnDefs: [
      {
        "class": "text-center",
        "width": "5%",
        "sortable": false,
        "searchable": false,
        "targets": 3,
        "data": null,
        "render": function () {
          return '<button class="btn btn-default btn-warehouse-expand"><span class="glyphicon glyphicon-menu-down"/></button> ';
        }
      },
      {
        "width": "4%",
        "targets": 0,
        "class": "text-center"
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
