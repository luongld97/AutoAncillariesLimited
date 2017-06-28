$(document).ready(function () {
  $("#btn-import-bill-row-add").click(function () {
    // Lấy ra cái table ngoài cùng
    var tableImportBill = $("#table-import-bill-create");
    // lấy cái dòng có index = 1 trong cái tableImportBill, clone nó
    var importBillRow = $("tr:eq(1)", tableImportBill).clone(true);
    // Cho dòng clone hiện lên và add vào tableImportBill
    importBillRow.removeClass("hidden");
    tableImportBill.append(importBillRow);
    // Lấy cái select có name ="warehouseId" trong cái importBillRow ở trên
    var masterSelect = $('select[name="warehouseId"]', importBillRow);
    // Lấy cái dòng có chứ cái selectMaster
    var tr = masterSelect.closest("tr");
    // Kiếm mấy cái select, input xong set lại nhóm "name" cho nó
    $(".product-select", tr).attr("name", "productId_" + masterSelect.val());
    $(".product-quantity", tr).attr("name", "quantity_" + masterSelect.val());
    $(".product-price", tr).attr("name", "price_" + masterSelect.val());
    setProductPriceValue(1, $(".product-price", tr));
  });

  $(".product-select").change(function () {
    var currentSelect = $(this);
    var productId = currentSelect.val();
    var currentRow = currentSelect.closest("tr");
    var productPrice = $(".product-price", currentRow);
    setProductPriceValue(productId, productPrice);
  });
  function setProductPriceValue(id, obj) {
    $.ajax({
      url: "/Product/ProductPrice",
      data: { id: id },
      success: function (price) {
        obj.val(price);
      }
    });
  }
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
      $(".product-select", tr).attr("name", "productId_" + $(this).val());
      $(".product-quantity", tr).attr("name", "quantity_" + $(this).val());
      $(".product-price", tr).attr("name", "price_" + $(this).val());
    });

});