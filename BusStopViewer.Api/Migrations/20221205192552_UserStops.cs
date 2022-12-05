using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusStopViewer.Api.Migrations
{
    public partial class UserStops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserStops",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StopId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStops", x => new { x.UserId, x.StopId });
                    table.ForeignKey(
                        name: "FK_UserStops_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserStops");
        }
    }
}
