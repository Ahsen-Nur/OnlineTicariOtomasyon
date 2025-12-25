using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCTicariOtomasyonWeb.Migrations
{
    /// <inheritdoc />
    public partial class updateUrunYorum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Puan",
                table: "UrunYorumlar",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Puan",
                table: "UrunYorumlar");
        }
    }
}
