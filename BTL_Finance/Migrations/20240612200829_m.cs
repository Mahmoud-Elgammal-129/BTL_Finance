using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTL_Finance.Migrations
{
    /// <inheritdoc />
    public partial class m : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Channel",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Channel",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "Channel",
                table: "OrderSheets");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "OrderSheets");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "OrderSheets");

            migrationBuilder.DropColumn(
                name: "Channel",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Channel",
                table: "DeliveryNotes");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "DeliveryNotes");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "DeliveryNotes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Channel",
                table: "Quotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Quotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Quotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Channel",
                table: "PurchaseOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "PurchaseOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "PurchaseOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Channel",
                table: "OrderSheets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "OrderSheets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "OrderSheets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Channel",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Channel",
                table: "DeliveryNotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "DeliveryNotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "DeliveryNotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
