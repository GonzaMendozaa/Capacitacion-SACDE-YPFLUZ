var isCreating = false;

$(document).ready(function () {
    $('#datatable-venta').DataTable();
});


// Abrir modal Crear
$('#buttonCreate-venta').click(function () {
    limpiarAdvertencias();
    isCreating = true;

    // reset inputs
    $('#id-venta').val('');
    $('#venta-tienda').val('');
    $('#venta-fechapago').val('');
    $('#venta-vendedor').val('');
    $('#calzados-seleccionados').empty(); // limpiamos lista detalle

    $('.modal-title').text('Crear venta');
    $('#modalScrollable-venta').modal('show');

    // cargar tiendas al abrir
    cargarTiendas();
});


//Cargar vendedores
$('#venta-tienda').change(function () {
    var tiendaId = $(this).val();

    if (tiendaId) {
        $.get('/Vendedor/GetByTienda/' + tiendaId, function (data) {
            $('#venta-vendedor').empty().append('<option value="">-- Seleccione --</option>');
            $.each(data, function (i, v) {
                $('#venta-vendedor').append(`<option value="${v.id}">${v.nombre}</option>`);
            });
        });
    }
});

$('#buttonAdd-calzado').click(function () {
    var tiendaId = $('#venta-tienda').val();

    if (!tiendaId) {
        Swal.fire('Atención', 'Seleccione una tienda primero', 'warning');
        return;
    }

    $.get('/Calzados/GetByTienda/' + tiendaId, function (data) {
        var options = '<option value="">-- Seleccione --</option>';
        $.each(data, function (i, c) {
            options += `<option value="${c.id}">${c.modelo} - Stock: ${c.stock} - $${c.precio}</option>`;
        });

        var row = `
      <tr>
        <td>
          <select class="form-control calzado-id">${options}</select>
        </td>
        <td>
          <input type="number" class="form-control cantidad" value="1" min="1"/>
        </td>
        <td>
          <button type="button" class="btn btn-danger btn-sm remove-calzado">X</button>
        </td>
      </tr>
    `;

        $('#calzados-seleccionados').append(row);
    });
});

// eliminar fila de calzado
$(document).on('click', '.remove-calzado', function () {
    $(this).closest('tr').remove();
});


// Submit Crear
$('#form-venta').submit(function (event) {
    event.preventDefault();
    mostrarSpinner();

    if (validar_Submit()) {
        // armar lista de calzados
        var calzados = [];
        $('#calzados-seleccionados tr').each(function () {
            var calzadoId = $(this).find('.calzado-id').val();
            var cantidad = $(this).find('.cantidad').val();

            if (calzadoId && cantidad) {
                calzados.push({
                    CalzadoId: parseInt(calzadoId),
                    Cantidad: parseInt(cantidad)
                });
            }
        });

        var venta = {
            VendedorId: $('#venta-vendedor').val(),
            FechaPago: $('#venta-fechapago').val(),
            Calzados: calzados
        };

        if (isCreating) {
            createVenta(venta);
        } else {
            updateVenta(venta);
        }
    } else {
        ocultarSpinner();
        Swal.fire('Error!', 'Los datos ingresados no son válidos', 'error');
    }
});


//AJAX create
function createVenta(venta) {
    $.ajax({
        type: "POST",
        url: '/Venta/Create',
        data: JSON.stringify(venta),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (created) {
            ocultarSpinner();

            var table = $('#datatable-venta').DataTable();
            table.row.add([
                created.Id,
                created.FechaPago,
                created.Vendedor.Tienda.Nombre,
                created.Vendedor.nombre,
                created.TotalVenta,
                created.VentasXCalzado.map(v => `${v.Calzado.Modelo} x${v.Cantidad}`).join(", ") // según cómo lo devuelvas en el backend
            ]).draw(false);

            Swal.fire('Creado!', 'Venta creada con éxito!', 'success');
            $('#modalScrollable-venta').modal('hide');
        },
        error: function () {
            ocultarSpinner();
            Swal.fire('Error!', 'No se pudo crear', 'error');
        }
    });
}
function limpiarAdvertencias() {
    $('.text-danger').hide();
    $('.form-control').css("border-color", "");
}


function validar_Submit() {
    return validar_InputSimple('#venta-tienda')
        && validar_InputSimple('#venta-vendedor')
        && validar_InputSimple('#venta-fechapago')

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

function cargarTiendas() {
    $.get('/Tienda/GetAll', function (data) {
        $('#venta-tienda').empty().append('<option value="">-- Seleccione --</option>');
        $.each(data, function (i, t) {
            $('#venta-tienda').append(`<option value="${t.id}">${t.nombre}</option>`);
        });
    });
}


function mostrarSpinner() { $('#cover-spin').show(); }
function ocultarSpinner() { $('#cover-spin').hide(); }
