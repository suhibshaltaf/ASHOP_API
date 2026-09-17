using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASHOP.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addpublicIdtoproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainImagePublicId",
                table: "products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImagePublicId",
                table: "products");
        }
    }
}
