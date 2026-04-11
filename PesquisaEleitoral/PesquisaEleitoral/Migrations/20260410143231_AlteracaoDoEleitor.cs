using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PesquisaEleitoral.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoDoEleitor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Escolaridade",
                table: "Eleitores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Renda",
                table: "Eleitores",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Escolaridade",
                table: "Eleitores");

            migrationBuilder.DropColumn(
                name: "Renda",
                table: "Eleitores");
        }
    }
}
