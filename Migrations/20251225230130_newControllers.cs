using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCTicariOtomasyonWeb.Migrations
{
    /// <inheritdoc />
    public partial class newControllers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "KargoyaVerilmeTarihi",
                table: "SatisHarekets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SiparisDurum",
                table: "SatisHarekets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Kargos",
                columns: table => new
                {
                    KargoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KargoFirmasi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TakipKodu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Durum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SatisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kargos", x => x.KargoId);
                    table.ForeignKey(
                        name: "FK_Kargos_SatisHarekets_SatisId",
                        column: x => x.SatisId,
                        principalTable: "SatisHarekets",
                        principalColumn: "SatisId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kargos_SatisId",
                table: "Kargos",
                column: "SatisId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kargos");

            migrationBuilder.DropColumn(
                name: "KargoyaVerilmeTarihi",
                table: "SatisHarekets");

            migrationBuilder.DropColumn(
                name: "SiparisDurum",
                table: "SatisHarekets");
        }
    }
}
