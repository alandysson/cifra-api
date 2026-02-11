using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace controleDeGastos.Migrations
{
    /// <inheritdoc />
    public partial class AddDespesaRecorrente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DespesaRecorrenteId",
                table: "Despesas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DespesasRecorrentes",
                columns: table => new
                {
                    DespesaRecorrenteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    TipoDespesaId = table.Column<int>(type: "int", nullable: false),
                    SubCategoriaId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AnoInicio = table.Column<int>(type: "int", nullable: false),
                    MesInicio = table.Column<int>(type: "int", nullable: false),
                    AnoFim = table.Column<int>(type: "int", nullable: false),
                    MesFim = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesasRecorrentes", x => x.DespesaRecorrenteId);
                    table.ForeignKey(
                        name: "FK_DespesasRecorrentes_SubCategoriasDespesa_SubCategoriaId",
                        column: x => x.SubCategoriaId,
                        principalTable: "SubCategoriasDespesa",
                        principalColumn: "SubCategoriaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DespesasRecorrentes_TiposDespesa_TipoDespesaId",
                        column: x => x.TipoDespesaId,
                        principalTable: "TiposDespesa",
                        principalColumn: "TipoDespesaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DespesasRecorrentes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_DespesaRecorrenteId",
                table: "Despesas",
                column: "DespesaRecorrenteId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesasRecorrentes_SubCategoriaId",
                table: "DespesasRecorrentes",
                column: "SubCategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesasRecorrentes_TipoDespesaId",
                table: "DespesasRecorrentes",
                column: "TipoDespesaId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesasRecorrentes_UsuarioId",
                table: "DespesasRecorrentes",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_DespesasRecorrentes_DespesaRecorrenteId",
                table: "Despesas",
                column: "DespesaRecorrenteId",
                principalTable: "DespesasRecorrentes",
                principalColumn: "DespesaRecorrenteId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_DespesasRecorrentes_DespesaRecorrenteId",
                table: "Despesas");

            migrationBuilder.DropTable(
                name: "DespesasRecorrentes");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_DespesaRecorrenteId",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "DespesaRecorrenteId",
                table: "Despesas");
        }
    }
}
