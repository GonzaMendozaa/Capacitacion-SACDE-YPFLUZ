var isCreating = false;

$(document).ready(function () {
    $('#datatable-venta').DataTable();
});


// Abrir modal Crear
$('#buttonCreate-venta').click(function () {
    limpiarAdvertencias();
    isCreating = true;
    $('#id-venta').val('');
    $('#venta-tienda').val('');
    $('#venta-proveedor').val('');
    $('#venta-fechaventa').val('');
    $('#venta-totalventa').val('');
    $('#venta-ganancia').val('');
    $('.modal-title').text('Crear venta');
    $('#modalScrollable-venta').modal('show');
});


// Submit Crear
$('#form-venta').submit(function (event) {
    event.preventDefault();
    mostrarSpinner();

    if (validar_Submit()) {
        var venta = {
            Id: isCreating ? 0 : $('#id-venta').val(),
            Tienda: $('#venta-tienda').val(),
            Proveedor: $('#venta-proveedor').val(),
            Fechaventa: $('#venta-fechaventa').val(),
            Totalventa: $('#venta-totalventa').val(),
            Ganancia: $('#venta-ganancia').val()
        };

        if (isCreating) {
            createCompra(venta);
        } else {
            updateCompra(venta);
        }
    } else {
        ocultarSpinner();
        Swal.fire('Error!', 'Los datos ingresados no son válidos', 'error');
    }
});

//AJAX create
function createCompra(venta) {
    $.ajax({
        type: "POST",
        url: '/Compra/Create',
        data: JSON.stringify(venta),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (created) {
            ocultarSpinner();

            var table = $('#datatable-venta').DataTable();
        });

    table.row.add([
        created.id,
        created.tienda,
        created.proveedor,
        created.fechaventa,
        created.totalventa,
        created.ganancia
    ]).draw(false);

    Swal.fire('Creado!', 'Compra creada con éxito!', 'success');
    $('#modalCompra').modal('hide');
}
error: function () {
    ocultarSpinner();
    Swal.fire('Error!', 'No se pudo crear', 'error');
}
function limpiarAdvertencias() {
    $('#venta-tienda-obligatorio').hide();
    $('#venta-proveedor-obligatorio').hide();
    $('#venta-fechaventa-obligatorio').hide();
    $('#venta-totalventa-obligatorio').hide();
    $('#venta-ganancia-obligatorio').hide();
    $('.form-control').css("border-color", "");
}

function validar_Submit() {
    return validar_InputSimple('#venta-tienda')
        && validar_InputSimple('#venta-proveedor')
        && validar_InputSimple('#venta-fechaventa')
        && validar_InputSimple('#venta-totalventa')
        && validar_InputSimple('#venta-ganancia')
}

function validar_InputSimple(idInput) {
    if ($(idInput).val() == "") {
        $(idInput + '-obligatorio').show();
        $(idInput).css("border-color", "#FF0000");
        return false;
    } else {
        $(idInput + '-obligatorio').hide();
        $(idInput).css("border-color", "#00ff00");
        return true;
    }
}

function limpiarAdvertencias() {
    $('.text-danger').hide();
    $('.form-control').css("border-color", "");
}

function mostrarSpinner() { $('#cover-spin').show(); }
function ocultarSpinner() { $('#cover-spin').hide(); }
