using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCapacitacionSacdeYluz.Data.Migrations
{
    /// <inheritdoc />
    public partial class _20250909 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DwdCalzado_DwdProveedor_ProveedorId",
                table: "DwdCalzado");

            migrationBuilder.DropIndex(
                name: "IX_DwdCalzado_ProveedorId",
                table: "DwdCalzado");

            migrationBuilder.DropColumn(
                name: "ProveedorId",
                table: "DwdCalzado");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "DwdCalzado");

            migrationBuilder.CreateTable(
                name: "DwdTienda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Provincia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DwdTienda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DwdVendedor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TiendaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DwdVendedor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DwdVendedor_DwdTienda_TiendaId",
                        column: x => x.TiendaId,
                        principalTable: "DwdTienda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DwfCompra",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ganancia = table.Column<double>(type: "float", nullable: false),
                    ProveedorId = table.Column<int>(type: "int", nullable: false),
                    TiendaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DwfCompra", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DwfCompra_DwdProveedor_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "DwdProveedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DwfCompra_DwdTienda_TiendaId",
                        column: x => x.TiendaId,
                        principalTable: "DwdTienda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DwfTiendaXCalzado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    TiendaId = table.Column<int>(type: "int", nullable: false),
                    CalzadoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DwfTiendaXCalzado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DwfTiendaXCalzado_DwdCalzado_CalzadoId",
                        column: x => x.CalzadoId,
                        principalTable: "DwdCalzado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DwfTiendaXCalzado_DwdTienda_TiendaId",
                        column: x => x.TiendaId,
                        principalTable: "DwdTienda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DwfVentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VendedorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DwfVentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DwfVentas_DwdVendedor_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "DwdVendedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DwfCompraXCalzado",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    CompraId = table.Column<int>(type: "int", nullable: false),
                    CalzadoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DwfCompraXCalzado", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DwfCompraXCalzado_DwdCalzado_CalzadoId",
                        column: x => x.CalzadoId,
                        principalTable: "DwdCalzado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DwfCompraXCalzado_DwfCompra_CompraId",
                        column: x => x.CompraId,
                        principalTable: "DwfCompra",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DwfVentasXCalzados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    CalzadoId = table.Column<int>(type: "int", nullable: false),
                    VentasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DwfVentasXCalzados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DwfVentasXCalzados_DwdCalzado_CalzadoId",
                        column: x => x.CalzadoId,
                        principalTable: "DwdCalzado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DwfVentasXCalzados_DwfVentas_VentasId",
                        column: x => x.VentasId,
                        principalTable: "DwfVentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DwdVendedor_TiendaId",
                table: "DwdVendedor",
                column: "TiendaId");

            migrationBuilder.CreateIndex(
                name: "IX_DwfCompra_ProveedorId",
                table: "DwfCompra",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_DwfCompra_TiendaId",
                table: "DwfCompra",
                column: "TiendaId");

            migrationBuilder.CreateIndex(
                name: "IX_DwfCompraXCalzado_CalzadoId",
                table: "DwfCompraXCalzado",
                column: "CalzadoId");

            migrationBuilder.CreateIndex(
                name: "IX_DwfCompraXCalzado_CompraId",
                table: "DwfCompraXCalzado",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_DwfTiendaXCalzado_CalzadoId",
                table: "DwfTiendaXCalzado",
                column: "CalzadoId");

            migrationBuilder.CreateIndex(
                name: "IX_DwfTiendaXCalzado_TiendaId",
                table: "DwfTiendaXCalzado",
                column: "TiendaId");

            migrationBuilder.CreateIndex(
                name: "IX_DwfVentas_VendedorId",
                table: "DwfVentas",
                column: "VendedorId");

            migrationBuilder.CreateIndex(
                name: "IX_DwfVentasXCalzados_CalzadoId",
                table: "DwfVentasXCalzados",
                column: "CalzadoId");

            migrationBuilder.CreateIndex(
                name: "IX_DwfVentasXCalzados_VentasId",
                table: "DwfVentasXCalzados",
                column: "VentasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DwfCompraXCalzado");

            migrationBuilder.DropTable(
                name: "DwfTiendaXCalzado");

            migrationBuilder.DropTable(
                name: "DwfVentasXCalzados");

            migrationBuilder.DropTable(
                name: "DwfCompra");

            migrationBuilder.DropTable(
                name: "DwfVentas");

            migrationBuilder.DropTable(
                name: "DwdVendedor");

            migrationBuilder.DropTable(
                name: "DwdTienda");

            migrationBuilder.AddColumn<int>(
                name: "ProveedorId",
                table: "DwdCalzado",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "DwdCalzado",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DwdCalzado_ProveedorId",
                table: "DwdCalzado",
                column: "ProveedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_DwdCalzado_DwdProveedor_ProveedorId",
                table: "DwdCalzado",
                column: "ProveedorId",
                principalTable: "DwdProveedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
