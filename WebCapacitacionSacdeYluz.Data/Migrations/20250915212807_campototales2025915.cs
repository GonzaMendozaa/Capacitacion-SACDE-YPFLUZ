using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCapacitacionSacdeYluz.Data.Migrations
{
    /// <inheritdoc />
    public partial class campototales2025915 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "DwfCompraXCalzado",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "DwfCompra",
                newName: "Id");

            migrationBuilder.AddColumn<double>(
                name: "TotalVenta",
                table: "DwfVentas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalCompra",
                table: "DwfCompra",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalVenta",
                table: "DwfVentas");

            migrationBuilder.DropColumn(
                name: "TotalCompra",
                table: "DwfCompra");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DwfCompraXCalzado",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DwfCompra",
                newName: "ID");
        }
    }
}
