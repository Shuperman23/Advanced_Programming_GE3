using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlBiblioteca.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AUTOR",
                columns: table => new
                {
                    AutorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    Estado = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AUTOR__F58AE909ACD96785", x => x.AutorID);
                });

            migrationBuilder.CreateTable(
                name: "GENERO_LITERARIO",
                columns: table => new
                {
                    GeneroLiterarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    Estado = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GENERO_L__5B6322709BE81B8B", x => x.GeneroLiterarioID);
                });

            migrationBuilder.CreateTable(
                name: "PARAMETRO",
                columns: table => new
                {
                    ParametroID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Valor = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PARAMETR__2B3CE672E26A000B", x => x.ParametroID);
                });

            migrationBuilder.CreateTable(
                name: "LIBRO",
                columns: table => new
                {
                    LibroID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeneroLiterarioID = table.Column<int>(type: "int", nullable: false),
                    AutorID = table.Column<int>(type: "int", nullable: false),
                    NombreLibro = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    Estado = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LIBRO__35A1EC8D202E18EE", x => x.LibroID);
                    table.ForeignKey(
                        name: "FK__LIBRO__AutorID__3C69FB99",
                        column: x => x.AutorID,
                        principalTable: "AUTOR",
                        principalColumn: "AutorID");
                    table.ForeignKey(
                        name: "FK__LIBRO__GeneroLit__3B75D760",
                        column: x => x.GeneroLiterarioID,
                        principalTable: "GENERO_LITERARIO",
                        principalColumn: "GeneroLiterarioID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LIBRO_AutorID",
                table: "LIBRO",
                column: "AutorID");

            migrationBuilder.CreateIndex(
                name: "IX_LIBRO_GeneroLiterarioID",
                table: "LIBRO",
                column: "GeneroLiterarioID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LIBRO");

            migrationBuilder.DropTable(
                name: "PARAMETRO");

            migrationBuilder.DropTable(
                name: "AUTOR");

            migrationBuilder.DropTable(
                name: "GENERO_LITERARIO");
        }
    }
}
