using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class addUserTeamTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTeams_AspNetUsers_MembersId",
                table: "UserTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTeams_Teams_TeamsTeamId",
                table: "UserTeams");

            migrationBuilder.DropIndex(
                name: "IX_UserTeams_TeamsTeamId",
                table: "UserTeams");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "305e461a-f377-47c5-8f3d-28e3d8b57817");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a271770a-c57b-4ca0-9d51-a5ff78e7ee0b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b05a03af-28b6-4efb-9c34-2dbc6aa31283");

            migrationBuilder.RenameColumn(
                name: "TeamsTeamId",
                table: "UserTeams",
                newName: "TeamId");

            migrationBuilder.RenameColumn(
                name: "MembersId",
                table: "UserTeams",
                newName: "UserId");

            migrationBuilder.CreateTable(
                name: "TeamUser",
                columns: table => new
                {
                    MembersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TeamsTeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamUser", x => new { x.MembersId, x.TeamsTeamId });
                    table.ForeignKey(
                        name: "FK_TeamUser_AspNetUsers_MembersId",
                        column: x => x.MembersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamUser_Teams_TeamsTeamId",
                        column: x => x.TeamsTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamUser_TeamsTeamId",
                table: "TeamUser",
                column: "TeamsTeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamUser");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "UserTeams",
                newName: "TeamsTeamId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserTeams",
                newName: "MembersId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "305e461a-f377-47c5-8f3d-28e3d8b57817", null, "User", "USER" },
                    { "a271770a-c57b-4ca0-9d51-a5ff78e7ee0b", null, "Admin", "ADMIN" },
                    { "b05a03af-28b6-4efb-9c34-2dbc6aa31283", null, "TeamLeader", "TEAMLEADER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTeams_TeamsTeamId",
                table: "UserTeams",
                column: "TeamsTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTeams_AspNetUsers_MembersId",
                table: "UserTeams",
                column: "MembersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTeams_Teams_TeamsTeamId",
                table: "UserTeams",
                column: "TeamsTeamId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
