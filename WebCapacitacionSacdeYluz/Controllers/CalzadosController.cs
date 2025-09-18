using Microsoft.AspNetCore.Mvc;
using WebCapacitacionSacdeYluz.BL.Services.Interfaces;
using WebCapacitacionSacdeYluz.Data.Models;
using WebCapacitacionSacdeYluz.Data.Repositories.Interfaces;

namespace WebCapacitacionSacdeYluz.Controllers
{
    [Route("Calzados")]
    public class CalzadosController : Controller
    {
        public readonly ICalzadoService _calzadoService;
        public readonly ICalzadoRepository _calzadoRepository;
        public CalzadosController(ICalzadoService calzadoService, ICalzadoRepository calzadoRepository)
        {
            _calzadoService = calzadoService;
            _calzadoRepository = calzadoRepository;
        }

        [Route("[controller]")]

        #region GET
        [HttpGet("Index")]
        public IActionResult Index()
        {
            var calzados = _calzadoService.GetAllCalzados();
            return View(calzados);
        }

        #endregion

        #region Post
        [HttpPost("/Calzados/Create")]
        public IActionResult Create([FromBody] DwdCalzado calzado)
        {
            try
            {
                calzado.MarcaId = 1; //hardcodeado para que funcione
                return Ok(_calzadoService.CrearCalzado(calzado));
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                return Problem(error);
            }
        }
        #endregion

        #region Put
        [HttpPost("/Calzados/Update")]
        public IActionResult Update([FromBody] DwdCalzado calzado)
        {
            try
            {
                calzado.MarcaId = 1; //hardcodeado para que funcione
                return Ok(_calzadoService.UpdateCalzado(calzado));
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                return Problem(error);
            }
        }

        #endregion

        #region Delete
        [HttpPost("/Calzados/Delete")]
        public IActionResult Delete([FromBody] int idCalzado)
        {
            _calzadoService.DeleteCalzado(idCalzado);
  
            return Ok("Se borró correctamente");
        }
        #endregion


        // GET: /Calzado/GetByTienda/1
        [HttpGet("GetByTienda/{tiendaId}")]
        public IActionResult GetByTienda(int tiendaId)
        {
            var calzados = _calzadoService.GetCalzadosByTienda(tiendaId)
                .Select(tx => new
                {
                    id = tx.Calzado.Id,
                    modelo = tx.Calzado.Modelo,
                    stock = tx.Stock,
                    precio = tx.Calzado.Precio
                })
                .ToList();

            return Ok(calzados);
        }
    }
}

