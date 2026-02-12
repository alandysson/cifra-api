using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace controleDeGastos.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateWithAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CategoriasDespesa",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasDespesa", x => x.CategoriaId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TiposDespesa",
                columns: table => new
                {
                    TipoDespesaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDespesa", x => x.TipoDespesaId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SenhaHash = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RefreshToken = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RefreshTokenExpiry = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SubCategoriasDespesa",
                columns: table => new
                {
                    SubCategoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoriasDespesa", x => x.SubCategoriaId);
                    table.ForeignKey(
                        name: "FK_SubCategoriasDespesa_CategoriasDespesa_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "CategoriasDespesa",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Anos",
                columns: table => new
                {
                    AnoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anos", x => x.AnoId);
                    table.ForeignKey(
                        name: "FK_Anos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Meses",
                columns: table => new
                {
                    MesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AnoId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Numero = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meses", x => x.MesId);
                    table.ForeignKey(
                        name: "FK_Meses_Anos_AnoId",
                        column: x => x.AnoId,
                        principalTable: "Anos",
                        principalColumn: "AnoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Despesas",
                columns: table => new
                {
                    DespesaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MesId = table.Column<int>(type: "int", nullable: false),
                    TipoDespesaId = table.Column<int>(type: "int", nullable: false),
                    SubCategoriaId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PercentualReceita = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despesas", x => x.DespesaId);
                    table.ForeignKey(
                        name: "FK_Despesas_Meses_MesId",
                        column: x => x.MesId,
                        principalTable: "Meses",
                        principalColumn: "MesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Despesas_SubCategoriasDespesa_SubCategoriaId",
                        column: x => x.SubCategoriaId,
                        principalTable: "SubCategoriasDespesa",
                        principalColumn: "SubCategoriaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Despesas_TiposDespesa_TipoDespesaId",
                        column: x => x.TipoDespesaId,
                        principalTable: "TiposDespesa",
                        principalColumn: "TipoDespesaId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Investimentos",
                columns: table => new
                {
                    InvestimentoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MesId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PercentualReceita = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investimentos", x => x.InvestimentoId);
                    table.ForeignKey(
                        name: "FK_Investimentos_Meses_MesId",
                        column: x => x.MesId,
                        principalTable: "Meses",
                        principalColumn: "MesId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Receitas",
                columns: table => new
                {
                    ReceitaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MesId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receitas", x => x.ReceitaId);
                    table.ForeignKey(
                        name: "FK_Receitas_Meses_MesId",
                        column: x => x.MesId,
                        principalTable: "Meses",
                        principalColumn: "MesId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Saldos",
                columns: table => new
                {
                    SaldoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MesId = table.Column<int>(type: "int", nullable: false),
                    Receita = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Investimentos = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DespesasFixas = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DespesasVariaveis = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DespesasExtras = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DespesasAdicionais = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Balanco = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Observacao = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saldos", x => x.SaldoId);
                    table.ForeignKey(
                        name: "FK_Saldos_Meses_MesId",
                        column: x => x.MesId,
                        principalTable: "Meses",
                        principalColumn: "MesId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "CategoriasDespesa",
                columns: new[] { "CategoriaId", "Nome" },
                values: new object[,]
                {
                    { 1, "Habitação" },
                    { 2, "Transporte" },
                    { 3, "Saúde" },
                    { 4, "Educação" },
                    { 5, "Impostos" },
                    { 6, "Alimentação" },
                    { 7, "Cuidados pessoais" },
                    { 8, "Lazer" },
                    { 9, "Vestuário" },
                    { 10, "Manutenção/prevenção" },
                    { 11, "Outros" }
                });

            migrationBuilder.InsertData(
                table: "TiposDespesa",
                columns: new[] { "TipoDespesaId", "Nome" },
                values: new object[,]
                {
                    { 1, "Fixa" },
                    { 2, "Variável" },
                    { 3, "Extra" },
                    { 4, "Adicional" }
                });

            migrationBuilder.InsertData(
                table: "SubCategoriasDespesa",
                columns: new[] { "SubCategoriaId", "CategoriaId", "Nome" },
                values: new object[,]
                {
                    { 1, 1, "Aluguel" },
                    { 2, 1, "Condomínio" },
                    { 3, 1, "Luz" },
                    { 4, 1, "Água" },
                    { 5, 1, "Spotify" },
                    { 6, 1, "Telefone celular" },
                    { 7, 1, "Gás" },
                    { 8, 1, "Mensalidade TV" },
                    { 9, 1, "Animais" },
                    { 10, 1, "Internet" },
                    { 11, 2, "Prestação da moto" },
                    { 12, 2, "Seguro da moto" },
                    { 13, 2, "Transporte por app" },
                    { 14, 2, "Combustível" },
                    { 15, 2, "Estacionamento" },
                    { 16, 6, "Supermercado" },
                    { 17, 6, "Feira" },
                    { 18, 6, "Hortifruti" },
                    { 19, 6, "Padaria" },
                    { 20, 3, "Plano de saúde" },
                    { 21, 3, "Medicamentos" },
                    { 22, 3, "Consultas" },
                    { 23, 4, "Cursos" },
                    { 24, 4, "Livros" },
                    { 25, 8, "Restaurantes" },
                    { 26, 8, "Cinema" },
                    { 27, 8, "Viagens" },
                    { 28, 11, "Outros" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anos_Numero_UsuarioId",
                table: "Anos",
                columns: new[] { "Numero", "UsuarioId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Anos_UsuarioId",
                table: "Anos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_MesId",
                table: "Despesas",
                column: "MesId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_SubCategoriaId",
                table: "Despesas",
                column: "SubCategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_TipoDespesaId",
                table: "Despesas",
                column: "TipoDespesaId");

            migrationBuilder.CreateIndex(
                name: "IX_Investimentos_MesId",
                table: "Investimentos",
                column: "MesId");

            migrationBuilder.CreateIndex(
                name: "IX_Meses_AnoId",
                table: "Meses",
                column: "AnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Receitas_MesId",
                table: "Receitas",
                column: "MesId");

            migrationBuilder.CreateIndex(
                name: "IX_Saldos_MesId",
                table: "Saldos",
                column: "MesId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoriasDespesa_CategoriaId",
                table: "SubCategoriasDespesa",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Despesas");

            migrationBuilder.DropTable(
                name: "Investimentos");

            migrationBuilder.DropTable(
                name: "Receitas");

            migrationBuilder.DropTable(
                name: "Saldos");

            migrationBuilder.DropTable(
                name: "SubCategoriasDespesa");

            migrationBuilder.DropTable(
                name: "TiposDespesa");

            migrationBuilder.DropTable(
                name: "Meses");

            migrationBuilder.DropTable(
                name: "CategoriasDespesa");

            migrationBuilder.DropTable(
                name: "Anos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
