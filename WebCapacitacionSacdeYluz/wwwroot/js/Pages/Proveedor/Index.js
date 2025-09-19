var isCreating = false;
$(document).ready(function () {
    $('#datatable-proveedor').DataTable();
});

$('#buttonCreate-proveedor').click(function () {
    limpiarAdvertencias();
    isCreating = true;
    //holagonza
    $('#id-proveedor').val('');
    $('#proveedor-descripcion').val('');
    $('#proveedor-comision').val('');
    $('.modal-title').text('Crear Proveedor');
    $('#modalProveedor').modal('show');
});

$('#form-proveedor').submit(function (event) {
    event.preventDefault();
    mostrarSpinner();

    if (validar_Submit()) {
        var proveedor = {
            Id: isCreating ? 0 : $('#id-proveedor').val(),
            Descripcion: $('#proveedor-descripcion').val(),
            Comision: obtenerComisionValida(),
        };

        if (isCreating) {
            createProveedor(proveedor);
        } else {
            updateProveedor(proveedor);
        }
    } else {
        ocultarSpinner();
        Swal.fire('Error!', 'Los datos ingresados no son válidos', 'error');
    }
});

$('#datatable-proveedor tbody').on('click', '.edit', function (e) {
    isCreating = false;
    var element = e.currentTarget.parentElement.parentElement;
    var table = $('#datatable-proveedor').DataTable();
    var proveedor = table.row(element).data();
    var idProveedor = proveedor[0];

    $('#modalProveedor .modal-title').text("Editar Proveedor");
    $('#id-proveedor').val(proveedor[0]);
    $('#proveedor-descripcion').val(proveedor[1]);
    $('#proveedor-comision').val(proveedor[2]);

    $('#modalProveedor').modal('show');
});

$('#datatable-proveedor tbody').on('click', '.delete', function (e) {
    var table = $('#datatable-proveedor').DataTable();
    var $tr = $(this).closest('tr');
    var proveedor = table.row($tr).data();

    if (!proveedor) {
        console.error("Fila no encontrada");
        return;
    }

    var idProveedor = proveedor[0];

    Swal.fire({
        title: '¿Estás seguro?',
        text: "Esta acción no se puede deshacer",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Sí, eliminar'
    }).then((result) => {
        if (result.isConfirmed) {
            eliminarProveedor(idProveedor);
        }
    });
});

function createProveedor(proveedor) {
    $.ajax({
        type: "POST",
        url: '/Proveedor/Create',
        data: JSON.stringify(proveedor),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (created) {
            ocultarSpinner();

            var table = $('#datatable-proveedor').DataTable();


            var buttonDelete = $("<button>", {
                type: 'button',
                class: 'btn btn-danger btn-sm delete',
                id: 'delete-' + created.Id,
                style: 'padding: 2px 6px;',
                text: 'Eliminar'
            });

            var buttonEdit = $("<button>", {
                type: 'button',
                class: 'btn btn-warning btn-sm edit',
                id: 'edit-' + created.Id,
                style: 'padding: 2px 6px;',
                text: 'Editar'
            });

            table.row.add([
                created.id,
                created.descripcion,
                created.comision,
                buttonDelete.prop('outerHTML') + " " + buttonEdit.prop('outerHTML') 
            ]).draw(false);

            Swal.fire('Creado!', 'Proveedor creado con éxito!', 'success');
            $('#modalProveedor').modal('hide');
        },
        error: function () {
            ocultarSpinner();
            Swal.fire('Error!', 'No se pudo crear', 'error');
        }
    });
}

function updateProveedor(proveedor) {
    $.ajax({
        type: "POST",
        url: '/Proveedor/Update',
        data: JSON.stringify(proveedor),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (updated) {
            ocultarSpinner();

            var table = $('#datatable-proveedor').DataTable();

            var $row = $('#edit-' + updated.id).closest('tr');

            var rowData = table.row($row).data();

            rowData[1] = updated.descripcion;
            rowData[2] = updated.comision;

            table.row($row).data(rowData).draw(false);

            Swal.fire('Actualizado!', 'Proveedor actualizado con éxito!', 'success');

            $('#modalProveedor').modal('hide');
        },
        error: function () {
            ocultarSpinner();
            Swal.fire('Error!', 'No se pudo actualizar', 'error');
        }
    });
}

function eliminarProveedor(id) {
    mostrarSpinner();

    $.ajax({
        type: "POST", 
        url: '/Proveedor/Delete', 
        data: JSON.stringify(id),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (resultado) {
            ocultarSpinner();

            var table = $('#datatable-proveedor').DataTable();

            table.rows().every(function () {
                var data = this.data();

                if (data[0] == id) {
                    this.remove();
                }
            });

            table.draw(false);

            Swal.fire('Eliminado!', 'Proveedor eliminado con éxito!', 'success');
            
        },
        error: function () {
            ocultarSpinner();
            Swal.fire('Error!', 'No se pudo eliminar', 'error');
        }
    });
}

function limpiarAdvertencias() {
    $('#proveedor-descripcion-obligatorio').hide();
    $('#proveedor-comision-obligatorio').hide();
    $('.form-control').css("border-color", "");
}

function validar_Submit() {
    return validar_InputSimple('#proveedor-descripcion')
        && validar_InputSimple('#proveedor-comision')
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

function obtenerComisionValida() {
    let valor = $('#proveedor-comision').val().trim();

    if (valor === "") return null;

    valor = valor.replace(",", ".");

    let numero = parseFloat(valor);

    return isNaN(numero) ? null : numero;
}

function mostrarSpinner() { $('#cover-spin').show(); }
function ocultarSpinner() { $('#cover-spin').hide(); }
