using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCapacitacionSacdeYluz.Data.Migrations
{
    /// <inheritdoc />
    public partial class activocalzados2025918 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cantidad",
                table: "DwfCompraXCalzado",
                newName: "Cantidad");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "DwdCalzado",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "DwdCalzado");

            migrationBuilder.RenameColumn(
                name: "Cantidad",
                table: "DwfCompraXCalzado",
                newName: "cantidad");
        }
    }
}
