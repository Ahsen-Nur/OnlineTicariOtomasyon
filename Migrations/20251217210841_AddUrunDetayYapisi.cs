using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCTicariOtomasyonWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddUrunDetayYapisi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UrunAciklamalar",
                columns: table => new
                {
                    AciklamaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunId = table.Column<int>(type: "int", nullable: false),
                    Metin = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrunAciklamalar", x => x.AciklamaId);
                    table.ForeignKey(
                        name: "FK_UrunAciklamalar_Uruns_UrunId",
                        column: x => x.UrunId,
                        principalTable: "Uruns",
                        principalColumn: "UrunId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UrunOzellikler",
                columns: table => new
                {
                    OzellikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OzellikAd = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrunOzellikler", x => x.OzellikId);
                });

            migrationBuilder.CreateTable(
                name: "UrunYorumlar",
                columns: table => new
                {
                    YorumId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunId = table.Column<int>(type: "int", nullable: false),
                    KullaniciAd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Yorum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrunYorumlar", x => x.YorumId);
                    table.ForeignKey(
                        name: "FK_UrunYorumlar_Uruns_UrunId",
                        column: x => x.UrunId,
                        principalTable: "Uruns",
                        principalColumn: "UrunId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UrunOzellikDegerleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunId = table.Column<int>(type: "int", nullable: false),
                    OzellikId = table.Column<int>(type: "int", nullable: false),
                    Deger = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrunOzellikDegerleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UrunOzellikDegerleri_UrunOzellikler_OzellikId",
                        column: x => x.OzellikId,
                        principalTable: "UrunOzellikler",
                        principalColumn: "OzellikId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UrunOzellikDegerleri_Uruns_UrunId",
                        column: x => x.UrunId,
                        principalTable: "Uruns",
                        principalColumn: "UrunId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UrunAciklamalar_UrunId",
                table: "UrunAciklamalar",
                column: "UrunId");

            migrationBuilder.CreateIndex(
                name: "IX_UrunOzellikDegerleri_OzellikId",
                table: "UrunOzellikDegerleri",
                column: "OzellikId");

            migrationBuilder.CreateIndex(
                name: "IX_UrunOzellikDegerleri_UrunId",
                table: "UrunOzellikDegerleri",
                column: "UrunId");

            migrationBuilder.CreateIndex(
                name: "IX_UrunYorumlar_UrunId",
                table: "UrunYorumlar",
                column: "UrunId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrunAciklamalar");

            migrationBuilder.DropTable(
                name: "UrunOzellikDegerleri");

            migrationBuilder.DropTable(
                name: "UrunYorumlar");

            migrationBuilder.DropTable(
                name: "UrunOzellikler");
        }
    }
}
