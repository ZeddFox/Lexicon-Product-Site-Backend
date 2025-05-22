using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lexicon_Product_Site_Backend.Migrations
{
    /// <inheritdoc />
    public partial class addThumbnail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsThumbnail",
                table: "ProductImages",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsThumbnail",
                table: "ProductImages");
        }
    }
}
