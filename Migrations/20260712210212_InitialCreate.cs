using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SGE.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "personas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dni = table.Column<long>(type: "bigint", nullable: false),
                    apellido_nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    sexo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    domicilio = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    localidad = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    departamento = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    circuito = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    escuela = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    mesa = table.Column<int>(type: "integer", nullable: true),
                    orden = table.Column<int>(type: "integer", nullable: true),
                    cambio = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    observaciones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "referentes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    persona_id = table.Column<int>(type: "integer", nullable: false),
                    telefono = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    observaciones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    fecha_alta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_referentes", x => x.id);
                    table.ForeignKey(
                        name: "FK_referentes_personas_persona_id",
                        column: x => x.persona_id,
                        principalTable: "personas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "asignaciones",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    referente_id = table.Column<int>(type: "integer", nullable: false),
                    persona_id = table.Column<int>(type: "integer", nullable: false),
                    rol = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    vehiculo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    patente = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    fecha_asignacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asignaciones", x => x.id);
                    table.CheckConstraint("CK_asignaciones_datos_movilizador", "(\r\n    rol = 'Movilizador'\r\n    AND vehiculo IS NOT NULL\r\n    AND patente IS NOT NULL\r\n)\r\nOR\r\n(\r\n    rol = 'Votante'\r\n    AND vehiculo IS NULL\r\n    AND patente IS NULL\r\n)");
                    table.ForeignKey(
                        name: "FK_asignaciones_personas_persona_id",
                        column: x => x.persona_id,
                        principalTable: "personas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_asignaciones_referentes_referente_id",
                        column: x => x.referente_id,
                        principalTable: "referentes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_asignaciones_persona_id",
                table: "asignaciones",
                column: "persona_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_asignaciones_referente_id",
                table: "asignaciones",
                column: "referente_id");

            migrationBuilder.CreateIndex(
                name: "IX_personas_dni",
                table: "personas",
                column: "dni",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_referentes_persona_id",
                table: "referentes",
                column: "persona_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "asignaciones");

            migrationBuilder.DropTable(
                name: "referentes");

            migrationBuilder.DropTable(
                name: "personas");
        }
    }
}
