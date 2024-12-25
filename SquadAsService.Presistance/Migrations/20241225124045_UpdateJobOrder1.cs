using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SquadAsService.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateJobOrder1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdersJobTitles",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersJobTitles", x => new { x.OrderId, x.JobTitle });
                    table.CheckConstraint("Quantity_Constrain", "[Quantity] <= 21");
                    table.ForeignKey(
                        name: "FK_OrdersJobTitles_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdersJobTitles");
        }
    }
}
