using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCTicariOtomasyonWeb.Migrations
{
    /// <inheritdoc />
    public partial class carisifre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sifre",
                table: "Carilers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sifre",
                table: "Carilers");
        }
    }
}
