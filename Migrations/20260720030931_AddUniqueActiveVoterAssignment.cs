using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGE.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueActiveVoterAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_asignaciones_votantes_persona_id",
                table: "asignaciones_votantes");

            migrationBuilder.AddColumn<int>(
                name: "PersonaId1",
                table: "asignaciones_votantes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_asignaciones_votantes_persona_id",
                table: "asignaciones_votantes",
                column: "persona_id",
                unique: true,
                filter: "activo = true");

            migrationBuilder.CreateIndex(
                name: "IX_asignaciones_votantes_PersonaId1",
                table: "asignaciones_votantes",
                column: "PersonaId1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_asignaciones_votantes_personas_PersonaId1",
                table: "asignaciones_votantes",
                column: "PersonaId1",
                principalTable: "personas",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_asignaciones_votantes_personas_PersonaId1",
                table: "asignaciones_votantes");

            migrationBuilder.DropIndex(
                name: "IX_asignaciones_votantes_persona_id",
                table: "asignaciones_votantes");

            migrationBuilder.DropIndex(
                name: "IX_asignaciones_votantes_PersonaId1",
                table: "asignaciones_votantes");

            migrationBuilder.DropColumn(
                name: "PersonaId1",
                table: "asignaciones_votantes");

            migrationBuilder.CreateIndex(
                name: "IX_asignaciones_votantes_persona_id",
                table: "asignaciones_votantes",
                column: "persona_id",
                unique: true);
        }
    }
}
