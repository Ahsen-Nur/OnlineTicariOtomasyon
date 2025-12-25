using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCTicariOtomasyonWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddCariIdToUrunYorum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CariId",
                table: "UrunYorumlar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UrunYorumlar_CariId",
                table: "UrunYorumlar",
                column: "CariId");

            migrationBuilder.AddForeignKey(
                name: "FK_UrunYorumlar_Carilers_CariId",
                table: "UrunYorumlar",
                column: "CariId",
                principalTable: "Carilers",
                principalColumn: "CariId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UrunYorumlar_Carilers_CariId",
                table: "UrunYorumlar");

            migrationBuilder.DropIndex(
                name: "IX_UrunYorumlar_CariId",
                table: "UrunYorumlar");

            migrationBuilder.DropColumn(
                name: "CariId",
                table: "UrunYorumlar");
        }
    }
}
