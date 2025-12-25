using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCTicariOtomasyonWeb.Migrations
{
    /// <inheritdoc />
    public partial class addsatisdurum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Durum",
                table: "SatisHarekets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Durum",
                table: "SatisHarekets");
        }
    }
}
