// global variables
var categoriesDataTable;

$(document).ready(function () {
  categoriesDataTable = fillCategoriesTable();
  $("#category-insert-form").validate({
    rules: {
      Name: {
        required: true
      }
    },
    message: {
      Name: {
        required: 'This field is required!'
      }
    }
  });
});

// function fill data from database to table

var fillCategoriesTable = function () {
  return $('#table-categories').DataTable({
    ajax: {
      dataType: "json",
      contentType: "application/json; charset=utf-8",
      url: "/Category/Categories/",
      dataSrc: ""
    },
    columns: [
      { data: "Id" },
      { data: "Name" }
    ],
    columnDefs: [
      {
        "class": "text-center",
        "width": "25%",
        "sortable": true,
        "searchable": false,
        "targets": 2,
        "data": null,
        "render": function () {
          return '<button class="btn btn-default btn-category-delete"><span class="glyphicon glyphicon-remove"/></button> ' +
            '<button class="btn btn-default btn-category-update"><span class="glyphicon glyphicon-pencil"/></button>';
        }
      },
      {
        "width": "10%",
        "targets": 0,
        "class": "text-center"
      }
    ],
    responsive: true
  });
}
