$(document).ready(function () {
  $("#btn-import-bill-row-add").click(function () {
    var tableImportBill = $("#table-import-bill-create");
    var importBillRow = $("tr:eq(1)", tableImportBill).clone(true);
    importBillRow.removeClass("hidden");
    tableImportBill.append(importBillRow);
    var masterSelect = $('select[name="warehouseId"]', importBillRow);
    var tr = masterSelect.closest("tr");
    $('.product-select', tr).attr("name", "productId_" + masterSelect.val());
    $('.product-quantity', tr).attr("name", "quantity_" + masterSelect.val());
    $('.product-price', tr).attr("name", "price_" + masterSelect.val());
  });

  $("#table-import-bill-create tr").on("click",
    ".btn-import-bill-add-product",
    function () {
      var currentRow = $(this).closest("tr");
      var tableProduct = $(".table-import-bill-product", currentRow);
      var productRow = $("tr:eq(1)", tableProduct).clone(true);
      productRow.removeClass("hidden");
      tableProduct.append(productRow);
    });
  $("#table-import-bill-create tr").on("click",
    ".btn-import-bill-remove-row",
    function () {
      var currentRow = $(this).closest("tr");
      currentRow.remove();
    });
  $("#table-import-bill-create tr").on("click",
    ".btn-import-bill-remove-product",
    function () {
      var currentRow = $(this).closest("tr");
      currentRow.remove();
    });
  $("#table-import-bill-create tr").on("change",
    'select[name="warehouseId"]',
    function () {
      var tr = $(this).closest("tr");
      $('.product-select', tr).attr("name", "productId_" + $(this).val());
      $('.product-quantity', tr).attr("name", "quantity_" + $(this).val());
      $('.product-price', tr).attr("name", "price_" + $(this).val());
    });

});