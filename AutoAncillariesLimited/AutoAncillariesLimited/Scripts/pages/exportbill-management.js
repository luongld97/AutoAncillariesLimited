$(document).ready(function () {
    $("#btn-export-bill-row-add").click(function () {
        var tableImportBill = $("#table-export-bill-create");
        var importBillRow = $("tr:eq(1)", tableImportBill).clone(true);
        importBillRow.removeClass("hidden");
        tableImportBill.append(importBillRow);
        var masterSelect = $('select[name="warehouseId"]', importBillRow);
        var tr = masterSelect.closest("tr");
        $('.product-select', tr).attr("name", "productId_" + masterSelect.val());
        $('.product-quantity', tr).attr("name", "quantity_" + masterSelect.val());
    });

    $("#table-export-bill-create tr").on("click",
        ".btn-export-bill-add-product",
        function () {
            var currentRow = $(this).closest("tr");
            var tableProduct = $(".table-export-bill-product", currentRow);
            var productRow = $("tr:eq(1)", tableProduct).clone(true);
            productRow.removeClass("hidden");
            tableProduct.append(productRow);
        });
    $("#table-export-bill-create tr").on("click",
        ".btn-export-bill-remove-row",
        function () {
            var currentRow = $(this).closest("tr");
            currentRow.remove();
        });
    $("#table-export-bill-create tr").on("click",
        ".btn-export-bill-remove-product",
        function () {
            var currentRow = $(this).closest("tr");
            currentRow.remove();
        });
    $("#table-export-bill-create tr").on("change",
        'select[name="warehouseId"]',
        function () {
            var tr = $(this).closest("tr");
            $('.product-select', tr).attr("name", "productId_" + $(this).val());
            $('.product-quantity', tr).attr("name", "quantity_" + $(this).val());
        });

    $('.warehouse-select').change(function () {
        // Cái này là cần thiết này
        var currentCombobox = $(this);
        var currentRow = currentCombobox.closest("tr");
        var productsInCurrentRow = $('.product-select', currentRow);
        $.get('/ExportBill/ProductsInWarehouse', { warehouseId: currentCombobox.val() }, function (data) {
            productsInCurrentRow.empty();
            $.each(data,
                function () {
                    productsInCurrentRow.append('<option value="' + this.productId + '" >' + this.productName + '</option>');
                });
        });
    });

    $('.product-select').change(function () {
        var currentComboBox = $(this);
        var currentRow = currentComboBox.closest("tr");
        var maxQuantityInCurrentRow = $('.product-maxQuantity', currentRow);
        var masterRow = currentRow.closest("table").closest("tr");
        var masterCombobox = $(".warehouse-select", masterRow);
        $.get('/ExportBill/getMaxQuantity',
            { warehouseId: masterCombobox.val(), productId: currentComboBox.val() },
            function (data) {
                $(maxQuantityInCurrentRow).val(data);
            });
    });

    $(function () {
        $('#btn-export-bill-create').click(function () {
            $.getScript("/ExportBill/ExportBillManagement");
        });
    });

    function ExportBillSuccess() {
        $('#errorCreatExportBill').hide();
    }
});