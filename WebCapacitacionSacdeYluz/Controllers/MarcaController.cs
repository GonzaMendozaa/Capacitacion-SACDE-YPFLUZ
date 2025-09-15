using Microsoft.AspNetCore.Mvc;
using WebCapacitacionSacdeYluz.BL.Services.Interfaces;
using WebCapacitacionSacdeYluz.Data.Models;

namespace WebCapacitacionSacdeYluz.Controllers
{
    [Route("Marca")]
    public class MarcaController : Controller
    {
        public readonly IMarcaService _MarcaService;
        public MarcaController(IMarcaService marcaService)
        {
            _MarcaService = marcaService;
        }

        [Route("[controller]")]

        #region GET
        [HttpGet("Index")]
        public IActionResult Index()
        {
            var marca = _MarcaService.GetAllMarcas();
            return View(marca);
        }

        #endregion

        #region Post
        [HttpPost("/Marca/Create")]
        public IActionResult Create([FromBody] DwdMarca marca)
        {
            try
            {
                return Ok(_MarcaService.CrearMarca(marca));
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                return Problem(error);
            }
        }
        #endregion

        #region Put
        [HttpPost("/Marca/Update")]
        public IActionResult Update([FromBody] DwdMarca marca)
        {
            try
            {

                return Ok(_MarcaService.UpdateMarca(marca));
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                return Problem(error);
            }
        }

        #endregion

        #region Delete
        [HttpPost("/Marca/Delete")]
        public IActionResult Delete(int idMarca)
        {
            try
            {
                _MarcaService.DeleteMarca(idMarca);
                return Ok(idMarca);
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

