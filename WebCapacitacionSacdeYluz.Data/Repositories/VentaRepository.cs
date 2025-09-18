using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebCapacitacionSacdeYluz.Data.Models;
using WebCapacitacionSacdeYluz.Data.Repositories.Interfaces;

namespace WebCapacitacionSacdeYluz.Data.Repositories
{
    public class VentaRepository : IVentaRepository
    {
        private readonly WebCapacitacionSacdeLuzDbContext _context;

        public VentaRepository(WebCapacitacionSacdeLuzDbContext context)
        {
            _context = context;
        }

        // Obtener todas las ventas
        public List<DwfVentas> GetAllVentas()
        {
            return _context.DwfVentas
                .Include(v => v.VentasXCalzado)
                    .ThenInclude(vc => vc.Calzado)
                .Include(v => v.Vendedor)
                    .ThenInclude(ven => ven.Tienda) 
                .ToList();
        }


        // CRUD Ventas
        public DwfVentas CrearVenta(DwfVentas venta)
        {
            _context.DwfVentas.Add(venta);
            _context.SaveChanges();

            // Recargar la venta con relaciones necesarias
            var ventaCreada = _context.DwfVentas
                .Include(v => v.VentasXCalzado)
                    .ThenInclude(vc => vc.Calzado)
                .Include(v => v.Vendedor)
                    .ThenInclude(ven => ven.Tienda)
                .FirstOrDefault(v => v.Id == venta.Id);

            return ventaCreada!;
        }






        // Funciones necesarias para el servicio
        public DwdVendedor? GetVendedorById(int vendedorId)
        {
            return _context.DwdVendedor
                .Include(v => v.Tienda)
                .FirstOrDefault(v => v.Id == vendedorId);
        }

        public DwdCalzado? GetCalzadoById(int calzadoId)
        {
            return _context.DwdCalzado.FirstOrDefault(c => c.Id == calzadoId);
        }

        public DwfTiendaXCalzado? GetStock(int tiendaId, int calzadoId)
        {
            return _context.DwfTiendaXCalzado
                .FirstOrDefault(s => s.TiendaId == tiendaId && s.CalzadoId == calzadoId);
        }

        public DwfVentasXCalzado AddVentaXCalzado(DwfVentasXCalzado ventaXCalzado)
        {
            _context.DwfVentasXCalzados.Add(ventaXCalzado); // corregido
            _context.SaveChanges();
            return ventaXCalzado;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
