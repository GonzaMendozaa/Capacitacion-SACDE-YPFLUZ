using Microsoft.AspNetCore.Mvc;
using WebCapacitacionSacdeYluz.BL.Services.Interfaces;
using WebCapacitacionSacdeYluz.Data.Models;

namespace WebCapacitacionSacdeYluz.Controllers
{
    public class CalzadosController : Controller
    {
        public readonly ICalzadoService _calzadoService;
        public CalzadosController(ICalzadoService calzadoService)
        {
            _calzadoService = calzadoService;
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
        public IActionResult Delete([FromBody]int idCalzado)
        {
            try
            {
                _calzadoService.DeleteCalzado(idCalzado);
                return Ok(idCalzado);
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
