using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SquadAsService.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateJobOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdersJobTitles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdersJobTitles",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    JobTitleId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersJobTitles", x => new { x.OrderId, x.JobTitleId });
                    table.CheckConstraint("Quantity_Constrain", "[Quantity] <= 21");
                    table.ForeignKey(
                        name: "FK_OrdersJobTitles_JobTitles_JobTitleId",
                        column: x => x.JobTitleId,
                        principalTable: "JobTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersJobTitles_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdersJobTitles_JobTitleId",
                table: "OrdersJobTitles",
                column: "JobTitleId");
        }
    }
}
