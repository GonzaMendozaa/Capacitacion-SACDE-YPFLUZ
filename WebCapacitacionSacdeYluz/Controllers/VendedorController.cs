using Microsoft.AspNetCore.Mvc;
using WebCapacitacionSacdeYluz.BL.Services.Interfaces;
using WebCapacitacionSacdeYluz.Data.Models;


namespace WebCapacitacionSacdeYluz.Controllers
{
    [Route("Vendedor")]
    public class VendedorController : Controller
    {
        public readonly IVendedorService _vendedorService;
        public VendedorController(IVendedorService vendedorService)
        {
            _vendedorService = vendedorService;
        }

        [Route("[controller]")]

        #region GET
        [HttpGet("Index")]
        public IActionResult Index()
        {
            var proveedores = _vendedorService.GetAllVendedores();
            return View(proveedores);
        }

        [HttpGet("GetAllVendedor")]
        public ActionResult<List<DwdVendedor>> GetAllVendedores()
        {
            var vendedores = _vendedorService.GetAllVendedores();

            return Ok(vendedores);
        }

        #endregion

        #region Post
        [HttpPost("Create")]
        public IActionResult Create([FromBody] DwdVendedor vendedor)
        {
            try
            {
                DwdVendedor vendCreado = _vendedorService.CrearVendedor(vendedor);
                return Ok(vendCreado);
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                return Problem(error);
            }
        }
        #endregion

        #region UPDATE
        [HttpPost("Update")]
        public IActionResult Update([FromBody] DwdVendedor vendedor)
        {
            try
            {
                DwdVendedor vendActualizado = _vendedorService.UpdateVendedor(vendedor);
                return Ok(vendActualizado);
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                return Problem(error);
            }
        }
        #endregion

        #region DELETE
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] int id)
        {
            try
            {
                _vendedorService.DeleteVendedor(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return Problem(ex.ToString());
            }
        }

        #endregion


        // GET: /Vendedor/GetByTienda/1
        [HttpGet("GetByTienda/{tiendaId}")]
        public IActionResult GetByTienda(int tiendaId)
        {
            var vendedores = _vendedorService.GetVendedoresByTienda(tiendaId)
                .Select(v => new {
                    id = v.Id,
                    nombre = v.Nombre + " " 
                })
                .ToList();

            return Ok(vendedores);
        }

    }
}
