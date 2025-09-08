$(document).ready(function () {
    $("#datatable").DataTable(),
        $("#datatable-proveedor")
            .DataTable({
                lengthChange: !1,
                buttons: ["copy", "excel", "pdf"]
            })
            .buttons()
            .container()
            .appendTo("#datatable-proveedor .col-md-6:eq(0)"),
        $(".dataTables_length select").addClass("form-select form-select-sm")
});