using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCapacitacionSacdeYluz.BL.Services.Interfaces;
using WebCapacitacionSacdeYluz.Data;
using WebCapacitacionSacdeYluz.Data.Models;
using WebCapacitacionSacdeYluz.Data.Models.DTOS;

namespace WebCapacitacionSacdeYluz.Controllers
{
    [Route("Tienda")]
    public class TiendaController : Controller
    {
        public readonly ITiendaService _tiendaService;
        public readonly WebCapacitacionSacdeLuzDbContext _context;
        public TiendaController(ITiendaService tiendaService, WebCapacitacionSacdeLuzDbContext context)
        {
            _tiendaService = tiendaService;
            _context = context;
        }

        [Route("[controller]")]

        #region GET
        [HttpGet("Index")]
        public IActionResult Index()
        {
            var tienda = _tiendaService.GetAllTiendas();
            return View(tienda);
        }

        #endregion

        #region Post
        [HttpPost("/Tienda/Create")]
        public IActionResult Create([FromBody] DwdTienda tienda)
        {
            try
            {
                return Ok(_tiendaService.CrearTienda(tienda));
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                return Problem(error);
            }
        }
        #endregion

        #region Put
        [HttpPost("/Tienda/Update")]
        public IActionResult Update([FromBody] DwdTienda tienda)
        {
            try
            {

                return Ok(_tiendaService.UpdateTienda(tienda));
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                return Problem(error);
            }
        }

        #endregion

        #region Delete
        [HttpPost("/Tienda/Delete")]
        public IActionResult Delete(int idTienda)
        {
            try
            {
                _tiendaService.DeleteTienda(idTienda);
                return Ok(idTienda);
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                return Problem(error);
            }
        }
        #endregion

        [HttpGet("/Tienda/Stock")]
        public IActionResult Stock(int idTienda)
        {
            var stock = _context.DwfTiendaXCalzado
                .Include(txc => txc.Calzado)
                .ThenInclude(c => c.Marca)
                .Where(txc => txc.TiendaId == idTienda)
                .Select(txc => new StockCalzadoDTO
                {
                    CalzadoId = txc.CalzadoId,
                    Talle = txc.Calzado.Talle,
                    Modelo = txc.Calzado.Modelo,
                    Marca = txc.Calzado.Marca.Nombre,
                    Stock = txc.Stock
                })
                .ToList();

            return Json(stock);
        }

        [HttpGet("/Tienda/Vendedores")]
        public IActionResult Vendedores(int idTienda)
        {
            try
            {
                var vendedores = _context.DwdVendedor
                    .Where(v => v.TiendaId == idTienda)
                    .Select(v => new
                    {
                        v.Id,
                        v.Nombre
                    })
                    .ToList();

                return Json(vendedores);
            }
            catch (Exception ex)
            {
                return Problem(ex.ToString());
            }
        }





    }
}

