using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SquadAsService.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class AreaRemoveIcon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "Areas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "Areas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
