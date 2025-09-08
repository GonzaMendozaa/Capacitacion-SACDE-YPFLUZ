using Microsoft.AspNetCore.Mvc;
using WebCapacitacionSacdeYluz.BL.Services;
using WebCapacitacionSacdeYluz.BL.Services.Interfaces;
using WebCapacitacionSacdeYluz.Data.Models;


namespace WebCapacitacionSacdeYluz.Controllers
{
    [Route("Proveedor")]
    public class ProveedorController : Controller
    {
        public readonly IProveedorService _proveedorService;
        public ProveedorController(IProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        [Route("[controller]")]

        #region GET
        [HttpGet("Index")]
        public IActionResult Index()
        {
            var proveedores = _proveedorService.GetAllProveedores();
            return View(proveedores);
        }

        [HttpGet("/Proveedor/GetAllProveedores")]
        public ActionResult<List<DwdProveedor>> GetAllProveedores()
        {
            var proveedores = _proveedorService.GetAllProveedores();
            return Ok(proveedores);
        }

        #endregion

        #region Post
        [HttpPost("/Proveedor/Create")]
        public IActionResult Create([FromBody] DwdProveedor proveedor)
        {
            try
            {
                DwdProveedor provCreado = _proveedorService.CrearProveedor(proveedor);
                return Ok(provCreado);
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                return Problem(error);
            }
        }
        #endregion

        #region UPDATE
        [HttpPost("/Proveedor/Update")]
        public IActionResult Update([FromBody] DwdProveedor proveedor)
        {
            try
            {
                DwdProveedor provActualizado = _proveedorService.ActualizarProveedor(proveedor);
                return Ok(provActualizado);
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                return Problem(error);
            }
        }
        #endregion

        #region DELETE
        [HttpPost("/Proveedor/Delete")]
        public IActionResult Delete([FromBody] int id)
        {
            try
            {
                bool eliminado = _proveedorService.EliminarProveedor(id);
                return Ok(eliminado);
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
