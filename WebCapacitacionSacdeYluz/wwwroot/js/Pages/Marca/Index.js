var isCreating = false;
$(document).ready(function () {
    $('#datatable-marca').DataTable();
});


// Abrir modal Crear
$('#buttonCreate-marca').click(function () {
    limpiarAdvertencias();
    isCreating = true;
    $('#id-marca').val('');
    $('#marca-nombre').val('');
    $('#marca-pais').val('');
    $('#marca-').val('');
    $('.modal-title').text('Crear marca');
    $('#modalScrollable-marca').modal('show');
});


// Submit Crear/Editar
$('#form-marca').submit(function (event) {
    event.preventDefault();
    mostrarSpinner();

    if (validar_Submit()) {
        var marca = {
            Id: isCreating ? 0 : $('#id-marca').val(),
            Nombre: $('#marca-nombre').val(),
            Pais: $('#marca-pais').val(),
        };

        if (isCreating) {
            createmarca(marca);
        } else {
            updateMarca(marca);
        }
    } else {
        ocultarSpinner();
        Swal.fire('Error!', 'Los datos ingresados no son válidos', 'error');
    }
});


//editar
$('#datatable-marca tbody').on('click', '.edit', function (e) {
    isCreating = false;
    var element = e.currentTarget.parentElement.parentElement;
    var table = $('#datatable-marca').DataTable();
    var marca = table.row(element).data();
    var idMarca = marca[0];

    $('#modalScrollable-marca .modal-title').text("Editar marca");
    //se llenan con los datos de la vista anterior
    $('#id-marca').val(marca[0]);
    $('#marca-nombre').val(marca[1]);
    $('#marca-pais').val(marca[2]);

    var modalEl = document.getElementById('modalScrollable-marca');
    var bsModal = new bootstrap.Modal(modalEl);
    bsModal.show();
});

//eliminar
$('#datatable-marca tbody').on('click', '.delete', function (e) {
    var table = $('#datatable-marca').DataTable();
    var $tr = $(this).closest('tr');
    var marca = table.row($tr).data();

    if (!marca) {
        console.error("Fila no encontrada");
        return;
    }

    var idMarca = marca[0];

    Swal.fire({
        title: '¿Estás seguro?',
        text: "Esta acción no se puede deshacer",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Sí, eliminar'
    }).then((result) => {
        if (result.isConfirmed) {
            eliminarMarca(idMarca);
        }
    });
});

//AJAX crear
function createmarca(marca) {
    $.ajax({
        type: "POST",
        url: '/Marca/Create',
        data: JSON.stringify(marca),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (created) {
            ocultarSpinner();

            var table = $('#datatable-marca').DataTable();


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
                created.nombre,
                created.pais,
                buttonDelete.prop('outerHTML') + " " + buttonEdit.prop('outerHTML')
            ]).draw(false);

            Swal.fire('Creado!', 'Marca creada con éxito!', 'success');
            $('#modalMarca').modal('hide');
        },
        error: function () {
            ocultarSpinner();
            Swal.fire('Error!', 'No se pudo crear', 'error');
        }
    });
}

//AJAX update
function updateMarca(marca) {
    $.ajax({
        type: "POST",
        url: '/Marca/Update',
        data: JSON.stringify(marca),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (updated) {
            ocultarSpinner();

            var table = $('#datatable-marca').DataTable();

            var $row = $('#edit-' + updated.id).closest('tr');

            var rowData = table.row($row).data();

            rowData[1] = updated.nombre;
            rowData[2] = updated.pais;

            table.row($row).data(rowData).draw(false);

            Swal.fire('Actualizado!', 'Marca actualizada con éxito!', 'success');

            $('#modalMarca').modal('hide');
        },
        error: function () {
            ocultarSpinner();
            Swal.fire('Error!', 'No se pudo actualizar', 'error');
        }
    });
}

function eliminarMarca(id) {
    mostrarSpinner();

    $.ajax({
        type: "POST",
        url: '/Marca/Delete?idMarca=' + id, //antes pasaba aca que al poner solo /Marca/Delete tiraba error NOTION
        data: JSON.stringify(id),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (resultado) {
            ocultarSpinner();

            var table = $('#datatable-marca').DataTable();

            table.rows().every(function () {
                var data = this.data();

                if (data[0] == id) {
                    this.remove();
                }
            });

            table.draw(false);

            Swal.fire('Eliminado!', 'Marca eliminado con éxito!', 'success');

        },
        error: function () {
            ocultarSpinner();
            Swal.fire('Error!', 'No se pudo eliminar', 'error');
        }
    });
}

function limpiarAdvertencias() {
    $('#marca-nombre-obligatorio').hide();
    $('#marca-pais-obligatorio').hide();
    $('.form-control').css("border-color", "");
}

function validar_Submit() {
    return validar_InputSimple('#marca-nombre')
        && validar_InputSimple('#marca-pais')
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
