using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCTicariOtomasyonWeb.Migrations
{
    /// <inheritdoc />
    public partial class newupdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kargos_SatisHarekets_SatisId",
                table: "Kargos");

            migrationBuilder.DropIndex(
                name: "IX_Kargos_SatisId",
                table: "Kargos");

            migrationBuilder.DropColumn(
                name: "SatisId",
                table: "Kargos");

            migrationBuilder.CreateTable(
                name: "KargoDetays",
                columns: table => new
                {
                    KargoDetayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KargoId = table.Column<int>(type: "int", nullable: false),
                    SatisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KargoDetays", x => x.KargoDetayId);
                    table.ForeignKey(
                        name: "FK_KargoDetays_Kargos_KargoId",
                        column: x => x.KargoId,
                        principalTable: "Kargos",
                        principalColumn: "KargoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KargoDetays_SatisHarekets_SatisId",
                        column: x => x.SatisId,
                        principalTable: "SatisHarekets",
                        principalColumn: "SatisId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KargoDetays_KargoId",
                table: "KargoDetays",
                column: "KargoId");

            migrationBuilder.CreateIndex(
                name: "IX_KargoDetays_SatisId",
                table: "KargoDetays",
                column: "SatisId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KargoDetays");

            migrationBuilder.AddColumn<int>(
                name: "SatisId",
                table: "Kargos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Kargos_SatisId",
                table: "Kargos",
                column: "SatisId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Kargos_SatisHarekets_SatisId",
                table: "Kargos",
                column: "SatisId",
                principalTable: "SatisHarekets",
                principalColumn: "SatisId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
