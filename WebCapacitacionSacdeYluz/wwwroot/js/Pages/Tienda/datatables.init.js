$(document).ready(function () {
    $("#datatable").DataTable(),
        $("#datatable-tienda")
            .DataTable({
                lengthChange: !1,
                
            })
            .buttons()
            .container()
            .appendTo("#datatable-tienda_wrapper .col-md-6:eq(0)"),
        $(".dataTables_length select").addClass("form-select form-select-sm")
});