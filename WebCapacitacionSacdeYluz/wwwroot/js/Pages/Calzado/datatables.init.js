$(document).ready(function () {
    $("#datatable").DataTable(),
        $("#datatable-calzados")
            .DataTable({
                lengthChange: !1,
                buttons: ["copy", "excel", "pdf"]
            })
            .buttons()
            .container()
            .appendTo("#datatable-calzado_wrapper .col-md-6:eq(0)"),
        $(".dataTables_length select").addClass("form-select form-select-sm")
});