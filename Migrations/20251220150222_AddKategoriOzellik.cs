using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCTicariOtomasyonWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddKategoriOzellik : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KategoriOzellikler",
                columns: table => new
                {
                    KategoriOzellikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriId = table.Column<int>(type: "int", nullable: false),
                    OzellikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KategoriOzellikler", x => x.KategoriOzellikId);
                    table.ForeignKey(
                        name: "FK_KategoriOzellikler_Kategoris_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategoris",
                        principalColumn: "KategoriId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KategoriOzellikler_UrunOzellikler_OzellikId",
                        column: x => x.OzellikId,
                        principalTable: "UrunOzellikler",
                        principalColumn: "OzellikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KategoriOzellikler_KategoriId",
                table: "KategoriOzellikler",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_KategoriOzellikler_OzellikId",
                table: "KategoriOzellikler",
                column: "OzellikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KategoriOzellikler");
        }
    }
}
