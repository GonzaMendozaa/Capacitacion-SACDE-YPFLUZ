using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCapacitacionSacdeYluz.BL.Services.Interfaces;
using WebCapacitacionSacdeYluz.Data;
using WebCapacitacionSacdeYluz.Data.Models;
using WebCapacitacionSacdeYluz.Data.Models.DTOS;

namespace WebCapacitacionSacdeYluz.Controllers
{
    [Route("Venta")]
    public class VentaController : Controller
    {
        public readonly IVentaService _ventaService;
        public readonly WebCapacitacionSacdeLuzDbContext _context;
        public VentaController(IVentaService ventaService, WebCapacitacionSacdeLuzDbContext context)
        {
            _ventaService = ventaService;
            _context = context;
        }

        [Route("[controller]")]

        #region GET
        [HttpGet("Index")]
        public IActionResult Index()
        {
            var venta = _ventaService.GetAllVentas();
            return View(venta);
        }
        #endregion

        #region Post
        [HttpPost("/Venta/Create")]
        public IActionResult Create([FromBody] CrearVentaDTO dto)
        {
            try
            {
                var ventaId = _ventaService.CrearVenta(dto);
                return Ok(ventaId);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        #endregion

    }
}


