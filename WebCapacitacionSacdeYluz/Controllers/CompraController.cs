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
            try
            {
                // Cabecera de la compra
                var compra = new DwfCompra
                {
                    FechaCompra = DateTime.Now,
                    ProveedorId = dto.ProveedorId,
                    TiendaId = dto.TiendaId
                };

                double total = 0;

                foreach (var item in dto.Calzados)
                {
                    var calzado = _context.DwdCalzado.FirstOrDefault(c => c.Id == item.CalzadoId);
                    if (calzado == null) return BadRequest($"El calzado {item.CalzadoId} no existe");

                    // sumo al total
                    total += calzado.Precio * item.Cantidad;

                    // agrego la relación CompraXCalzado
                    var compraXCalzado = new DwfCompraXCalzado
                    {
                        Compra = compra,
                        CalzadoId = calzado.Id,
                        Cantidad = item.Cantidad
                    };

                    _context.DwfCompraXCalzado.Add(compraXCalzado);

                    // sumo al stock de la tienda
                    var stockTienda = _context.DwfTiendaXCalzado
                        .FirstOrDefault(x => x.TiendaId == dto.TiendaId && x.CalzadoId == calzado.Id);

                    if (stockTienda != null)
                        stockTienda.Stock += item.Cantidad;
                    else
                        _context.DwfTiendaXCalzado.Add(new DwfTiendaXCalzado
                        {
                            TiendaId = dto.TiendaId,
                            CalzadoId = calzado.Id,
                            Stock = item.Cantidad
                        });
                }
                // Calcular total y ganancia
                compra.TotalCompra = total;

                var proveedor = _context.DwdProveedor.First(p => p.Id == dto.ProveedorId);
                compra.Ganancia = total * (1 + proveedor.Comision / 100);

                _context.DwfCompra.Add(compra);
                _context.SaveChanges();

                return Ok(compra.Id);
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                return Problem(error);
            }
        }
        #endregion

    }
}


        