using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCTicariOtomasyonWeb.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Satis_Cari_Nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SatisHarekets_Carilers_CariId",
                table: "SatisHarekets");

            migrationBuilder.AlterColumn<int>(
                name: "CariId",
                table: "SatisHarekets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SatisHarekets_Carilers_CariId",
                table: "SatisHarekets",
                column: "CariId",
                principalTable: "Carilers",
                principalColumn: "CariId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SatisHarekets_Carilers_CariId",
                table: "SatisHarekets");

            migrationBuilder.AlterColumn<int>(
                name: "CariId",
                table: "SatisHarekets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SatisHarekets_Carilers_CariId",
                table: "SatisHarekets",
                column: "CariId",
                principalTable: "Carilers",
                principalColumn: "CariId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
