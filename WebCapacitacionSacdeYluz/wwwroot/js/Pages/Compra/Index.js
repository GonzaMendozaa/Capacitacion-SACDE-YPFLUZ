var isCreating = false;
var calzadosDisponibles = [];

$(document).ready(function () {

    $("#buttonCreate-compra").click(function () {
        $("#modalScrollable-compra").modal("show");
        $("#exampleModalScrollableTitle-compra").text("Nueva compra");
    });

    // cargar proveedores
    $.get("/Compra/proveedores", function (data) {
        $("#compra-proveedor").empty().append(`<option value="0">-- Seleccione un proveedor --</option>`);
        data.forEach(p => {
            $("#compra-proveedor").append(`<option value="${p.id}">${p.descripcion}</option>`);
        });
    });


    // cargar tiendas
    $.get("/Compra/tiendas", function (data) {
        $("#compra-tienda").empty().append(`<option value="0">-- Seleccione una tienda --</option>`);
        data.forEach(t => {
            $("#compra-tienda").append(`<option value="${t.id}">${t.nombre}</option>`);
        });
    });

    // cargar calzados disponibles
    $.get("/Compra/calzados", function (data) {
        calzadosDisponibles = data;
    });

    // agregar un calzado
    $("#add-calzado").click(function () {
        let select = `<select class="form-select calzado-select">`;
        calzadosDisponibles.forEach(c => {
            select += `<option value="${c.id}">${c.modelo} - Talle ${c.talle} - $${c.precio}</option>`;
        });
        select += `</select>`;

        let row = `
            <div class="calzado-row mb-2 d-flex">
                ${select}
                <input type="number" class="form-control cantidad-input ms-2" placeholder="Cantidad" min="1" value="1"/>
                <button type="button" class="btn btn-danger btn-sm ms-2 remove-calzado">X</button>
            </div>`;
        $("#calzado-container").append(row);
    });

    // eliminar fila de calzado
    $(document).on("click", ".remove-calzado", function () {
        $(this).closest(".calzado-row").remove();
    });

    // guardar compra
    $("#form-Compra").submit(function (event) {
        event.preventDefault();
        mostrarSpinner();

        let compra = {
            proveedorId: $("#compra-proveedor").val(),
            tiendaId: $("#compra-tienda").val(),
            calzados: []
        };

        $("#calzado-container .calzado-row").each(function () {
            compra.calzados.push({
                calzadoId: $(this).find(".calzado-select").val(),
                cantidad: parseInt($(this).find(".cantidad-input").val())
            });
        });

        if (compra.calzados.length === 0) {
            ocultarSpinner();
            Swal.fire("Error", "Debe agregar al menos un calzado", "error");
            return;
        }

        $.ajax({
            type: "POST",
            url: '/Compra/Create',
            data: JSON.stringify(compra),
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                ocultarSpinner();
                Swal.fire("Éxito", "Compra creada con éxito. Total: $" + res.total, "success");
                $("#form-Compra")[0].reset();
                $("#calzado-container").empty();
                $("#modalScrollable-compra").modal("hide");

       
                location.reload();  
            },
            error: function () {
                ocultarSpinner();
                Swal.fire("Error", "No se pudo guardar la compra", "error");
            }
        });
    });
});

function mostrarSpinner() { $('#cover-spin').show(); }
function ocultarSpinner() { $('#cover-spin').hide(); }
