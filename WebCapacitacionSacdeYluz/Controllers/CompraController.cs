using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCapacitacionSacdeYluz.BL.Services.Interfaces;
using WebCapacitacionSacdeYluz.Data;
using WebCapacitacionSacdeYluz.Data.Models;
using WebCapacitacionSacdeYluz.Data.Models.DTOS;

namespace WebCapacitacionSacdeYluz.Controllers
{
    [Route("Compra")]
    public class CompraController : Controller
    {
        public readonly ICompraService _compraService;
        public readonly WebCapacitacionSacdeLuzDbContext _context;
        public CompraController(ICompraService compraService, WebCapacitacionSacdeLuzDbContext context)
        {
            _compraService = compraService;
            _context = context;
        }

        [Route("[controller]")]

        #region GET
        [HttpGet("Index")]
        public IActionResult Index()
        {
            var compra = _compraService.GetAllCompras();
            return View(compra);
        }
        #endregion



        #region Post
        [HttpPost("/Compra/Create")]
        public IActionResult Create([FromBody] CrearCompraDTO dto)
        {
            if (dto == null || !dto.Calzados.Any())
                return BadRequest("Debe seleccionar al menos un calzado.");

            // Crear la compra principal
            var compra = new DwfCompra
            {
                TiendaId = dto.TiendaId,
                ProveedorId = dto.ProveedorId,
                FechaCompra = DateTime.Now
            };

            double total = 0;
            var proveedor = _context.DwdProveedor.Find(dto.ProveedorId);

            _context.DwfCompra.Add(compra);
            _context.SaveChanges(); // para generar el Id de compra

            foreach (var item in dto.Calzados)
            {
                var tiendaCalzado = _context.DwfTiendaXCalzado
           .FirstOrDefault(tc => tc.TiendaId == compra.TiendaId && tc.CalzadoId == item.CalzadoId);

                if (tiendaCalzado == null)
                {
                    tiendaCalzado = new DwfTiendaXCalzado
                    {
                        TiendaId = compra.TiendaId,
                        CalzadoId = item.CalzadoId,
                        Stock = item.Cantidad
                    };
                    _context.DwfTiendaXCalzado.Add(tiendaCalzado);
                }
                else
                {
                    tiendaCalzado.Stock += item.Cantidad;
                }
                //si no funciona borrar lo anterior desde var tiendaCalzado
                var calzado = _context.DwdCalzado.FirstOrDefault(c => c.Id == item.CalzadoId);
                if (calzado == null) continue;

                var compraCalzado = new DwfCompraXCalzado
                {
                    CompraId = compra.Id,
                    CalzadoId = calzado.Id,
                    Cantidad = item.Cantidad
                };

                total += item.Cantidad * calzado.Precio;

                _context.DwfCompraXCalzado.Add(compraCalzado);
            }

            compra.TotalCompra = total;
            compra.Ganancia = total + (total * proveedor.Comision / 100);

            _context.SaveChanges();

            return Ok(new { mensaje = "Compra registrada con éxito", compraId = compra.Id, total });
        }

        [HttpGet("proveedores")]
        public IActionResult GetProveedores()
        {
            var proveedores = _context.DwdProveedor
                .Select(p => new { id = p.Id, descripcion = p.Descripcion })
                .ToList();
            return Ok(proveedores);
        }


        [HttpGet("tiendas")]
        public IActionResult GetTiendas()
        {
            var tiendas = _context.DwdTienda
                .Select(t => new { id = t.Id, nombre = t.Nombre })
                .ToList();
            return Ok(tiendas);
        }

        [HttpGet("calzados")]
        public IActionResult GetCalzados()
        {
            var calzados = _context.DwdCalzado
                .Select(c => new
                {
                    id = c.Id,
                    modelo = c.Modelo,
                    talle = c.Talle,
                    precio = c.Precio
                })
                .ToList();

            return Ok(calzados);
        }



    }
            
}


#endregion