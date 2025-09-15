var isCreating = false;
$(document).ready(function () {
    $('#datatable-calzados').DataTable();
});


// Abrir modal Crear
$('#buttonCreate-calzado').click(function () {
    limpiarAdvertencias();
    isCreating = true;
    //vaciar imputs
    $('#id-calzado').val('');
    $('#calzado-modelo').val('');
    $('#calzado-talle').val('');
    $('#calzado-precio').val('');
    $('.modal-title').text('Crear Calzado');
    $('#modalScrollable-calzado').modal('show');
});

// Submit Crear/Editar
$('#form-calzado').submit(function (event) {
    event.preventDefault();
    mostrarSpinner();

    if (validar_Submit()) {
        var calzado = {
            Id: isCreating ? 0 : $('#id-calzado').val(),
            Modelo: $('#calzado-modelo').val(),
            Talle: $('#calzado-talle').val(),
            Precio: $('#calzado-precio').val()
        };

        if (isCreating) {
            createCalzado(calzado);
        } else {
            updateCalzado(calzado);
        }
    } else {
        ocultarSpinner();
        Swal.fire('Error!', 'Los datos ingresados no son válidos', 'error');
    }
});


//editar
$('#datatable-calzados tbody').on('click', '.edit', function (e) {
    isCreatingCalzado = false;
    var element = e.currentTarget.parentElement.parentElement;
    var table = $('#datatable-calzados').DataTable();
    var calzado = table.row(element).data();
    var idCalzado = calzado[0];


    $('#modalScrollable-calzado .modal-title').text("Editar Calzado");
    //se llenan con los datos de la vista anterior
    $('#id-calzado').val(calzado[0]);
    $('#calzado-modelo').val(calzado[1]);
    $('#calzado-talle').val(calzado[2]);
    $('#calzado-precio').val(calzado[4]);

    $('#modalScrollable-calzado').modal('show');
});

// eliminar
$('#datatable-calzados tbody').on('click', '.delete', (e) => {
    isCreatingCalzado = true;
    var element = e.currentTarget.parentElement.parentElement;
    var table = $('#datatable-calzados').DataTable();
    var calzado = table.row(element).data();
    var idCalzado = calzado[0];


    Swal.fire({
        title: '¿Estás seguro/a?',
        text: "Esta acción no podrá ser revertida. Esta asignación será eliminada.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, eliminar'

    }).then((result) => {
        if (result.isConfirmed) {
            mostrarSpinner();
            $.ajax({
                type: "POST",
                url: 'Calzados/Delete',
                data: JSON.stringify(idCalzado),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function () {
                    ocultarSpinner();

                    Swal.fire(
                        'Eliminado!',
                        'La asignacion fue eliminada con éxito!',
                        'success'
                    )

                    table
                        .row(element)
                        .remove()
                        .draw();
                },
                error: function () {
                    ocultarSpinner();
                    Swal.fire(
                        'Error!',
                        'Ha ocurrido un error',
                        'error'
                    )
                }
            });
        }
    });
})
// AJAX Crear
function createCalzado(calzado) {
    $.ajax({
        type: "POST",
        url: '/Calzados/Create',
        data: JSON.stringify(calzado),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (created) {
            ocultarSpinner();

            var table = $('#datatable-calzados').DataTable();


            var buttonDelete = $("<button>", {
                type: 'button',
                class: 'btn btn-danger btn-sm delete',
                id: 'delete-' + created.id,
                style: 'padding: 2px 6px;',
                text: 'Eliminar'
            });
            var buttonEdit = $("<button>", {
                type: 'button',
                class: 'btn btn-warning btn-sm edit',
                id: 'edit-' + created.id,
                style: 'padding: 2px 6px;',
                text: 'Editar'
            });

            table.row.add([
                created.id,
                created.modelo,
                created.talle,
                created.precio,
                buttonDelete.prop('outerHTML') + " " + buttonEdit.prop('outerHTML') // ambos botones en la misma celda
            ]).draw(false);

            Swal.fire('Creado!', 'Calzado creado con éxito!', 'success');
            $('#modalScrollable-calzado').modal('hide');
        },
        error: function () {
            ocultarSpinner();
            Swal.fire('Error!', 'No se pudo crear', 'error');
        }
    });
}

// AJAX Update
function updateCalzado(calzado) {
    $.ajax({
        type: "POST",
        url: '/Calzados/Update',
        data: JSON.stringify(calzado),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (updated) {
            ocultarSpinner();

            var buttonDelete = $("<button>", {
                type: 'button',
                class: 'btn btn-danger btn-sm delete',
                id: 'delete-' + updated.id,
                style: 'padding: 2px 6px;',
                text: 'Eliminar'
            });
            var buttonEdit = $("<button>", {
                type: 'button',
                class: 'btn btn-warning btn-sm edit',
                id: 'edit-' + updated.id,
                style: 'padding: 2px 6px;',
                text: 'Editar'
            });

            var table = $('#datatable-calzados').DataTable();
            var row = table.row(function (idx, data, node) {
                return data[0] == updated.id;
            });

            if (row.node()) {
                // Actualizamos los valores de esa fila
                row.data([
                    updated.id,
                    updated.modelo,
                    updated.talle,
                    updated.precio,
                    buttonDelete.prop('outerHTML') + " " + buttonEdit.prop('outerHTML') // ambos botones en la misma celda
                ]).draw(false);

            }
              

            Swal.fire('Actualizado!', 'Calzado actualizado con éxito!', 'success');

            $('#modalScrollable-calzado').modal('hide');
        },
        error: function () {
            ocultarSpinner();
            Swal.fire('Error!', 'No se pudo actualizar', 'error');
        }
    });
}

// Helpers

function limpiarAdvertencias() {
    $('#calzado-precio-obligatorio').hide();
    $('#calzado-talle-obligatorio').hide();
    $('#calzado-modelo-obligatorio').hide();
    $('.form-control').css("border-color", "");
}

function validar_Submit() {
    return validar_InputSimple('#calzado-modelo')
        && validar_InputSimple('#calzado-talle')
        && validar_InputSimple('#calzado-precio');
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
