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
                    observaciones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    id_seccion = table.Column<int>(type: "integer", nullable: true),
                    domicilio_escuela = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    localidad_escuela = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
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
                name: "movilizadores",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    persona_id = table.Column<int>(type: "integer", nullable: false),
                    referente_id = table.Column<int>(type: "integer", nullable: false),
                    vehiculo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    patente = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    fecha_alta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movilizadores", x => x.id);
                    table.ForeignKey(
                        name: "FK_movilizadores_personas_persona_id",
                        column: x => x.persona_id,
                        principalTable: "personas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_movilizadores_referentes_referente_id",
                        column: x => x.referente_id,
                        principalTable: "referentes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "asignaciones_votantes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    persona_id = table.Column<int>(type: "integer", nullable: false),
                    movilizador_id = table.Column<int>(type: "integer", nullable: false),
                    fecha_asignacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asignaciones_votantes", x => x.id);
                    table.ForeignKey(
                        name: "FK_asignaciones_votantes_movilizadores_movilizador_id",
                        column: x => x.movilizador_id,
                        principalTable: "movilizadores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_asignaciones_votantes_personas_persona_id",
                        column: x => x.persona_id,
                        principalTable: "personas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_asignaciones_votantes_movilizador_id",
                table: "asignaciones_votantes",
                column: "movilizador_id");

            migrationBuilder.CreateIndex(
                name: "IX_asignaciones_votantes_persona_id",
                table: "asignaciones_votantes",
                column: "persona_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_movilizadores_persona_id",
                table: "movilizadores",
                column: "persona_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_movilizadores_referente_id",
                table: "movilizadores",
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
                name: "asignaciones_votantes");

            migrationBuilder.DropTable(
                name: "movilizadores");

            migrationBuilder.DropTable(
                name: "referentes");

            migrationBuilder.DropTable(
                name: "personas");
        }
    }
}
