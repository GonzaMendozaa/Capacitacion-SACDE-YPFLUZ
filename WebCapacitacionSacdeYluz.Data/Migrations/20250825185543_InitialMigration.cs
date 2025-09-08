using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCapacitacionSacdeYluz.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DwdMarca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DwdMarca", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DwdProveedor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comision = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DwdProveedor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DwdCalzado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Talle = table.Column<double>(type: "float", nullable: false),
                    MarcaId = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    ProveedorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DwdCalzado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DwdCalzado_DwdMarca_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "DwdMarca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DwdCalzado_DwdProveedor_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "DwdProveedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DwdCalzado_MarcaId",
                table: "DwdCalzado",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_DwdCalzado_ProveedorId",
                table: "DwdCalzado",
                column: "ProveedorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DwdCalzado");

            migrationBuilder.DropTable(
                name: "DwdMarca");

            migrationBuilder.DropTable(
                name: "DwdProveedor");
        }
    }
}
