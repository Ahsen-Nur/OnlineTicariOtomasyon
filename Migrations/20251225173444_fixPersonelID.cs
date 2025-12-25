using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCTicariOtomasyonWeb.Migrations
{
    /// <inheritdoc />
    public partial class fixPersonelID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SatisHarekets_Personels_PersonelId",
                table: "SatisHarekets");

            migrationBuilder.AlterColumn<int>(
                name: "PersonelId",
                table: "SatisHarekets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SatisHarekets_Personels_PersonelId",
                table: "SatisHarekets",
                column: "PersonelId",
                principalTable: "Personels",
                principalColumn: "PersonelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SatisHarekets_Personels_PersonelId",
                table: "SatisHarekets");

            migrationBuilder.AlterColumn<int>(
                name: "PersonelId",
                table: "SatisHarekets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SatisHarekets_Personels_PersonelId",
                table: "SatisHarekets",
                column: "PersonelId",
                principalTable: "Personels",
                principalColumn: "PersonelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
