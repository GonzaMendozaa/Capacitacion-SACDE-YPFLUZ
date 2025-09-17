var isCreating = false;

$(document).ready(function () {
    if (!$.fn.DataTable.isDataTable('#datatable-vendedor')) {
        table = $('#datatable-vendedor').DataTable({
            columns: [
                { title: "Id" },
                { title: "Nombre" },
                { title: "TiendaId", visible: false },
                { title: "Acciones" }
            ]
        });
    } else {
        table = $('#datatable-vendedor').DataTable();
    }
});


// Abrir modal Crear
$('#buttonCreate-vendedor').click(function () {
    limpiarAdvertencias();
    isCreating = true;
    $('#id-vendedor').val('');
    $('#vendedor-nombre').val('');

    $('.modal-title').text('Crear Vendedor');
    cargarTiendas();
    $('#modalVendedor').modal('show');
});

// Submit Crear/Editar
$('#form-vendedor').submit(function (event) {
    event.preventDefault();
    mostrarSpinner();

    if (validar_Submit()) {
        // Obtener TiendaId del select y validar
        var tiendaId = parseInt($('#vendedor-tienda').val());
        if (isNaN(tiendaId) || tiendaId === 0) {
            Swal.fire('Error!', 'Debe seleccionar una tienda válida', 'error');
            ocultarSpinner();
            return; // sale del submit
        }

        // Crear objeto vendedor
        var vendedor = {
            Id: isCreating ? 0 : parseInt($('#id-vendedor').val()),
            Nombre: $('#vendedor-nombre').val().trim(),
            TiendaId: tiendaId
        };

        // Llamar a create o update según corresponda
        if (isCreating) {
            createVendedor(vendedor);
        } else {
            updateVendedor(vendedor);
        }

    } else {
        ocultarSpinner();
        Swal.fire('Error!', 'Los datos ingresados no son válidos', 'error');
    }
});


// Editar
$('#datatable-vendedor tbody').on('click', '.edit', function (e) {
    isCreating = false;
    var element = e.currentTarget.parentElement.parentElement;
    //var table = $('#datatable-vendedor').DataTable();
    var vendedor = table.row(element).data();
    var idvendedor = vendedor[0];


    $('#modalVendedor .modal-title').text("Editar vendedor");
    //se llenan con los datos de la vista anterior
    $('#id-vendedor').val(vendedor[0]);
    $('#vendedor-nombre').val(vendedor[1]);
    cargarTiendas(vendedor[2]);

    $('#modalVendedor').modal('show');
});

// Eliminar
$('#datatable-vendedor tbody').on('click', '.delete', (e) => {
    isCreating = true;
    var element = e.currentTarget.parentElement.parentElement;
    //var table = $('#datatable-vendedor').DataTable();
    var vendedor = table.row(element).data();
    var idvendedor = vendedor[0];


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
            eliminarvendedor(idvendedor);
        }

    });
})

// ----------- AJAX ------------

function createVendedor(vendedor) {
    $.ajax({
        type: "POST",
        url: '/Vendedor/Create',
        data: JSON.stringify(vendedor),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (created) {
            ocultarSpinner();

            //var table = $('#datatable-vendedor').DataTable();


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
                created.nombre,
                created.tiendaId,
                buttonDelete.prop('outerHTML') + " " + buttonEdit.prop('outerHTML') // ambos botones en la misma celda
            ]).draw(false);

            Swal.fire('Creado!', 'vendedor creado con éxito!', 'success');
            $('#modalVendedor').modal('hide');
        },
        error: function () {
            ocultarSpinner();
            Swal.fire('Error!', 'No se pudo crear', 'error');
        }
    });
}

// AJAX Update
function updateVendedor(vendedor) {
    $.ajax({
        type: "POST",
        url: '/Vendedor/Update',
        data: JSON.stringify(vendedor),
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

            //var table = $('#datatable-vendedor').DataTable();
            var row = table.row(function (idx, data, node) {
                return data[0] == updated.id;
            });

            if (row.node()) {
                // Actualizamos los valores de esa fila
                row.data([
                    updated.id,
                    updated.nombre,
                    updated.tiendaId,
                    buttonDelete.prop('outerHTML') + " " + buttonEdit.prop('outerHTML') // ambos botones en la misma celda
                ]).draw(false);

            }


            Swal.fire('Actualizado!', 'vendedor actualizado con éxito!', 'success');

            $('#modalVendedor').modal('hide');
        },
        error: function () {
            ocultarSpinner();
            Swal.fire('Error!', 'No se pudo actualizar', 'error');
        }
    });
}

function eliminarvendedor(id) {
    mostrarSpinner();

    $.ajax({
        type: "POST",
        url: '/Vendedor/Delete?idvendedor=' + id, //antes pasaba aca que al poner solo /Tienda/Delete tiraba error NOTION
        data: JSON.stringify(id),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (resultado) {
            ocultarSpinner();

            //var table = $('#datatable-vendedor').DataTable();

            table.rows().every(function () {
                var data = this.data();

                if (data[0] == id) {
                    this.remove();
                }
            });

            table.draw(false);

            Swal.fire('Eliminado!', 'Vendedor eliminado con éxito!', 'success');

        },
        error: function () {
            ocultarSpinner();
            Swal.fire('Error!', 'No se pudo eliminar', 'error');
        }
    });
}
// Helpers

function cargarTiendas(selectedId) {
    $.ajax({
        type: "GET",
        url: '/Tienda/GetAll', // este endpoint debe devolver todas las tiendas
        success: function (tiendas) {
            var select = $('#vendedor-tienda');
            select.empty();
            select.append('<option value="">-- Seleccione una tienda --</option>');

            tiendas.forEach(function (tienda) {
                var selected = (tienda.id == selectedId) ? 'selected' : '';
                select.append(`<option value="${tienda.id}" ${selected}>${tienda.nombre}</option>`);
            });
        },
        error: function (err) {
            console.error('Error al cargar tiendas', err);
        }
    });
}
function limpiarAdvertencias() {
    $('#vendedor-nombre-obligatorio').hide();
    $('.form-control').css("border-color", "");
}

function validar_Submit() {
    return validar_InputSimple('#vendedor-nombre') && validar_Select('#vendedor-tienda');
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

function validar_Select(idSelect) {
    if ($(idSelect).val() === "") {
        $(idSelect + '-obligatorio').show();
        $(idSelect).css("border-color", "#FF0000");
        return false;
    } else {
        $(idSelect + '-obligatorio').hide();
        $(idSelect).css("border-color", "#00FF00");
        return true;
    }
}

function limpiarAdvertencias() {
    $('.text-danger').hide();
    $('.form-control').css("border-color", "");
}

function mostrarSpinner() { $('#cover-spin').show(); }
function ocultarSpinner() { $('#cover-spin').hide(); }