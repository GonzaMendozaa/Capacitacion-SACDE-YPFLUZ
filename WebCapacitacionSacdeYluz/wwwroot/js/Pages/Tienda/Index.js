var isCreating = false;

$(document).ready(function () {
    $('#datatable-tienda').DataTable();
});


// Abrir modal Crear
$('#buttonCreate-tienda').click(function () {
    limpiarAdvertencias();
    isCreating = true;
    $('#id-tienda').val('');
    $('#tienda-nombre').val('');
    $('#tienda-provincia').val('');
    $('#tienda-direccion').val('');
    $('.modal-title').text('Crear tienda');
    $('#modalScrollable-tienda').modal('show');
});


// Submit Crear/Editar
$('#form-tienda').submit(function (event) {
    event.preventDefault();
    mostrarSpinner();

    if (validar_Submit()) {
        var tienda = {
            Id: isCreating ? 0 : $('#id-tienda').val(),
            Nombre: $('#tienda-nombre').val(),
            Provincia: $('#tienda-provincia').val(),
            Direccion: $('#tienda-direccion').val()
        };

        if (isCreating) {
            createTienda(tienda);
        } else {
            updateTienda(tienda);
        }
    } else {
        ocultarSpinner();
        Swal.fire('Error!', 'Los datos ingresados no son válidos', 'error');
    }
});


//editar
$('#datatable-tienda tbody').on('click', '.edit', function (e) {
    isCreating = false;
    var element = e.currentTarget.parentElement.parentElement;
    var table = $('#datatable-tienda').DataTable();
    var tienda = table.row(element).data();
    var idTienda = tienda[0];

    $('#modalScrollable-tienda .modal-title').text("Editar tienda");
    //se llenan con los datos de la vista anterior
    $('#id-tienda').val(tienda[0]);
    $('#tienda-nombre').val(tienda[1]);
    $('#tienda-provincia').val(tienda[2]);
    $('#tienda-direccion').val(tienda[3]);

    var modalEl = document.getElementById('modalScrollable-tienda');
    var bsModal = new bootstrap.Modal(modalEl);
    bsModal.show();
});

//eliminar
$('#datatable-tienda tbody').on('click', '.delete', function (e) {
    var table = $('#datatable-tienda').DataTable();
    var $tr = $(this).closest('tr');
    var tienda = table.row($tr).data();

    if (!tienda) {
        console.error("Fila no encontrada");
        return;
    }

    var idTienda = tienda[0];

    Swal.fire({
        title: '¿Estás seguro?',
        text: "Esta acción no se puede deshacer",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Sí, eliminar'
    }).then((result) => {
        if (result.isConfirmed) {
            eliminarTienda(idTienda);
        }
    });
});

//AJAX create
function createTienda(tienda) {
    $.ajax({
        type: "POST",
        url: '/Tienda/Create',
        data: JSON.stringify(tienda),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (created) {
            ocultarSpinner();

            var table = $('#datatable-tienda').DataTable();

            var buttonStock = $("<button>", {
                type: 'button',
                class: 'btn btn-info btn-sm stock',   // azul/celeste
                id: 'stock-' + created.id,
                style: 'padding: 2px 6px;',
                text: 'Stock'
            });

            var buttonVendedor = $("<button>", {
                type: 'button',
                class: 'btn btn-secondary btn-sm vendedor',  // gris
                id: 'vendedor-' + created.id,
                style: 'padding: 2px 6px;',
                text: 'Ver'
            });



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
                created.provincia,
                created.direccion,
                buttonStock.prop('outerHTML'),
                buttonVendedor.prop('outerHTML'),
                buttonDelete.prop('outerHTML') + " " + buttonEdit.prop('outerHTML')
            ]).draw(false);

            Swal.fire('Creado!', 'Tienda creada con éxito!', 'success');
            $('#modalTienda').modal('hide');
        },
        error: function () {
            ocultarSpinner();
            Swal.fire('Error!', 'No se pudo crear', 'error');
        }
    });
}

//AJAX update
function updateTienda(tienda) {
    $.ajax({
        type: "POST",
        url: '/Tienda/Update',
        data: JSON.stringify(tienda),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (updated) {
            ocultarSpinner();

            var table = $('#datatable-tienda').DataTable();

            var $row = $('#edit-' + updated.id).closest('tr');

            var rowData = table.row($row).data();

            rowData[1] = updated.nombre;
            rowData[2] = updated.provincia;
            rowData[3] = updated.direccion;

            table.row($row).data(rowData).draw(false);

            Swal.fire('Actualizado!', 'Tienda actualizado con éxito!', 'success');

            $('#modalTienda').modal('hide');
        },
        error: function () {
            ocultarSpinner();
            Swal.fire('Error!', 'No se pudo actualizar', 'error');
        }
    });
}

function eliminarTienda(id) {
    mostrarSpinner();

    $.ajax({
        type: "POST",
        url: '/Tienda/Delete?idTienda=' + id, //antes pasaba aca que al poner solo /Tienda/Delete tiraba error NOTION
        data: JSON.stringify(id),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (resultado) {
            ocultarSpinner();

            var table = $('#datatable-tienda').DataTable();

            table.rows().every(function () {
                var data = this.data();

                if (data[0] == id) {
                    this.remove();
                }
            });

            table.draw(false);

            Swal.fire('Eliminado!', 'Tienda eliminado con éxito!', 'success');

        },
        error: function () {
            ocultarSpinner();
            Swal.fire('Error!', 'No se pudo eliminar', 'error');
        }
    });
}

function verStock(idTienda) {
    mostrarSpinner();

    $.ajax({
        url: '/Tienda/Stock?idTienda=' + idTienda,
        type: 'GET',
        dataType: 'json',
        success: function (stock) {
            ocultarSpinner();

            const tbody = $('#tabla-stock tbody');
            tbody.empty();

            if (stock.length === 0) {
                tbody.append('<tr><td colspan="5">No hay stock disponible</td></tr>');
            } else {
                stock.forEach(item => {
                    tbody.append(`
                        <tr>
                            <td>${item.calzadoId}</td>
                            <td>${item.talle}</td>
                            <td>${item.modelo}</td>
                            <td>${item.marca}</td>
                            <td>${item.stock}</td>
                        </tr>
                    `);
                });
            }

            const modalEl = document.getElementById('modalStock-tienda');
            const bsModal = new bootstrap.Modal(modalEl);
            bsModal.show();
        },
        error: function () {
            ocultarSpinner();
            Swal.fire('Error!', 'No se pudo ver el stock', 'error');
        }
    });
}

function verVendedores(idTienda) {
    mostrarSpinner();

    $.ajax({
        url: '/Tienda/Vendedores?idTienda=' + idTienda,
        type: 'GET',
        dataType: 'json',
        success: function (vendedores) {
            ocultarSpinner();

            const ul = $('#lista-vendedores');
            ul.empty();

            if (vendedores.length === 0) {
                ul.append('<li class="list-group-item">No hay vendedores asignados</li>');
            } else {
                vendedores.forEach(v => {
                    ul.append(`<li class="list-group-item">${v.nombre}</li>`);
                });
            }

            const modalEl = document.getElementById('modalVendedores-tienda');
            const bsModal = new bootstrap.Modal(modalEl);
            bsModal.show();
        },
        error: function (err) {
            ocultarSpinner();
            Swal.fire('Error!', 'No se pudieron ver los vendedores', 'error');
            console.error(err);
        }
    });
}



function limpiarAdvertencias() {
    $('#tienda-nombre-obligatorio').hide();
    $('#tienda-provincia-obligatorio').hide();
    $('#tienda-direccion-obligatorio').hide();
    $('.form-control').css("border-color", "");
}

function validar_Submit() {
    return validar_InputSimple('#tienda-nombre')
        && validar_InputSimple('#tienda-provincia')
        && validar_InputSimple('#tienda-direccion')
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
