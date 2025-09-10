using Microsoft.AspNetCore.Mvc;
using WebCapacitacionSacdeYluz.BL.Services.Interfaces;
using WebCapacitacionSacdeYluz.Data.Models;

namespace WebCapacitacionSacdeYluz.Controllers
{
    public class TiendaController : Controller
    {
        public readonly ITiendaService _tiendaService;
        public TiendaController(ITiendaService tiendaService)
        {
            _tiendaService = tiendaService;
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
        public IActionResult Delete([FromBody] int idTienda)
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
    }
}
