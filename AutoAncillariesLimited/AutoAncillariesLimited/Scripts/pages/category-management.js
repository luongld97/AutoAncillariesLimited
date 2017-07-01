// global variables
var categoriesDataTable;

$(document).ready(function () {
  categoriesDataTable = fillCategoriesTable();
  $("#table-categories tr").on("click",
    "btn-category-update",
    function () {
      var row = $(this).closest("tr");
      var categoryId = row.data().Id;
      $.ajax({
        url: "Category/CategoryUpdate",
        data: { id: categoryId },
        success: function(data) {
          alert("done")
          console.log(data);
        }
      });
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
        "sortable": false,
        "searchable": false,
        "targets": 2,
        "data": null,
        "render": function () {
          return '<button class="btn btn-default btn-category-update"><span class="glyphicon glyphicon-pencil"/></button>';
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
