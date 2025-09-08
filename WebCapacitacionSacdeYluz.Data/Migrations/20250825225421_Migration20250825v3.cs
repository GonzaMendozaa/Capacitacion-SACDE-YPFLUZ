using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCapacitacionSacdeYluz.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migration20250825v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Modelo",
                table: "DwdCalzado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "DwdCalzado",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Modelo",
                table: "DwdCalzado");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "DwdCalzado");
        }
    }
}
