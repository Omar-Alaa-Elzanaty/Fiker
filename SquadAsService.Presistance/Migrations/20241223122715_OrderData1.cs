using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SquadAsService.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class OrderData1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Question",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddCheckConstraint(
                name: "Quantity_Constrain",
                table: "OrdersJobTitles",
                sql: "[Quantity] <= 21");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "Quantity_Constrain",
                table: "OrdersJobTitles");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Question",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "Orders");
        }
    }
}
