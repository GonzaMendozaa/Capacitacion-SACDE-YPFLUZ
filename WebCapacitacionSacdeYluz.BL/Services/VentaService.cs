using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCapacitacionSacdeYluz.BL.Services.Interfaces;
using WebCapacitacionSacdeYluz.Data;
using WebCapacitacionSacdeYluz.Data.Models;
using WebCapacitacionSacdeYluz.Data.Models.DTOS;
using WebCapacitacionSacdeYluz.Data.Repositories;
using WebCapacitacionSacdeYluz.Data.Repositories.Interfaces;

namespace WebCapacitacionSacdeYluz.BL.Services
{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _ventaRepository;

        public VentaService(IVentaRepository VentaRepository)
        {
            _ventaRepository = VentaRepository;
        }

       public List<DwfVentas> GetAllVentas()
        {
            return _ventaRepository.GetAllVentas();
        }


        public int CrearVenta(CrearVentaDTO dto)
        {
            try
            {
                // Obtener vendedor y su tienda
                var vendedor = _ventaRepository.GetVendedorById(dto.VendedorId);
                if (vendedor == null)
                    throw new Exception("Vendedor no existe");

                var tiendaId = vendedor.TiendaId;

                // Crear venta
                var venta = new DwfVentas
                {
                    FechaPago = DateTime.Now,
                    VendedorId = vendedor.Id
                };

                double total = 0;

                foreach (var item in dto.Calzados)
                {
                    var calzado = _ventaRepository.GetCalzadoById(item.CalzadoId);
                    if (calzado == null)
                        throw new Exception($"El calzado {item.CalzadoId} no existe");

                    // Consultar stock usando repositorio
                    var stockTienda = _ventaRepository.GetStock(tiendaId, calzado.Id);
                    if (stockTienda == null || stockTienda.Stock < item.Cantidad)
                        throw new Exception($"Stock insuficiente para el calzado {calzado.Id}");

                    // Restar stock
                    stockTienda.Stock -= item.Cantidad;

                    // Crear relación Venta-Calzado
                    var ventaXCalzado = new DwfVentasXCalzado
                    {
                        Ventas = venta,
                        CalzadoId = calzado.Id,
                        Cantidad = item.Cantidad
                    };

                    _ventaRepository.AddVentaXCalzado(ventaXCalzado);

                    total += calzado.Precio * item.Cantidad;
                }

                venta.TotalVenta = total;

                _ventaRepository.CrearVenta(venta);
                _ventaRepository.SaveChanges();

                return venta.Id;
            }
            catch
            {
                throw;
            }
        }

        public DwfVentas UpdateVenta(DwfVentas Venta)
        {
            try
            {
                return _ventaRepository.UpdateVenta(Venta);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteVenta(int id)
        {
            try
            {
                _ventaRepository.DeleteVenta(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}

