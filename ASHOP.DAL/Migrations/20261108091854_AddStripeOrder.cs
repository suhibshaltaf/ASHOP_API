using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASHOP.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddStripeOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StripeSeesioId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "paidAT",
                table: "Orders",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StripeSeesioId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "paidAT",
                table: "Orders");
        }
    }
}
