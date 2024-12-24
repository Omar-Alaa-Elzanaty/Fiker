using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SquadAsService.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class TechnologyJobTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TechnologyJobTitles",
                columns: table => new
                {
                    TechnologyId = table.Column<int>(type: "int", nullable: false),
                    JobTitleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnologyJobTitles", x => new { x.TechnologyId, x.JobTitleId });
                    table.ForeignKey(
                        name: "FK_TechnologyJobTitles_JobTitles_JobTitleId",
                        column: x => x.JobTitleId,
                        principalTable: "JobTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechnologyJobTitles_Technologies_TechnologyId",
                        column: x => x.TechnologyId,
                        principalTable: "Technologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechnologyJobTitles_JobTitleId",
                table: "TechnologyJobTitles",
                column: "JobTitleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechnologyJobTitles");
        }
    }
}
