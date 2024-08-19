using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTL_Finance.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status_Delivery_Note",
                table: "Requests",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status_Invoice",
                table: "Requests",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status_PO",
                table: "Requests",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status_Quote",
                table: "Requests",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status_Request",
                table: "Requests",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status_order_sheet",
                table: "Requests",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status_Delivery_Note",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Status_Invoice",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Status_PO",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Status_Quote",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Status_Request",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Status_order_sheet",
                table: "Requests");
        }
    }
}
