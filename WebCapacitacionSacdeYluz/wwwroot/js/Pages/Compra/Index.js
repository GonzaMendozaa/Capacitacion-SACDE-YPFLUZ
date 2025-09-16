var isCreating = false;

$(document).ready(function () {
    $('#datatable-compra').DataTable();
});


// Abrir modal Crear
$('#buttonCreate-compra').click(function () {
    limpiarAdvertencias();
    isCreating = true;
    $('#id-compra').val('');
    $('#compra-tienda').val('');
    $('#compra-proveedor').val('');
    $('#compra-fechacompra').val('');
    $('#compra-totalcompra').val('');
    $('#compra-ganancia').val('');
    $('.modal-title').text('Crear compra');
    $('#modalScrollable-compra').modal('show');
});


// Submit Crear
$('#form-compra').submit(function (event) {
    event.preventDefault();
    mostrarSpinner();

    if (validar_Submit()) {
        var compra = {
            Id: isCreating ? 0 : $('#id-compra').val(),
            Tienda: $('#compra-tienda').val(),
            Proveedor: $('#compra-proveedor').val(),
            Fechacompra: $('#compra-fechacompra').val(),
            Totalcompra: $('#compra-totalcompra').val(),
            Ganancia: $('#compra-ganancia').val()
        };

        if (isCreating) {
            createCompra(compra);
        } else {
            updateCompra(compra);
        }
    } else {
        ocultarSpinner();
        Swal.fire('Error!', 'Los datos ingresados no son válidos', 'error');
    }
});

//AJAX create
function createCompra(compra) {
    $.ajax({
        type: "POST",
        url: '/Compra/Create',
        data: JSON.stringify(compra),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (created) {
            ocultarSpinner();

            var table = $('#datatable-compra').DataTable();
            });

            table.row.add([
                created.id,
                created.tienda,
                created.proveedor,
                created.fechacompra,
                created.totalcompra,
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
    $('#compra-tienda-obligatorio').hide();
    $('#compra-proveedor-obligatorio').hide();
    $('#compra-fechacompra-obligatorio').hide();
    $('#compra-totalcompra-obligatorio').hide();
    $('#compra-ganancia-obligatorio').hide();
    $('.form-control').css("border-color", "");
}

function validar_Submit() {
    return validar_InputSimple('#compra-tienda')
        && validar_InputSimple('#compra-proveedor')
        && validar_InputSimple('#compra-fechacompra')
        && validar_InputSimple('#compra-totalcompra')
        && validar_InputSimple('#compra-ganancia')
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
